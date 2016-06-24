using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Linq;

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
        int goodFreq = int.Parse(args[0]);
        int hotFreq = int.Parse(args[1]), maxQueriesPerBucket = int.Parse(args[2]);
        string url = string.Empty;

        List<QUInfo> allQueries = new List<QUInfo>();
        int count = 0;
        foreach (Row row in input.Rows)
        {
            count++;
            string query = row["query"].String;
            url = row["url"].String;
            long queryFreq = row["queryFreq"].Long;

            if (queryFreq > hotFreq)
            {
                continue;
            }

            QUInfo info = new QUInfo
            {
                impressions = row["impressionCnt"].Long,
                query = query,
                queryFreq = queryFreq,
                satClicks = row["satCnt"].Long
            };

            allQueries.Add(info);
        }

        if (allQueries.Count <= 2)
            yield break;

        long medianFreq = allQueries[allQueries.Count / 2].queryFreq;

        List<QUInfo> goodQueries = (from q in allQueries
                                    where q.queryFreq >= Math.Max(goodFreq, medianFreq)
                                    select q).Take(maxQueriesPerBucket).ToList();

        List<QUInfo> tailQueries = (from q in allQueries
                                   where q.queryFreq < medianFreq
                                   select q).Take(maxQueriesPerBucket).ToList(); ;

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

        // QU level
        public long impressions;
        public long satClicks;
    }
}

