using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using ScopeRuntime;

/// <summary>
/// extract TM file, if not " ||| " delimited, falls back to tab-delimited.
/// </summary>
public class TMExtractor : Extractor
{
    public override Schema Produces(string[] requestedColumns, string[] args)
    {
        return new Schema(requestedColumns);
    }

    public override IEnumerable<Row> Extract(StreamReader streamReader, Row outputRow, string[] args)
    {
        string line;
        while ((line = streamReader.ReadLine()) != null)
        {
            int offset1 = line.IndexOf(" ||| ", StringComparison.Ordinal);
            IEnumerable<string> fields;
            if (offset1 < 0)
            {
                // Tab delimited
                fields = line.Split('\t');
            }
            else
            {
                int offset2 = offset1 + " ||| ".Length;
                int offset3 = line.IndexOf(" ||| ", offset2, StringComparison.Ordinal);
                int offset4 = offset3 + " ||| ".Length;
                var src = line.Substring(0, offset1);
                var tgt = line.Substring(offset2, offset3 - offset2);
                var rest = line.Substring(offset4).Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                fields = new string[] { src, tgt }.Concat(rest).ToArray();
            }

            outputRow[0].Set(fields.ElementAt(0));
            outputRow[1].Set(fields.ElementAt(1));

            int i = 2;
            foreach (var field in fields.Skip(2))
                outputRow[i++].Set(double.Parse(field));

            while (i < outputRow.Columns.Length)
                outputRow[i++].Set(0.0);

            yield return outputRow;
        }
    }
}
