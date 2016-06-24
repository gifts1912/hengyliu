using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyMisc = Utility;

namespace QU.Miscs
{
    public class AnalyzePattern
    {
        public static void Run(string[] args)
        {
            AnalyzePatternArgs arguments = new AnalyzePatternArgs();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.Input))
            {
                Console.WriteLine("No Pattern File!");
                return;
            }

            // Load stopwords
            HashSet<string> stopwords = MyMisc.StopWordUtil.LoadFromFile(
                                                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "stopwords.txt")
                                                );

            Dictionary<string, ReformulationPattern> dict = new Dictionary<string, ReformulationPattern>();
            int id = 0;
            using (StreamWriter sw = new StreamWriter(arguments.FilteredFile))
            {
                using (StreamReader sr = new StreamReader(arguments.Input))
                {
                    string query = string.Empty;
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (id++ % 10000 == 0)
                        {
                            Console.WriteLine("Process Line {0}", id);
                        }

                        ReformulationPattern p = ReformulationPattern.ReadFromLineWith3Items(line);
                        if (p == null)
                            continue;

                        string left = p.Left;
                        string right = p.Right;

                        // force left and right must have one overlapped slot.
                        if (arguments.MatchSlot)
                        {
                            HashSet<string> leftSlots = new HashSet<string>(left.Split(' ').Where(t => t.StartsWith("Slot^")));
                            bool foundOverlap = false;
                            foreach (var r in right.Split(' ').Where(t => t.StartsWith("Slot^")))
                            {
                                if (leftSlots.Contains(r))
                                {
                                    foundOverlap = true;
                                    break;
                                }
                            }

                            if (!foundOverlap)
                            {
                                continue;
                            }
                        }

                        int cnt = p.L2R;
                        string key = left + "#&#" + right;
                        string reverseKey = right + "#&#" + left;

                        if (dict.ContainsKey(key))
                        {
                            continue;
                        }

                        ReformulationPattern reversePattern;
                        if (dict.TryGetValue(reverseKey, out reversePattern))
                        {
                            reversePattern.R2L = cnt;
                        }
                        else
                        {
                            // filter logic
                            if (cnt < arguments.MinOccurrence)
                            {
                                continue;
                            }

                            if (IsSimpleReformulation(left, right, stopwords)
                                || SlotMismatch(left, right)
                                || WildMismatch(left, right))
                            {
                                continue;
                            }

                            dict.Add(key, p);
                        }

                        sw.WriteLine(line);
                    }
                }
            }

            Dictionary<MyMisc.SimilarQueryType, int> dictType2Cnt = new Dictionary<MyMisc.SimilarQueryType, int>();
            var sorted = from p in dict
                         orderby p.Value.L2R descending, p.Value.R2L ascending
                         select p;
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                foreach (var s in sorted)
                {
                    sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", s.Value.Left, s.Value.Right, s.Value.L2R, s.Value.R2L, s.Value.Type);
                    if (!dictType2Cnt.ContainsKey(s.Value.Type))
                    {
                        dictType2Cnt.Add(s.Value.Type, 0);
                    }
                    dictType2Cnt[s.Value.Type]++;
                }
            }

            using (StreamWriter sw = new StreamWriter(arguments.StatFile))
            {
                foreach (var p in dictType2Cnt)
                {
                    sw.WriteLine(p.Key + "\t" + p.Value);
                }
            }
        }

        static char[] seperators = new char[] { ' ', '*' };
        static char[] space = new char[] { ' ' };

        /// <summary>
        /// Simple Reform: just some stopwords in patterns
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="stopwords"></param>
        /// <returns></returns>
        private static bool IsSimpleReformulation(string left, string right, HashSet<string> stopwords)
        {
            // left should not be just "*".
            var items = left.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length == 0)
            {
                return true;
            }

            foreach (var item in items)
            {
                if (!stopwords.Contains(item))
                {
                    return false;
                }
            }

            items = right.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                if (!stopwords.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool SlotMismatch(string left, string right)
        {
            var leftItems =
                new HashSet<string>(left.Split(space, StringSplitOptions.RemoveEmptyEntries).Where(t => t.StartsWith("Slot^")));
            var rightItems =
                new HashSet<string>(right.Split(space, StringSplitOptions.RemoveEmptyEntries).Where(t => t.StartsWith("Slot^")));
            if (leftItems.Count != rightItems.Count)
                return true;
            HashSet<string> overlapped;
            int overlap = MyMisc.CommonUtils.Overlap(leftItems, rightItems, out overlapped);
            if (overlap != leftItems.Count)
                return true;
            return false;
        }

        private static bool WildMismatch(string left, string right)
        {
            int leftWild = left.Count(c => c == '*');
            int rightWild = right.Count(c => c == '*');
            if (leftWild != rightWild || rightWild > 2)
                return true;
            return false;
        }


        class AnalyzePatternArgs
        {
            [Argument(ArgumentType.Required, ShortName = "in")]
            public string Input;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;

            [Argument(ArgumentType.Required, ShortName = "stat")]
            public string StatFile;

            [Argument(ArgumentType.Required, ShortName = "filtered")]
            public string FilteredFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "min")]
            public int MinOccurrence = 5;

            [Argument(ArgumentType.AtMostOnce, ShortName = "slot")]
            public bool MatchSlot = false;
        }
    }
}
