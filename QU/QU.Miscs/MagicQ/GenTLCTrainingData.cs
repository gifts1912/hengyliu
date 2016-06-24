using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;

namespace QU.Miscs.MagicQ
{
    public class GenTLCTrainingData
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "train")]
            public string TrainingFile = "";

            [Argument(ArgumentType.Required, ShortName = "truth")]
            public string TruthFile = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName="features")]
            public string Features = "HasQAFact;WebOcc;WebPos;QAOcc;QAScore;MaxBM25QAPattern;MaxBM25Entity;MaxDRScore";

            [Argument(ArgumentType.AtMostOnce, ShortName = "key")]
            public string Key = "Query;Candidate";

            [Argument(ArgumentType.AtMostOnce, ShortName = "method")]
            public string Method = "ranking";

            [Argument(ArgumentType.AtMostOnce, ShortName = "dup")]
            public int DupPos = 8;
        }

        enum Method
        {
            ranking,
            classification
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            Dictionary<string, string> truth = ReadTruth(arguments.TruthFile);
            string[] features = arguments.Features.Split(';');
            string[] keys = arguments.Key.Split(';');
            Method method = (Method)Enum.Parse(typeof(Method), arguments.Method);

            GenTLCFile(arguments.TrainingFile, arguments.Output, method, features, keys, truth, arguments.DupPos);
            GenSVMFile(arguments.TrainingFile, "svm.txt", truth);
        } // Run

        static void GenTLCFile(string trainingFile, string outFile,
            Method method, string[] features, string[] keys, Dictionary<string, string> truth, int dupPos)
        {
            using (StreamWriter sw = new StreamWriter(outFile))
            {
                if (method == Method.ranking)
                {
                    sw.WriteLine("m:Query\tm:Url\tm:Rating\t" + string.Join("\t", features));
                }
                else if (method == Method.classification)
                {
                    sw.WriteLine("\t\t" + string.Join("\t", features));
                }

                using (StreamReader sr = new StreamReader(trainingFile))
                {
                    TSVReader tsvReader = new TSVReader(sr, true);

                    while (!tsvReader.EndOfTSV)
                    {
                        TSVLine line = tsvReader.ReadLine();
                        string key = string.Join("|", from k in keys select line[k]);

                        if (int.Parse(line["ProdTopWebPos"]) > 3
                                && int.Parse(line["ImdbTopPos"]) > 3
                                && int.Parse(line["ApfTopPos"]) > 3
                                && int.Parse(line["GSTopPos"]) > 3)
                            continue;

                        if (method == Method.ranking)
                        {
                            sw.WriteLine(
                                "{0}\t{1}\t{2}\t{3}",
                                line["Query"],
                                line["Candidate"],
                                truth.ContainsKey(key) ? truth[key] : "Bad",
                                string.Join("\t", from f in features select line[f]));
                        }
                        else if (method == Method.classification)
                        {
                            bool pos = truth.ContainsKey(key);
                            sw.WriteLine(
                                "{0}\t{1}\t{2}",
                                "_" + key,
                                pos ? 1 : 0,
                                string.Join("\t", from f in features select line[f]));
                            if (pos && dupPos > 0)
                            {
                                for (int i = 0; i < dupPos; i++)
                                {
                                    sw.WriteLine(
                                                "{0}\t{1}\t{2}",
                                                "_" + key,
                                                pos ? 1 : 0,
                                                string.Join("\t", from f in features select line[f]));
                                }
                            }
                        }
                    }

                }
            }
        }

        static void GenSVMFile(string trainingFile, string outFile, 
            Dictionary<string, string> truth)
        {
            Dictionary<string, Dictionary<long, MovieCandidateFeature>> dictQ2CandFeat;
            Dictionary<string, Dictionary<long, double>> dictQ2CandScore;
            MovieRankingUtility.ReadFeatureFile(trainingFile, out dictQ2CandFeat, out dictQ2CandScore);

            using (StreamWriter sw = new StreamWriter(outFile))
            {
                int qid = 0;
                foreach (var p in dictQ2CandFeat)
                {
                    string q = p.Key;
                    bool hasPos = false;
                    foreach (var cf in p.Value)
                    {
                        if (cf.Value.ProdTopWebPos > 3 && cf.Value.ImdbTopPos > 3
                            && cf.Value.ApfTopPos > 3 && cf.Value.GSTopPos > 3)
                            continue;
                        string key = MovieRankingUtility.BuildKey(q, cf.Key.ToString());
                        if (truth.ContainsKey(key))
                        {
                            hasPos = true;
                            break;
                        }
                    }

                    if (!hasPos)
                        continue;

                    qid++;

                    foreach (var cf in p.Value)
                    {
                        if (cf.Value.ProdTopWebPos > 3 && cf.Value.ImdbTopPos > 3
                            && cf.Value.ApfTopPos > 3 && cf.Value.GSTopPos > 3)
                            continue;

                        string key = MovieRankingUtility.BuildKey(q, cf.Key.ToString());
                        int label = truth.ContainsKey(key) ? 1 : 0;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(label + " ");
                        sb.AppendFormat("qid:{0} ", qid);
                        sb.AppendFormat("1:{0} ", cf.Value.ProdWebScore);
                        sb.AppendFormat("2:{0} ", cf.Value.ProdQAScore);
                        sb.AppendFormat("3:{0} ", cf.Value.ImdbScore);
                        sb.AppendFormat("4:{0}", cf.Value.GSScore);
                        sw.WriteLine(sb.ToString());
                    }
                }
            }
        }

        static Dictionary<string, string> ReadTruth(string truthFile)
        {
            Dictionary<string, string> truth = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(truthFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string[] items = line.Split('\t');
                    if (string.IsNullOrEmpty(items[0]))
                        continue;

                    string key = MovieRankingUtility.BuildKey(items[1], items[0]);
                    if (!truth.ContainsKey(key))
                        truth.Add(key, items[2]);
                }
            }

            return truth;
        }
    }
}
