using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Linq;

public class SplitInstanceProcessor : Processor
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
        string[] seperators = new string[] { args[0] };
        foreach (Row row in input.Rows)
        {
            string url = row[0].String;
            string inst = row[1].String;

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(inst))
            {
                continue;
            }

            string[] insts = inst.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var i in insts)
            {
                output[0].Set(url);
                string ni = Utility.Normalizer.NormalizeQuery(i);
                output[1].Set(ni);
                yield return output;
            }
        }
    }
}


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
        int min = int.Parse(args[0]), max = int.Parse(args[1]);
        foreach (Row row in input.Rows)
        {
            string url = row[0].String;
            string inst = row[1].String;

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(inst))
            {
                continue;
            }

            var insts = Split(inst, min, max);
            if (inst == null)
                continue;

            foreach (var i in insts)
            {
                output[0].Set(url);
                output[1].Set(i);
                yield return output;
            }
        }
    }

    static char[] space = new char[] { ' ' };
    private static IEnumerable<string> Split(string query, int minngram, int maxngram)
    {
        string[] items = query.Split(space);
        for (int i = 0; i < items.Length; i++)
        {
            for (int j = minngram; j <= Math.Min(maxngram, items.Length - i); j++)
            {
                yield return string.Join(" ", items.Skip(i).Take(j));
            }
        }
    }
}

