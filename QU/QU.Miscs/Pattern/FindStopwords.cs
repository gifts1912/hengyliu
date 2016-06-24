using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMisc = Utility;

namespace QU.Miscs
{
    class FindStopwords
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.AtMostOnce, ShortName = "p")]
            public string PatternFile = "";

            [Argument(ArgumentType.Required, ShortName = "o")]
            public string Output = "";

            public bool InputValid { get { return File.Exists(PatternFile); } }
        }

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

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            char[] space = new char[] { ' ' };
            char[] wild = new char[] { '*'};
            Dictionary<string, int> stopwordsOcc = new Dictionary<string, int>();

            using (StreamReader sr = new StreamReader(arguments.PatternFile))
            {
                string query = string.Empty;
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    ReformulationPattern p = ReformulationPattern.ReadFromLineWith5Items(line);
                    if (p == null || p.Type != MyMisc.SimilarQueryType.DropWord)
                        continue;

                    string left = p.Left;
                    string right = p.Right;

                    string[] leftItems = left.Split(space, StringSplitOptions.RemoveEmptyEntries);
                    string[] rightItems = right.Split(space, StringSplitOptions.RemoveEmptyEntries);
                    ReplaceCommonWord(leftItems, new HashSet<string>(rightItems));
                    ReplaceCommonWord(rightItems, new HashSet<string>(leftItems));

                    bool rightIsEmpty = true;
                    foreach (var r in rightItems)
                    {
                        if (r != "*")
                        {
                            rightIsEmpty = false;
                            break;
                        }
                    }

                    if (!rightIsEmpty)
                        continue;

                    List<string> stopwords = new List<string>();
                    left = string.Join(" ", leftItems);
                    leftItems = left.Split(wild, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var l in leftItems)
                    {
                        string temp = l.Trim(' ');
                        if (!string.IsNullOrEmpty(temp))
                            stopwords.Add(temp);
                    }

                    foreach (var s in stopwords)
                    {
                        if (!stopwordsOcc.ContainsKey(s))
                        {
                            stopwordsOcc.Add(s, 1);
                        }
                        else
                        {
                            ++stopwordsOcc[s];
                        }
                    }
                }
            }

            var sorted = from s in stopwordsOcc
                         where s.Value > 2
                         orderby s.Value descending
                         select s;
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                foreach (var s in sorted)
                {
                    sw.WriteLine(s.Key + "\t" + s.Value);
                }
            }
        }
    }
}
