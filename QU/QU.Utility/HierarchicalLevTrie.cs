using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility
{
    public class HierarchicalLevTrie
    {
        static char[] Seperators = new char[] { ' ', '.', ',', '-', '+', '_', '\'', '"', '(', ')', ';', ':' };

        Trie firstLayerTrie;

        public HierarchicalLevTrie()
        {
            this.firstLayerTrie = new Trie();
        }

        public void Add(string str)
        {
            string[] terms = str.Split(Seperators, StringSplitOptions.RemoveEmptyEntries);
            if (null == terms || terms.Length == 0)
                return;

            // first layer
            Trie.Node tempLeaf;
            this.firstLayerTrie.Add(terms[0], out tempLeaf);

            // second layer
            Trie.Node prevLeaf = tempLeaf;
            for (int i = 1; i < terms.Length; i++)
            {
                if (null == prevLeaf.NextTrie)
                    prevLeaf.NextTrie = new Trie();
                prevLeaf.NextTrie.Add(terms[i], out tempLeaf);
                prevLeaf = tempLeaf;
            }
        }

        /// <summary>
        /// Build tree.
        /// </summary>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public void BuildTree(IEnumerable<string> data)
        {
            // Enumerate all of the string.
            foreach (string p in data)
            {
                Add(p);
            }
        }

        public List<HierarchyLevSearchResult> LevSearch(string str, int maxTotalEditDist, int maxEditDistPerTerm)
        {
            string[] terms = str.Split(Seperators, StringSplitOptions.RemoveEmptyEntries);
            if (null == terms || terms.Length == 0)
                return null;

            List<Trie.LevSearchResult> currLayerResults = new List<Trie.LevSearchResult>();

            Trie currTrie = this.firstLayerTrie;
            if (!currTrie.LevSearch(terms[0], maxEditDistPerTerm, ref currLayerResults))
            {
                return null;
            }

            List<HierarchyLevSearchResult> hierarchyResults = currLayerResults.Select(r => Convert(r, 0, 0, new List<string>())).ToList();
            List<HierarchyLevSearchResult> matched = new List<HierarchyLevSearchResult>();

            while (hierarchyResults.Count > 0)
            {
                HierarchyLevSearchResult r = hierarchyResults[0];
                hierarchyResults.RemoveAt(0);
                if (r.editDist > maxTotalEditDist)
                {
                    continue;
                }

                Trie.Node landing = r.result.LandingNode;
                if ((landing == null || landing.NextTrie == null))
                {
                    if (r.termId == terms.Length - 1)
                    {
                        // matched
                        matched.Add(r);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (r.termId == terms.Length - 1)
                    {
                        continue;
                    }
                    else
                    {
                        currTrie = landing.NextTrie;
                        currLayerResults.Clear();
                        if (currTrie.LevSearch(terms[r.termId + 1], maxEditDistPerTerm, ref currLayerResults))
                        {
                            hierarchyResults.AddRange(
                                currLayerResults.Select(c => Convert(c, r.editDist, r.termId + 1, r.prevTerms))
                                .Where(c => c.editDist <= maxTotalEditDist)
                                );
                        }
                    }
                }
            }

            return matched;
        }

        static HierarchyLevSearchResult Convert(Trie.LevSearchResult r, int prevEditDist, int termId, List<string> prevTerms)
        {
            var hlr = new HierarchyLevSearchResult { result = r, editDist = prevEditDist + r.EditDist, termId = termId };
            hlr.prevTerms.AddRange(prevTerms);
            hlr.prevTerms.Add(r.Term);
            return hlr;
        }

        public class HierarchyLevSearchResult
        {
            public Trie.LevSearchResult result;
            public int termId;
            public int editDist;
            public List<string> prevTerms = new List<string>();
        }
    }
}
