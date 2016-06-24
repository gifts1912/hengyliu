using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class OutputFirstReducer : Reducer
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
        return new Schema("NormQuery, AlteredQuery, RequestTime");
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
        foreach (Row row in input.Rows)
        {
            if (++count != 1)
                break;

            output["NormQuery"].Set(row["NormQuery"].String);
            output["AlteredQuery"].Set(row["AlteredQuery"].String);
            output["RequestTime"].Set(row["RequestTime"].String);
            yield return output;
        }
    }
}
