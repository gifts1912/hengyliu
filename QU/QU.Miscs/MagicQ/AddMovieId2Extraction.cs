using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using MyUtil = Utility;

namespace QU.Miscs.MagicQ
{
    public class AddMovieId2Extraction
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "extraction")]
            public string Extraction = "";

            [Argument(ArgumentType.Required, ShortName = "mapping")]
            public string Url2MovieIdMapping = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string outFile;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            var dict = ReadMapping(arguments.Url2MovieIdMapping);
            using (StreamWriter sw = new StreamWriter(arguments.outFile))
            {
                using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.Extraction)))
                {
                    TSVReader tsvReader = new TSVReader(sr, true);
                    //sw.WriteLine("m:EntityIds\t" + tsvReader.HeaderTSVLine.GetWholeLineString());
                    sw.WriteLine(tsvReader.HeaderTSVLine.GetWholeLineString());
                    while (!tsvReader.EndOfTSV)
                    {
                        var line = tsvReader.ReadLine();
                        string url = line["m:Url"];
                        url = MyUtil.Normalizer.NormalizeUrl(url);
                        string qafact = line["m:QAFact"];
                        string apf = line["AdvancedPreferFeature_1194"];

                        HashSet<string> mids;
                        string strMid = "";
                        if (dict.TryGetValue(url, out mids))
                        {
                            //strMid = string.Join(",", mids);
                            strMid = string.Join(",", from m in mids select m + "|40");
                        }

                        //sw.WriteLine(strMid + "\t" + line.GetWholeLineString());
                        string toappend = "";
                        if (apf == "0" && !string.IsNullOrEmpty(strMid))
                        {
                            toappend = strMid;
                            if (!string.IsNullOrEmpty(qafact))
                                toappend += ",";
                        }

                        sw.WriteLine(toappend + line.GetWholeLineString());
                    }
                }
            }
        }

        private static Dictionary<string, HashSet<string>> ReadMapping(string file)
        {
            var dict = new Dictionary<string, HashSet<string>>();
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    string[] items = line.Split('\t');
                    if (items.Length < 2)
                        continue;
                    if (!dict.ContainsKey(items[0]))
                    {
                        dict.Add(items[0], new HashSet<string>());
                    }

                    dict[items[0]].Add(items[1]);
                }
            }

            return dict;
        }
    }
}
