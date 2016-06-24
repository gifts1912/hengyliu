using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QU.Utility;
using System.IO;

namespace QU.Miscs
{
    public class PatternMatch
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "slot")]
            public string SlotParsingFile;

            [Argument(ArgumentType.Required, ShortName = "pattern")]
            public string PatternFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "min")]
            public int MinReformulationCount = 5;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;
        }

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
            Dictionary<string, List<ReformulationPattern>> dictLeftP2ReformPatterns 
                = ReadReformPatterns(arguments.PatternFile, arguments.MinReformulationCount);
            DateTime curr = DateTime.Now;
            Console.WriteLine("Parse pattern file: {0}s", (curr - prev).TotalSeconds);
            Console.WriteLine("Patterns: {0}", dictLeftP2ReformPatterns.Count);

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.SlotParsingFile))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length < 2)
                            continue;

                        string q = items[0];
                        string slots = items[1];

                        prev = DateTime.Now;
                        string qWithSlots = ReformulationPatternMatch.ReplaceSlot(q, slots, true);
                        bool hasMatch = false;
                        foreach (var pair in dictLeftP2ReformPatterns)
                        {
                            bool match = ReformulationPatternMatch.Match(qWithSlots, pair.Key);
                            if (match)
                            {
                                sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", q, slots, qWithSlots, pair.Key, pair.Value.Count);
                                hasMatch = true;
                            }
                        }

                        if (!hasMatch)
                        {
                            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", q, slots, "", "", 0);
                        }

                        curr = DateTime.Now;
                        Console.WriteLine("Query: {0}, Duration: {1}s", q, (curr - prev).TotalSeconds);
                    }
                }
            }
        }

        private static Dictionary<string, List<ReformulationPattern>> ReadReformPatterns(string file, int minCnt)
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

                    if (p.L2R < minCnt)
                        continue;

                    // remove all of the wide-match or slot<2 patterns.
                    if (p.Left.Split(new char[] { ' ', '*' }, StringSplitOptions.RemoveEmptyEntries).Length < 2)
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
