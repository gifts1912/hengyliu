using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class BingClicksStep1SessionDataExtractor : Extractor
{
    public override Schema Produces(string[] columns, string[] args)
    {
        return new Schema(columns);
    }

    public override IEnumerable<Row> Extract(StreamReader reader, Row output, string[] args)
    {
        string line;
        string[] strIndexes = args[0].Split(',');
        int[] indexes = new int[strIndexes.Length];
        for (int i = 0; i < strIndexes.Length; i++)
        {
            indexes[i] = int.Parse(strIndexes[i]);
        }

        while ((line = reader.ReadLine()) != null)
        {
            string[] tokens = line.Split('\t');
            for (int i = 0; i < indexes.Length; ++i)
            {
                output[i].Set(tokens[indexes[i]]);
            }
            yield return output;
        }
    }
}

