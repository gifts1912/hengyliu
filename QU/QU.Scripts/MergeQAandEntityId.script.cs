using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Linq;

/// <summary>
///  
/// </summary>
public class MetawordMerger : Reducer
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
        string name1 = "24_MSQAFirstMovieId2015Jun", name2 = "24_MSQASecondMovieId2015Jun", name3 = "24_MSQAThirdMovieId2015Jun", 
            id1 = "", id2 = "", id3 = "";
        string url = "";
        int count = 0;
        HashSet<string> injHdrIds = new HashSet<string>();
        foreach (Row row in input.Rows)
        {
            if (++count == 1)
            {
                url = row["url"].String;
                string info = row["info"].String;
                if (string.IsNullOrEmpty(info))
                {
                    row.CopyTo(output);
                    yield return output;
                }
                else
                {
                    injHdrIds = new HashSet<string>(info.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            else
            {
                id1 = row["id1"].String;
                id2 = row["id2"].String;
                id3 = row["id3"].String;
            }

            row.CopyTo(output);
        }

        injHdrIds.Remove(id1);
        injHdrIds.Remove(id2);
        injHdrIds.Remove(id3);

        if (injHdrIds.Count == 0)
        {
            yield break;
        }

        string[] ids = injHdrIds.ToArray();
        int currIdx = 0;
        if (string.IsNullOrEmpty(id1) && ids.Length > currIdx)
        {
            output["name1"].Set(name1);
            output["id1"].Set(ids[currIdx++]);
        }

        if (string.IsNullOrEmpty(id2) && ids.Length > currIdx)
        {
            output["name2"].Set(name2);
            output["id2"].Set(ids[currIdx++]);
        }

        if (string.IsNullOrEmpty(id3) && ids.Length > currIdx)
        {
            output["name3"].Set(name3);
            output["id3"].Set(ids[currIdx++]);
        }

        yield return output;
    }
}


