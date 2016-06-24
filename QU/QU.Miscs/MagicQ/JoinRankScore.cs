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
    public class JoinRankScore
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "feature")]
            public string FeatureFile = "";

            [Argument(ArgumentType.Required, ShortName = "score")]
            public string ScoreFile = "";

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

            var dictQU2Score = ReadScoreFile(arguments.ScoreFile);
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.FeatureFile)))
                {
                    TSVReader tsvReader = new TSVReader(sr, true);

                    sw.WriteLine(tsvReader.HeaderTSVLine.GetWholeLineString() 
                        + "\tm:QAFact\tAdvancedPreferFeature_1194\tRerankScore");

                    while (!tsvReader.EndOfTSV)
                    {
                        TSVLine line = tsvReader.ReadLine();
                        if (line == null)
                            continue;

                        string q = line["m:Query"];
                        string u = line["m:Url"];
                        string k = q + u;
                        int s = dictQU2Score.ContainsKey(k) ? dictQU2Score[k] : 0;

                        string qafact = "";
                        if (line.GetFeatureValue("Marker_247") != 0)
                        {
                            qafact = line.GetFeatureValue("Marker_247").ToString() + "|30";
                            if (line.GetFeatureValue("Marker_248") != 0)
                            {
                                qafact += "," + line.GetFeatureValue("Marker_248").ToString() + "|20";
                                if (line.GetFeatureValue("Marker_249") != 0)
                                {
                                    qafact += "," + line.GetFeatureValue("Marker_249").ToString() + "|10";
                                }
                            }
                        }

                        uint apf1194 = line.GetFeatureValue("Marker_241");

                        sw.WriteLine(line.GetWholeLineString() 
                            + string.Format("\t{0}\t{1}\t{2}", qafact, apf1194, s));
                    }
                }
            }
        }

        static Dictionary<string, int> ReadScoreFile(string file)
        {
            var dict = new Dictionary<string, int>();
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(file)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                while (!tsvReader.EndOfTSV)
                {
                    TSVLine line = tsvReader.ReadLine();
                    if (line == null)
                        continue;

                    string q = line["m:Query"];
                    string u = line["m:Url"];
                    int s = (int)(double.Parse(line["m:RankScore"]));

                    string k = q + u;
                    if (dict.ContainsKey(k))
                        dict[k] = Math.Max(s, dict[k]);
                    else
                        dict[k] = s;
                }
            }

            return dict;
        }
    }
}
