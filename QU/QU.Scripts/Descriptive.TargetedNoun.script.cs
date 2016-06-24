using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;


/// <summary>
/// 
/// </summary>
public class TermOfInterestExtractor : Processor
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
        return new Schema("term");
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
        Regex regex = new Regex(args[0], RegexOptions.IgnoreCase);
        foreach (Row row in input.Rows)
        {
            string q = row[0].String;
            var mc = regex.Matches(q);

            if (mc == null || mc.Count == 0)
                continue;

            foreach (Match m in mc)
            {
                if (!m.Success)
                    continue;

                string val = m.Groups["term"].Value;
                if (string.IsNullOrWhiteSpace(val))
                    continue;

                output["term"].Set(val);
                yield return output;
            }
        }
    }
}


