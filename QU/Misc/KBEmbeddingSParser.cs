using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utility.NLP;
using Utility.NLP.SemanticParsing;
using Utility.StringUtils;

namespace Misc
{
    public class KBEmbeddingSParser
    {
        private StopWordDict _stopwords = new StopWordDict();
        private PredicateTable _predTable;
        private TextEmbedding _phraseEmbedding;
        private TextEmbedding _predicateEmbedding;

        public KBEmbeddingSParser(PredicateTable predTable,
                                    TextEmbedding phraseEmbedding,
                                    TextEmbedding predicateEmbedding)
        {
            this._predTable = predTable;
            this._phraseEmbedding = phraseEmbedding;
            this._predicateEmbedding = predicateEmbedding;
        }

        public static double CalcCosinSim(float[] v1, float[] v2)
        {
            if (v1 == null || v2 == null || v1.Length == 0 || v1.Length != v2.Length)
            {
                return 0;
            }

            int len = v1.Length;
            double v1Norm = 0, v2Norm = 0, innerProduct = 0;
            for (int i = 0; i < len; i++)
            {
                innerProduct += (v1[i] * v2[i]);
                v1Norm += (v1[i] * v1[i]);
                v2Norm += (v2[i] * v2[i]);
            }

            return innerProduct / (Math.Sqrt(v1Norm) * Math.Sqrt(v2Norm));
        }

        public static void AddVector(ref float[] v1, float[] v2)
        {
            if (v1 == null || v2 == null || v1.Length == 0 || v1.Length != v2.Length)
            {
                return;
            }

            int len = v1.Length;
            for (int i = 0; i < len; i++)
            {
                v1[i] = v1[i] + v2[i];
            }
        }

        public List<RelationTripleNode> GenCandidatesFromRelationPattern(string query, Match<string, List<SemanticAtom>>[] matches)
        {
            if (matches == null || matches.Length == 0)
            {
                return null;
            }

            List<RelationTripleNode> tripleNodes = new List<RelationTripleNode>();
            foreach (var match in matches)
            {
                if (match.Value == null || match.Value.Count == 0)
                    continue;

                var ctxNGrams = GetContextPhrases(query, match);

                float[] vecPhrase = new float[0];
                bool first = true;
                foreach (var v in ctxNGrams.Values)
                {
                    foreach (var ngram in v.Keys)
                    {
                        if (first)
                        {
                            first = false;
                            vecPhrase = this._phraseEmbedding.GetVector(ngram);
                        }
                        else
                        {
                            AddVector(ref vecPhrase, this._phraseEmbedding.GetVector(ngram));
                        }
                    }
                }

                HashSet<string> types = new HashSet<string>();
                foreach (var metadata in match.Value)
                {
                    string entityTypes = metadata.Metadata;
                    foreach (var entityType in entityTypes.Split(';'))
                    {
                        types.Add(entityType);
                    }
                }

                foreach (var domain in types)
                {
                    var predicates = this._predTable.GetPredicatesBySrcType(domain);
                    if (predicates == null || predicates.Count == 0)
                        continue;

                    foreach (string predicate in predicates)
                    {
                        double sim = 0;
                        float[] vec = this._predicateEmbedding.GetVector(predicate);

                        sim = CalcCosinSim(vec, vecPhrase);

                        if (sim > 0)
                        {
                            RelationTripleNode node = new RelationTripleNode();
                            string key = string.Join(" ", match.Capture);
                            if (tripleNodes.Any(innerNode => innerNode.Key == key && innerNode.Predicate == predicate))
                                continue;
                            node.Key = key;
                            node.Predicate = predicate;
                            node.Score = (float)sim;
                            node.UNKCount = 0;
                            tripleNodes.Add(node);
                        }
                    }

                }
            }

            return tripleNodes;
        }

        Dictionary<int, Dictionary<string, float>> GetContextPhrases(string query, Match<string, List<SemanticAtom>> match)
        {
            if (match == null || match.Begin < 0)
                return null;

            string[] words = query.Split(' ');
            int indexUpdateBeg = match.Begin;
            int indexUpdateEnd = match.End;

            Dictionary<int, Dictionary<string, float>> ctxNGrams = new Dictionary<int, Dictionary<string, float>>();
            ctxNGrams.Add(1, new Dictionary<string, float>());
            ctxNGrams.Add(2, new Dictionary<string, float>());
            ctxNGrams.Add(3, new Dictionary<string, float>());
            ctxNGrams.Add(4, new Dictionary<string, float>());
            ctxNGrams.Add(5, new Dictionary<string, float>());

            for (int i = 0; i < words.Length; i++)
            {
                if (i < indexUpdateBeg || i >= indexUpdateEnd)
                {
                    IncNGramCount(ref ctxNGrams, 1, words[i], 1);

                    if ((i + 1 < indexUpdateBeg || i + 1 >= indexUpdateEnd) && i + 1 < words.Length)
                    {
                        IncNGramCount(ref ctxNGrams, 2, words[i] + ' ' + words[i + 1], 1);

                        if ((i + 2 < indexUpdateBeg || i + 2 >= indexUpdateEnd) && i + 2 < words.Length)
                        {
                            IncNGramCount(ref ctxNGrams, 3, words[i] + ' ' + words[i + 1] + ' ' + words[i + 2], 1);

                            if ((i + 3 < indexUpdateBeg || i + 3 >= indexUpdateEnd) && i + 3 < words.Length)
                            {
                                IncNGramCount(ref ctxNGrams, 4, words[i] + ' ' + words[i + 1] + ' ' + words[i + 2] + ' ' + words[i + 3], 1);

                                if ((i + 4 < indexUpdateBeg || i + 4 >= indexUpdateEnd) && i + 4 < words.Length)
                                {
                                    IncNGramCount(ref ctxNGrams, 5, words[i] + ' ' + words[i + 1] + ' ' + words[i + 2] + ' ' + words[i + 3] + ' ' + words[i + 4], 1);
                                }
                            }
                        }
                    }
                }
                else
                {
                    continue;
                }
            }

            return ctxNGrams;
        }

        void IncNGramCount(ref Dictionary<int, Dictionary<string, float>> CTXNGrams, int order, string NGram, int count)
        {
            if (IsValidContextPhrase(NGram))
            {
                if (!CTXNGrams[order].ContainsKey(NGram))
                {
                    CTXNGrams[order].Add(NGram, count);
                }
                else
                {
                    CTXNGrams[order][NGram] += count;
                }
            }
        }

        private bool IsValidContextPhrase(string NGram)
        {
            if (NGram.Trim() == "" || Regex.IsMatch(NGram, @"[^a-z0-9 -]") || _stopwords.Contains(NGram))
            {
                return false;
            }

            return true;
        }
    }
}
