using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility
{
    public class ReformulationPatternTrie<T>
    {
        public class Node
        {
            private string content;
            /// <summary>
            /// Content in Node.
            /// </summary>
            public string Content
            {
                get { return content; }
                set { content = value; }
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

            private Dictionary<string, Node> children;
            /// <summary>
            /// Children indexed by their content.
            /// </summary>
            public Dictionary<string, Node> Children
            {
                get { return children; }
                set { children = value; }
            }

            private string pattern = string.Empty;
            /// <summary>
            /// Actual Pattern which is aggregation from root to leaf.
            /// Only exist when this node is a leaf.
            /// </summary>
            public string Pattern
            {
                get { return pattern; }
                set { pattern = value; }
            }

            /// <summary>
            /// Reformulations. Only exist when this node is a leaf.
            /// </summary>
            private T metadata;
            public T Metadata
            {
                get { return metadata; }
                set { metadata = value; }
            }

            /// <summary>
            /// If current node has any child.
            /// </summary>
            /// <param name="child"></param>
            /// <returns></returns>
            public bool HasChild(Node child)
            {
                return children.ContainsKey(child.content);
            }

            /// <summary>
            /// If current node has any child with the specified content.
            /// </summary>
            /// <param name="child"></param>
            /// <returns></returns>
            public bool HasChild(string child)
            {
                return children.ContainsKey(child);
            }

            /// <summary>
            /// Get specified child. Null if none found.
            /// </summary>
            /// <param name="child"></param>
            /// <returns></returns>
            public Node GetChild(string child)
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
                children[child.content] = child;
            }

            public override int GetHashCode()
            {
                return pattern.GetHashCode();
            }
        }

        static char[] Space = new char[] { ' ' };
        static string Start = "^^^";
        static string End = "$$$";
        private Node root;
        /// <summary>
        /// Root of the tree.
        /// </summary>
        public Node Root
        {
            get { return root; }
            set { root = value; }
        }

        /// <summary>
        /// Build tree.
        /// </summary>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public Node BuildTree(IEnumerable<string> patterns, Dictionary<string, T> dict)
        {
            Node root = new Node()
            {
                Content = Start,
                Parent = null,
                Children = new Dictionary<string, Node>()
            };

            // Enumerate all of the patterns.
            foreach (string p in patterns)
            {
                string[] items = p.Split(Space, StringSplitOptions.RemoveEmptyEntries);
                int start = 0;
                if (items[0].Equals(Start))
                {
                    start = 1;
                }

                Node currRoot = root;
                for (int i = start; i < items.Length; i++)
                {
                    Node child = currRoot.GetChild(items[i]);
                    if (child == null)
                    {
                        child = new Node { Content = items[i], Parent = currRoot, Children = new Dictionary<string, Node>() };
                        currRoot.AddChild(child);
                    }

                    currRoot = child;
                }

                if (items[items.Length - 1] != End)
                {
                    Node end = new Node { Content = End, Parent = currRoot, Children = null, Pattern = p };
                    T reformulations;
                    if (!dict.TryGetValue(p, out reformulations))
                        reformulations = default(T);
                    end.Metadata = reformulations;
                    currRoot.AddChild(end);
                }
                else
                {
                    currRoot.Pattern = p;
                    T reformulations;
                    if (!dict.TryGetValue(p, out reformulations))
                        reformulations = default(T);
                    currRoot.Metadata = reformulations;
                }
            }

            this.root = root;
            return root;
        }

        public class MatchInfo
        {
            public List<string> wildPhrases = new List<string>();
            public T metadata;
        }

        /// <summary>
        /// Find matches.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="matches"></param>
        /// <returns></returns>
        public bool FindMatches(string pattern, out List<MatchInfo> matches)
        {
            matches = new List<MatchInfo>();
            if (root == null)
            {
                return false;
            }

            if (!pattern.EndsWith(End))
            {
                pattern = pattern + " " + End;
            }

            string[] items = pattern.Split(Space, StringSplitOptions.RemoveEmptyEntries);
            Node currRoot = root;
            int start = 0;
            if (items[0].Equals(Start))
            {
                start = 1;
            }

            List<string> wilds = new List<string>();
            return FindMatches(items, start, currRoot, wilds, ref matches);
        }

        // Find matches.
        private bool FindMatches(string[] items, int start, Node currRoot, List<string> wilds, ref List<MatchInfo> matches)
        {
            if (start > items.Length - 1)
            {
                return false;
            }
            else if (start == items.Length - 1)
            {
                if (currRoot.HasChild(End))
                {
                    List<string> finalWilds = new List<string>(wilds);
                    matches.Add(new MatchInfo
                    {
                        metadata = currRoot.GetChild(End).Metadata,
                        wildPhrases = finalWilds
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            }

            string item = items[start];

            // wild match, cannot be "$".
            if (currRoot.HasChild("*"))
            {
                List<MatchInfo> wildChildMatches = new List<MatchInfo>();
                var child = currRoot.GetChild("*");
                wilds.Add(item);
                if (FindMatches(items, start + 1, child, wilds, ref wildChildMatches))
                {
                    matches.AddRange(wildChildMatches);
                }
                wilds.RemoveAt(wilds.Count - 1);
            }

            // current wild match, cannot be "$"
            if (currRoot.Content == "*")
            {
                List<MatchInfo> currWildMatches = new List<MatchInfo>();
                string temp = wilds[wilds.Count - 1] + " " + item;
                wilds[wilds.Count - 1] = temp;
                if (FindMatches(items, start + 1, currRoot, wilds, ref currWildMatches))
                {
                    matches.AddRange(currWildMatches);
                }
                wilds[wilds.Count - 1] = temp.Substring(0, temp.Length - (" " + item).Length);
                //wilds.RemoveAt(wilds.Count - 1);
            }

            // child match content.
            if (currRoot.HasChild(item))
            {
                List<MatchInfo> childMatches = new List<MatchInfo>();
                if (FindMatches(items, start + 1, currRoot.GetChild(item), wilds, ref childMatches))
                {
                    matches.AddRange(childMatches);
                }
            }

            return matches.Count > 0;
        }
    }
}
