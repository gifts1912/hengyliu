using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QU.Utility;
using System.Data;

namespace QU.Miscs.Entity
{
    public class LowDistortionEntityRewriting
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "q")]
            public string Query;

            [Argument(ArgumentType.Required, ShortName = "e")]
            public string EntityFile;
        }

        static void LSHFuzzyMatch(string query, string entityFile)
        {
            EntityFuzzyMatcher matcher = new EntityFuzzyMatcher(entityFile);

            DateTime prev = DateTime.Now;

            for (int i = 0; i < 100; i++)
            {
                DataTable dt;
                matcher.Match(query, out dt);

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(string.Join("\t", row.ItemArray));
                }
            }

            Console.WriteLine("Total Elapsed: {0}ms", (DateTime.Now - prev).TotalMilliseconds);
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.EntityFile))
            {
                Console.WriteLine("No Valid File!");
                return;
            }

            DateTime prev = DateTime.Now;
            HierarchicalLevTrie trie = BuildHierarchicalTrie(arguments.EntityFile);
            Console.WriteLine("BuildTrie Elapsed: {0} s", (DateTime.Now - prev).TotalSeconds);

            prev = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                var results = trie.LevSearch(arguments.Query, 2, 1);
                //foreach (var r in results)
                //{
                //    Console.WriteLine(r.editDist + "\t" + string.Join(" ", r.prevTerms));
                //}
            }
            Console.WriteLine("LevSearch Elapsed: {0} ms", (DateTime.Now - prev).TotalMilliseconds);
        }

        static HierarchicalLevTrie BuildHierarchicalTrie(string file)
        {
            HierarchicalLevTrie trie = new HierarchicalLevTrie();

            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    trie.Add(line);
                }
            }

            return trie;
        }
    }
}
