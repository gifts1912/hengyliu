using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QU.Utility;
using System.Reflection;
using MyMisc = Utility;

namespace QU.Miscs
{
    class GetImplicitIntent
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "qp")]
            public string QueryParseFile = "";

            [Argument(ArgumentType.Required, ShortName = "it")]
            public string IntentTranslationFile = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "minm")]
            public double MinMatch = 0.5;

            [Argument(ArgumentType.AtMostOnce, ShortName = "maxi")]
            public int MaxIntent = 8;

            public bool InputValid { get { return File.Exists(QueryParseFile) && File.Exists(IntentTranslationFile); } }
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            // Load stopwords
            HashSet<string> stopwords = MyMisc.StopWordUtil.LoadFromFile(
                                                Path.Combine(
                                                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                    "stopwords.txt"
                                                    )
                                                );

            Console.WriteLine("Building pattern trie");
            Dictionary<string, List<ReformulationPattern>> dictLeftP2ReformPatterns
                = ReadReformPatterns(arguments.IntentTranslationFile);
            ReformulationPatternTrie trie = new ReformulationPatternTrie();
            trie.BuildTree(dictLeftP2ReformPatterns.Keys, dictLeftP2ReformPatterns);

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.QueryParseFile))
                {
                    string line;
                    StringBuilder sbOut = new StringBuilder();

                    string q = "";
                    int count = 0, hasImplicitIntCount = 0, hasExplicitIntCount = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length == 0)
                            continue;

                        q = items[0];
                        string slots = items.Length > 1 ? items[1] : q;
                        count++;

                        List<QueryParseResult> results;
                        string qWithSlots = ReformulationPatternMatch.ReplaceSlot(q, slots, out results, true);

                        bool hasInt = false;
                        if (null != results)
                        {
                            string intent;
                            ReformulationPatternMatch.GetIntentPhraseLen(qWithSlots, out intent);

                            if (!string.IsNullOrEmpty(intent))
                            {
                                sw.WriteLine("{0}\t{1}\t{2}\t{3}", q, "Slot^" + intent, "", 0);
                                hasExplicitIntCount++;
                                continue;
                            }
                        }

                        // enumerate all of the pairs to get reformulations.
                        List<ReformulationPatternTrie.MatchInfo> matchedNodes;
                        if (!trie.FindMatches(qWithSlots, out matchedNodes))
                        {
                            continue;
                        }

                        hasInt = false;
                        HashSet<string> setPatternPairs = new HashSet<string>();
                        HashSet<string> setRightPatterns = new HashSet<string>();
                        List<ReformulationPattern> patterns = new List<ReformulationPattern>();
                        foreach (var node in matchedNodes)
                        {
                            foreach (var p in node.reformPatterns)
                            {
                                string key = ReformulationPatternMatch.MakePair(p.Left, p.Right);
                                if (setPatternPairs.Contains(key))
                                {
                                    continue;
                                }

                                if (!HasImplicitIntentPotential(p.Left, stopwords))
                                    continue;

                                setPatternPairs.Add(key);

                                ReformulationFeatures features = ReformulationPatternMatch.ExtractReformulationFeatures(qWithSlots, p);
                                if (features.ExactMatch >= arguments.MinMatch)
                                {
                                    setRightPatterns.Add(p.Right);
                                    //sw.WriteLine("{0}\t{1}\t{2}\t{3}", q, p.Right, p.Left, p.L2R);
                                    patterns.Add(p);
                                    hasInt = true;
                                }
                            }
                        }

                        if (setRightPatterns.Count >= arguments.MaxIntent)
                        {
                            continue;
                        }

                        foreach (var p in patterns)
                        {
                            sw.WriteLine("{0}\t{1}\t{2}\t{3}", q, p.Right, p.Left, p.L2R);
                        }

                        if (hasInt)
                        {
                            hasImplicitIntCount++;
                        }
                    }

                    Console.WriteLine("{0}/{1} has explicit intents.", hasExplicitIntCount, count);
                    Console.WriteLine("{0}/{1} has implicit intents.", hasImplicitIntCount, count);
                }
            }
        }

        private static bool HasImplicitIntentPotential(string pattern, HashSet<string> stopwords)
        {
            string[] items = pattern.Split(new char[] { ' ', '*' }, StringSplitOptions.RemoveEmptyEntries);
            int termCnt = 0;
            foreach (var item in items)
            {
                if (stopwords.Contains(item) || item.StartsWith("Slot^Ent_"))
                    continue;
                termCnt++;
            }
            return termCnt > 0;
        }

        private static Dictionary<string, List<ReformulationPattern>> ReadReformPatterns(string file)
        {
            Dictionary<string, List<ReformulationPattern>> dictLeftP2ReformPatterns
                = new Dictionary<string, List<ReformulationPattern>>();

            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    ReformulationPattern p = ReformulationPattern.ReadFromLineWith3Items(line);
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
            }

            return dictLeftP2ReformPatterns;
        }
    }
}
