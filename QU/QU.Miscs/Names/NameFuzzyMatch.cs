using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs
{
    class NameFuzzyMatch
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "data")]
            public string DataFile;

            [Argument(ArgumentType.Required, ShortName = "query")]
            public string QueryFile;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;

            [Argument(ArgumentType.AtMostOnce, ShortName = "dist")]
            public int MaxEditDist = 1;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            DateTime prev = DateTime.Now;
            Trie firstNameTrie, lastNameTrie;
            BuildTries(arguments.DataFile, out firstNameTrie, out lastNameTrie);
            Console.WriteLine("Build Trie: {0} s", (DateTime.Now - prev).TotalSeconds);

            int count = 0, match = 0;
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.QueryFile))
                {
                    prev = DateTime.Now;
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        count++;
                        string[] terms = line.Split(Seperators, StringSplitOptions.RemoveEmptyEntries);

                        if (terms.Length < 2 || terms.Length > 6)
                        {
                            sw.WriteLine("{0}\t{1}\t{2}", line, line, 100);
                            continue;
                        }

                        int exactMatchTerm = 0, noalterTerm = 0, fuzzyMatchTerm = 0, noMatchTerm = 0;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < terms.Length; ++i)
                        {
                            string term = terms[i];
                            if (term.Length > 2)
                            {
                                Trie trie = (i == terms.Length - 1 ? lastNameTrie : firstNameTrie);
                                var results = new List<Trie.LevSearchResult>();
                                int dist = term.Length >= 8 ? Math.Max(2, arguments.MaxEditDist) : arguments.MaxEditDist;
                                if (trie.LevSearch(term, dist, ref results))
                                {
                                    if (results.Count > 1)
                                    {
                                        if (results[0].EditDist == 0 && results[0].Freq >= 200)
                                        {
                                            sb.Append(term);
                                            sb.Append(" ");
                                            continue;
                                        }

                                        sb.Append("word:(");
                                        if (results[0].EditDist != 0)
                                        {
                                            sb.Append(term);
                                            sb.Append(" ");
                                            fuzzyMatchTerm++;
                                        }
                                        else
                                        {
                                            exactMatchTerm++;
                                        }

                                        foreach (var r in results)
                                        {
                                            if (r.Term[0].Equals(term[0]))
                                            {
                                                sb.Append(r.Term);
                                                sb.Append(" ");
                                            }
                                        }
                                        sb.Remove(sb.Length - 1, 1);
                                        sb.Append(")");
                                    }
                                    else
                                    {
                                        if (results[0].EditDist != 0 && results[0].Term[0].Equals(term[0]))
                                        {
                                            sb.Append("word:(");
                                            sb.Append(term);
                                            sb.Append(" ");
                                            sb.Append(results[0].Term);
                                            sb.Append(")");
                                            fuzzyMatchTerm++;
                                        }
                                        else
                                        {
                                            sb.Append(term);
                                            exactMatchTerm++;
                                        }
                                    }
                                }
                                else
                                {
                                    sb.Append(term);
                                    noMatchTerm++;
                                }
                            }
                            else
                            {
                                sb.Append(term);
                                noalterTerm++;
                            }
                            sb.Append(" ");
                        }

                        if (noMatchTerm == 0)
                        {
                            match++;
                        }

                        sw.WriteLine("{0}\t{1}\t{2}", line, sb.ToString(0, sb.Length - 1), noMatchTerm);
                    }

                    Console.WriteLine("{0} Queries, {1} Matched, Duration: {2}s", 
                        count, match, (DateTime.Now - prev).TotalSeconds);
                }
            }
        }

        static char[] Seperators = new char[] { ' ', '.', ',' };

        static void BuildTries(string file, out Trie firstNameTrie, out Trie lastNameTrie)
        {
            firstNameTrie = new Trie();

            lastNameTrie = new Trie();

            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] terms = line.Split(Seperators, StringSplitOptions.RemoveEmptyEntries);
                    if (terms.Length < 2)
                        continue;

                    var firstNameTerms = terms.Take(terms.Length - 1);
                    foreach (var term in firstNameTerms)
                    {
                        if (term.Length > 2)
                            firstNameTrie.Add(term);
                    }

                    var lastName = terms[terms.Length - 1];
                    if (lastName.Length > 2)
                        lastNameTrie.Add(lastName);
                }
            }
        }
    }
}
