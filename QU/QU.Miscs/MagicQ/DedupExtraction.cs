using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;

namespace QU.Miscs.MagicQ
{
    class DedupExtraction
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "extraction")]
            public string Extraction = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "topn")]
            public int TopN = 20;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            TSVLine headerLine;
            List<QueryBlock> blocks = null;
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.Extraction)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                QueryBlockReader qbReader = new QueryBlockReader(tsvReader);
                blocks = qbReader.ReadQueryBlocks().ToList();
                headerLine = tsvReader.HeaderTSVLine;
            }

            using (StreamWriter swOut = new StreamWriter(arguments.Output))
            {
                swOut.WriteLine(headerLine.GetWholeLineString());

                foreach (var block in blocks)
                {
                    HashSet<string> existing = new HashSet<string>();
                    foreach (var line in block.Lines)
                    {
                        string url = line["m:Url"];
                        if (existing.Contains(url))
                        {
                            continue;
                        }

                        existing.Add(url);
                        swOut.WriteLine(line.GetWholeLineString());
                    }
                }
            }
        }
    }
}
