using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using QU.Utility;
using System.Text.RegularExpressions;
using System.Linq;


/// <summary>
/// 
/// </summary>
public class ParaQueryUsefulnessProcessor : Processor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input">Query, ParaphrasedQuery, PatternId, Urls, TotalImpressionCnt, ParaQueryAggUrlClicks</param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return new Schema("Query, ParaphrasedQuery, PatternId, TotalImpressionCnt:long, QueryReformSuccess:long, ReformImpressionSuccess:long");
    }

    static char[] UrlSeperator = new char[] { '|' };
    static char[] ImpressionSeperator = new char[] { ',' };

    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {

        foreach (Row row in input.Rows)
        {
            string query = row["Query"].String;
            string paraQuery = row["ParaphrasedQuery"].String;
            string patternId = row["PatternId"].String;
            output["Query"].Set(query);
            output["ParaphrasedQuery"].Set(paraQuery);
            output["PatternId"].Set(patternId);
            output["TotalImpressionCnt"].Set(row["TotalImpressionCnt"].Long);
            string urls = row["Urls"].String;
            string paraQClicks = row["ParaQueryAggUrlClicks"].String;
            if (string.IsNullOrEmpty(paraQClicks))
            {
                output["QueryReformSuccess"].Set(0);
                output["ReformImpressionSuccess"].Set(0);
                yield return output;
                continue;
            }

            HashSet<string> paraQClickedUrls = new HashSet<string>(
                from u in paraQClicks.Split(UrlSeperator, StringSplitOptions.RemoveEmptyEntries)
                where u.Contains(',')
                select u.Split(ImpressionSeperator, StringSplitOptions.RemoveEmptyEntries)[0]
                    );

            long reformImpressionSuccess = 0;
            foreach (var u in urls.Split(UrlSeperator, StringSplitOptions.RemoveEmptyEntries))
            {
                var items = u.Split(ImpressionSeperator, StringSplitOptions.RemoveEmptyEntries);
                if (items.Length != 2)
                {
                    continue;
                }

                if (paraQClickedUrls.Contains(items[0]))
                {
                    continue;
                }

                reformImpressionSuccess += long.Parse(items[1]);
            }

            output["QueryReformSuccess"].Set(reformImpressionSuccess == 0 ? 0 : 1);
            output["ReformImpressionSuccess"].Set(reformImpressionSuccess);

            yield return output;
        }
    }
}


/// <summary>
///  
/// </summary>
public class AggregateNoClickUrlsReducer : Reducer
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return new Schema("query, urls, totalImpressionCnt:long");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override IEnumerable<Row> Reduce(RowSet input, Row output, string[] args)
    {
        int maxUrls = int.Parse(args[0]);
        int count = 0;
        StringBuilder sb = new StringBuilder();
        string query = "";
        long totalImpressions = 0;
        foreach (Row row in input.Rows)
        {
            ++count;
            if (count == 1)
            {
                query = row["query"].String;
            }

            if (count > maxUrls)
            {
                break;
            }

            sb.Append(row["url"].String);
            sb.Append(',');
            long impression = row["impressionCnt"].Long;
            sb.Append(impression);
            sb.Append('|');
            totalImpressions += impression;
        }
        output["query"].Set(query);
        output["urls"].Set(sb.ToString());
        output["totalImpressionCnt"].Set(totalImpressions);
        yield return output;
    }
}



/// <summary>
/// 
/// </summary>
public class BreakPatternProcessor : Processor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        Schema output = input.Clone();
        output.Add(new ColumnInfo("LeftP", ColumnDataType.String));
        output.Add(new ColumnInfo("RightP", ColumnDataType.String));
        return output;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {

        foreach (Row row in input.Rows)
        {
            row.CopyTo(output);
            string patternId = row["PatternId"].String;
            if (string.IsNullOrEmpty(patternId))
                continue;
            string[] items = patternId.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length < 2)
                continue;
            output["LeftP"].UnsafeSet(items[0]);
            output["RightP"].UnsafeSet(items[1]);
            yield return output;
        }
    }
}


public class AggregatedQueryClicksReducer : Reducer
{
    private static bool s_initialized = false;
    private static int s_maxUrlsPerQuery = 100;

    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return new Schema("Query, AggregatedUrlClicks, TotalClickCnt:long, TotalSatCnt:long");
    }

    public void Initialize(Schema input, Schema output, string[] args)
    {
        lock (this)
        {
            if (!s_initialized)
            {
                if (args.Length > 0)
                {
                    s_maxUrlsPerQuery = int.Parse(args[0]);
                }
                s_initialized = true;
            }
        }
    }

    private class UrlClickInfo
    {
        public long ClickCount
        {
            get;
            set;
        }
        public long SatCount
        {
            get;
            set;
        }
    }

    public override IEnumerable<Row> Reduce(RowSet input, Row output, string[] args)
    {
        Dictionary<string, int> dictUrlHash2ClickCount = new Dictionary<string, int>();
        int count = 0;
        long totalClickCnt = 0;
        long totalSatCnt = 0;
        Dictionary<string, UrlClickInfo> dictUrlClicks = new Dictionary<string, UrlClickInfo>();
        foreach (Row row in input.Rows)
        {
            if (++count == 1)
            {
                string query = row["query"].String;
                output["Query"].UnsafeSet(query);
            }
            if (s_maxUrlsPerQuery > 0 && count > s_maxUrlsPerQuery)
            {
                break;
            }
            long clickCnt = row["clickCnt"].Long;
            long satCnt = row["satCnt"].Long;
            string url = row["url"].String;
            var urlClicks = new UrlClickInfo();
            urlClicks.ClickCount = clickCnt;
            urlClicks.SatCount = satCnt;
            dictUrlClicks[url] = urlClicks;
            totalClickCnt += clickCnt;
            totalSatCnt += satCnt;
        }
        string aggregatedUrlClicks = "";
        count = 0;
        foreach (var kvp in dictUrlClicks)
        {
            string urlClicksStr = string.Format("{0},{1},{2}", kvp.Key, kvp.Value.ClickCount, kvp.Value.SatCount);
            if (count++ == 0)
            {
                aggregatedUrlClicks = urlClicksStr;
            }
            else
            {
                aggregatedUrlClicks = string.Format("{0}|{1}", aggregatedUrlClicks, urlClicksStr);
            }
        }
        output["AggregatedUrlClicks"].UnsafeSet(aggregatedUrlClicks);
        output["TotalClickCnt"].UnsafeSet(totalClickCnt);
        output["TotalSatCnt"].UnsafeSet(totalSatCnt);
        yield return output;
    }
}


/// <summary>
/// 
/// </summary>
public class ApplyPatternProcessor : Processor
{
    static string GetResourceFileName(string filePath)
    {
        int idx = filePath.LastIndexOfAny(new char[] { '/', '\\' });
        return filePath.Substring(idx + 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        Schema output = new Schema();
        output.Add(new ColumnInfo("query", ColumnDataType.String));
        output.Add(new ColumnInfo("alterQ", ColumnDataType.String));
        output.Add(new ColumnInfo("patternId", ColumnDataType.String));
        return output;
    }

    ReformulationPatternTrie trie = new ReformulationPatternTrie();

    /// <summary>
    /// Initialize patterns here.
    /// </summary>
    /// <param name="args"></param>
    private void InitializeModel(string[] args)
    {
        string patternFile = GetResourceFileName(args[0]);
        var dictLeftP2ReformPatterns = ReformulationPatternMatch.ReadReformPatterns(patternFile);
        trie.BuildTree(dictLeftP2ReformPatterns.Keys, dictLeftP2ReformPatterns);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        InitializeModel(args);
        foreach (Row row in input.Rows)
        {
            string query = row["query"].String;
            string slots = row["slots"].String;

            // apply pattern
            List<QueryParseResult> results;
            string qWithSlots = ReformulationPatternMatch.ReplaceSlot(query, slots, out results, true);
            // enumerate all of the pairs to get reformulations.
            List<ReformulationPatternTrie.MatchInfo> matchedNodes;
            if (!trie.FindMatches(qWithSlots, out matchedNodes))
            {
                continue;
            }

            foreach (var node in matchedNodes)
            {
                if (node.reformPatterns == null || node.reformPatterns.Count == 0)
                    continue;

                foreach (var val in node.reformPatterns)
                {
                    // apply patterns.
                    string alteredQ = ReformulationPatternMatch.ApplyPattern(query, results, qWithSlots, node.wildPhrases, val.Right);
                    if (string.IsNullOrEmpty(alteredQ))
                    {
                        continue;
                    }
                    output["query"].UnsafeSet(query);
                    output["alterQ"].UnsafeSet(alteredQ);
                    output["patternId"].UnsafeSet(val.Left + "|||" + val.Right);
                    yield return output;
                }
            }
        }
    }
}

public class CalculateClickOverlapProcessor : Processor
{
    private class UrlClickInfo
    {
        public long ClickCount
        {
            get;
            set;
        }

        public long SatCount
        {
            get;
            set;
        }
    }

    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        Schema outSchema = new Schema("Query, ParaphrasedQuery, PatternId, BinaryOverlap:int, FloatOverlap:float, IsValidSatOverlap:bool, BinarySatOverlap:int, FloatSatOverlap:float");
        return outSchema;
    }

    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        foreach (Row row in input.Rows)
        {
            Dictionary<string, UrlClickInfo> dictUrlClicks = new Dictionary<string, UrlClickInfo>();
            output["Query"].UnsafeSet(row["Query"].String);
            output["ParaphrasedQuery"].UnsafeSet(row["ParaphrasedQuery"].String);
            output["PatternId"].UnsafeSet(row["PatternId"].String);
            long totalClickCount = row["TotalClickCnt"].Long;
            long totalSatCount = row["TotalSatCnt"].Long;
            //long targetTotalClickCount = row["TargetTotalClickCnt"].Long;
            long targetTotalSatCount = row["TargetTotalSatCnt"].Long;
            string strUrlClicks = row["AggregatedUrlClicks"].String;
            foreach (var urlClick in strUrlClicks.Split('|'))
            {
                string[] items = urlClick.Split(',');
                if (items.Length == 3)
                {
                    UrlClickInfo urlClickInfo = new UrlClickInfo();
                    long clickCount, satCount;
                    if (!long.TryParse(items[1], out clickCount))
                        clickCount = 0;
                    if (!long.TryParse(items[2], out satCount))
                        satCount = 0;
                    urlClickInfo.ClickCount = clickCount;
                    urlClickInfo.SatCount = satCount;
                    dictUrlClicks[items[0]] = urlClickInfo;
                }
            }
            if (dictUrlClicks.Count < 1)
            {
                yield break;
            }
            string strTargetUrlClicks = row["TargetAggregatedUrlClicks"].String;
            long overlapClickCount = 0;
            long overlapSatCount = 0;
            foreach (var urlClick in strTargetUrlClicks.Split('|'))
            {
                string[] items = urlClick.Split(',');
                if (items.Length == 3)
                {
                    long clickCount, satCount;
                    if (!long.TryParse(items[1], out clickCount))
                        clickCount = 0;
                    if (!long.TryParse(items[2], out satCount))
                        satCount = 0;
                    if (dictUrlClicks.ContainsKey(items[0]))
                    {
                        if (clickCount > 0)
                        {
                            overlapClickCount += dictUrlClicks[items[0]].ClickCount;
                        }
                        if (satCount > 0)
                        {
                            overlapSatCount += dictUrlClicks[items[0]].SatCount;
                        }
                    }
                }
            }

            output["BinaryOverlap"].UnsafeSet(overlapClickCount > 0 ? 1 : 0);
            output["FloatOverlap"].UnsafeSet(totalClickCount > 0 ? ((float)overlapClickCount / (float)totalClickCount) : .0f);
            if (totalSatCount > 0 && targetTotalSatCount > 0)
            {
                output["IsValidSatOverlap"].UnsafeSet(true);
            }
            else
            {
                output["IsValidSatOverlap"].UnsafeSet(false);
            }
            output["BinarySatOverlap"].UnsafeSet(overlapSatCount > 0 ? 1 : 0);
            output["FloatSatOverlap"].UnsafeSet(totalSatCount > 0 ? ((float)overlapSatCount / (float)totalSatCount) : .0f);
            yield return output;
        }
    }
}

