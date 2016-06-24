using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using QU.Utility;
using System.Text.RegularExpressions;


/// <summary>
/// Replace slots found in query
/// </summary>
public class ReplaceSlotsProcessor : Processor
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
        Schema output = new Schema("query, slots, qWithSlots");
        return output;
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
        bool ignoreIntent = false;
        if (null != args && args.Length != 0)
        {
            foreach (string arg in args)
            {
                if (arg.Equals("-ignoreintent", StringComparison.OrdinalIgnoreCase))
                {
                    ignoreIntent = true;
                }
            }
        }

        foreach (Row row in input.Rows)
        {
            string query = row["query"].String;
            if (string.IsNullOrEmpty(query))
            {
                continue;
            }

            output["query"].Set(query);
            string slots = row["slots"].String;

            string qWithSlots = ReformulationPatternMatch.ReplaceSlot(query, slots, ignoreIntent);
            output["slots"].Set(slots);
            output["qWithSlots"].Set(qWithSlots);
            yield return output;
        }
    }

}



