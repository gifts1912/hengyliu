using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;


/// <summary>
/// 
/// </summary>
public class ScrapeProcessor : Processor
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
        return new Schema("Query, Id");
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
            output[0].Set(row[0].String);
            string str = row[1].String;
            if (string.IsNullOrEmpty(str))
                continue;

            string[] ids = str.Split(new string[] { "|||" }, 
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var id in ids)
            {
                if (id.StartsWith("http://knowledge.microsoft.com/"))
                {
                    output[1].Set(id);
                }
                else
                {
                    output[1].Set("http://knowledge.microsoft.com/" + id);
                }
                yield return output;
            }
        }
    }
}

