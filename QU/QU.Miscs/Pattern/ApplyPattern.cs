using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TSVUtility;

namespace QU.Miscs
{
    public class ApplyPattern
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "slot")]
            public string SlotParsingFile;

            [Argument(ArgumentType.Required, ShortName = "pattern")]
            public string PatternFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "feature")]
            public string FeatureFile;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;

            [Argument(ArgumentType.AtMostOnce, ShortName = "h")]
            public bool HasHeader = false;
        }

        static void ReadFeatureFile(string file, ref Dictionary<string, ReformulationFeatures> dictPatterns2Prec)
        {
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(file)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);

                TSVLine line;
                while ((line = tsvReader.ReadLine()) != null)
                {
                    ReformulationFeatures features = new ReformulationFeatures();
                    features.BinaryClickCoverage = line.GetDoubleFeature("BinaryClickCoverage", 2);
                    features.FloatClickCoverage = line.GetDoubleFeature("FloatClickCoverage", 3);
                    features.BinarySatClickCoverage = line.GetDoubleFeature("BinarySatClickCoverage", 4);
                    features.FloatSatClickCoverage = line.GetDoubleFeature("FloatSatClickCoverage", 5);
                    features.ClickRatio = line.GetDoubleFeature("ClickRatio", 6);
                    features.SatClickRatio = line.GetDoubleFeature("SatClickRatio", 7);
                    features.ClickYield = line.GetDoubleFeature("ClickYield", 8);
                    features.SatClickYield = line.GetDoubleFeature("SatClickYield", 9);

                    dictPatterns2Prec[ReformulationPatternMatch.MakePair(line.GetString("Query", 0), line.GetString("AlteredQuery", 1))] = features;
                }
            }
        }

        /// <summary>
        /// Apply filtered patterns. Assume that the patterns are pre-filtered.
        /// </summary>
        /// <param name="args"></param>
        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.SlotParsingFile) || !File.Exists(arguments.PatternFile))
            {
                Console.WriteLine("No Pattern File!");
                return;
            }

            DateTime prev = DateTime.Now;
            List<ReformulationPattern> patterns = Extensions.ReadPatterns(arguments.PatternFile, 5);
            if (File.Exists(arguments.FeatureFile))
            {
                patterns.GetFeatures(arguments.FeatureFile, arguments.HasHeader);
            }

            Dictionary<string, List<ReformulationPattern>> dictLeftP2ReformPatterns = BuildDict(patterns);
            ReformulationPatternTrie trie = new ReformulationPatternTrie();
            trie.BuildTree(dictLeftP2ReformPatterns.Keys, dictLeftP2ReformPatterns);
            DateTime curr = DateTime.Now;
            Console.WriteLine("Parse pattern file: {0}s", (curr - prev).TotalSeconds);
            Console.WriteLine("Patterns: {0}", dictLeftP2ReformPatterns.Count);

            //Dictionary<string, ReformulationFeatures> dictPatterns2Features
            //    = new Dictionary<string, ReformulationFeatures>();
            //if (File.Exists(arguments.FeatureFile))
            //{
            //    Extensions.ReadFeatureFile(arguments.FeatureFile, arguments.HasHeader, ref dictPatterns2Features);
            //}

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.SlotParsingFile))
                {
                    string line;
                    StringBuilder sbOut = new StringBuilder();
                    prev = DateTime.Now;

                    string q = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length == 0)
                            continue;

                        q = items[0];
                        string slots = items.Length > 1 ? items[1] : q;

                        List<QueryParseResult> results;
                        string qWithSlots = ReformulationPatternMatch.ReplaceSlot(q, slots, out results, true);

                        // classify if a query should be altered.
                        bool toBeAltered = ReformulationPatternMatch.HasReformulationPotential(q, qWithSlots);
                        if (!toBeAltered)
                        {
                            continue;
                        }

                        // enumerate all of the pairs to get reformulations.
                        List<ReformulationPatternTrie.MatchInfo> matchedNodes;
                        if (!trie.FindMatches(qWithSlots, out matchedNodes))
                        {
                            continue;
                        }

                        Console.WriteLine("{0}\t{1}", q, matchedNodes.Count);

                        HashSet<string> setPatternPairs = new HashSet<string>();
                        foreach (var node in matchedNodes)
                        {
                            if (node.reformPatterns == null || node.reformPatterns.Count == 0)
                                continue;

                            foreach (var val in node.reformPatterns)
                            {
                                string key = ReformulationPatternMatch.MakePair(val.Left, val.Right);
                                if (setPatternPairs.Contains(key))
                                {
                                    continue;
                                }

                                setPatternPairs.Add(key);

                                //string alteredQ = "";
                                val.UpdateFeature(qWithSlots);
                                string alteredQ = ReformulationPatternMatch.ApplyPattern(q, results, qWithSlots, node.wildPhrases, val.Right);
                                if (string.IsNullOrEmpty(alteredQ))
                                {
                                    continue;
                                }

                                ReformulationFeatures features = val.Features != null ? val.Features : new ReformulationFeatures();
                                sbOut.Clear();

                                sbOut.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                                    q, alteredQ, qWithSlots, val.Left, val.Right, node.reformPatterns.Count);
                                // features
                                sbOut.Append('\t');
                                sbOut.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}",
                                    features.BinaryClickCoverage,
                                    features.FloatClickCoverage,
                                    features.BinarySatClickCoverage,
                                    features.FloatSatClickCoverage,
                                    features.ClickRatio,
                                    features.SatClickRatio,
                                    features.ClickYield,
                                    features.SatClickYield,
                                    features.L2R,
                                    features.ReformProb,
                                    features.ExactMatch,
                                    features.DeltaQueryLength,
                                    features.RIntentPhraseLen,
                                    features.WildcardCount,
                                    features.WildcardMatchtedTerms, 
                                    features.L2RPercent);

                                sw.WriteLine(sbOut.ToString());
                            }
                        }
                    }

                    curr = DateTime.Now;
                    Console.WriteLine("Parsing Duration: {0}s", (curr - prev).TotalSeconds);
                }
            }
        }

        private static Dictionary<string, List<ReformulationPattern>> BuildDict(List<ReformulationPattern> patterns)
        {
            Dictionary<string, List<ReformulationPattern>> dictLeftP2ReformPatterns
                = new Dictionary<string, List<ReformulationPattern>>();

            foreach (var p in patterns)
            {
                if (p == null)
                    continue;

                // remove all of the wild-match.
                if (p.Left.Split(new char[] { ' ', '*' }, StringSplitOptions.RemoveEmptyEntries).Length < 1)
                    continue;

                if (dictLeftP2ReformPatterns.ContainsKey(p.Left))
                {
                    dictLeftP2ReformPatterns[p.Left].Add(p);
                }
                else
                {
                    dictLeftP2ReformPatterns.Add(p.Left, new List<ReformulationPattern>(new[] { p }));
                }
            }

            return dictLeftP2ReformPatterns;
        }
    }
}
