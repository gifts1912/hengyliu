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
}

/// <summary>
///  
/// </summary>
public class Top3MovieReducer : Reducer
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override IEnumerable<Row> Reduce(RowSet input, Row output, string[] args)
    {

        string currName = "";
        int cnt = 0;
        foreach (Row row in input.Rows)
        {
            if (cnt >= 3)
                break;

            string name = row["name"].String;
            if (name == currName)
            {
                continue;
            }

            string id = "";
            string urls = row["repUrls"].String;
            foreach (var u in urls.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (u.Contains("imdb.com"))
                {
                    id = ImdbUtil.GetImdbId(u).ToString();
                    break;
                }
            }

            if (string.IsNullOrEmpty(id))
                continue;

            row.CopyTo(output);
            output["repUrls"].Set(id);
            currName = name;
            cnt++;
            yield return output;
        }
    }
}


