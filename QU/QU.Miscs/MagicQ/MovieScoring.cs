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
    class MovieScoring
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "extraction")]
            public string Extraction = "";

            [Argument(ArgumentType.Required, ShortName = "feature")]
            public string FeatureOutput = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "expression")]
            public string ScoringExpression = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "topn")]
            public int TopN = 20;

            [Argument(ArgumentType.AtMostOnce, ShortName = "truth")]
            public string truthFile;

            [Argument(ArgumentType.Required, ShortName = "pr")]
            public string prFile;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            Dictionary<string, int> truth = new Dictionary<string,int>();
            if (!string.IsNullOrEmpty(arguments.truthFile))
            {
                truth = MovieRankingUtility.ReadTruth(arguments.truthFile);
            }

            int allTruthCnt = truth.Count == 0 ? 1000 : truth.Count;

            MovieExpressionEvaluator evaluator = null;
            if (!string.IsNullOrEmpty(arguments.ScoringExpression))
            {
                evaluator = MovieExpressionEvaluator.ParseExpression(arguments.ScoringExpression);
            }
            TSVLine headerLine;
            List<QueryBlock> blocks;
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.Extraction)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                QueryBlockReader qbReader = new QueryBlockReader(tsvReader);
                blocks = qbReader.ReadQueryBlocks().ToList();
                headerLine = tsvReader.HeaderTSVLine;
            }

            using (StreamWriter swPR = new StreamWriter(arguments.prFile))
            {
                using (StreamWriter swFeature = new StreamWriter(arguments.FeatureOutput))
                {
                    swFeature.WriteLine("Query\tCandidate\t" + MovieCandidateFeature.LongHeader + "\tMovieScore");
                    int total = 0, right = 0;
                    foreach (var block in blocks)
                    {
                        string query = block.Lines.First()["m:Query"];
                        Dictionary<long, MovieCandidateFeature> dictMovie2Features = MovieRankingUtility.ExtractMovieCandidateFeature(block, arguments.TopN);
                        Dictionary<long, double> dictMovie2Score = MovieRankingUtility.EvaluateMovieCandidates(evaluator, dictMovie2Features);

                        foreach (var p in dictMovie2Features)
                        {
                            if (dictMovie2Score.ContainsKey(p.Key))
                                swFeature.WriteLine(query + "\t" + p.Key + "\t" + p.Value.ToLongString() + "\t" + dictMovie2Score[p.Key]);
                        }

                        var sorted = from p in dictMovie2Score
                                        orderby p.Value descending
                                        select p;
                        int idx = 1;
                        foreach (var p in sorted)
                        {
                            bool isRight = truth.ContainsKey(MovieRankingUtility.BuildKey(query, p.Key.ToString()));
                            swPR.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", query,
                                p.Key, p.Value, idx++, isRight ? 1 : 0);
                            if (p.Value > 0)
                            {
                                total++;
                                right += (isRight ? 1 : 0);
                            }
                        }
                    }

                    Console.WriteLine("Precision: {0}/{1} = {2}", right, total, (double)right / total);
                }
            }

            LineSearch(arguments.prFile, allTruthCnt);
        }

        static void LineSearch(string prFile, int allTruthCnt)
        {
            List<Result> results = new List<Result>();
            using (StreamReader sr = new StreamReader(prFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    string[] items = line.Split('\t');
                    var r = new Result { score = double.Parse(items[2]), Truth = int.Parse(items[4]) > 0 };
                    if (r.score <= -100)
                        continue;

                    results.Add(r);
                }
            }

            double minScore = results.Min(r => r.score);
            double maxScore = results.Max(r => r.score);
            double inc = (maxScore - minScore) / 100;
            //int allTruth = (from r in results where r.Truth select r).Count();
            int allTruth = allTruthCnt;
            for (double thresh = minScore; thresh <= maxScore; thresh += inc)
            {
                int predRight = (from r in results where r.score >= thresh select r).Count();
                int actualRight = (from r in results where r.score >= thresh && r.Truth select r).Count();
                double prec = (double)actualRight / predRight;
                double recall = (double)actualRight / allTruth;
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", thresh, prec, recall, 2.0/(1.0/prec + 1.0/recall), actualRight);
            }
        }

        class Result
        {
            public double score;
            public bool Truth;
        }
    }
}
