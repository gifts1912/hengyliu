using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

/// <summary>
///  
/// </summary>
public class MetadataGenReducer : Reducer
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
        return new Schema("url, key1, val1, key2, val2, key3, val3");
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
        string[] keys = new string[] { "24_MSQAFirstMovieId2015Jun", "24_MSQASecondMovieId2015Jun", "24_MSQAThirdMovieId2015Jun" };
        string[] values = new string[3];
        string url = "";
        int count = 0;
        foreach (Row row in input.Rows)
        {
            if (++count == 1)
            {
                url = row["url"].String;
            }

            if (count <= 3)
            {
                string id = row["imdbId"].String;
                if (!string.IsNullOrEmpty(id))
                    values[count - 1] = id;
            }
        }

        output[0].Set(url);
        for (int i = 0; i < 3; i++)
        {
            if (!string.IsNullOrEmpty(values[i]))
            {
                output[2 * i + 1].Set(keys[i]);
                output[2 * i + 2].Set(values[i]);
            }
            else
            {
                break;
            }
        }

        yield return output;
    }
}


