using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using Utility.Entity;
using System.Linq;


/// <summary>
/// 
/// </summary>
public class PropertyFilter : Processor
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
        string resourcePath = args[0];

        if (!IsLoaded)
        {
            LoadFilterSet(resourcePath);
        }

        foreach (Row row in input.Rows)
        {
            string key = row["_P"].String;

            if (filterSet.Contains(key))
            {
                row.CopyTo(output);
                yield return output;
            }
        }
    }

    protected static HashSet<string> filterSet;
    protected static bool IsLoaded = false;

    protected static void LoadFilterSet(string resourcePath)
    {
        if (IsLoaded)
        {
            return;
        }

        string resourceFile = Utility.ScopeUtils.GetResourceFileName(resourcePath);
        filterSet = new HashSet<string>();
        using (StreamReader sr = new StreamReader(resourceFile))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] items = line.Split('\t');
                if (items.Length < 8)
                    continue;

                for (int i = 7; i >= 5; i--)
                {
                    if (string.IsNullOrEmpty(items[i]))
                        continue;
                    else
                    {
                        filterSet.Add(items[i]);
                        break;
                    }
                }
            }
        }

        IsLoaded = true;
    }
}



/// <summary>
/// 
/// </summary>
public class Layer1PropertyFilter : Processor
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
        Schema output = new Schema();
        output.Add(new ColumnInfo("_S", ColumnDataType.String));
        output.Add(new ColumnInfo("Curr_S", ColumnDataType.String));
        output.Add(new ColumnInfo("Property", ColumnDataType.String));
        output.Add(new ColumnInfo("Pattern", ColumnDataType.String));
        output.Add(new ColumnInfo("EntityType", ColumnDataType.String));
        output.Add(new ColumnInfo("RelationProperty", ColumnDataType.String));
        output.Add(new ColumnInfo("RelationConstraint", ColumnDataType.String));
        output.Add(new ColumnInfo("Layer1", ColumnDataType.String));
        output.Add(new ColumnInfo("Layer2", ColumnDataType.String));
        output.Add(new ColumnInfo("Layer3", ColumnDataType.String));
        return output;
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
        string resourcePath = args[0];

        if (!IsLoaded)
        {
            LoadPropertyPatternInfo(resourcePath);
        }

        foreach (Row row in input.Rows)
        {
            string _p = row["_P"].String;
            string _o = row["_O"].String;
            if (!_o.StartsWith("http://knowledge.microsoft.com"))
                continue;

            List<PropertyPatternInfo> info;
            if (infos.TryGetValue(_p, out info))
            {
                foreach (var i in info)
                {
                    output["_S"].Set(row["_S"].String);
                    output["Curr_S"].Set(_o);
                    output["Property"].Set(i.Property);
                    output["Pattern"].Set(i.Pattern);
                    output["EntityType"].Set(i.EntityType);
                    output["RelationProperty"].Set(i.RelationProperty);
                    output["RelationConstraint"].Set(i.RelationConstraint);
                    output["Layer1"].Set(i.Layer1);
                    output["Layer2"].Set(i.Layer2);
                    output["Layer3"].Set(i.Layer3);
                    yield return output;
                }
            }
        }
    }

    public class PropertyPatternInfo
    {
        public string Property;
        public string Pattern;
        public string EntityType;
        public string RelationProperty;
        public string RelationConstraint;
        public string Layer1;
        public string Layer2;
        public string Layer3;
    }

    protected static Dictionary<string, List<PropertyPatternInfo>> infos;
    protected static bool IsLoaded = false;

    protected static void LoadPropertyPatternInfo(string resourcePath)
    {
        if (IsLoaded)
        {
            return;
        }

        string resourceFile = Utility.ScopeUtils.GetResourceFileName(resourcePath);
        infos = new Dictionary<string, List<PropertyPatternInfo>>();
        using (StreamReader sr = new StreamReader(resourceFile))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] items = line.Split('\t');
                if (items.Length < 8)
                    continue;

                string layer1 = items[5];
                if (string.IsNullOrEmpty(layer1))
                    continue;

                if (!infos.ContainsKey(layer1))
                {
                    infos.Add(layer1, new List<PropertyPatternInfo>());
                }

                infos[layer1].Add(new PropertyPatternInfo
                {
                    Property = items[0],
                    Pattern = items[1],
                    EntityType = items[2],
                    RelationProperty = items[3],
                    RelationConstraint = items[4],
                    Layer1 = layer1,
                    Layer2 = items[6],
                    Layer3 = items[7]
                });
            }
        }

        IsLoaded = true;
    }
}

