using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class SplitNGramProcessor : Processor
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
        var output = input.Clone();
        output.Add(new ColumnInfo("ngram", ColumnDataType.String));
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
        int min = int.Parse(args[0]), max = int.Parse(args[1]);
        foreach (Row row in input.Rows)
        {
            string q = row[0].String;

            if (string.IsNullOrEmpty(q))
            {
                continue;
            }

            var insts = Split(q, min, max);
            if (q == null)
                continue;

            foreach (var i in insts)
            {
                output[0].Set(q);
                output[1].Set(i.Item1);
                yield return output;
            }
        }
    }

    static char[] space = new char[] { ' ' };
    private static IEnumerable<Tuple<string, int>> Split(string query, int minngram, int maxngram)
    {
        string[] items = query.Split(space);
        for (int i = 0; i < items.Length; i++)
        {
            for (int j = minngram; j <= Math.Min(maxngram, items.Length - i); j++)
            {
                yield return new Tuple<string, int>(string.Join(" ", items.Skip(i).Take(j)), j);
            }
        }
    }
}


