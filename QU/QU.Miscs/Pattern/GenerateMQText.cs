using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace QU.Miscs
{
    /// <summary>
    /// Generate multiquery augmentation for queries.
    /// </summary>
    class GenerateMQText
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "raw")]
            public string RawQueryMapping = "";

            [Argument(ArgumentType.Required, ShortName = "cand")]
            public string BestCandidateMapping = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.RawQueryMapping) || !File.Exists(arguments.BestCandidateMapping))
            {
                Console.WriteLine("No Input Files!");
                return;
            }

            Dictionary<string, string> dictQ2Cand = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(arguments.BestCandidateMapping))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split('\t');
                    if (items.Length < 2)
                        continue;
                    if (!dictQ2Cand.ContainsKey(items[0]))
                    {
                        dictQ2Cand.Add(items[0], items[1]);
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                //sw.WriteLine("m:RawQuery\tm:MQText\tm:Augmentation");
                using (StreamReader sr = new StreamReader(arguments.RawQueryMapping))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length < 2)
                            continue;
                        string rawQuery = items[0];
                        string spellerQuery = items[1];
                        string cand;
                        if (!dictQ2Cand.TryGetValue(spellerQuery, out cand))
                        {
                            continue;
                        }

                        string mq = rawQuery.AddExtraQuery(cand, "QPP", "0.5");
                        sw.WriteLine(rawQuery + "\t" + mq + "\t" + mq.Substring(0, mq.Length - rawQuery.Length));
                    }
                }
            }
        }
    }
}
