using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using MyUtil = Utility;

namespace QU.Miscs
{
    public class StatHostPopularity
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "b")]
            public string BingExtraction = "";

            [Argument(ArgumentType.Required, ShortName = "g")]
            public string GoogleExtraction = "";

            [Argument(ArgumentType.Required, ShortName = "o")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "n")]
            public int TopN = 10;

            public bool InputValid { get { return File.Exists(BingExtraction) && File.Exists(GoogleExtraction); } }
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }


            Dictionary<string, int> dictBingHost2Cnt = ReadHost2Cnt(arguments.BingExtraction, arguments.TopN);
            Dictionary<string, int> dictGoogleHost2Cnt = ReadHost2Cnt(arguments.GoogleExtraction, arguments.TopN);
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                var sorted = from p in dictBingHost2Cnt orderby p.Value descending select p;
                foreach (var p in sorted)
                {
                    if (!dictGoogleHost2Cnt.ContainsKey(p.Key)
                        //|| dictGoogleHost2Cnt[p.Key] < 2
                        )
                        sw.WriteLine(p.Key + "\t" + p.Value);
                }
            }
        }

        static Dictionary<string, int> ReadHost2Cnt(string file, int topn)
        {
            Dictionary<string, int> dictHost2Cnt = new Dictionary<string, int>();
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(file)))
            {
                TSVReader tsv = new TSVReader(sr, true);

                while (!tsv.EndOfTSV)
                {
                    TSVLine line = tsv.ReadLine();
                    string url = line["m:Url"];
                    uint DocumentPosition = line.GetFeatureValue("DocumentPosition");
                    if (DocumentPosition > topn)
                        continue;

                    string host = MyUtil.Normalizer.GetUrlHost(url);
                    if (!dictHost2Cnt.ContainsKey(host))
                        dictHost2Cnt.Add(host, 1);
                    else
                        ++dictHost2Cnt[host];
                }
            }

            return dictHost2Cnt;
        }
    }
}
