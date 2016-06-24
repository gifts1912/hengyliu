using AEtherUtilities;
using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs.MagicQ
{
    public class RankMagicMovieCandidate
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "train")]
            public string TrainingFile = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.Required, ShortName = "expression")]
            public string ScoringExpression = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "min")]
            public double MinScore = -10;
        }

        private static double CalScoreWithExpression(MovieCandidateFeature features, MovieExpressionEvaluator evaluator)
        {
            return evaluator.Evaluate(features);
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            int good = 0, hasIdeal = 0, totalquery = 0;

            MovieExpressionEvaluator evaluator = MovieExpressionEvaluator.ParseExpression(arguments.ScoringExpression);
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.TrainingFile))
                {
                    string line;
                    string currQ = "";
                    Dictionary<string, MovieCandidateFeature> dictCand2Score = new Dictionary<string, MovieCandidateFeature>();
                    HashSet<string> groundtruth = new HashSet<string>();
                    bool isGood, isIdeal;

                    sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length < 10)
                            continue;

                        string q = items[0];
                        if (!string.IsNullOrEmpty(currQ) && !string.Equals(q, currQ))
                        {
                            Evaluate(dictCand2Score, evaluator, groundtruth, out isGood, out isIdeal);
                            if (isGood) good++;
                            if (isIdeal) hasIdeal++;
                            totalquery++;

                            dictCand2Score.Clear();
                            groundtruth.Clear();
                        }

                        currQ = q;

                        string cand = items[1];
                        MovieCandidateFeature f = MovieCandidateFeature.FromString(items, 2);
                        if (null == f)
                            continue;

                        //if (f.webPos > 20)
                        if (evaluator.Evaluate(f) < arguments.MinScore)
                            continue;

                        //double score = evaluator.Evaluate(f);
                        dictCand2Score[cand] = f;

                        int matchG = int.Parse(items[9]);
                        if (matchG > 0)
                            groundtruth.Add(cand);
                    }

                    Evaluate(dictCand2Score, evaluator, groundtruth, out isGood, out isIdeal);
                    if (isGood) good++;
                    if (isIdeal) hasIdeal++;
                    totalquery++;
                }

                sw.WriteLine("Good: {0}", good);
                sw.WriteLine("HasIdeal: {0}", hasIdeal);
                sw.WriteLine("Precision: {0}", (double)good / hasIdeal);
                sw.WriteLine("Total: {0}", totalquery);
            }
        }

        public static void Evaluate(Dictionary<string, MovieCandidateFeature> dictCand2Score,
            MovieExpressionEvaluator evaluator, HashSet<string> groundtruth, out bool good, out bool hasIdeal)
        {
            good = false;
            if (groundtruth.Count == 0)
            {
                hasIdeal = false;
                return;
            }

            //if (dictCand2Score.Count > 1)
            //{
            //    hasIdeal = false;
            //    return;
            //}

            hasIdeal = true;

            var sorted = from p in dictCand2Score
                         orderby evaluator.Evaluate(p.Value) descending
                         //, p.Value.webPos ascending
                         select p;
            double bestScore = evaluator.Evaluate(sorted.ElementAt(0).Value);
            List<string> best = new List<string>();
            foreach (var s in sorted)
            {
                if (evaluator.Evaluate(s.Value) != bestScore)
                    break;
                best.Add(s.Key);
            }

            foreach (var b in best)
            {
                if (groundtruth.Contains(b))
                {
                    good = true;
                    break;
                }
            }
        }
    }

    public class MovieExpressionEvaluator
    {
        private CSharpCompiler.Invoker _invoker = null;

        public static MovieExpressionEvaluator ParseExpression(string expression)
        {
            string errMsg;

            string source = CSharpCompiler.BuildCSharpSource(
                string.Format("double Evaluator(QU.Utility.MovieCandidateFeature f) {{ return {0}; }}", expression));

            //Console.WriteLine(source);

            //string source = string.Format("using System;\nusing TSVUtility;\nnamespace AEtherUtilities.Dynamic {{\npublic class Expression_1 {{public static bool Evaluator(TSVLine line) {{ return {0}; }}}}}}", expression);

            var results = CSharpCompiler.CompileSource(source, new string[] { "QU.Utility.dll" }, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Console.Error.WriteLine(errMsg);
                return null;
            }

            MovieExpressionEvaluator evaluator = new MovieExpressionEvaluator();
            evaluator._invoker = CSharpCompiler.GetInvoker(
                CSharpCompiler.GetMethod(results, "Evaluator", BindingFlags.Static | BindingFlags.Public)
                );

            return evaluator;
        }

        public double Evaluate(MovieCandidateFeature features)
        {
            return (double)_invoker(features);
        }
    }
}
