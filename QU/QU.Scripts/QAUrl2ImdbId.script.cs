using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;


/// <summary>
/// 
/// </summary>
public class QAMappingProcessor : Processor
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
        return new Schema("url, imdbId:long, score:int");
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
            output[0].Set(row[0].String);
            output[2].Set(row[2].Integer);
            string[] repUrls = row[1].String.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string u in repUrls)
            {
                long id = GetImdbId(u);
                if (id < 0)
                    continue;

                output[1].Set(id);
                yield return output;
            }
        }
    }

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
}


