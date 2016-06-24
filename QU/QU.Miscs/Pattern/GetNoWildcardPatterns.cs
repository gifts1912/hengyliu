using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs
{
    class GetNoWildcardPatterns
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "p")]
            public string PatternFile = "";

            [Argument(ArgumentType.Required, ShortName = "f")]
            public string FeatureFile = "";

            [Argument(ArgumentType.Required, ShortName = "o")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "of")]
            public bool OutputFeature = false;

            [Argument(ArgumentType.AtMostOnce, ShortName = "uniq")]
            public bool Uniq = false;

            [Argument(ArgumentType.AtMostOnce, ShortName = "mins")]
            public double MinScore = 0.5;

            [Argument(ArgumentType.AtMostOnce, ShortName = "expression")]
            public string ScoringExpression = "";

            public bool InputValid { get { return File.Exists(PatternFile) && File.Exists(FeatureFile); } }
        }

        private static double Score(ReformulationFeatures features)
        {
            return features.FloatClickCoverage * 0.8
                + features.ClickYield * 0.1
                + features.L2RPercent * 0.1
                - 0.1 * Math.Max(-3, Math.Min(3, (features.DeltaQueryLength - features.RIntentPhraseLen)))
                //- 0.1 * (features.DeltaQueryLength - features.RIntentPhraseLen)
                //+ 0.15 * (features.RIntentPhraseLen > 0 ? 1 : 0)
                ;
        }

        private static double Score(ReformulationFeatures features, ExpressionEvaluator evaluator)
        {
            if (evaluator == null)
                return Score(features);

            return evaluator.Evaluate(features);
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            ExpressionEvaluator evaluator = null;
            if (!string.IsNullOrEmpty(arguments.ScoringExpression))
            {
                evaluator = ExpressionEvaluator.ParseExpression(arguments.ScoringExpression);
            }

            var patterns = Extensions.ReadPatterns(arguments.PatternFile);
            patterns.GetFeatures(arguments.FeatureFile, false);

            var sorted = from p in patterns
                         where p.Features != null
                         orderby p.Left, Score(p.Features, evaluator) descending
                         select p;

            int total = 0, noWildCount = 0;
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                string currLeft = string.Empty;
                foreach (var p in sorted)
                {
                    total++;

                    if (arguments.Uniq)
                    {
                        if (p.Left == currLeft)
                            continue;
                    }

                    if (string.IsNullOrEmpty(p.Left)
                        || string.IsNullOrEmpty(p.Right)
                        || p.Features == null
                        || p.Left.Contains('*')
                        || p.Right.Contains('*')
                        || p.Features.FloatClickCoverage < 0.3
                        || p.Features.IntentMissing
                        )
                    {
                        continue;
                    }

                    currLeft = p.Left;

                    double score = 0;
                    score = Score(p.Features, evaluator);
                    if (score >= arguments.MinScore
                        )
                    {
                        noWildCount++;
                        if (arguments.OutputFeature)
                        {
                            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                                p.Left, p.Right, p.L2R, p.R2L,
                                p.Features.FloatClickCoverage, p.Features.ClickYield,
                                p.Features.L2RPercent,
                                score
                                );
                        }
                        else
                        {
                            sw.WriteLine("{0}\t{1}\t{2}\t{3}", p.Left, p.Right, p.L2R, p.R2L);
                        }
                    }
                }
            }

            Console.WriteLine("==============Stat================");
            Console.WriteLine("Total: {0}", total);
            Console.WriteLine("NoWildCard: {0}", noWildCount);
        }


    }
}
