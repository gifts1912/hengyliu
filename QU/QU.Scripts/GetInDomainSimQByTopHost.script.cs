using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;


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
            string urls = row["urls"].String;
            if (string.IsNullOrEmpty(urls))
                continue;

            string[] items = urls.Split(new string[] {"##"}, StringSplitOptions.RemoveEmptyEntries);
            bool match = false;
            foreach (var item in items)
            {
                string host = Utility.Normalizer.GetUrlHost(Utility.Normalizer.NormalizeUrl(item));
                if (topHosts.Contains(host))
                    match = true;
            }

            if (match)
            {
                row.CopyTo(output);
                yield return output;
            }
        }
    }
}


