using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class Misc
{
    public static string LastLayerOfUrl(string url)
    {
        int lastSlash = url.LastIndexOf('/');
        if (lastSlash <= 0)
            return url;
        if (url[lastSlash - 1] == '/')
            return url;

        return url.Substring(0, lastSlash);
    }
}

/// <summary>
///  
/// </summary>
public class SymptomQueryReducer : Reducer
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
        output.Add(new ColumnInfo("isSymptom", ColumnDataType.Boolean));
        return output;
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
        string url = string.Empty;
        List<Tuple<string, long, long>> queries = new List<Tuple<string, long, long>>();
        bool isSymptomRelated = false;
        foreach (Row row in input.Rows)
        {
            if (++count == 1)
            {
                url = row["url"].String;
                output["url"].Set(url);
            }

            string q = row["query"].String;
            long impressions = row["impressionCnt"].Long;
            long clicks = row["clickCnt"].Long;
            queries.Add(new Tuple<string, long, long>(q, impressions, clicks));

            if (isSymptomRelated)
                continue;

            if (url.Contains("symptom") || url.Contains("rightdiagnosis.com/sym/"))
            {
                isSymptomRelated = true;
            }

            var terms = new HashSet<string>(q.Split(' '));
            if (terms.Contains("symptom") || terms.Contains("symptoms"))
            {
                isSymptomRelated = true;
            }
        }

        foreach (var q in queries)
        {
            output["query"].Set(q.Item1);
            output["impressionCnt"].Set(q.Item2);
            output["clickCnt"].Set(q.Item3);
            output["isSymptom"].Set(isSymptomRelated);
            yield return output;
        }
    }
}

