using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility
{
    public class Trie
    {
        public class Node
        {
            private char character;
            /// <summary>
            /// Content in Node.
            /// </summary>
            public char Character
            {
                get { return character; }
                set { character = value; }
            }

            private Node parent;
            /// <summary>
            /// Parent.
            /// </summary>
            public Node Parent
            {
                get { return parent; }
                set { parent = value; }
            }

            private Dictionary<char, Node> children;
            /// <summary>
            /// Children indexed by their content.
            /// </summary>
            public Dictionary<char, Node> Children
            {
                get { return children; }
                set { children = value; }
            }

            private string content = string.Empty;
            /// <summary>
            /// Actual Pattern which is aggregation from root to leaf.
            /// Only exist when this node is a leaf.
            /// </summary>
            public string Content
            {
                get { return content; }
                set { content = value; }
            }

            private int freq = 0;
            /// <summary>
            /// Frequency. Only exist when this node is a leaf.
            /// </summary>
            public int Freq
            {
                get { return freq; }
                set { freq = value; }
            }

            private Trie nextTrie;
            /// <summary>
            /// For layered Trie.
            /// </summary>
            public Trie NextTrie
            {
                get { return nextTrie; }
                set { nextTrie = value; }
            }

            /// <summary>
            /// If current node has any child.
            /// </summary>
            /// <param name="child"></param>
            /// <returns></returns>
            public bool HasChild(Node child)
            {
                if (children == null)
                    return false;

                return children.ContainsKey(child.character);
            }

            /// <summary>
            /// If current node has any child with the specified content.
            /// </summary>
            /// <param name="child"></param>
            /// <returns></returns>
            public bool HasChild(char child)
            {
                if (children == null)
                    return false;

                return children.ContainsKey(child);
            }

            /// <summary>
            /// Get specified child. Null if none found.
            /// </summary>
            /// <param name="child"></param>
            /// <returns></returns>
            public Node GetChild(char child)
            {
                Node node;
                if (!Children.TryGetValue(child, out node))
                {
                    node = null;
                }

                return node;
            }

            /// <summary>
            /// Append child to current node.
            /// </summary>
            /// <param name="child"></param>
            public void AddChild(Node child)
            {
                children[child.character] = child;
            }

            public override int GetHashCode()
            {
                return content.GetHashCode();
            }
        }

        public class LevSearchResult
        {
            private int editDist = -1;
            public int EditDist
            {
                get { return editDist; }
                set { editDist = value; }
            }

            private string term = "";
            public string Term
            {
                get { return term; }
                set { term = value; }
            }

            private int freq = 0;
            public int Freq
            {
                get { return freq; }
                set { freq = value; }
            }

            private List<Trail> trails = new List<Trail>();
            public List<Trail> Trails
            {
                get { return trails; }
                set { trails = value; }
            }

            private Node landingNode;
            public Node LandingNode
            {
                get { return landingNode; }
                set { landingNode = value; }
            }
            
        }

        public enum LevPattern
        {
            Exact,
            Add,
            Delete,
            Replace
        }

        public class Trail
        {
            public LevPattern pattern = LevPattern.Exact;
            public int srcPos = 0;
            public int tgtPos = 0;

            public override string ToString()
            {
                return string.Format("{0}:{1}:{2}", pattern, srcPos, tgtPos);
            }
        }

        public Trie()
        {
            this.root = new Node()
            {
                Character = Start,
                Parent = null,
                Children = new Dictionary<char, Node>()
            };
        }

        private static char Start = '^';
        private static char End = '$';
        private int maxFreq = 0;
        public int MaxFreq
        {
            get { return maxFreq; }
            set { maxFreq = value; }
        }
        private Node root;
        /// <summary>
        /// Root of the tree.
        /// </summary>
        public Node Root
        {
            get { return root; }
            set { root = value; }
        }

        public virtual void Add(string p, out Node currRoot)
        {
            char[] items = p.ToCharArray();
            int start = 0;
            if (items[0].Equals(Start))
            {
                start = 1;
            }

            currRoot = root;
            for (int i = start; i < items.Length; i++)
            {
                Node child = currRoot.GetChild(items[i]);
                if (child == null)
                {
                    child = new Node { Character = items[i], Parent = currRoot, Children = new Dictionary<char, Node>() };
                    currRoot.AddChild(child);
                }

                currRoot = child;
            }

            if (items[items.Length - 1] != End)
            {
                if (currRoot.HasChild(End))
                {
                    Node end = currRoot.GetChild(End);
                    int freq = ++end.Freq;
                    MaxFreq = Math.Max(freq, MaxFreq);
                    currRoot = end;
                }
                else
                {
                    Node end = new Node
                    {
                        Character = End,
                        Parent = currRoot,
                        Children = null,
                        Content = p
                    };
                    end.Freq++;
                    currRoot.AddChild(end);
                    MaxFreq = Math.Max(end.Freq, MaxFreq);
                    currRoot = end;
                }
            }
            else
            {
                currRoot.Content = p;
                currRoot.Freq++;
                MaxFreq = Math.Max(currRoot.Freq, MaxFreq);
            }
        }

        public virtual void Add(string p)
        {
            Node curr;
            Add(p, out curr);
        }

        /// <summary>
        /// Build tree.
        /// </summary>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public virtual void BuildTree(IEnumerable<string> data)
        {
            // Enumerate all of the string.
            foreach (string p in data)
            {
                Add(p);
            }
        }

        public bool LevSearch(string term, int maxEditDist, ref List<LevSearchResult> results)
        {
            if (root == null)
            {
                return false;
            }

            char[] items = (term + End.ToString()).ToCharArray();
            Node currRoot = root;
            int start = 0;
            List<Trail> trails = new List<Trail>();

            bool foundMatch = LevSearch(items, start, 0, maxEditDist, currRoot, ref results, LevPattern.Exact, ref trails);
            if (!foundMatch)
                return false;

            Dictionary<string, LevSearchResult> dictTerm2Edt = new Dictionary<string, LevSearchResult>();
            foreach (var r in results)
            {
                LevSearchResult temp;
                if (dictTerm2Edt.TryGetValue(r.Term, out temp))
                {
                    if (temp.EditDist > r.EditDist)
                        dictTerm2Edt[r.Term] = r;
                }
                else
                {
                    dictTerm2Edt[r.Term] = r;
                }
            }

            results = dictTerm2Edt.Values.ToList();

            return foundMatch;
        }

        private bool LevSearch(char[] items, int start, int editDist, int maxEditDist, Node currRoot, 
            ref List<LevSearchResult> results, LevPattern prevPattern, ref List<Trail> trails)
        {
            List<LevSearchResult> childResults = new List<LevSearchResult>();
            if (start > items.Length - 1 || editDist > maxEditDist)
            {
                return false;
            }
            else if (start == items.Length - 1)
            {
                if (currRoot == null || currRoot.Children == null)
                {
                    return false;
                }

                bool foundMatch = false;
                foreach (var child in currRoot.Children)
                {
                    if (child.Key == End)
                    {
                        LevSearchResult result = new LevSearchResult()
                        {
                            EditDist = editDist,
                            Term = currRoot.Children[End].Content,
                            Freq = currRoot.Children[End].Freq
                        };
                        result.Trails.AddRange(trails);
                        result.LandingNode = currRoot.Children[End];
                        results.Add(result);
                        foundMatch = true;
                    }
                    else
                    {
                        trails.Add(new Trail { pattern = LevPattern.Add, srcPos = items.Length - 1 });
                        trails[trails.Count - 1].tgtPos = TargetPos(trails, start);
                        if (LevSearch(items, start, editDist + 1, maxEditDist, child.Value, ref childResults, LevPattern.Add, ref trails))
                        {
                            results.AddRange(childResults);
                            foundMatch = true;
                        }
                        trails.RemoveAt(trails.Count - 1);
                    }
                }

                return foundMatch;
            }

            char item = items[start];

            bool currCharMatched = false;
            childResults.Clear();
            // Exact match next character
            if (currRoot.HasChild(item)
                && LevSearch(items, start + 1, editDist, maxEditDist, currRoot.GetChild(item), ref childResults, LevPattern.Exact, ref trails)
                )
            {
                results.AddRange(childResults);
                currCharMatched = true;
            }

            // Delete character
            childResults.Clear();
            if (prevPattern != LevPattern.Add)
            {
                trails.Add(new Trail { pattern = LevPattern.Delete, srcPos = start});
                trails[trails.Count - 1].tgtPos = TargetPos(trails, start);
                if (LevSearch(items, start + 1, editDist + 1, maxEditDist, currRoot, ref childResults, LevPattern.Delete, ref trails))
                {
                    results.AddRange(childResults);
                }
                trails.RemoveAt(trails.Count - 1);
            }

            if (currRoot.Children != null)
            {
                foreach (var child in currRoot.Children)
                {
                    if (child.Key == item && currCharMatched)
                        continue;

                    // Wrong character
                    childResults.Clear();
                    trails.Add(new Trail { pattern = LevPattern.Replace, srcPos = start});
                    trails[trails.Count - 1].tgtPos = TargetPos(trails, start);
                    if (LevSearch(items, start + 1, editDist + 1, maxEditDist, child.Value, ref childResults, LevPattern.Replace, ref trails))
                    {
                        results.AddRange(childResults);
                    }
                    trails.RemoveAt(trails.Count - 1);

                    // Add character
                    childResults.Clear();
                    if (prevPattern != LevPattern.Delete)
                    {
                        trails.Add(new Trail { pattern = LevPattern.Add, srcPos = start});
                        trails[trails.Count - 1].tgtPos = TargetPos(trails, start);
                        if (LevSearch(items, start, editDist + 1, maxEditDist, child.Value, ref childResults, LevPattern.Add, ref trails))
                        {
                            results.AddRange(childResults);
                        }
                        trails.RemoveAt(trails.Count - 1);
                    }
                }
            }

            return results.Count > 0;
        }

        static int TargetPos(List<Trail> trails, int srcPos)
        {
            if (trails.Count == 0)
                return srcPos;

            int tgtPos = srcPos;
            foreach (var t in trails)
            {
                if (t.pattern == LevPattern.Add)
                {
                    tgtPos++;
                }
                else if (t.pattern == LevPattern.Delete)
                {
                    tgtPos--;
                }
            }

            return tgtPos;
        }
    }
}
