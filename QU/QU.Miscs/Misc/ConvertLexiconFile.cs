using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs.Misc
{
    public class ConvertLexiconFile
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "in")]
            public string InFile;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string OutDir;

            [Argument(ArgumentType.Required, ShortName = "list")]
            public string OutTrieList;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.InFile))
            {
                Console.Error.WriteLine("No Input!");
                return;
            }

            if (!Directory.Exists(arguments.OutDir))
                Directory.CreateDirectory(arguments.OutDir);

            Dictionary<string, List<string>> dictCategory2Lexicon = new Dictionary<string, List<string>>();
            using (StreamReader sr = new StreamReader(arguments.InFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string[] items = line.Split('\t');
                    if (items.Length < 2)
                        continue;

                    if (string.IsNullOrEmpty(items[1]) || string.IsNullOrEmpty(items[0]))
                        continue;

                    string[] categories = items[1].Split(';');
                    foreach (var c in categories)
                    {
                        if (!dictCategory2Lexicon.ContainsKey(c))
                            dictCategory2Lexicon.Add(c, new List<string>());

                        dictCategory2Lexicon[c].Add(items[0]);
                    }
                }
            }

            using (StreamWriter swTrie = new StreamWriter(arguments.OutTrieList))
            {
                foreach (var p in dictCategory2Lexicon)
                {
                    swTrie.WriteLine("{0}:Trie({0}.txt)", p.Key.Replace('.', '_'));
                    using (StreamWriter sw = new StreamWriter(Path.Combine(arguments.OutDir, p.Key.Replace('.', '_') + ".txt")))
                    {
                        foreach (var l in p.Value)
                        {
                            sw.WriteLine(l);
                        }

                        sw.Flush();
                        sw.Close();
                    }
                }
            }
        }

        public static IEnumerable<Tuple<string, string>> ExtractPhoneNumber(string query)
        {
            var regex = new System.Text.RegularExpressions.Regex("\\d{3} \\d{3} \\d{4}");
            foreach (System.Text.RegularExpressions.Match m in regex.Matches(query))
            {
                yield return Tuple.Create(m.Groups[0].Value, "TelPhone");
            }
        }

        public static IEnumerable<Tuple<string, string>> ExtractTimexResult(string query, string s) 
        {
            HashSet<string> processed = new HashSet<string>();
            var regex = new System.Text.RegularExpressions.Regex("<TIMEX3 .*?type=\"Date\".*?>(.+?)</TIMEX3>"); 
            foreach (System.Text.RegularExpressions.Match m in regex.Matches(s)) 
            {
                processed.Add(m.Groups[1].Value);
                yield return Tuple.Create(m.Groups[1].Value, "Date"); 
            }

            var regexYears = new System.Text.RegularExpressions.Regex("\\b([1|2]\\d\\d0) ?s\\b");
            foreach (System.Text.RegularExpressions.Match m in regexYears.Matches(query))
            {
                if (processed.Contains(m.Groups[1].Value) || processed.Contains(m.Groups[0].Value))
                    continue;

                processed.Add(m.Groups[0].Value);
                processed.Add(m.Groups[1].Value);
                yield return Tuple.Create(m.Groups[1].Value, "YearRange");
            }

            var regexYear = new System.Text.RegularExpressions.Regex("\\b([1|2]\\d\\d\\d)\\b");
            foreach (System.Text.RegularExpressions.Match m in regexYear.Matches(query))
            {
                if (processed.Contains(m.Groups[1].Value))
                    continue;

                processed.Add(m.Groups[1].Value);
                yield return Tuple.Create(m.Groups[1].Value, "Year");
            }
        }

        public static string Replace(string query, string tag)
        {
            if (string.IsNullOrEmpty(tag))
                return query;

            string[] tags = tag.Split('|');
            foreach (var t in tags)
            {
                string[] items = t.Split(':');
                if (items.Length != 2)
                    continue;
                query = System.Text.RegularExpressions.Regex.Replace(query, "\\b" + items[0] + "\\b", items[1]);
            }

            return query;
        }

        public static string GenLabel(string r, string q)
        {
            if (string.IsNullOrEmpty(r))
                return q;

            foreach (string item in r.Split(';'))
            {
                string[] lr = item.Split(':');
                if (lr.Length != 2)
                    continue;

                q = q.Replace(lr[1], string.Format("<{0}> {1} </{0}>", lr[0], lr[1]));
            }

            return q;
        }
    }
}
