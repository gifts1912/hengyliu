using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;


/// <summary>
/// 
/// </summary>
public class InjHdrEntityIdExtractor : Processor
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
        output.Add(new ColumnInfo("Role", ColumnDataType.String));
        return output;
    }

    static Regex regex = new Regex("\"Id\":\"(?<Id>.*?)\",\"IsSatoriId\":true,.Roles.+?Name.:\"(?<Role>.*?)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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
            string hdr = row[1].String;
            var mc = regex.Matches(hdr);

            if (null == mc || mc.Count == 0)
                continue;

            foreach (Match m in mc)
            {
                string role = m.Groups["Role"].Value;
                if (role.Equals("primary", StringComparison.CurrentCultureIgnoreCase))
                {
                    output[0].Set(row[0].String);
                    output[1].Set(m.Groups["Id"].Value);
                    output[2].Set(m.Groups["Role"].Value);
                    yield return output;
                }
            }
        }
    }
}


