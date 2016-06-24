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
    class MovieMergeRerank
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "main")]
            public string MainExtraction = "";

            [Argument(ArgumentType.Required, ShortName = "aux")]
            public string AuxilliaryExtraction = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.Required, ShortName = "expression")]
            public string ScoringExpression = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "min")]
            public double MinScore = -10;

            [Argument(ArgumentType.AtMostOnce, ShortName = "topn")]
            public int TopN = 20;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            MovieExpressionEvaluator evaluator = MovieExpressionEvaluator.ParseExpression(arguments.ScoringExpression);
            TSVLine headerLine;
            List<QueryBlock> blocks;
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.MainExtraction)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                QueryBlockReader qbReader = new QueryBlockReader(tsvReader);
                blocks = qbReader.ReadQueryBlocks().ToList();
                headerLine = tsvReader.HeaderTSVLine;
            }

            // Aux extraction.


            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                sw.WriteLine(headerLine.GetWholeLineString() + "\tRerankScore");
                foreach (var block in blocks)
                {
                    Dictionary<long, double> dictMovie2Score;
                    Dictionary<long, MovieCandidateFeature> dictMovie2Features;
                    EvaluateMovieCandidates(evaluator, block, arguments.TopN, out dictMovie2Score, out dictMovie2Features);

                    bool triggered = Rerank(dictMovie2Score, block, arguments.TopN, arguments.MinScore, sw);
                    if (triggered)
                    {
                        Console.WriteLine("Trigger QueryId: {0}", block.QueryId);
                    }
                }
            }
        }

        static bool Rerank(Dictionary<long, double> dictMovie2Score, QueryBlock block, int topN, double minScore, StreamWriter sw)
        {
            bool triggered = false;
            Dictionary<long, double> top3Movies = new Dictionary<long, double>();
            if (dictMovie2Score != null && dictMovie2Score.Count > 0)
            {
                var temp = (from p in dictMovie2Score
                            //where p.Value >= minScore
                            orderby p.Value descending
                            select p).Take(3);
                foreach (var t in temp)
                {
                    top3Movies[t.Key] = t.Value;
                }
            }
            triggered = top3Movies.Count > 0;

            if (!triggered)
                return triggered;

            int score;
            foreach (var line in block.Lines)
            {
                int position = (int)line.GetFeatureValue("DocumentPosition");
                if (position > topN)
                    continue;

                long apf1194 = (long)line.GetFeatureValue("AdvancedPreferFeature_1194");
                string qaFact = line["m:QAFact"];

                score = 1000 - position;
                if (apf1194 > 0)
                {
                    double movieScore;
                    if (top3Movies.TryGetValue(apf1194, out movieScore))
                    {
                        score += (int)(1010 + movieScore);
                    }
                }
                else if (!string.IsNullOrEmpty(qaFact))
                {
                    double maxMovieScore = -1000;
                    foreach (var f in qaFact.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        long cand = long.Parse(f.Split('|')[0]);
                        double movieScore;
                        if (top3Movies.TryGetValue(cand, out movieScore))
                        {
                            maxMovieScore = Math.Max(movieScore, maxMovieScore);
                        }
                    }
                    score += (int)(1000 + maxMovieScore);
                }

                sw.WriteLine(line.GetWholeLineString() + "\t" + score);
            }

            return triggered;
        }

        static void EvaluateMovieCandidates(MovieExpressionEvaluator evaluator, QueryBlock block, int topN,
            out Dictionary<long, double> dictMovie2Score,
            out Dictionary<long, MovieCandidateFeature> dictMovie2Features)
        {
            dictMovie2Features = new Dictionary<long, MovieCandidateFeature>();

            #region Extract feature
            foreach (var line in block.Lines)
            {
                string query = line["m:Query"];
                string url = line["m:Url"];
                int position = (int)line.GetFeatureValue("DocumentPosition");
                if (position > topN)
                    continue;

                uint apf1194 = line.GetFeatureValue("AdvancedPreferFeature_1194");
                string qaFact = line["m:QAFact"];

                if (apf1194 > 0)
                {
                    if (!dictMovie2Features.ContainsKey(apf1194))
                    {
                        dictMovie2Features.Add(apf1194, new MovieCandidateFeature());
                    }

                    dictMovie2Features[apf1194].hasWebAns = true;
                    ++dictMovie2Features[apf1194].webOcc;
                    dictMovie2Features[apf1194].webPos = Math.Min(dictMovie2Features[apf1194].webPos, position);
                }

                Dictionary<long, int> QAFacts = null;
                if (!string.IsNullOrEmpty(qaFact))
                {
                    QAFacts = new Dictionary<long, int>();
                    foreach (var f in qaFact.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string[] items = f.Split('|');
                        int score = 1;
                        if (items.Length > 1)
                            score = int.Parse(items[1]);
                        QAFacts[long.Parse(items[0])] = score;
                    }

                    foreach (var f in QAFacts)
                    {
                        if (!dictMovie2Features.ContainsKey(f.Key))
                        {
                            dictMovie2Features.Add(f.Key, new MovieCandidateFeature());
                        }

                        dictMovie2Features[f.Key].hasQAFact = true;
                        dictMovie2Features[f.Key].QAScore += f.Value;
                        ++dictMovie2Features[f.Key].qaOcc;
                        dictMovie2Features[f.Key].qaPos = Math.Min(dictMovie2Features[f.Key].qaPos, position);
                    }
                }
            }
            #endregion

            dictMovie2Score = new Dictionary<long, double>();
            foreach (var cand in dictMovie2Features)
            {
                if (cand.Value.hasWebAns && cand.Value.hasQAFact)
                    dictMovie2Score[cand.Key] = evaluator.Evaluate(cand.Value);
            }
        }
    }
}
