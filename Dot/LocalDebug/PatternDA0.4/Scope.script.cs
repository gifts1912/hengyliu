using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using FrontEndUtil;

public class HelperFunction
{
    public static string GetUrlHost(string url)
    {
        return CURLUtilities.GetHostNameFromUrl(url);
    }
}

public class TopNReducer : Reducer
{
    public override Schema Produces(string[] requestedColumns, string[] args, Schema input)
    {
        return input.Clone();
    }

    public override IEnumerable<Row> Reduce(RowSet input, Row outputRow, string[] args)
    {
        int topn = int.Parse(args[0]);
        int num = 0;

        foreach (Row row in input.Rows)
        {
            for (int col = 0; col < input.Schema.Count; col++)
            {
                row[col].CopyTo(outputRow[col]);
            }

            yield return outputRow;

            num++;
            if (num == topn)
            {
                break;
            }
        }
    }
}
