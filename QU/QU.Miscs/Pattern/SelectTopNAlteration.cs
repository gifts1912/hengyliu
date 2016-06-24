using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMisc = Utility;
using QU.Utility;
using AEtherUtilities;
using System.Reflection;

namespace QU.Miscs
{
    class SelectTopNAlteration
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "alter")]
            public string AlterationFile = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.Required, ShortName = "best")]
            public string BestOutput = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "topn")]
            public int TopN = 3;

            [Argument(ArgumentType.AtMostOnce, ShortName = "minm")]
            public double MinMatch = 0.7;

            [Argument(ArgumentType.AtMostOnce, ShortName = "minp")]
            public double MinProb = 0.333333;

            [Argument(ArgumentType.AtMostOnce, ShortName = "minl")]
            public double MinL2R = 30;

            [Argument(ArgumentType.AtMostOnce, ShortName = "minc")]
            public double MinCoverage = 0.2;

            [Argument(ArgumentType.AtMostOnce, ShortName = "mins")]
            public double MinScore = 0.35;

            [Argument(ArgumentType.AtMostOnce, ShortName = "hrs")]
            public string HrsFile = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "expression")]
            public string ScoringExpression = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "maxWildcard")]
            public double MaxWildcard = 2;

            [Argument(ArgumentType.AtMostOnce, ShortName = "avgWildcardTerm")]
            public double AvgWildcardTerm = -1;
        }

        private static double CalScore(string q, string alteredQ, double[] features)
        {
            double score = 0;

            double em = features[3], l2r = features[1], rp = features[0];
            if (em >= 0.8 && l2r >= 100)
            {
                score = rp + 0.6;
            }
            else if (em >= 0.8 && l2r >= 50)
            {
                score = rp + 0.5;
            }
            else if (em >= 0.6 && l2r >= 100)
            {
                score = rp + 0.4;
            }
            else if (em >= 0.6 && l2r >= 50)
            {
                score = rp + 0.3;
            }
            else if (em >= 0.8 && l2r < 50)
            {
                score = rp + 0.2;
            }
            else if (em < 0.5 && l2r >= 100)
            {
                score = rp + 0.1;
            }
            else
            {
                score = rp;
            }

            if (features[6] > 0)
            {
                score -= (0.1 * features[6]);
            }

            // some intents
            if (alteredQ.Contains("symptom")
                || alteredQ.Contains("symptoms")
                || alteredQ.Contains("interaction")
                || alteredQ.Contains("interactions")
                || alteredQ.Contains(" vs ")
                || alteredQ.Contains("remedy")
                || alteredQ.Contains("remedies")
                || alteredQ.Contains("dose")
                || alteredQ.Contains("dosage")
                )
            {
                score += 0.2;
            }


            return score;
        }

        private static double CalScore2(string q, string alteredQ, ReformulationFeatures features)
        {
            return features.ExactMatch * features.FloatClickCoverage
                //- 0.03 * Math.Max(-3, Math.Min(3, features.DeltaQueryLength))
                + features.ClickYield
                ;
        }

        private static double CalScoreWithExpression(ReformulationFeatures features, ExpressionEvaluator evaluator)
        {
            return evaluator.Evaluate(features);
        }

        private static Dictionary<string, Dictionary<string, int>> LoadHrs(string file)
        {
            return MyMisc.CommonUtils.ReadQueryUrlPositionFile(file);
        }

        private static void Evaluate(Dictionary<string, Dictionary<string, double>> rankedResults,
            Dictionary<string, Dictionary<string, int>> hrs)
        {
            int hrsFound = 0;
            int bestFound = 0, atLeastFairFound = 0, shouldNotAltered = 0;
            foreach (var results in rankedResults)
            {
                string srcQ = results.Key;
                Dictionary<string, int> srcQHrs;
                if (!hrs.TryGetValue(srcQ, out srcQHrs))
                {
                    continue;
                }

                hrsFound++;
                double maxScore = results.Value.Values.Max();
                List<string> bestTgtQs = new List<string>(from p in results.Value where p.Value == maxScore select p.Key);
                int maxRating = srcQHrs.Values.Max();
                if (maxRating == 0)
                {
                    shouldNotAltered++;
                    continue;
                }

                foreach (var bestTgtQ in bestTgtQs)
                {
                    int tgtQRating;
                    if (!srcQHrs.TryGetValue(bestTgtQ, out tgtQRating))
                        continue;
                    if (tgtQRating == maxRating)
                    {
                        bestFound++;
                    }

                    if (tgtQRating > 0)
                    {
                        atLeastFairFound++;
                    }
                }
            }

            Console.WriteLine("-----------Evaluation Results-----------");
            Console.WriteLine("With HRS\t{0}", hrsFound);
            Console.WriteLine("Can be altered\t{0}", hrsFound - shouldNotAltered);
            Console.WriteLine("Best Found\t{0}", bestFound);
            Console.WriteLine("At least fair Found\t{0}", atLeastFairFound);
            Console.WriteLine("Precision@Best\t{0}", (double)bestFound / (hrsFound - shouldNotAltered));
            Console.WriteLine("Precision@Fair\t{0}", (double)atLeastFairFound / (hrsFound - shouldNotAltered));
            Console.WriteLine("False position\t{0}", (double)shouldNotAltered / hrsFound);
        }

        private static ReformulationFeatures GetFeatures(string[] items)
        {
            if (items.Length < 18)
            {
                return null;
            }

            return new ReformulationFeatures
            {
                BinaryClickCoverage = double.Parse(items[6]),
                FloatClickCoverage = double.Parse(items[7]),
                BinarySatClickCoverage = double.Parse(items[8]),
                FloatSatClickCoverage = double.Parse(items[9]),
                ClickRatio = double.Parse(items[10]),
                SatClickRatio = double.Parse(items[11]),
                ClickYield = double.Parse(items[12]),
                SatClickYield = double.Parse(items[13]),
                L2R = double.Parse(items[14]),
                ReformProb = double.Parse(items[15]),
                ExactMatch = double.Parse(items[16]),
                DeltaQueryLength = double.Parse(items[17]),
                RIntentPhraseLen = items.Length > 18 ? double.Parse(items[18]) : 0,
                WildcardCount = items.Length > 19 ? int.Parse(items[19]) : 0,
                WildcardMatchtedTerms = items.Length > 20 ? int.Parse(items[20]) : 0,
                L2RPercent = items.Length > 21 ? double.Parse(items[21]) : 0
            };
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.AlterationFile))
            {
                Console.WriteLine("No Alteration File!");
                return;
            }

            ExpressionEvaluator evaluator = null;
            if (!string.IsNullOrEmpty(arguments.ScoringExpression))
            {
                evaluator = ExpressionEvaluator.ParseExpression(arguments.ScoringExpression);
            }

            var dictSrc2TgtScore = new Dictionary<string, Dictionary<string, double>>();
            using (StreamWriter swBest = new StreamWriter(arguments.BestOutput))
            {
                using (StreamWriter sw = new StreamWriter(arguments.Output))
                {
                    using (StreamReader sr = new StreamReader(arguments.AlterationFile))
                    {
                        string line;
                        string currQ = "";
                        List<Tuple<string, ReformulationFeatures, double>> queryLines
                            = new List<Tuple<string, ReformulationFeatures, double>>();

                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] items = line.Split('\t');
                            if (items.Length < 18)
                                continue;

                            string q = items[0];
                            if (!string.IsNullOrEmpty(currQ) && !string.Equals(q, currQ))
                            {
                                var output = (from l in queryLines
                                              orderby l.Item3 descending, l.Item2.ExactMatch descending, l.Item2.FloatClickCoverage descending
                                              select l.Item1 + "\t" + l.Item3).Take(arguments.TopN);
                                foreach (var o in output)
                                {
                                    sw.WriteLine(o);
                                }

                                if (output.Count() > 0)
                                {
                                    var o = output.ElementAt(0).Split('\t');
                                    swBest.WriteLine(string.Join("\t", o.Take(2)) + "\t" + o[o.Length - 1]);
                                }
                                queryLines.Clear();
                            }

                            string tgtQ = items[1];

                            ReformulationFeatures features = GetFeatures(items);
                            double score = 0;
                            if (evaluator == null)
                                score = CalScore2(items[0], items[1], features);
                            else
                                score = CalScoreWithExpression(features, evaluator);

                            if (features.ExactMatch < arguments.MinMatch
                                || features.ReformProb < arguments.MinProb
                                || features.L2R < arguments.MinL2R
                                || features.FloatClickCoverage < arguments.MinCoverage
                                || features.WildcardCount > arguments.MaxWildcard
                                || score < arguments.MinScore
                                )
                            {
                                continue;
                            }

                            if (arguments.AvgWildcardTerm > 0)
                            {
                                if (features.WildcardMatchtedTerms > arguments.AvgWildcardTerm)
                                {
                                    continue;
                                }
                            }

                            queryLines.Add(new Tuple<string, ReformulationFeatures, double>(line, features, score));
                            if (!dictSrc2TgtScore.ContainsKey(q))
                            {
                                dictSrc2TgtScore.Add(q, new Dictionary<string, double>());
                            }
                            dictSrc2TgtScore[q][tgtQ] = score;

                            currQ = q;
                        }

                        var outputs = (from l in queryLines
                                       orderby l.Item3 descending, l.Item2.ExactMatch descending, l.Item2.FloatClickCoverage descending
                                       select l.Item1 + "\t" + l.Item3).Take(arguments.TopN);
                        if (outputs.Count() > 0)
                        {
                            var o = outputs.ElementAt(0).Split('\t');
                            swBest.WriteLine(string.Join("\t", o.Take(2)) + "\t" + o[o.Length - 1]);
                        }
                        foreach (var o in outputs)
                        {
                            sw.WriteLine(o);
                        }

                        if (!string.IsNullOrEmpty(arguments.HrsFile) && File.Exists(arguments.HrsFile))
                        {
                            var hrs = LoadHrs(arguments.HrsFile);
                            Evaluate(dictSrc2TgtScore, hrs);
                        }
                    }
                }
            }
        }
    }
}
