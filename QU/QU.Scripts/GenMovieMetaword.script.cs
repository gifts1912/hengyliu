using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

class ImdbUtil
{
    public static long GetImdbId(string url)
    {
        long id = -1;
        string part = string.Empty;
        if (url.StartsWith("http://imdb.com/title/"))
        {
            string temp = url.Substring("http://imdb.com/title/tt".Length);
            int slash = temp.IndexOf('/');
            if (slash < 0)
                part = temp;
            else
                part = temp.Substring(0, slash);
        }

        if (!long.TryParse(part, out id))
            return -1;

        return id;
    }

    public static string CanonicalFilmUrl(string url)
    {
        if (url.StartsWith("http://imdb.com/title/"))
        {
            string temp = url.Substring("http://imdb.com/title/".Length);
            int slash = temp.IndexOf('/');
            if (slash < 0)
                return url;
            else
                return url.Substring(0, "http://imdb.com/title/".Length + slash);
        }

        return url;
    }
}


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

            output[0].Set(row[0].String);
            string url = row[1].String;

            if (url.StartsWith("http://imdb.com/title/"))
            {
                string temp = url.Substring("http://imdb.com/title/".Length);
                int slash = temp.IndexOf('/');
                if (slash < 0)
                {
                    foreach (var suffix in ImdbSuffix)
                    {
                        string extUrl = url + suffix;
                        output[1].Set(extUrl);
                        yield return output;
                    }
                }
            }
        }
    }
}


