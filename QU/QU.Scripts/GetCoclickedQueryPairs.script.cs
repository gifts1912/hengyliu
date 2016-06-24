using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class Misc
{
    public static bool IsDateTime(string str)
    {
        DateTime dt;
        if (DateTime.TryParse(str, out dt))
            return true;
        return false;
    }
}

public class JoinUrl_String : Aggregate1<string, string>
{
    string u;
    public override void Initialize() { u = null; }
    public override void Add(string s)
    {
        if (u == null)
            u = s;
        else
            u = u + "##" + s;
    }
    public override string Finalize() { return u; }
}

public class JoinFreqClick_LongLong : Aggregate2<long, long, string>
{
    string u;
    public override void Initialize() { u = null; }
    public override void Add(long n1, long n2)
    {
        if (u == null)
            u = n1 + "," + n2;
        else
            u = u + "##" + n1 + "," + n2;
    }
    public override string Finalize() { return u; }
}

/// <summary>
///  
/// </summary>
public class CoclickedQueryPairReducer : Reducer
{
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return new Schema("leftQ, rightQ, url, leftQFreq:long, rightQFreq:long, leftQUFreq:long, rightQUFreq:long, leftQUSatCnt:long, rightQUSatCnt:long");
    }

    public override IEnumerable<Row> Reduce(RowSet input, Row output, string[] args)
    {
        int tailFreq = int.Parse(args[0]), goodFreq = int.Parse(args[1]);
        int hotFreq = int.Parse(args[2]), maxBucket = int.Parse(args[3]);
        string url = string.Empty;

        List<QUInfo> tailQueries = new List<QUInfo>(), goodQueries = new List<QUInfo>();
        foreach (Row row in input.Rows)
        {
            string query = row["query"].String;
            url = row["url"].String;
            long queryFreq = row["queryLevelRoughImpressionCnt"].Long;

            // Filter non
            if ((queryFreq > tailFreq && queryFreq < goodFreq) 
                || queryFreq >= hotFreq
                || (queryFreq <= tailFreq && tailQueries.Count >= maxBucket)
                || (queryFreq >= goodFreq && goodQueries.Count >= maxBucket))
            {
                continue;
            }

            QUInfo info = new QUInfo
            {
                impressions = row["impressionCnt"].Long,
                query = query,
                queryFreq = queryFreq,
                //querySatClicks = row["queryLevelSatClickCnt"].Integer,
                //querySatUrls = row["queryLevelSatUrlCnt"].Integer,
                satClicks = row["satCnt"].Long
            };

            if (queryFreq <= tailFreq)
            {
                tailQueries.Add(info);
            }
            else if (queryFreq >= goodFreq)
            {
                goodQueries.Add(info);
            }
        }

        foreach (var good in goodQueries)
        {
            foreach (var tail in tailQueries)
            {
                output["leftQ"].Set(tail.query);
                output["rightQ"].Set(good.query);
                output["url"].Set(url);
                output["leftQFreq"].Set(tail.queryFreq);
                output["rightQFreq"].Set(good.queryFreq);
                output["leftQUFreq"].Set(tail.impressions);
                output["rightQUFreq"].Set(good.impressions);
                output["leftQUSatCnt"].Set(tail.satClicks);
                output["rightQUSatCnt"].Set(good.satClicks);
                yield return output;
            }
        }
    }

    class QUInfo
    {
        // query level
        public string query;
        public long queryFreq;
        //public int querySatClicks;
        //public int querySatUrls;

        // QU level
        public long impressions;
        public long satClicks;
    }
}


