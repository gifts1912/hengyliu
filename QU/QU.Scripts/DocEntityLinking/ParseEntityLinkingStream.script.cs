using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;


/// <summary>
/// 
/// </summary>
public class SegmentFilter : Processor
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
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        string[] validTypes = args[0].Split(';');
        foreach (Row row in input.Rows)
        {
            string types = row["Types"].String;
            if (string.IsNullOrEmpty(types))
                continue;

            HashSet<string> set = new HashSet<string>(types.Split(new string[] { "#TAB#", "\t" }, StringSplitOptions.RemoveEmptyEntries));
            foreach (var t in validTypes)
            {
                if (set.Contains(t))
                {
                    row.CopyTo(output);
                    yield return output;
                    break;
                }
            }
        }
    }
}


