﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;

/// <summary>
///  
/// </summary>
public class OutputFirstReducer : Reducer
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
        return new Schema("NormQuery, AlteredQuery, RequestTime");
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
        foreach (Row row in input.Rows)
        {
            if (++count != 1)
                break;

            output["NormQuery"].Set(row["NormQuery"].String);
            output["AlteredQuery"].Set(row["AlteredQuery"].String);
            output["RequestTime"].Set(row["RequestTime"].String);
            yield return output;
        }
    }
}

public class Normalizer
{
    public static string NormalizeQuery(string str_in)
    {
        string fcsNormalized = FrontEndUtil.CQueryParser.GetFcsNormalizedQueryUTF8(str_in);

        string nq1 = Regex.Replace(fcsNormalized, "[\"]", " ");
        nq1 = nq1.Replace(";", " ");
        nq1 = nq1.ToLower();
        nq1 = Regex.Replace(nq1, "[ ]+", " ");
        nq1 = nq1.Trim();
        return nq1;
    }
}


public class FilterHelper
{
    public static string GetScope(string url)
    {
        Match match = Regex.Match(url, "&scope=([^&]*)", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        return "";
    }

    public static bool IsQueryBad(string query, uint maxBytes)
    {
        //maxBytes = 246
        if (String.IsNullOrEmpty(query) == true)
        { // Empty Phrase
            return true;
        }
        if (query.EndsWith(";") == true)
        { // Trailing Semicolon
            return true;
        }
        if (query.Contains(":"))
        { // internal queries
            return true;
        }
        if (query.IndexOf("word:(") >= 0)
        { // word: operator
            return true;
        }
        string[] terms = query.Split(' '); // Long Terms
        uint length;
        foreach (string term in terms)
        {
            length = (uint)(System.Text.Encoding.UTF8.GetByteCount(term));
            if (length > maxBytes)
                return true;
        }
        if (query.Contains("\0"))
        { // null bytes : 22 Aug 2010
            return true;
        }

        return false;
    }
}
