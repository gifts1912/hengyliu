using Microsoft.SCOPE.Types;
using Microsoft.SCOPE.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
public class SplitProcessor : Processor
{
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return input.Clone();
    }

    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        foreach (Row row in input.Rows)
        {
            output[0].Set(row[0].String);
            foreach (var s in row[1].String.Split(','))
            {
                output[1].Set(s.Split(':')[0]);
                yield return output;
            }
        }
    }
}