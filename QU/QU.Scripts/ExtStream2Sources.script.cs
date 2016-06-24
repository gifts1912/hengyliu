using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;


/// <summary>
/// 
/// </summary>
public class ExtMovieUrlProcessor : Processor
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

    static string[] ImdbSuffix = new string[] 
    {
        "/plotsummary",
        "/reviews",
        "/releaseinfo",
        "/synopsis",
        "/trivia",
        "/quotes",
        "/taglines",
        "/keywords"
    };

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
            yield return output;

            string url = row["url"].String;

            if (url.StartsWith("http://imdb.com/title/"))
            {
                string temp = url.Substring("http://imdb.com/title/".Length);
                int slash = temp.IndexOf('/');
                if (slash < 0)
                {
                    foreach (var suffix in ImdbSuffix)
                    {
                        string extUrl = url + suffix;
                        output["url"].Set(extUrl);
                        yield return output;
                    }
                }
            }
        }
    }
}
