using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyMisc = Utility;

namespace QU.Miscs
{
    public class IntentTranslation
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.AtMostOnce, ShortName = "p")]
            public string PatternFile = "";

            [Argument(ArgumentType.Required, ShortName = "o")]
            public string Output = "";

            [Argument(ArgumentType.Required, ShortName = "op")]
            public string PatternOutput = "";

            //[Argument(ArgumentType.Required, ShortName = "i")]
            //public string IntentFile = "";

            //[Argument(ArgumentType.Required, ShortName = "m")]
            //public string ModelDirectory = "";

            public bool InputValid { get { return File.Exists(PatternFile); } }
        }

        //static HashSet<string> ReadIntentFile(string file)
        //{
        //    var intents = new HashSet<string>();
        //    using (StreamReader sr = new StreamReader(file))
        //    {
        //        string line;

        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            if (string.IsNullOrEmpty(line))
        //                continue;

        //            intents.Add(line);
        //        }
        //    }

        //    return intents;
        //}

        static void ReplaceCommonWord(string[] left, HashSet<string> right)
        {
            for (int i = 0; i < left.Length; i++)
            {
                if (right.Contains(left[i]))
                {
                    left[i] = "*";
                }
            }
        }

        //static List<string> ParseIntents(CRFModel model, string query)
        //{
        //    SlotInfo[] slots = model.Parser.Evaluate(query);
        //    if (null == slots || slots.Length == 0)
        //    {
        //        return null;
        //    }

        //    var intents = new List<string>();
        //    foreach (var slot in slots)
        //    {
        //        if (slot.Tag.StartsWith("Int_"))
        //        {
        //            intents.Add(slot.Tag);
        //        }
        //    }

        //    return intents;
        //}

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            //CRFModel model = new CRFModel(arguments.ModelDirectory, "health.default", "health");

            //HashSet<string> intents = ReadIntentFile(arguments.IntentFile);
            // Load stopwords
            HashSet<string> stopwords = MyMisc.StopWordUtil.LoadFromFile(
                                                Path.Combine(
                                                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                    "stopwords.txt"
                                                    )
                                                );

            List<ReformulationPattern> patterns = new List<ReformulationPattern>();
            using (StreamReader sr = new StreamReader(arguments.PatternFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    ReformulationPattern p = ReformulationPattern.ReadFromLineWith5Items(line);
                    if (p == null)
                        continue;

                    patterns.Add(p);
                }
            }

            Dictionary<string, int> tuples = new Dictionary<string, int>();
            Dictionary<string, int> patternIntent = new Dictionary<string, int>();

            int id = 0;
            foreach (var p in patterns)
            {
                if (id++ % 1000 == 0)
                {
                    Console.WriteLine("Processed {0}/{1}", id - 1, patterns.Count);
                }

                string leftIntent;
                ReformulationPatternMatch.GetIntentPhraseLen(p.Left, out leftIntent);
                if (!string.IsNullOrEmpty(leftIntent))
                    continue;

                string rightIntent;
                ReformulationPatternMatch.GetIntentPhraseLen(p.Right, out rightIntent);
                if (string.IsNullOrEmpty(rightIntent))
                    continue;
                rightIntent = "Slot^" + rightIntent;

                string[] leftItems = p.Left.Split(space, StringSplitOptions.RemoveEmptyEntries);
                string[] rightItems = p.Right.Split(space, StringSplitOptions.RemoveEmptyEntries);
                ReplaceCommonWord(leftItems, new HashSet<string>(rightItems));

                string[] leftNgrams = GetNgrams(leftItems);
                HashSet<string> allLeftNgrams = new HashSet<string>();
                foreach (var ln in leftNgrams)
                {
                    foreach (var i in Split(ln, 4))
                        allLeftNgrams.Add(i);
                }

                foreach (var l in allLeftNgrams)
                {
                    if (l.Equals(rightIntent, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    //tuples.Add(new Tuple<string, string, int>(l, r, p.L2R));
                    string k = ReformulationPatternMatch.MakePair(l, rightIntent);
                    if (!tuples.ContainsKey(k))
                    {
                        tuples.Add(k, p.L2R);
                    }
                    else
                    {
                        tuples[k] += p.L2R;
                    }
                }

                //tuples.Add(new Tuple<string, string, int>(l, r, p.L2R));
                string k2 = ReformulationPatternMatch.MakePair(p.Left, rightIntent);
                if (!patternIntent.ContainsKey(k2))
                {
                    patternIntent.Add(k2, p.L2R);
                }
                else
                {
                    patternIntent[k2] += p.L2R;
                }
            }

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                var sorted = from t in tuples
                             where t.Value >= 60
                             orderby t.Key, t.Value descending
                             select t;
                foreach (var s in sorted)
                {
                    string[] lr = ReformulationPatternMatch.DecomposePair(s.Key);
                    if (lr.Length != 2)
                        continue;

                    if (stopwords.Contains(lr[0]) || stopwords.Contains(lr[1]))
                        continue;

                    //if (intents.Contains(lr[0]))
                    //continue;

                    //if (intents.Contains(lr[1]))
                    sw.WriteLine("{0}\t{1}\t{2}", lr[0], lr[1], s.Value);
                }
            }

            using (StreamWriter sw = new StreamWriter(arguments.PatternOutput))
            {
                var sorted = from t in patternIntent
                             where t.Value >= 30
                             orderby t.Key, t.Value descending
                             select t;
                foreach (var s in sorted)
                {
                    string[] lr = ReformulationPatternMatch.DecomposePair(s.Key);
                    if (lr.Length != 2)
                        continue;

                    if (stopwords.Contains(lr[0]) || stopwords.Contains(lr[1]))
                        continue;

                    //if (intents.Contains(lr[0]))
                    //continue;

                    //if (intents.Contains(lr[1]))
                    sw.WriteLine("{0}\t{1}\t{2}", lr[0], lr[1], s.Value);
                }
            }
        }

        static char[] wild = new char[] { '*' };
        static char[] space = new char[] { ' ' };
        private static string[] GetNgrams(string[] terms)
        {
            var q = string.Join(" ", terms);
            return q.Split(wild, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).Where(t => !string.IsNullOrEmpty(t)).ToArray();
        }

        private static IEnumerable<string> Split(string query, int ngram)
        {
            string[] items = query.Split(space);
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 1; j <= Math.Min(ngram, items.Length - i); j++)
                {
                    yield return string.Join(" ", items.Skip(i).Take(j));
                }
            }
        }
    }
}
