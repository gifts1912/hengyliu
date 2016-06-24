using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using Utility.Entity;

/// <summary>
///  
/// </summary>
public class PatternAggregator : Reducer
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
        int count = 0;
        string p = "";
        foreach (Row row in input.Rows)
        {
            if (++count == 1)
            {
                row.CopyTo(output);
            }

            p += row["Pattern"].String + "||";
        }

        output["Pattern"].Set(p);
        yield return output;
    }
}



/// <summary>
/// 
/// </summary>
public class PropertyAnalyzer : Processor
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
        Schema output = input.Clone();
        output.Add(new ColumnInfo("EntityType", ColumnDataType.String));
        output.Add(new ColumnInfo("RelationProperty", ColumnDataType.String));
        output.Add(new ColumnInfo("RelationConstraint", ColumnDataType.String));
        output.Add(new ColumnInfo("Layer1", ColumnDataType.String));
        output.Add(new ColumnInfo("Layer2", ColumnDataType.String));
        output.Add(new ColumnInfo("Layer3", ColumnDataType.String));
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

        foreach (Row row in input.Rows)
        {
            string property = row["Property"].String;
            string pattern = row["Pattern"].String;

            Property p = PropertyParser.Parse(property);
            if (p == null)
                continue;

            output["Property"].Set(property);
            output["Pattern"].Set(pattern);
            output["EntityType"].Set(p.EntityType);
            output["RelationProperty"].Set(p.TargetProperty);
            output["RelationConstraint"].Set(string.Join(":", from r in p.Relationships select r.Value));
            string[] items = p.TargetProperty.Split(':');

            if (items.Length == 0 || items.Length > 3)
                continue;

            output["Layer1"].Set("http://knowledge.microsoft.com/mso/" + items[0]);
            if (items.Length >= 2)
            {
                output["Layer2"].Set("http://knowledge.microsoft.com/mso/" + items[1]);
            }
            else
            {
                output["Layer2"].Set("");
            }

            if (items.Length >= 3)
            {
                output["Layer3"].Set("http://knowledge.microsoft.com/mso/" + items[2]);
            }
            else
            {
                output["Layer3"].Set("");
            }

            yield return output;
        }
    }
}



