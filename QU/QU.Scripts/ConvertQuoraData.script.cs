using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

/// <summary>
/// 
/// </summary>
public class QuoraDataExtractor : Extractor
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
        
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            try
            {
                line = line.Replace(@"\""", @"#QUOTE#").Replace(@"""", "");
                string[] tokens = line.Split(',');
                for (int i = 0; i < tokens.Length; ++i)
                {
                    output[i].Set(tokens[i].Replace(@"#QUOTE#", @""""));
                }
            }
            catch { continue; }
            yield return output;
        }
    }
}


