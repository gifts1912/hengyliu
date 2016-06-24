using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class Misc
{
    public static bool IsDateTime(string str)
    {
        DateTime dt;
        if (DateTime.TryParse(str, out dt))
            return true;
        return false;
    }
}

/// <summary>
/// 
/// </summary>
public class FilterSimQByTopHostProcessor : Processor
{
    HashSet<string> topHosts = new HashSet<string>();

    void LoadTopHosts(string file)
    {
        using (StreamReader sr = new StreamReader(file))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] items = line.Split('\t');
                if (items.Length < 1)
                    continue;
                string host = items[0];
                int idSlash = host.IndexOf('/');
                if (idSlash >= 0)
                {
                    host = host.Substring(0, idSlash);
                }

                if (!string.IsNullOrEmpty(host))
                {
                    topHosts.Add(host);
                }
            }
        }
    }

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
        LoadTopHosts(args[0]);
        foreach (Row row in input.Rows)
        {
            string host = row["host"].String;
            if (string.IsNullOrEmpty(host))
                continue;

            if (topHosts.Contains(host.ToLower()))
            {
                row.CopyTo(output);
                yield return output;
            }
        }
    }
}


