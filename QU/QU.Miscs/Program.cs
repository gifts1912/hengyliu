using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QU.Utility;
using System.IO;

namespace QU.Miscs
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No valid args");
                return;
            }

            string[] cmdArgs = args.Skip(1).ToArray();
            if (args[0].Equals("getdomainqueries", StringComparison.OrdinalIgnoreCase))
            {
                GetDomainQueries.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("analyzepattern", StringComparison.OrdinalIgnoreCase))
            {
                AnalyzePattern.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("matchpattern", StringComparison.OrdinalIgnoreCase))
            {
                PatternMatch.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("applypattern", StringComparison.OrdinalIgnoreCase))
            {
                ApplyPattern.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("seltopn", StringComparison.OrdinalIgnoreCase))
            {
                SelectTopNAlteration.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("GenerateMQText", StringComparison.OrdinalIgnoreCase))
            {
                GenerateMQText.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("AnalyzePostwebPotential", StringComparison.OrdinalIgnoreCase))
            {
                PostwebQU.AnalyzePotential.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("StatPrecision", StringComparison.OrdinalIgnoreCase))
            {
                StatPrecision.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("GetSymptoms", StringComparison.OrdinalIgnoreCase))
            {
                GetSymptoms.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MatchSymptoms", StringComparison.OrdinalIgnoreCase))
            {
                MatchSymptoms.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("DropWordsInQas", StringComparison.OrdinalIgnoreCase))
            {
                DropWordsInQas.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("FindStopwords", StringComparison.OrdinalIgnoreCase))
            {
                FindStopwords.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("ClusterPatterns", StringComparison.OrdinalIgnoreCase))
            {
                ClusterPatterns.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("GetNoWildcardPatterns", StringComparison.OrdinalIgnoreCase))
            {
                GetNoWildcardPatterns.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("IntentTranslation", StringComparison.OrdinalIgnoreCase))
            {
                IntentTranslation.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("GetImplicitIntent", StringComparison.OrdinalIgnoreCase))
            {
                GetImplicitIntent.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("NameFuzzyMatch", StringComparison.OrdinalIgnoreCase))
            {
                NameFuzzyMatch.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("InjectNamePath", StringComparison.OrdinalIgnoreCase))
            {
                InjectNamePath.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("FilterCharEM", StringComparison.OrdinalIgnoreCase))
            {
                FilterCharEM.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("AggregatePattern", StringComparison.OrdinalIgnoreCase))
            {
                AggregatePattern.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("FilterDesiredPatterns", StringComparison.OrdinalIgnoreCase))
            {
                FilterDesiredPatterns.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("FuzzyEntity", StringComparison.OrdinalIgnoreCase))
            {
                Entity.LowDistortionEntityRewriting.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MeasureERPrecision", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MeasureERPrecision.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MeasureBingPrecision", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MeasureBingPrecision.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("GetPostWebMovieName", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.GetPostWebMovieName.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MergeMovieScrapes", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MergeMovieScrapes.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MovieQU", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MovieQU.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MusicSiteMQ", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MusicSiteExtraQuery.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("ConvertWebHrsWrtERHrs", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.ConvertWebHrsWrtERHrs.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MeasurePrecBasedonAPF", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MeasurePrecBasedonAPF.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("RankMagicMovieCandidate", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.RankMagicMovieCandidate.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("GenTLCTrainingData", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.GenTLCTrainingData.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MovieRerank", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MovieRerank.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("ExtractResultsFromPBXML", StringComparison.OrdinalIgnoreCase))
            {
                Entity.ExtractResultsFromPBXML.Run(cmdArgs);
                return;
            }
            else if (args[0].Equals("MovieRuleTuning", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MovieRuleTuning.Run(cmdArgs);
            }
            else if (args[0].Equals("MovieScoring", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MovieScoring.Run(cmdArgs);
            }
            else if (args[0].Equals("StatHostPopularity", StringComparison.OrdinalIgnoreCase))
            {
                StatHostPopularity.Run(cmdArgs);
            }
            else if (args[0].Equals("judge", StringComparison.OrdinalIgnoreCase))
            {
                GenFakeJudge(args[1], args[2]);
            }
            else if (args[0].Equals("AddMovieId2Extraction", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.AddMovieId2Extraction.Run(cmdArgs);
            }
            else if (args[0].Equals("MeasureProdMovieCandidates", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MeasureProdMovieCandidates.Run(cmdArgs);
            }
            else if (args[0].Equals("MovieRerankV2", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.MovieRerankV2.Run(cmdArgs);
            }
            else if (args[0].Equals("Dedup", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.DedupExtraction.Run(cmdArgs);
            }
            else if (args[0].Equals("JoinRankScore", StringComparison.OrdinalIgnoreCase))
            {
                MagicQ.JoinRankScore.Run(cmdArgs);
            }
            else if (args[0].Equals("ConvertLexiconFile", StringComparison.OrdinalIgnoreCase))
            {
                Misc.ConvertLexiconFile.Run(cmdArgs);
            }
            else if (args[0].Equals("test", StringComparison.OrdinalIgnoreCase))
            {
                Test(cmdArgs);
            }
        }

        static void Test(string[] args)
        {
            Console.WriteLine("\x01");
            string[] data = new string[] 
            {
                "ac",
                "ad",
                "abd",
                "abe",
                "abf",
                "abde",
                "bd",
                "cbd",
                "eabd"
            };
            Trie trie = new Trie();
            trie.BuildTree(data);
            List<Trie.LevSearchResult> results = new List<Trie.LevSearchResult>();
            trie.LevSearch(args[0], int.Parse(args[1]), ref results);
            foreach (var r in results)
            {
                Console.WriteLine(r.EditDist + "\t" + r.Term + "\t" + string.Join("|||", r.Trails));
            }
        }

        static void GenFakeJudge(string queryFile, string outFile)
        {
            using (StreamWriter sw = new StreamWriter(outFile))
            {
                sw.WriteLine("HitID,Query,JudgeID,BaseName,ExpName,Judgment,ReadableJudgment,JudgmentSubmitTime,JudgmentSpentTime,OverallReason,Comment,Link to HIT Review");
                using (StreamReader sr = new StreamReader(queryFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;
                        sw.WriteLine("12,\"{0}\",12,\"baseline.xml\",\"treatment.xml\",\"0\",\"About the Same\",,149,,,\"http\"", line.Replace("\"", ""));
                    }
                }
            }
        }
    }
}
