using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class OFEDataExtractor : Extractor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args)
    {
        return new Schema(columns);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override IEnumerable<Row> Extract(StreamReader reader, Row output, string[] args)
    {
        string headerFile = Utility.ScopeUtils.GetResourceFileName(args[0]);

        Dictionary<string, int> dictMetaFiled2Pos = new Dictionary<string, int>();
        Dictionary<string, int> dictValueFiled2Pos = new Dictionary<string, int>();
        string[] fields = File.ReadAllLines(headerFile)[0].Split('\t');
        var allMetaCols = new HashSet<string>(from c in output.Schema.Columns
                                          where c.Type == ColumnDataType.String
                                          select "m:" + c.Name);
        var allValueCols = new HashSet<string>(from c in output.Schema.Columns
                                              where c.Type == ColumnDataType.Integer
                                              select c.Name);
        for (int i = 0; i < fields.Length; i++)
        {
            if (allMetaCols.Contains(fields[i]))
            {
                dictMetaFiled2Pos.Add(fields[i], i);
            }
            else if (allValueCols.Contains(fields[i]))
            {
                dictValueFiled2Pos.Add(fields[i], i);
            }
        }

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            try
            {
                string[] tokens = line.Split('\t');
                foreach (var p in dictMetaFiled2Pos)
                {
                    output[p.Key.Substring(2)].Set(tokens[p.Value]);
                }

                foreach (var p in dictValueFiled2Pos)
                {
                    output[p.Key].Set(int.Parse(tokens[p.Value]));
                }
            }
            catch
            {
                continue;
            }

            yield return output;
        }
    }
}


