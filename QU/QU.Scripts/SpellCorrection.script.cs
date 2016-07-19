﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using QueryAlterationExtractor;


/// <summary>
/// 
/// </summary>
public class SpellCorrectionProcessor : Processor
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
        
        foreach (Row row in input.Rows)
        {
            row.CopyTo(output);
            yield return output;
        }
    }

    private string RewriteQuery(string query, string alteredQ, string refQ)
    {

    }
}
