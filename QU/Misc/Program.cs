using DBUtility.SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Entity;
using Utility.NLP.SemanticParsing;
using Utility.StringUtils;

namespace Misc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0].Equals("/sp"))
            {
                SP(args.Skip(1).ToArray());
            }
            else if (args[0].Equals("/table"))
            {
                CreateTable(args.Skip(1).ToArray());
            }
        }

        static void CreateTable(string[] args)
        {
            string dbName = args[0], tableName = args[1];
            string fileName = args[2];
            int qCol = int.Parse(args[3]), vCol = int.Parse(args[4]);
            string dbFile = dbName + ".sqlite";
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
            }
            var conn = new SQLiteConnection(string.Format("Data Source={0}.sqlite;Version=3;", dbName));
            conn.Open();
            string sql = string.Format("create table {0} (word varchar(100), vector varchar(1000))", tableName);
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            int n = 0;
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (++n % 10000 == 0)
                    {
                        Console.WriteLine("Line {0} processed", n);
                    }

                    try
                    {
                        string[] items = line.Split('\t');
                        string q = items[qCol].Replace('\'', ' ');
                        string v = items[vCol];

                        sql = string.Format("insert into {0} (word, vector) values ('{1}', '{2}')", tableName, q, v);
                        command = new SQLiteCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        Console.WriteLine("Error: {0}", line);
                    }
                }
            }
        }

        static void SP(string[] args)
        {
            string input = args[0], output = args[1], dbName = args[2];
            OntologyIndex ontology = new OntologyIndex("Ontology.gz");

            Console.WriteLine("Loading Embedding");
            TextEmbedding embPhrase = new SQLiteTextEmbedding(dbName, "EmbWordVec");
            TextEmbedding embRelation = new InMemoryTextEmbedding(Path.Combine(Utility.NLP.NLPConstants.NLPModelDir, "Emb_Jointly_AAAN.bin.base64relation.satori"), 0, 1);

            PredicateTable predTable = new PredicateTable(embRelation.GetKeys());
            KBEmbeddingSParser parser = new KBEmbeddingSParser(predTable, embPhrase, embRelation);
            Console.WriteLine("Loaded!");

            DateTime start, end;

            using (StreamWriter sw = new StreamWriter(output))
            {
                using (StreamReader sr = new StreamReader(input))
                {
                    string line;
                    while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                    {
                        string[] items = line.Split('\t');
                        Console.WriteLine(line);
                        if (items.Length < 1)
                            continue;

                        string query = items[0];

                        start = DateTime.Now;
                        var matches = GenMatches(query, ontology);
                        var tripleNodes = parser.GenCandidatesFromRelationPattern(query, matches);
                        end = DateTime.Now;
                        Console.WriteLine("Query: {0}, Duration: {1}", query, (end - start).TotalMilliseconds);
                        //var tripleNodes = cdssmParser.GenCandidates(query, matches);

                        if (tripleNodes == null || tripleNodes.Count == 0)
                            continue;

                        var sorted = from n in tripleNodes orderby n.Score descending select n;

                        foreach (var node in sorted)
                        {
                            if (node == null)
                                continue;

                            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", query, node.Key, node.Predicate, node.Score, node.UNKCount);
                        }
                    }
                }
            }

            embPhrase.Dispose();
            embRelation.Dispose();
        }

        static Match<string, List<SemanticAtom>>[] GenMatches(string pattern, OntologyIndex ontology)
        {
            var matches = new List<Match<string, List<SemanticAtom>>>();
            string[] terms = pattern.Split(' ');
            for (int i = 0; i < terms.Length; i++)
            {
                string term = terms[i];
                if (!term.Contains('.')
                    || term.StartsWith("E.")
                    || term.StartsWith("I.")
                    || term.StartsWith("C.")
                    )
                    continue;

                Match<string, List<SemanticAtom>> m = new Match<string, List<SemanticAtom>>();
                m.Capture = new string[] { term };
                m.Begin = i;
                m.End = i + 1;
                m.Value = new List<SemanticAtom>();
                m.Value.Add(new SemanticAtom() { Metadata = string.Join(";", ExpandType(term, ontology)) });
                matches.Add(m);
            }

            return matches.ToArray();
        }

        /// <summary>
        /// Expand current type to all of the base types
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static HashSet<string> ExpandType(string type, OntologyIndex ontology)
        {
            HashSet<string> types = new HashSet<string>();
            if (string.IsNullOrWhiteSpace(type) || !type.Contains('.') || !type.IsValidType())
                return types;

            types.Add(type);

            try
            {
                var st = ontology.TypeLookup["mso:" + type];
                var sts = st.IncludesString;
                foreach (var s in sts)
                {
                    if (s.IsValidType())
                        types.Add(s.Split(':')[1]);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception: {0} for Type: {1}", ex.Message, type);
            }

            return types;
        }
    }
}
