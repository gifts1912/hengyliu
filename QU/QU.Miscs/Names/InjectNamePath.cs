using Microsoft.TMSN.CommandLine;
using QU.Utility;
using QueryUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs
{
    class InjectNamePath
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

            [Argument(ArgumentType.AtMostOnce, ShortName = "smt")]
            public string SmtModel;
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
            //BuildSingleTrie(arguments.DataFile, out firstNameTrie);
            //lastNameTrie = firstNameTrie;
            Console.WriteLine("Build Trie: {0} s", (DateTime.Now - prev).TotalSeconds);
            Console.WriteLine("First Name Trie MaxFreq: {0}", firstNameTrie.MaxFreq);
            Console.WriteLine("Last Name Trie MaxFreq: {0}", lastNameTrie.MaxFreq);

            CharEM em = new CharEM();
            bool modelLoaded = false;
            if (!string.IsNullOrEmpty(arguments.SmtModel))
            {
                prev = DateTime.Now;
                modelLoaded = em.ReadModel(arguments.SmtModel, 3);
                Console.WriteLine("Load SMT: {0} s", (DateTime.Now - prev).TotalSeconds);
            }

            int count = 0;
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.QueryFile))
                {
                    prev = DateTime.Now;
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        count++;
                        string[] items = line.Split('\t');
                        if (items.Length < 2)
                            continue;

                        string origQ = items[0];
                        string alterQ = items[1];
                        string injectedAlterQ = InjectNameCandidate(alterQ, 
                            firstNameTrie, lastNameTrie, arguments.MaxEditDist, em);
                        if (string.IsNullOrEmpty(injectedAlterQ))
                        {
                            injectedAlterQ = origQ;
                        }

                        sw.WriteLine("{0}\t{1}\t{2}", origQ, alterQ, injectedAlterQ);
                    }

                    Console.WriteLine("{0} Queries, Duration: {1}s, Average: {2}ms",
                        count, (DateTime.Now - prev).TotalSeconds, 
                        (DateTime.Now - prev).TotalMilliseconds / count);
                }
            }
        }

        static char[] Seperators = new char[] { ' ', '.', ',', '-', '+', '_', '\'', '"', '(', ')', ';', ':' };
        static double[] Distortion = new double[] { 0.1, 0.006, 0.0036, 0.000729 };

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

        static void BuildSingleTrie(string file, out Trie nameTrie)
        {
            nameTrie = new Trie();

            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] terms = line.Split(Seperators, StringSplitOptions.RemoveEmptyEntries);
                    if (terms.Length < 2)
                        continue;

                    foreach (var t in terms)
                        nameTrie.Add(t);
                }
            }
        }

        private static string Normalize(string query)
        {
            query = query.Replace("\\\"", "\"");
            return query;
        }

        private static string RemoveAddQuery(string query)
        {
            if (query.Contains("addquery:"))
            {
                return query.Substring(0, query.IndexOf("addquery:"));
            }

            return query;
        }

        static string InjectNameCandidate(string queryAug,
            Trie firstNameTrie,
            Trie lastNameTrie,
            int maxEditDist, 
            CharEM em)
        {
            CALQuery calQuery;

            try
            {
                queryAug = Normalize(queryAug);
                queryAug = RemoveAddQuery(queryAug);
                calQuery = new CALQuery(queryAug, "en-us");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return queryAug;
            }

            if (calQuery.Words.Count > 6 || calQuery.Words.Count < 2)
            {
                return queryAug;
            }

            for (int i = 0; i < calQuery.Words.Count; ++i)
            {
                QueryWord word = calQuery.Words[i];
                if (word == null
                    || word.IsRankOnly()
                    || word.IsStopWord()
                    || (word.PhraseWords != null && word.PhraseWords.Count > 0)
                    )
                {
                    continue;
                }

                var firstWord = word.GetAlterWordByIdx(0);
                if (firstWord == null
                    || firstWord.String == null
                    || firstWord.String.Length <= 2
                    )
                {
                    continue;
                }

                string origTerm = firstWord.String;
                HashSet<string> candidates = new HashSet<string>();
                foreach (var aw in word.AlterWords)
                {
                    candidates.Add(aw.String);
                }

                Trie trie = (i == calQuery.Words.Count - 1 ? lastNameTrie : firstNameTrie);
                var results = new List<Trie.LevSearchResult>();
                //int dist = origTerm.Length >= 7 ? Math.Max(2, maxEditDist) : maxEditDist;
                int dist = maxEditDist;
                if (trie.LevSearch(origTerm, dist, ref results))
                {
                    if (results[0].EditDist == 0)
                    {
                        if (results[0].Freq >= 200)
                            continue;
                    }
                    else
                    {
                        word.AlterWords.Add(new QueryWord { String = origTerm, Type = QueryTokenType.ParmAlteration }
                            );
                    }

                    //List<string> alterations = new List<string>();
                    foreach (var r in results)
                    {
                        if (candidates.Contains(r.Term))
                            continue;

                        // first chracter cannot be wrong
                        bool firstCharAdded = false;
                        if (!r.Term[0].Equals(origTerm[0]))
                        {
                            // first character added
                            foreach (var trail in r.Trails)
                            {
                                if (trail.pattern == Trie.LevPattern.Add && trail.srcPos == 0)
                                {
                                    firstCharAdded = true;
                                    break;
                                }
                            }

                            if (!firstCharAdded || (firstCharAdded && (i != 0)))
                            {
                                continue;
                            }
                        }

                        if (r.Freq < results[0].Freq / 10)
                            continue;

                        if (em.Loaded)
                        {
                            double score = em.CalScore(r, origTerm);
                            score *= Math.Max(1.0, Math.Log(((double)r.Freq / results[0].Freq), 5.0));
                            Console.WriteLine(origTerm + "\t" + r.Term + "\t" + score + "\t" + results[0].Freq + "\t" + r.Freq);
                            //if ((!firstCharAdded && score < Math.Pow(0.03 * r.EditDist, r.EditDist)) 
                            if ((!firstCharAdded && score < Distortion[Math.Min(r.EditDist, Distortion.Length - 1)]) 
                                || (firstCharAdded && score < 0.008))
                            //if (score < 0.008)
                                continue;
                        }

                        //alterations.Add(r.Term);

                        word.AlterWords.Add(new QueryWord { String = r.Term, Type = QueryTokenType.ParmAlteration }
                            );
                    }
                }

                // only one alter word
                if (word.AlterWords.Count <= 1)
                    word.AlterWords.Clear();
            }

            return calQuery.ToString(false);
        }
    }
}
