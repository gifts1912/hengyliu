using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Misc.Utility;

namespace QU.Utility
{
    public class ReformulationPatternMatch
    {
        private static char[] WildAndSpaceSeperator = new char[] { '*', ' ' };
        private static char[] WildSeperator = new char[] { '*' };
        private static char[] SpaceSeperator = new char[] { ' ' };
        private static string[] PairSeperator = new string[] { "|||" };
        private static Dictionary<string, string> IntentMapping = new Dictionary<string, string>();

        static ReformulationPatternMatch()
        {
            string intentFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "intents.txt");
            if (File.Exists(intentFile))
            {
                using (StreamReader sr = new StreamReader(intentFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length < 2)
                            continue;
                        IntentMapping[items[0]] = items[1];
                    }
                }
            }
        }

        public static string MakePair(string left, string right)
        {
            return left + "|||" + right;
        }

        public static string[] DecomposePair(string item)
        {
            return item.Split(PairSeperator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Read reformulation patterns to memory.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Dictionary<string, List<ReformulationPattern>> ReadReformPatterns(string file)
        {
            Dictionary<string, List<ReformulationPattern>> dictLeftP2ReformPatterns
                = new Dictionary<string, List<ReformulationPattern>>();

            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    ReformulationPattern p = ReformulationPattern.ReadFromLineWith5Items(line);
                    if (p == null)
                        continue;

                    // remove all of the wild-match.
                    if (p.Left.Split(new char[] { ' ', '*' }, StringSplitOptions.RemoveEmptyEntries).Length < 1)
                        continue;

                    if (dictLeftP2ReformPatterns.ContainsKey(p.Left))
                    {
                        dictLeftP2ReformPatterns[p.Left].Add(p);
                    }
                    else
                    {
                        dictLeftP2ReformPatterns.Add(p.Left, new List<ReformulationPattern>(new[] { p }));
                    }
                }
            }

            return dictLeftP2ReformPatterns;
        }

        /// <summary>
        /// Classify if a query needs to be reformulated in terms of the complexity of the query and structure.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryWithSlots"></param>
        /// <returns></returns>
        public static bool HasReformulationPotential(string query, string queryWithSlots)
        {
            // simple queries are not supposed to be altered.
            int qTermCnt = query.Split(SpaceSeperator, StringSplitOptions.RemoveEmptyEntries).Length;
            if (qTermCnt < 5)
            {
                return false;
            }

            // queries with simple structure are not supposed to be altered.
            int qWithSlotTermCnt = 0, slotTermCnt = 0, otherTermCnt = 0;
            var terms = queryWithSlots.Split(WildAndSpaceSeperator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in terms)
            {
                qWithSlotTermCnt++;
                if (t.StartsWith("Slot^"))
                {
                    slotTermCnt++;
                }
                else
                {
                    otherTermCnt++;
                }
            }

            if (qWithSlotTermCnt < 3 || otherTermCnt < 2)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if queryWithSlots matches pattern.
        /// </summary>
        /// <param name="queryWithSlots"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool Match(string queryWithSlots, string pattern)
        {
            Match match = null;
            return Match(queryWithSlots, pattern, out match);
        }

        /// <summary>
        /// Check if queryWithSlots matches pattern.
        /// </summary>
        /// <param name="queryWithSlots"></param>
        /// <param name="pattern"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static bool Match(string queryWithSlots, string pattern, out Match match)
        {
            match = null;

            if (!pattern.Contains('*'))
            {
                return queryWithSlots.Equals(pattern, StringComparison.OrdinalIgnoreCase);
            }

            pattern = GenRegex(pattern);
            match = Regex.Match(queryWithSlots, pattern);
            return (null != match && match.Success);
        }

        /// <summary>
        /// Check if queryWithSlots matches pattern.
        /// </summary>
        /// <param name="queryWithSlots"></param>
        /// <param name="pattern"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static bool Match(string queryWithSlots, ReformulationPattern reformPattern, out Match match)
        {
            match = null;
            string pattern = reformPattern.Left;

            if (!pattern.Contains('*'))
            {
                return queryWithSlots.Equals(pattern, StringComparison.OrdinalIgnoreCase);
            }

            //if (!queryWithSlots.Contains(reformPattern.PrefilteredKey))
            //{
            //    return false;
            //}

            pattern = GenRegex(pattern);
            match = Regex.Match(queryWithSlots, pattern);
            return (null != match && match.Success);
        }

        /// <summary>
        /// Replace * as meaningful regex.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string GenRegex(string pattern)
        {
            string[] items = pattern.Split(SpaceSeperator, StringSplitOptions.RemoveEmptyEntries);
            string regex = string.Empty;
            int idWild = 0;
            foreach (var item in items)
            {
                if (item.Equals("*"))
                {
                    regex += item.Replace("*", string.Format("(?<wild{0}>.*?)", idWild++));
                }
                else
                {
                    regex += item;
                }

                regex += " ";
            }

            regex = regex.TrimEnd(' ').Replace("^", "\\^").Replace("$", "\\$");
            regex = "^" + regex + "$";

            return regex;
        }

        /// <summary>
        /// Replace slots in query with "Slot^Type".
        /// </summary>
        /// <param name="query"></param>
        /// <param name="slots"></param>
        /// <returns></returns>
        public static string ReplaceSlot(string query, string slots, bool ignoreIntent = false)
        {
            List<QueryParseResult> results;
            return ReplaceSlot(query, slots, out results, ignoreIntent);
        }

        /// <summary>
        /// Replace slots in query with "Slot^Type".
        /// </summary>
        /// <param name="query"></param>
        /// <param name="slots"></param>
        /// <returns></returns>
        public static string ReplaceSlot(string query, string slots, out List<QueryParseResult> results, bool ignoreIntent = false)
        {
            results = null;

            if (string.IsNullOrEmpty(slots) || string.IsNullOrEmpty(query))
            {
                return query;
            }

            string replacedQ = query;

            try
            {
                results = CRFOutputParser.ParseResults(slots);
                replacedQ = ReplaceSlot(query, results, ignoreIntent);
            }
            catch (Exception ex)
            {
                results = null;
                replacedQ = query;
            }

            return replacedQ;
        }

        /// <summary>
        /// Replace Slot.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="results"></param>
        /// <param name="ignoreIntent"></param>
        /// <returns></returns>
        public static string ReplaceSlot(string query, List<QueryParseResult> results, bool ignoreIntent = false)
        {
            if (null == results || results.Count == 0)
            {
                return query;
            }

            List<int> toBeDel = new List<int>(results.Count);
            int id = 0;
            foreach (var r in results)
            {
                if (ignoreIntent && r.Type.ToLower().StartsWith("int_"))
                {
                    toBeDel.Add(id);
                }
                id++;
            }

            foreach (var del in toBeDel)
            {
                results.RemoveAt(del);
            }

            string replacedQ = query;

            try
            {
                int shift = 0;
                foreach (var r in results)
                {
                    string slot = r.Type;

                    // modify type like "Ent_Body_Structure4" as "Ent_Body_Structure"
                    int typeEnd = slot.Length - 1;
                    while (typeEnd >= 0)
                    {
                        if (slot[typeEnd] < '9' && slot[typeEnd] > '0')
                        {
                            typeEnd--;
                        }
                        else
                        {
                            break;
                        }
                    }

                    slot = slot.Substring(0, typeEnd + 1);
                    string slotString = "Slot^" + slot;
                    r.Type = slotString;

                    replacedQ = replacedQ.Substring(0, r.Begin + shift)
                                + slotString +
                                replacedQ.Substring(r.End + shift, replacedQ.Length - r.End - shift);
                    shift += slotString.Length - r.Span.Length;
                }
            }
            catch
            {
                results = null;
                replacedQ = query;
            }

            return replacedQ;
        }

        /// <summary>
        /// Calculate the score of reformulation pattern.
        ///   It's a function of (L2R, R2L, matched terms/slots except wildcard)
        /// </summary>
        /// <param name="queryWithSlot"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static double CalReformulationScore(string queryWithSlot, ReformulationPattern pattern)
        {
            ReformulationFeatures features = ExtractReformulationFeatures(queryWithSlot, pattern);

            if (features == null)
            {
                return -1;
            }

            double score = 1.0 * features.ReformProb;

            return score;
        }

        /// <summary>
        /// Extract Reformulation Features.
        /// </summary>
        /// <param name="queryWithSlot"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static ReformulationFeatures ExtractReformulationFeatures(string queryWithSlot, ReformulationPattern pattern)
        {
            if (pattern.L2R + pattern.R2L <= 0 || pattern.L2R < pattern.R2L)
            {
                return null;
            }

            // Feature 1: reformulation probability
            double reformProb = (double)(pattern.L2R - pattern.R2L) / (double)(pattern.L2R + pattern.R2L);

            // Feature 2: l2r
            double l2r = pattern.L2R;

            // Feature 3: matched terms/slots except wildcard
            double matchedTerms = pattern.Left.Split(WildAndSpaceSeperator, StringSplitOptions.RemoveEmptyEntries).Length;

            // Feature 4: exact match
            double querySlotTermCnt = queryWithSlot.Split(WildAndSpaceSeperator, StringSplitOptions.RemoveEmptyEntries).Length;
            double exactMatch = !pattern.Left.Contains('*') ? 1.0 : matchedTerms / querySlotTermCnt;
            exactMatch = Math.Min(exactMatch, 1.0);

            // Feature 5: delta length
            double deltaLen = pattern.Right.Split(WildAndSpaceSeperator, StringSplitOptions.RemoveEmptyEntries).Length - matchedTerms;
            string lIntent, rIntent;
            double LIntentPhraseLen = GetIntentPhraseLen(pattern.Left, out lIntent);
            double RIntentPhraseLen = GetIntentPhraseLen(pattern.Right, out rIntent);
            bool IntentMissing = LIntentPhraseLen > 0 && (RIntentPhraseLen <= 0 || rIntent != lIntent);

            int wildcardCount = pattern.Left.Count(c => c == '*');

            return new ReformulationFeatures 
                        { L2R = l2r, 
                            ReformProb = reformProb, 
                            ExactMatch = exactMatch, 
                            DeltaQueryLength = deltaLen, 
                            LIntentPhraseLen = LIntentPhraseLen,
                            RIntentPhraseLen = RIntentPhraseLen,
                            IntentMissing = IntentMissing,
                            WildcardCount = wildcardCount, 
                            WildcardMatchtedTerms = Math.Max(0, (int)(querySlotTermCnt - matchedTerms))
                        };
        }

        /// <summary>
        /// Extract Reformulation Features.
        /// </summary>
        /// <param name="queryWithSlot"></param>
        /// <param name="pattern"></param>
        /// <param name="dictPattern2Feature">Complementary features.</param>
        /// <returns></returns>
        public static ReformulationFeatures ExtractReformulationFeatures(string queryWithSlot, 
            ReformulationPattern pattern, Dictionary<string, ReformulationFeatures> dictPattern2Feature)
        {
            ReformulationFeatures feaOrig = ExtractReformulationFeatures(queryWithSlot, pattern);
            if (feaOrig == null)
            {
                return null;
            }

            ReformulationFeatures fea;
            if (!dictPattern2Feature.TryGetValue(MakePair(pattern.Left, pattern.Right), out fea))
            {
                fea = new ReformulationFeatures();
            }

            fea.L2R = feaOrig.L2R;
            fea.ReformProb = feaOrig.ReformProb;
            fea.ExactMatch = feaOrig.ExactMatch;
            fea.DeltaQueryLength = feaOrig.DeltaQueryLength;
            fea.LIntentPhraseLen = feaOrig.LIntentPhraseLen;
            fea.RIntentPhraseLen = feaOrig.RIntentPhraseLen;
            fea.IntentMissing = feaOrig.IntentMissing;
            fea.WildcardCount = feaOrig.WildcardCount;
            fea.WildcardMatchtedTerms = feaOrig.WildcardMatchtedTerms;

            return fea;
        }

        public static double GetIntentPhraseLen(string pattern)
        {
            string intentType = "";
            return GetIntentPhraseLen(pattern, out intentType);
        }

        public static double GetIntentPhraseLen(string pattern, out string intentType)
        {
            var terms = pattern.Split(WildAndSpaceSeperator, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.StartsWith("Slot^Ent_") ? "*" : t);
            var q = string.Join(" ", terms);
            var items = q.Split(WildSeperator, StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim())
                        .Where(t => !string.IsNullOrEmpty(t)).ToArray();

            intentType = "";

            int maxLen = 0;
            foreach (var ln in items)
            {
                foreach (var p in Split(ln, 3))
                {
                    if (IntentMapping.ContainsKey(p.Key))
                    {
                        if (p.Value > maxLen)
                        {
                            maxLen = p.Value;
                            intentType = IntentMapping[p.Key];
                        }
                    }
                }
            }

            return maxLen;
        }

        private static IEnumerable<KeyValuePair<string, int>> Split(string query, int ngram)
        {
            string[] items = query.Split(SpaceSeperator);
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 1; j <= Math.Min(ngram, items.Length - i); j++)
                {
                    yield return new KeyValuePair<string, int>(string.Join(" ", items.Skip(i).Take(j)), j);
                }
            }
        }

        /// <summary>
        /// Apply pattern to gen altered query.
        /// </summary>
        /// <param name="origQuery"></param>
        /// <param name="results"></param>
        /// <param name="queryWithSlots"></param>
        /// <param name="leftPMatch"></param>
        /// <param name="rightP"></param>
        /// <returns></returns>
        public static string ApplyPattern(string origQuery, List<QueryParseResult> results, string queryWithSlots, Match leftPMatch, string rightP)
        {
            // Build Slot Dict.
            Dictionary<string, List<string>> dictSlotType2Span = new Dictionary<string, List<string>>();
            if (null != results)
            {
                foreach (var r in results)
                {
                    if (!dictSlotType2Span.ContainsKey(r.Type))
                    {
                        dictSlotType2Span.Add(r.Type, new List<string>());
                    }

                    dictSlotType2Span[r.Type].Add(r.Span);
                }
            }

            string alteredQuery = string.Empty;
            var items = rightP.Split(SpaceSeperator, StringSplitOptions.RemoveEmptyEntries);
            int idWild = 0;
            foreach (var item in items)
            {
                if (item.Equals("*")) // Pattern transformation
                {
                    if (leftPMatch == null)
                    {
                        return string.Empty;
                    }

                    // assume that the order of "*" is the same in leftP and rightP.
                    string key = string.Format("wild{0}", idWild++);
                    Group matchedGroup = leftPMatch.Groups[key];
                    if (matchedGroup == null || !matchedGroup.Success)
                    {
                        // number of "*" mismatch.
                        return string.Empty;
                    }

                    alteredQuery += matchedGroup.Value;
                }
                else if (item.StartsWith("Slot^")) // Slot transformation.
                {
                    List<string> spans;
                    if (!dictSlotType2Span.TryGetValue(item, out spans))
                    {
                        return string.Empty;
                    }

                    if (null == spans || spans.Count == 0)
                    {
                        return string.Empty;
                    }

                    alteredQuery += spans.First();
                    spans.RemoveAt(0);
                }
                else
                {
                    alteredQuery += item;
                }

                alteredQuery += " ";
            }

            // Slot number mismatch.
            if (alteredQuery.Contains("Slot^") || dictSlotType2Span.Sum(p => p.Value.Count) > 0)
            {
                return string.Empty;
            }

            return alteredQuery.TrimEnd(' ');
        }

        /// <summary>
        /// Alter a query to another one with a given pattern.
        /// </summary>
        /// <param name="origQuery"></param>
        /// <param name="queryWithSlot"></param>
        /// <param name="pattern"></param>
        /// <returns>Altered Query if success, else empty.</returns>
        public static string ApplyPattern(string origQuery, List<QueryParseResult> results, string queryWithSlots, string leftP, string rightP)
        {
            // Build Regex.
            Match match = null;
            if (leftP.ToString().Contains("*"))
            {
                leftP = GenRegex(leftP);
                match = Regex.Match(queryWithSlots, leftP);
                if (null == match || !match.Success)
                {
                    return string.Empty;
                }
            }

            return ApplyPattern(origQuery, results, queryWithSlots, match, rightP);
        }

        public static string ApplyPattern(string origQuery, List<QueryParseResult> results, string queryWithSlots, List<string> wilds, string rightP)
        {
            List<string> clonedWilds = new List<string>(wilds);

            // Build Slot Dict.
            Dictionary<string, List<string>> dictSlotType2Span = new Dictionary<string, List<string>>();
            if (null != results)
            {
                foreach (var r in results)
                {
                    if (!dictSlotType2Span.ContainsKey(r.Type))
                    {
                        dictSlotType2Span.Add(r.Type, new List<string>());
                    }

                    dictSlotType2Span[r.Type].Add(r.Span);
                }
            }

            string[] items = rightP.Split(SpaceSeperator, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < items.Length; ++i)
            {
                string item = items[i];
                if (item.Equals("*")) // Pattern transformation
                {
                    if (clonedWilds == null || clonedWilds.Count == 0)
                    {
                        return string.Empty;
                    }

                    items[i] = clonedWilds[0];
                    clonedWilds.RemoveAt(0);
                }
            }

            string alteredQuery = string.Empty;
            foreach (var item in items)
            {
                if (item.StartsWith("Slot^")) // Slot transformation.
                {
                    List<string> spans;
                    if (!dictSlotType2Span.TryGetValue(item, out spans))
                    {
                        return string.Empty;
                    }

                    if (null == spans || spans.Count == 0)
                    {
                        return string.Empty;
                    }

                    alteredQuery += spans.First();
                    spans.RemoveAt(0);
                }
                else
                {
                    alteredQuery += item;
                }

                alteredQuery += " ";
            }

            // Slot number mismatch.
            if (alteredQuery.Contains("Slot^") || dictSlotType2Span.Sum(p => p.Value.Count) > 0)
            {
                return string.Empty;
            }

            return alteredQuery.TrimEnd(' ');
        }

        /// <summary>
        /// Alter a query to another one with a given pattern.
        /// </summary>
        /// <param name="origQuery"></param>
        /// <param name="results"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string ApplyPattern(string origQuery, List<QueryParseResult> results, string leftP, string rightP)
        {
            string queryWithSlot = ReplaceSlot(origQuery, results);
            return ApplyPattern(origQuery, results, queryWithSlot, leftP, rightP);
        }
    }

    /// <summary>
    /// Features extracted for scoring reformulated candidates.
    /// </summary>
    public class ReformulationFeatures
    {
        // pattern level features
        public double BinaryClickCoverage;
        public double FloatClickCoverage;
        public double BinarySatClickCoverage;
        public double FloatSatClickCoverage;
        public double ClickRatio;
        public double SatClickRatio;
        public double ClickYield;
        public double SatClickYield;
        public double L2R;
        public double ReformProb;
        public double L2RPercent;
        public double LIntentPhraseLen;
        public double RIntentPhraseLen;
        public bool IntentMissing;
        public int WildcardCount;
        public int WildcardMatchtedTerms;

        // run-time features
        public double ExactMatch;
        public double DeltaQueryLength;
    }

    public class ReformulationPattern
    {
        public string Left;
        public string Right;
        public int L2R = 0;
        public int R2L = 0;
        // Scoring.
        public double Score = 0;
        public Misc.SimilarQueryType Type = Misc.SimilarQueryType.Others;
        public ReformulationFeatures Features;

        public static ReformulationPattern ReadFromLineWith5Items(string line)
        {
            if (string.IsNullOrEmpty(line))
                return null;

            string[] items = line.Split('\t');
            if (items.Length < 4)
                return null;

            try
            {
                ReformulationPattern pattern = new ReformulationPattern()
                    {
                        Left = items[0],
                        Right = items[1],
                        L2R = int.Parse(items[2]),
                        R2L = int.Parse(items[3]),
                        Type = Misc.SimilarQueryUtils.DistinguishSimilarQueryType(items[0], items[1]),
                    };
                pattern.Score = (pattern.L2R - pattern.R2L) / (pattern.L2R + pattern.R2L);
                return pattern;
            }
            catch
            {
                return null;
            }
        }

        private static char[] Seperator = new char[] { '*' };
        private static char[] Space = new char[] { ' ' };

        private static string GenPrefilteredKey(string left)
        {
            string[] items = left.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);
            if (null == items || items.Length == 0)
            {
                return " ";
            }

            int maxTerms = -1;
            string key = " ";
            foreach (var item in items)
            {
                int len = item.Split(Space, StringSplitOptions.RemoveEmptyEntries).Length;
                if (len > maxTerms)
                {
                    maxTerms = len;
                    key = item.Trim(' ');
                }
            }

            return key;
        }

        public static ReformulationPattern ReadFromLineWith3Items(string line)
        {
            if (string.IsNullOrEmpty(line))
                return null;

            string[] items = line.Split('\t');
            if (items.Length < 3)
                return null;

            try
            {
                ReformulationPattern pattern = new ReformulationPattern()
                {
                    Left = items[0],
                    Right = items[1],
                    L2R = int.Parse(items[2]),
                    R2L = 0,
                    Type = Misc.SimilarQueryUtils.DistinguishSimilarQueryType(items[0], items[1])
                };
                pattern.Score = (pattern.L2R - pattern.R2L) / (pattern.L2R + pattern.R2L);
                return pattern;
            }
            catch
            {
                return null;
            }
        }
    }
}
