using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TSVUtility;

namespace QU.Miscs.MagicQ
{
    class RuleResult
    {
        public string rule;
        public double precision;
        public double recall;
        public double homoAverage;
        public int actualRight;
        public int predRight;
        public int allTruthCnt;

        public string ToLine()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                this.rule, this.precision, this.recall, this.homoAverage, this.actualRight, this.predRight, this.allTruthCnt);
        }

        public static RuleResult FromLine(string line)
        {
            RuleResult r = new RuleResult();
            string[] items = line.Split('\t');
            if (items.Length < 7)
                return null;

            try
            {
                r.rule = items[0];
                r.precision = double.Parse(items[1]);
                r.recall = double.Parse(items[2]);
                r.homoAverage = double.Parse(items[3]);
                r.actualRight = int.Parse(items[4]);
                r.predRight = int.Parse(items[5]);
                r.allTruthCnt = int.Parse(items[6]);
            }
            catch { return null; }

            return r;
        }

        public static RuleResult FromLine(string line, int allTruthCnt)
        {
            RuleResult r = new RuleResult();
            string[] items = line.Split('\t');
            if (items.Length < 4)
                return null;

            try
            {
                r.rule = items[0];
                r.precision = double.Parse(items[1]);
                r.recall = double.Parse(items[2]);
                r.homoAverage = double.Parse(items[3]);
                r.actualRight = (int)(r.recall * allTruthCnt);
                r.predRight = (int)(r.actualRight / r.precision);
                r.allTruthCnt = allTruthCnt;
            }
            catch { return null; }

            return r;
        }
    }

    public class MovieRuleTuning
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "extraction")]
            public string Extraction = "";

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

            #region Read Truth
            Dictionary<string, int> truth = new Dictionary<string, int>();
            if (!string.IsNullOrEmpty(arguments.truthFile))
            {
                truth = MovieRankingUtility.ReadTruth(arguments.truthFile);
            }
            #endregion

            #region Read Extraction
            TSVLine headerLine;
            List<QueryBlock> blocks;
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.Extraction)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                QueryBlockReader qbReader = new QueryBlockReader(tsvReader);
                blocks = qbReader.ReadQueryBlocks().ToList();
                headerLine = tsvReader.HeaderTSVLine;
            }
            #endregion

            #region Feature extraction
            Dictionary<string, MovieCandidateFeature> dictKey2Features = new Dictionary<string, MovieCandidateFeature>();
            foreach (var block in blocks)
            {
                string query = block.Lines.First()["m:Query"];
                Dictionary<long, MovieCandidateFeature> dictMovie2Features = MovieRankingUtility.ExtractMovieCandidateFeature(block, arguments.TopN);

                foreach (var p in dictMovie2Features)
                {
                    if (p.Value.ProdTopWebPos > 10 && p.Value.ImdbTopPos > 3
                        && p.Value.ApfTopPos > 3 && p.Value.GSTopPos > 3)
                        continue;

                    dictKey2Features[MovieRankingUtility.BuildKey(query, p.Key.ToString())] = p.Value;
                }
            }
            #endregion


            List<string> features = new List<string>();
            features.Add("ProdTopWebPos");
            features.Add("ProdWebOcc");
            features.Add("ProdTopQAPos");
            features.Add("ProdQAOcc");
            features.Add("ImdbTopPos");
            features.Add("ImdbOcc");
            features.Add("ApfTopPos");
            features.Add("ApfOcc");
            features.Add("GSTopPos");

            //TuneBooleanRules(truth, dictKey2Features, features);

            features.Clear();
            features.Add("ProdWebScore");
            features.Add("ProdQAScore");
            features.Add("ImdbScore");
            features.Add("ApfScore");
            features.Add("GSScore");

            TuneLinearRules(truth, dictKey2Features, /*features, */arguments);
        }

        static void TuneLinearRules(Dictionary<string, int> truth,
            Dictionary<string, MovieCandidateFeature> dictKey2Features, 
            //List<string> features, 
            Args arguments)
        {
            int allTruthCnt = truth.Count == 0 ? 1000 : truth.Count;
            //var fea = features.ToArray();
            //var weights = new double[5];
            //weights[0] = 1.0;

            double rate = 0.1;
            int cnt = (int)(3.0 / rate) + 1;
            var gridWeights = new double[5 - 1][];
            for (int i = 0; i < gridWeights.Length; i++)
            {
                gridWeights[i] = new double[cnt];
                for (int j = 0; j < gridWeights[i].Length; j++)
                {
                    gridWeights[i][j] = (double)j * rate;
                }
            }

            for (int i = 0; i < cnt; i++)
            {
                double w1 = gridWeights[0][i];
                //double w1 = 0;
                for (int j = 0; j < cnt; j++)
                {
                    double w2 = gridWeights[1][j];
                    for (int k = 0; k < cnt; k++)
                    {
                        double w3 = gridWeights[2][k];
                        //for (int l = 0; l < cnt; l++)
                        {
                            //double w4 = gridWeights[3][l];
                            double w4 = 0;
                            Dictionary<string, double> dictMovie2Score = new Dictionary<string, double>();
                            foreach (var p in dictKey2Features)
                            {
                                var f = p.Value;
                                dictMovie2Score[p.Key] = f.ProdWebScore + w1 * f.ProdQAScore
                                                            + w2 * f.ImdbScore + w3 * f.ApfScore + w4 * f.GSScore;
                            }

                            double minScore = dictMovie2Score.Values.Min();
                            double maxScore = dictMovie2Score.Values.Max();
                            double inc = (maxScore - minScore) / 50;
                            int allTruth = allTruthCnt;
                            double maxHomoAvg = -1;
                            double bestThresh = -1;
                            RuleResult maxResult = null;
                            for (double thresh = minScore; thresh <= maxScore; thresh += inc)
                            {
                                RuleResult result = new RuleResult();
                                result.predRight = (from p in dictMovie2Score where p.Value > thresh select p).Count();
                                result.actualRight = (from p in dictMovie2Score where (p.Value > thresh && truth.ContainsKey(p.Key)) select p).Count();
                                result.precision = (double)result.actualRight / result.predRight;
                                result.recall = (double)result.actualRight / allTruthCnt;
                                result.allTruthCnt = allTruthCnt;
                                result.homoAverage = 2.0 / (1.0 / result.precision + 1.0 / result.recall);
                                if (result.homoAverage > maxHomoAvg)
                                {
                                    maxHomoAvg = result.homoAverage;
                                    bestThresh = thresh;
                                    maxResult = result;
                                }
                            }

                            if (maxHomoAvg > 0.35)
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}", w1, w2, w3, w4, bestThresh,
                                    maxResult.homoAverage,
                                    maxResult.predRight, maxResult.actualRight, maxResult.precision, maxResult.recall);
                            }
                        }
                    }
                }
            }
        }

        static void TuneBooleanRules(Dictionary<string, int> truth,
            Dictionary<string, MovieCandidateFeature> dictKey2Features, List<string> features)
        {
            int allTruthCnt = truth.Count == 0 ? 1000 : truth.Count;

            List<Common.HomoAvgDecisionTree> trees = new List<Common.HomoAvgDecisionTree>();
            int idx = 0;
            List<string> sixFeatureCombinations = SelFeatures(features.ToArray(), 0, 6, "");
            foreach (var c in sixFeatureCombinations)
            {
                Common.HomoAvgDecisionTree tree = new Common.HomoAvgDecisionTree();
                tree.BuildTree(dictKey2Features, new HashSet<string>(truth.Keys), 0.7, c.Split(';').ToList(), idx++, 0.35, 0.2);
                Console.WriteLine("Tree {0}/{1} Built", idx, sixFeatureCombinations.Count);
                if (tree.Root != null)
                    trees.Add(tree);
            }

            string expression = "";
            foreach (var tree in trees)
            {
                string currExpression = "";
                var node = tree.Root;
                while (node != null)
                {
                    if (!string.IsNullOrEmpty(node.rule))
                    {
                        currExpression = string.IsNullOrEmpty(currExpression) ? node.rule : currExpression + "&&" + node.rule;
                    }
                    node = node.next;
                }
                currExpression = string.Format("({0} ? 1 : 0)", currExpression);

                expression = string.IsNullOrEmpty(expression) ? currExpression : expression + "+" + currExpression;
            }

            expression = string.Format("({0})/(double){1}", expression, trees.Count);
            Console.WriteLine(expression);
            MovieExpressionEvaluator evaluator = MovieExpressionEvaluator.ParseExpression(expression);
            Dictionary<string, double> dictMovie2Score
                = MovieRuleTuning.EvaluateMovieCandidates(evaluator, dictKey2Features);

            RuleResult result = new RuleResult();
            result.predRight = (from p in dictMovie2Score where p.Value > 15 select p).Count();
            result.actualRight = (from p in dictMovie2Score where (p.Value > 15 && truth.ContainsKey(p.Key)) select p).Count();
            result.precision = (double)result.actualRight / result.predRight;
            result.recall = (double)result.actualRight / allTruthCnt;
            result.allTruthCnt = allTruthCnt;
            result.homoAverage = 2.0 / (1.0 / result.precision + 1.0 / result.recall);

            Console.WriteLine(expression);
            Console.WriteLine("Precision: {0}", result.precision);
            Console.WriteLine("Recall: {0}", result.recall);

            #region Enumerate/evaluate all of rules
            //using (StreamWriter swPR = new StreamWriter(arguments.prFile))
            //{
            //    #region And Rules
            //    List<RuleResult> andRules = new List<RuleResult>();
            //    if (File.Exists(arguments.prFile))
            //    {
            //        using (StreamReader sr = new StreamReader(arguments.prFile))
            //        {
            //            while (!sr.EndOfStream)
            //            {
            //                string line = sr.ReadLine();
            //                var r = RuleResult.FromLine(line);
            //                if (r != null)
            //                    andRules.Add(r);
            //            }
            //        }
            //    }
            //    RuleResult lastProcessed = andRules.Last();

            //    foreach (var r in andRules)
            //    {
            //        swPR.WriteLine(r.ToLine());
            //    }
            //    // rules of "&&" operator
            //    var rules = EnumerateRules(dictKey2Features.Values);
            //    Console.WriteLine("Rules: {0}", rules.Count);
            //    Console.WriteLine("Already Processed Rules: {0}", andRules.Count);

            //    rules.Sort();
            //    Console.WriteLine("Sort done");

            //    int idx = 0;
            //    foreach (var r in rules)
            //    {
            //        idx++;
            //        if (r == lastProcessed.rule)
            //        {
            //            break;
            //        }
            //    }
            //    rules.RemoveRange(0, idx);

            //    Thread[] threads = new Thread[ThreadCount];
            //    List<RuleResult>[] threadResults = new List<RuleResult>[ThreadCount];
            //    int avgThreadRules = (int)(rules.Count / ThreadCount);
            //    for (int i = 0; i < ThreadCount; i++)
            //    {
            //        EvaluateThreadParameter param = new EvaluateThreadParameter
            //        {
            //            rules = rules.GetRange(i * avgThreadRules, avgThreadRules),
            //            results = threadResults[i],
            //            dictKey2Features = dictKey2Features,
            //            truth = truth,
            //            allTruthCnt = allTruthCnt,
            //            threadId = i,
            //            minPrecision = -1,
            //            minRecall = 0.01
            //        };

            //        threads[i] = new Thread(new ParameterizedThreadStart(EvaluateRuleSetWorker));
            //        threads[i].Start(param);
            //    }

            //    for (int i = 0; i < ThreadCount; i++)
            //    {
            //        if (threads[i].IsAlive)
            //        {
            //            threads[i].Join();
            //        }
            //    }

            //    for (int i = 0; i < ThreadCount; i++)
            //    {
            //        foreach (var r in threadResults[i])
            //        {
            //            swPR.WriteLine(r.ToLine());
            //        }
            //    }
            //    #endregion

            //    #region "A||B" rules
            //    RuleResult[] validAndRules = (from r in andRules where r.precision >= 0.4 select r).ToArray();
            //    Console.WriteLine("Candidate && rules: {0}", validAndRules.Length);
            //    Dictionary<string, RuleResult> sortedRuleDict = BuildSortedRuleDict(validAndRules);
            //    List<RuleResult> twoUnionRules = new List<RuleResult>();
            //    for (int i = 0; i < validAndRules.Length; i++)
            //    {
            //        for (int j = i + 1; j < validAndRules.Length; j++)
            //        {
            //            RuleResult union = UnionTwoSingleRules(validAndRules[i], validAndRules[j], sortedRuleDict);
            //            if (union != null && union.precision >= 0.4)
            //            {
            //                twoUnionRules.Add(union);
            //                swPR.WriteLine(union.ToLine());
            //            }
            //        }
            //    }
            //    Console.WriteLine("Union Two Rules: {0}", twoUnionRules.Count);
            //    #endregion
            //}
            #endregion
        }

        static List<string> SelFeatures(string[] totalFeatures, int start, int seln, string prefix)
        {
            if (seln > totalFeatures.Length - start || start >= totalFeatures.Length)
            {
                return null;
            }

            if (seln == 0)
            {
                return new List<string>(new string[] { prefix });
            }

            if (start == totalFeatures.Length - 1)
            {
                return new List<string>(new string[] {prefix + ";" + totalFeatures[start]});
            }

            List<string> all = new List<string>();
            List<string> withoutCurr = SelFeatures(totalFeatures, start + 1, seln, prefix);
            List<string> withCurr = SelFeatures(totalFeatures, start + 1, seln - 1, 
                string.IsNullOrEmpty(prefix) ? totalFeatures[start] : prefix + ";" + totalFeatures[start]);
            if (null != withCurr)
                all.AddRange(withCurr);

            if (null != withoutCurr)
                all.AddRange(withoutCurr);
            return all;
        }

        private static int ThreadCount = 8;

        static void EvaluateRuleSetWorker(object obj)
        {
            EvaluateThreadParameter param = obj as EvaluateThreadParameter;
            param.results = new List<RuleResult>(param.rules.Count);
            string lastFailed = "test";
            MovieExpressionEvaluator evaluator = null;
            using (StreamWriter sw = new StreamWriter("temp_" + param.threadId + ".txt"))
            {
                foreach (var r in param.rules)
                {
                    if (r.StartsWith(lastFailed))
                    {
                        continue;
                    }

                    RuleResult result = new RuleResult();
                    string expression = string.Format("({0}) ? 1 : 0", r);
                    evaluator = MovieExpressionEvaluator.ParseExpression(expression);
                    Dictionary<string, double> dictMovie2Score
                        = EvaluateMovieCandidates(evaluator, param.dictKey2Features);

                    result.predRight = (from p in dictMovie2Score where p.Value > 0 select p).Count();
                    result.actualRight = (from p in dictMovie2Score where (p.Value > 0 && param.truth.ContainsKey(p.Key)) select p).Count();
                    result.precision = (double)result.actualRight / result.predRight;
                    result.recall = (double)result.actualRight / param.allTruthCnt;
                    result.allTruthCnt = param.allTruthCnt;
                    result.homoAverage = 2.0 / (1.0 / result.precision + 1.0 / result.recall);
                    result.rule = r;

                    if (result.recall < param.minRecall || result.precision < param.minPrecision)
                    {
                        lastFailed = r;
                        continue;
                    }

                    param.results.Add(result);
                    sw.WriteLine(result.ToLine());
                }
            }
        }

        static RuleResult UnionTwoSingleRules(RuleResult r1, RuleResult r2, Dictionary<string, RuleResult> sortedRuleDict)
        {
            RuleResult unioned = new RuleResult();
            unioned.rule = "(" + r1.rule + ")||(" + r2.rule + ")";
            string joined = JoinTwoRules(r1.rule, r2.rule);
            RuleResult joinedResult;
            if (!sortedRuleDict.TryGetValue(joined, out joinedResult))
                return null;

            unioned.predRight = r1.predRight + r2.predRight - joinedResult.predRight;
            unioned.actualRight = r1.actualRight + r2.actualRight - joinedResult.actualRight;
            unioned.allTruthCnt = r1.allTruthCnt;
            unioned.precision = (double)unioned.actualRight / unioned.predRight;
            unioned.recall = (double)unioned.actualRight / unioned.allTruthCnt;
            unioned.homoAverage = 2.0 / (1.0 / unioned.precision + 1.0 / unioned.recall);
            return unioned;
        }

        class EvaluateThreadParameter
        {
            public int threadId;
            public List<string> rules;
            public List<RuleResult> results;
            public Dictionary<string, MovieCandidateFeature> dictKey2Features;
            public Dictionary<string, int> truth;
            public int allTruthCnt;
            public double minPrecision;
            public double minRecall;
        }

        static Dictionary<string, RuleResult> BuildSortedRuleDict(IEnumerable<RuleResult> rules)
        {
            Dictionary<string, RuleResult> dict = new Dictionary<string, RuleResult>();
            foreach (var r in rules)
            {
                string[] items = r.rule.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                Array.Sort(items);
                dict[string.Join("&&", items)] = r;
            }
            return dict;
        }

        static string JoinTwoRules(string r1, string r2)
        {
            string[] items1 = r1.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] items2 = r1.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] items = items1.Union(items2).ToArray();
            Array.Sort(items);
            string r = string.Join("&&", items.Distinct());
            return r;
        }


        static List<string> EnumerateRules(IEnumerable<MovieCandidateFeature> movieFeatures)
        {
            // valid features:
            //   f.ProdTopWebPos:-, f.ProdWebOcc:+, f.ProdHasQAFact:+, f.ProdTopQAPos:-
            //   f.ProdQAOcc:+, f.ImdbTopPos:-, f.ImdbOcc:+, f.ApfTopPos:-, f.ApfOcc:+, f.GSTopPos:-

            // step 1: enumerate all of the "&&" operators
            string[][] andFeatures = new string[9][];
            andFeatures[0] = BuildFeatureArr("ProdTopWebPos", 20, false);
            andFeatures[1] = BuildFeatureArr("ProdWebOcc", 5, true);
            andFeatures[2] = BuildFeatureArr("ProdTopQAPos", 20, false);
            andFeatures[3] = BuildFeatureArr("ProdQAOcc", 5, true);
            andFeatures[4] = BuildFeatureArr("ImdbTopPos", 20, false);
            andFeatures[5] = BuildFeatureArr("ImdbOcc", 5, true);
            andFeatures[6] = BuildFeatureArr("ApfTopPos", 20, false);
            andFeatures[7] = BuildFeatureArr("ApfOcc", 5, true);
            andFeatures[8] = BuildFeatureArr("GSTopPos", 5, false);

            return BuildAndFeatures(andFeatures);
        }

        static List<string> BuildAndFeatures(string[][] features)
        {
            List<string> allFeatures = new List<string>();
            int len = features.Length;
            int cnt = (int)Math.Pow(2, len) - 1;
            for (int i = 1; i <= cnt; i++)
            {
                List<string[]> selFeatures = new List<string[]>();
                for (int j = 0; j < len; j++)
                {
                    int bitVal = (int)Math.Pow(2, j) & i;
                    if (bitVal > 0)
                    {
                        selFeatures.Add(features[j]);
                    }
                }
                if (selFeatures.Count <= 4)
                    allFeatures.AddRange(BuildAndFeaturesAllCombination("", selFeatures));
            }

            return allFeatures;
        }

        static List<string> BuildAndFeaturesAllCombination(string prefix, List<string[]> features)
        {
            List<string> comb = new List<string>();
            if (features.Count == 0)
                return comb;

            string[] first = features.First();
            features.RemoveAt(0);
            for (int i = 0; i < first.Length; i++)
            {
                string newPrefix;
                if (string.IsNullOrEmpty(prefix))
                    newPrefix = first[i];
                else
                    newPrefix = prefix + "&&" + first[i];

                List<string> subComb = BuildAndFeaturesAllCombination(newPrefix, features);
                if (subComb.Count == 0)
                {
                    comb.Add(newPrefix);
                }
                else
                {
                    comb.AddRange(subComb);
                }
            }
            features.Insert(0, first);

            return comb;
        }


        static string[] BuildFeatureArr(string featureName, int range, bool asc)
        {
            string oper = asc ? ">=" : "<=";
            string[] arr = new string[range];
            for (int i = 0; i < range; i++)
            {
                arr[i] = string.Format("f.{0}{1}{2}", featureName, oper, i + 1);
            }
            return arr;
        }

        /// <summary>
        /// Evaluate movie candidate's score.
        /// </summary>
        /// <param name="evaluator"></param>
        /// <param name="dictMovie2Features"></param>
        /// <param name="shouldHaveQA"></param>
        public static Dictionary<string, double> EvaluateMovieCandidates(MovieExpressionEvaluator evaluator,
            Dictionary<string, MovieCandidateFeature> dictMovie2Features)
        {
            Dictionary<string, double> dictMovie2Score = new Dictionary<string, double>();
            foreach (var cand in dictMovie2Features)
            {
                if (!cand.Value.ProdHasWeb
                    && cand.Value.ImdbOcc == 0
                    && cand.Value.ApfOcc == 0
                    && cand.Value.GSOcc == 0)
                    continue;

                dictMovie2Score[cand.Key] = evaluator.Evaluate(cand.Value);
            }

            return dictMovie2Score;
        }
    }
}
