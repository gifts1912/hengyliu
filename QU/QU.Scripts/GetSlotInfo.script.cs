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
        Schema output = new Schema();
        output.Add(new ColumnInfo("leftSlotQ", ColumnDataType.String));
        output.Add(new ColumnInfo("rightSlotQ", ColumnDataType.String));
        output.Add(new ColumnInfo("sim", ColumnDataType.String));
        output.Add(new ColumnInfo("leftQ", ColumnDataType.String));
        output.Add(new ColumnInfo("rightQ", ColumnDataType.String));
        output.Add(new ColumnInfo("leftQSlots", ColumnDataType.String));
        output.Add(new ColumnInfo("rightQSlots", ColumnDataType.String));
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
            output["sim"].Set(row["sim"].String);
            string leftQ = row["leftQ"].String;
            string rightQ = row["rightQ"].String;
            output["leftQ"].Set(leftQ);
            output["rightQ"].Set(rightQ);

            string leftSlots = row["leftQSlots"].String;
            string rightSlots = row["rightQSlots"].String;
            output["leftQSlots"].Set(leftSlots);
            output["rightQSlots"].Set(rightSlots);

            string leftSlotQ = ReformulationPatternMatch.ReplaceSlot(leftQ, leftSlots, ignoreIntent);
            string rightSlotQ = ReformulationPatternMatch.ReplaceSlot(rightQ, rightSlots, ignoreIntent);
            output["leftSlotQ"].Set(leftSlotQ);
            output["rightSlotQ"].Set(rightSlotQ);
            yield return output;
        }
    }

}



