using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

/// <summary>
///  
/// </summary>
public class TriggeringAnalysisReducer : Reducer
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
        bool triggered = false;
        foreach (Row row in input.Rows)
        {
            bool impressionTriggered = row["Triggered"].Boolean;
            if (!triggered && impressionTriggered)
            {
                triggered = true;
            }

            if (triggered)
            {
                row.CopyTo(output);
                yield return output;
            }
        }
    }
}


