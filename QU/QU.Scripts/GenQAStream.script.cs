using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using Utility;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class ExtendDetailsProcessor : Processor
{
    static string[] LineSeparators = new string[] { "#R#", "#N#" };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return input.Clone();
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
            string details = row["detail"].String;
            if (string.IsNullOrEmpty(details))
                continue;

            string[] items = details.Split(LineSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length == 0)
                continue;

            string newDetail = string.Join(" ", items);
            newDetail = Normalizer.NormalizeQuery(newDetail);

            if (newDetail.Length > 256)
            {
                int idx = newDetail.LastIndexOf(' ', 256);
                newDetail = idx <= 0 ? newDetail : newDetail.Substring(0, idx);
            }

            row.CopyTo(output);
            output["detail"].Set(newDetail);
            yield return output;
        }
    }
}


/// <summary>
/// 
/// </summary>
public class FilteStopwordsProcessor : Processor
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
        return input.Clone();
    }

    static HashSet<string> stopwords = StopWordUtil.LoadFromFile("stopword.txt");

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
            string name = row[1].String;
            if (stopwords.Contains(name))
                continue;

            row.CopyTo(output);
            yield return output;
        }
    }
}



/// <summary>
/// 
/// </summary>
public class StreamGenProcessor : Processor
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
        return new Schema("url, query, score:int, rawScore:double, satoriId");
    }

    static string[] Separator = new string[] { "|||" };
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
            int len = row["canLen"].Integer;
            double score = row["score"].Long;
            double idf = row["idf"].Double;
            string query = row["question"].String;
            if (string.IsNullOrEmpty(query))
                continue;

            score *= Math.Log(1.0 + len, 2.0) * idf;

            string qaurl = Utility.Normalizer.NormalizeUrl(row["url"].String);
            string strRepUrls = row["repUrls"].String;
            string[] repUrls = strRepUrls.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

            if (repUrls.Length == 0)
                continue;

            output["query"].Set(query);
            output["score"].Set(Math.Max(1, Math.Min(255, (int)(score + 0.5))));
            output["rawScore"].Set(score);

            output["satoriId"].Set(string.Empty);
            output["url"].Set(qaurl);
            yield return output;

            foreach (var repUrl in repUrls)
            {
                output["url"].Set(Utility.Normalizer.NormalizeUrl(repUrl));
                output["satoriId"].Set(row["satoriId"].String);
                yield return output;
            }
        }
    }
}

