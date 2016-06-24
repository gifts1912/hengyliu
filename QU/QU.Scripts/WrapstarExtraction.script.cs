using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using Microsoft.WrapStar.Shared;

// processor to further handle WrapStar output and return particular attributes
public class WrapStarDataProcessor : Processor
{
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        if ((columns.Length == 1) && (columns[0] == "*"))
        {
            string featureline = "Url";
            string[] outputcolumns = args[1].Split(",;".ToCharArray());
            foreach (string column in outputcolumns)
            {
                featureline += "\t" + column;
            }
            return new Schema(featureline.Split("\t".ToCharArray()));
        }
        return new Schema(columns);
    }

    private static int GetColumnIndex(Schema schema, string name)
    {
        return schema.Contains(name) ? schema.IndexOf(name) : -1;
    }
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        string valuepath = args[0];
        // weird schema.IndexOf throw exceptions on non-existing columns
        int idxExtraction = GetColumnIndex(input.Schema, "Model_Extraction");
        int idxModelJson = GetColumnIndex(input.Schema, "Model_Json");
        int idxJsonOutput = GetColumnIndex(input.Schema, "WrapStarJsonOutput");

        foreach (Row row in input.Rows)
        {
            Dictionary<string, string> attributes = null;
            if (idxExtraction >= 0)
            {
                // flatten extraction available
                string extraction = row[idxExtraction].String;
                attributes = WrapStarJsonParser.DeserializeJsonData(extraction);

            }
            else if (idxModelJson >= 0)
            {
                // need to flatten model json before doing lookup
                string modelJson = row[idxModelJson].String;
                attributes = new Dictionary<string, string>();
                JsonParser jp = new JsonParser(modelJson);
                WrapStarJsonParser.SerializeWrapStarDataV2(jp.Root, attributes, null, modelJson);

                // alternatively, can use these two commented lines
                // but it will require additional serialization and deserialization steps
                //string extraction = WrapStarJsonParser.FlattenWrapStarJsonV2(json);
                //attributes = WrapStarJsonParser.DeserializeJsonData(extraction);
            }
            else if (idxJsonOutput >= 0)
            {
                // now handle full JSON blob. refer to sample code of WrapStarExtractor below
                // invoke ParseJson to remove Kif schema and unroll multiple models
                // by default, the extraction output at level 1 (just model json without flattening)
                string wrapstarJson = row[idxJsonOutput].String;
                foreach (Dictionary<string, string> extraction in WrapStarJsonParser.ParseJsonV2(wrapstarJson))
                {
                    // now parse json and flatten it out
                    attributes = new Dictionary<string, string>();
                    string modelJson = extraction["Model_Json"];
                    JsonParser jp = new JsonParser(modelJson);
                    WrapStarJsonParser.SerializeWrapStarDataV2(jp.Root, attributes, null, modelJson);
                    break;
                }
            }
            else
            {
                throw new Exception("No WrapStar data available");
            }

            // get attribute based on full path
            // complete path available per ontology at file://wrapstar/WrapStarShare/Ontology 
            // selected path can be generated in WrapStar client for selected attribute

            // if want more flexibility when handling list, full JSON tree is available in JsonParser.
            // It simply removed the Kif schema for easy access.
            // sample code for JSON parser is shared with sample scripts as well at
            // file://WrapStar/WrapStarShare/Extractor
            //
            if (attributes != null)
            {

                output[0].Set(row[0].String);
                string[] values = valuepath.Split(",;".ToCharArray());
                int idx = 1;
                foreach (string value in values)
                {
                    List<string> paths = new List<string>();
                    // deal with *
                    if (value.Contains("[*]"))
                    {
                        if (value.Split(new string[] { "[*]" }, StringSplitOptions.None).Length > 2)
                        {
                            paths.Add(value.Replace("[*]", "[0]"));
                        }
                        else
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                paths.Add(value.Replace("[*]", string.Format("[{0}]", i)));
                            }
                        }
                    }
                    else
                    {
                        paths.Add(value);
                    }

                    StringBuilder sb = new StringBuilder();
                    foreach (var path in paths)
                    {
                        string data = "";
                        attributes.TryGetValue(path, out data);
                        if (string.IsNullOrEmpty(data))
                            continue;

                        data = JsonParser.UnescapeString(data);  // need to unescape quote in raw JSON string
                        sb.Append(data);
                        sb.Append("#TTT#");
                    }

                    if (sb.Length > "#TTT#".Length)
                    {
                        output[idx++].Set(sb.ToString(0, sb.Length - "#TTT#".Length));
                    }
                    else
                    {
                        output[idx++].Set("");
                    }
                }
                yield return output;
            }
        }
    }
}

// extract wrapstar data from cosmos stream
public class WrapStarExtractor : Extractor
{
    public override Schema Produces(string[] columns, string[] args)
    {
        Schema defaultSchema = new Schema("DocumentURL, Timestamp, HttpReturnCode, Version, Error, ModelCount, Model_ID, Model_Version, Model_Ontology, Model_Latest, Model_Timestamp, Model_Json, Model_Json_Length, Model_TemplateIndex");

        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i] == "WrapStarJsonOutput")
            {
                defaultSchema.Add(new ColumnInfo("WrapStarJsonOutput", ColumnDataType.String));
                return defaultSchema;
            }
            if (columns[i] == "Model_Extraction")
            {
                defaultSchema.Add(new ColumnInfo("Model_Extraction", ColumnDataType.String));
                return defaultSchema;
            }
        }
        return defaultSchema;
    }

    public override IEnumerable<Row> Extract(StreamReader reader, Row output, string[] args)
    {
        bool showError = false;
        if (args != null)
        {
            foreach (string arg in args)
            {
                if (arg == "showError")
                    showError = true;
            }
        }

        int outputCount = output.Count;
        int level = 1;
        if (output.Schema.Contains("WrapStarJsonOutput"))
            level = 0;
        else if (output.Schema.Contains("Model_Extraction"))
            level = 2;

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            foreach (Dictionary<string, string> wsOutput in WrapStarJsonParser.Parse(line, level))
            {
                // Skip error by default.
                if (!showError && !string.IsNullOrEmpty(wsOutput["Error"]))
                    continue;

                for (int i = 0; i < outputCount; i++)
                {
                    string value;
                    if (wsOutput.TryGetValue(output.Schema[i].Name, out value))
                        output[i].Set(value);
                    else
                        output[i].Set("");
                }
                yield return output;
            }
        }
    }
}
