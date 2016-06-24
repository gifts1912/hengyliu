using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;
using System.Linq;

public class MyUtility
{
    public static string SimpleNormalizeQuery(string str_in)
    {
        string nq1 = Regex.Replace(str_in, "[\"'\\.,\\?{}\\[\\]]", " ");
        nq1 = nq1.Replace(";", " ");
        nq1 = nq1.ToLower();
        nq1 = Regex.Replace(nq1, "[ ]+", " ");
        nq1 = nq1.Trim();
        return nq1;
    }
}

/// <summary>
///  
/// </summary>
public class PatternReducer : Reducer
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
        return new Schema("p1, p2, ids");
    }

    class SpanInfo
    {
        public string span;
        public string stype;
        public string sid;

        public override bool Equals(object obj)
        {
            SpanInfo info = obj as SpanInfo;
            return span == info.span && stype == info.stype && sid == info.sid;
        }

        public override int GetHashCode()
        {
            return (span + stype + sid).GetHashCode();
        }
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
        int count = 0;
        string q1 = string.Empty, q2 = string.Empty;
        HashSet<SpanInfo> s1 = new HashSet<SpanInfo>();
        HashSet<SpanInfo> s2 = new HashSet<SpanInfo>();
        foreach (Row row in input.Rows)
        {
            if (++count == 1)
            {
                q1 = row["q1"].String;
                q2 = row["q2"].String;
            }

            s1.Add(new SpanInfo() { span = row["span1"].String, stype = row["stype1"].String, sid = row["sid1"].String.Substring("http://knowledge.microsoft.com/".Length) });
            s2.Add(new SpanInfo() { span = row["span2"].String, stype = row["stype2"].String, sid = row["sid2"].String.Substring("http://knowledge.microsoft.com/".Length) });
        }

        if (s2.Count != 1)
            yield break;

        string p2Id = s2.First().sid;
        string p2 = q2.Replace(s2.First().span, s2.First().stype);

        string p1Id = string.Empty;
        Dictionary<string, string> pattern2id = new Dictionary<string, string>();
        pattern2id.Add(q1, "");
        foreach (var s in s1)
        {
            var kvp = pattern2id.ToList();
            foreach (var p2i in kvp)
            {
                string newp = p2i.Key.Replace(s.span, s.stype);
                string newid = p2i.Value == "" ? s.sid : p2i.Value + ";" + s.sid;
                if (!pattern2id.ContainsKey(newp))
                {
                    pattern2id.Add(newp, newid);
                }
            }
        }

        foreach (var p2i in pattern2id)
        {
            output["p1"].Set(p2i.Key);
            output["p2"].Set(p2);
            output["ids"].Set(p2i.Value + "->" + p2Id);
            yield return output;
        }
    }
}
