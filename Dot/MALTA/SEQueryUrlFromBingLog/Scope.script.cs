using Microsoft.SCOPE.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class AggregateQuery : Reducer
{
    public override Schema Produces(string [] columns, string [] args, Schema input)
    {
        return new Schema("Url:string, QuerySet:string");
    }

    public override IEnumerable<Row> Reduce(RowSet input, Row output, string[] args)
    {
        HashSet<string> querySet = new HashSet<string>();
        bool first = true;
        string URL = "";
        foreach(Row row in input.Rows)
        {
            if(first)
            {
                URL = row["URL"].Value.ToString();
                first = false;
            }
            string query = row["Query"].Value.ToString();
            querySet.Add(query);
        }
        if(!first)
        {
            StringBuilder sb = new StringBuilder();
            bool firstQuery = true;
            foreach(string queryEle in querySet)
            {
                if(firstQuery)
                {
                    firstQuery = false;
                }
                else
                {
                    sb.Append(";");
                }
                sb.Append(queryEle);
            }
            string queryUni = sb.ToString();
            output[0].Set(URL);
            output[1].Set(queryUni);
            yield return output;
        }
        
    }
}
