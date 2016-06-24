using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility
{
    public class CharEM
    {
        public class Features
        {
            public double pst;
            public double pts;
            public double unknown1;
            public double unknown2;
        }

        public static string MakeKey(string left, string right)
        {
            return string.Format("{0}|||{1}", left, right);
        }

        static double ToDouble(string str)
        {
            double t;
            if (!double.TryParse(str, out t))
            {
                t = 0;
            }

            return t;
        }

        private static IEnumerable<string> Split(string query, int ngram)
        {
            char[] items = query.ToCharArray();
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 2; j <= Math.Min(ngram, items.Length - i); j++)
                {
                    yield return string.Join("", items.Skip(i).Take(j));
                }
            }
        }

        static char[] Tab = new char[] { '\t'};
        static char[] Space = new char[] { ' ' };
        Dictionary<string, Features> dictKey2Features = new Dictionary<string, Features>();
        bool loaded = false;
        public bool Loaded
        {
            get { return loaded; }
        }

        public bool ReadModel(string file, int maxOrder)
        {
            if (!File.Exists(file))
            {
                loaded = false;
                return false;
            }

            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] terms = line.Split(Tab, StringSplitOptions.RemoveEmptyEntries);

                    if (terms.Length < 6)
                    {
                        continue;
                    }

                    if (terms[0].Length > maxOrder || terms[1].Length > maxOrder)
                    {
                        continue;
                    }

                    dictKey2Features[MakeKey(terms[0], terms[1])] = new Features
                    {
                        pst = ToDouble(terms[2]),
                        pts = ToDouble(terms[3]),
                        unknown1 = ToDouble(terms[4]),
                        unknown2 = ToDouble(terms[5])
                    };
                }
            }

            loaded = dictKey2Features.Count > 0;

            return loaded;
        }

        public double CalScore(Trie.LevSearchResult result, string srcTerm)
        {
            if (result.EditDist == 0)
                return 1.0;

            string tgtTerm = result.Term;
            double score = 1;
            foreach (var trail in result.Trails)
            {
                double pathScore = -1;
                int sbeg = trail.srcPos - 1, send = trail.srcPos + 1;
                int tbeg = trail.tgtPos - 1, tend = trail.tgtPos + 1;
                if (trail.pattern == Trie.LevPattern.Add)
                {
                    sbeg = trail.srcPos - 1;
                    send = trail.srcPos;
                    tbeg = trail.tgtPos - 2;
                    tend = trail.tgtPos;
                }
                else if (trail.pattern == Trie.LevPattern.Delete)
                {
                    tbeg = trail.tgtPos;
                }

                // prefix bigram
                string key = MakeKey(GetNgram(srcTerm, sbeg, send - 1), GetNgram(tgtTerm, tbeg, tend - 1));
                pathScore = Math.Max(pathScore, GetScore(key));

                // postfix bigram
                key = MakeKey(GetNgram(srcTerm, sbeg + 1, send), GetNgram(tgtTerm, tbeg + 1, tend));
                pathScore = Math.Max(pathScore, GetScore(key));
                // trigram
                key = MakeKey(GetNgram(srcTerm, sbeg, send), GetNgram(tgtTerm, tbeg, tend));
                pathScore = Math.Max(pathScore, GetScore(key));
                score *= pathScore;
            }

            return score;
        }

        static string GetNgram(string term, int beg, int end)
        {
            beg = Math.Min(term.Length - 1, Math.Max(0, beg));
            end = Math.Min(term.Length - 1, Math.Max(0, end));
            int len = end - beg + 1;
            return string.Join("", term.Skip(beg).Take(len));
        }

        double GetScore(string key)
        {
            Features fea;
            if (dictKey2Features.TryGetValue(key, out fea))
            {
                return fea.pst;
            }

            return 0;
        }
    }
}
