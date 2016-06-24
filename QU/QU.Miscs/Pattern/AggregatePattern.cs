using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QU.Utility;

namespace QU.Miscs
{
    /// <summary>
    /// Aggregate patterns.
    ///   1) Stemming.
    ///   2) rankonly.
    ///   3) order change.
    /// </summary>
    class AggregatePattern
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "nowild")]
            public string NoWildPatternFile = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.Required, ShortName = "wild")]
            public string WildPatternFile = "";

            public bool InputValid { get { return File.Exists(NoWildPatternFile) && File.Exists(WildPatternFile); } }
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            var nowildPatterns = Extensions.ReadPatterns(arguments.NoWildPatternFile);
            var wildPatterns = Extensions.ReadPatterns(arguments.WildPatternFile);
            Dictionary<string, List<ReformulationPattern>> dictWild = BuildDict(wildPatterns);

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                foreach (var nowild in nowildPatterns)
                {
                    var replaced = ReplaceSlotWithWild(nowild.Left);
                    foreach (var r in replaced)
                    {
                        if (r.Equals(nowild.Left))
                            continue;

                        if (r.Split(new char[] { ' ', '*' }, StringSplitOptions.RemoveEmptyEntries).Length <= 2)
                            continue;

                        List<ReformulationPattern> potentialWild;
                        if (dictWild.TryGetValue(r, out potentialWild))
                        {
                            foreach (var p in potentialWild)
                            {
                                sw.WriteLine(nowild.Left + "\t" + p.Left + "\t" + p.Right + "\t" + p.L2R + "\t" + p.R2L);
                            }
                            //sw.WriteLine(nowild.Left + "\t" + r);
                        }
                    }
                }
            }
        }

        static List<string> ReplaceSlotWithWild(string pattern)
        {
            List<string> replaced = new List<string>();
            string[] items = pattern.Split(' ');
            Replace(items, 0, ref replaced);

            return replaced;
        }

        static void Replace(string[] items, int beg, ref List<string> replaced)
        {
            for (int i = beg; i < items.Length; i++)
            {
                string item = items[i];
                if (item.StartsWith("Slot^Ent"))
                {
                    items[i] = "*";
                    replaced.Add(string.Join(" ", items));
                    Replace(items, i + 1, ref replaced);
                    items[i] = item;
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
