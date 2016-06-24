using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Others.ParseFormatFile
{
    class ParseJsonFile
    {


        public static void Run(string[] args)
        {
            if (args.Length != 2)
            {
                args = new string[2];
                args[0] = @"D:\demo\jsonDemo.json";
                args[1] = @"D:\demo\watch.tsv";
            }
            string infile = args[0];
            string outfile = args[1];

            ParseFile(infile, outfile);
        }

        public static void ParseFile(string infile, string outfile)
        {
            StreamReader sr = new StreamReader(infile);
            StringBuilder sb = new StringBuilder();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sb.Append(line);
            }
            sr.Close();

            string seResult = WebAnserParsePbxml(sb.ToString(), @"SatoryId:results[*]\Kif;Con:results[*]\Containers[*]", "WebAnswer", "queryrequest", true);

        }

        public static Stream ToStream(string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string WebAnserParsePbxml(string jsonStream, string FieldName2PathMapping, string service = "WebAnswer", string scenario = "queryrequest", bool flag = true)
        {
            string seRes = null;
            // string FieldName2PathMapping = @"Result:Results[*]\ResultEntityList[*]\Id";
            string[] strArray1 = FieldName2PathMapping.Split(new char[1]
            {
                ';'
            }, StringSplitOptions.RemoveEmptyEntries);
            string[] strArray2 = new string[strArray1.Length];
            string[] fields = new string[strArray1.Length];
            for (int index = 0; index < strArray1.Length; ++index)
            {
                string[] strArray3 = strArray1[index].Split(':');
                strArray2[index] = strArray3[0];
                fields[index] = strArray3[1];
            }
            JObject json = JObject.Parse(jsonStream);
            string[] fieldValues = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                string field = fields[i];
                string[] hierachy = field.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> values = new List<string>();
                try
                {
                    GetFieldValue(hierachy, 0, json as JToken, ref values);
                    fieldValues[i] = string.Join("|||", values);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                    //  return null;
                }
            }
            List<string> conditionValueList = new List<string>();
            if (fields.Length != 2)
            {
                return null;
            }
            else
            {
                if (flag)
                {
                    string[] valueArr = fieldValues[0].Split(new string[] { "|||" }, StringSplitOptions.None);
                    string[] condArr = fieldValues[1].Split(new string[] { "|||" }, StringSplitOptions.None);
                    for (int i = 0; i < condArr.Length; i++)
                    {
                        if (condArr[i] == "film.film")
                        {
                            conditionValueList.Add(valueArr[i]);
                        }
                    }
                }
                else
                {
                    string[] condArr = fieldValues[1].Split(new string[] { "|||" }, StringSplitOptions.None);
                    if (condArr.Contains("film.film"))
                    {
                        conditionValueList.Add(fieldValues[0]);
                    }
                }

                seRes = string.Join("|||", conditionValueList.ToArray());
            }

            return seRes;
        }

        public static void GetFieldValue(string[] hierachy, int layer, JToken json, ref List<string> values)
        {
            if (layer == hierachy.Length)
            {
                values.Add(json.ToString());
                return;
            }
            //List<string> values = new List<string>();
            //string[] hierachy = field.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
            JToken currToken = json;
            if (currToken == null)
            {
                return;
            }

            string path = hierachy[layer];
            if (path.Contains('[') && path.Contains('*'))
            {
                JArray arr = currToken[path.Substring(0, path.IndexOf('['))] as JArray;
                if (arr == null)
                {
                    return;
                }
                if (layer == hierachy.Length - 1)
                {
                    List<string> arrRes = new List<string>();
                    for(int j = 0; j < arr.Count; j++)
                    {
                        JToken subToken = arr[j];
                        if (subToken == null)
                            continue;
                        arrRes.Add(subToken.ToString());
                    }
                    values.Add(string.Join(",", arrRes.ToArray()));
                    return;
                }
                for (int j = 0; j < arr.Count; j++)
                {
                    JToken subToken = arr[j];
                    
                    GetFieldValue(hierachy, layer + 1, subToken, ref values);
                }
            }
            else if (path.Contains('[') && !path.Contains('*'))
            {
                int idx = int.Parse(path.Substring(path.IndexOf('[') + 1, path.IndexOf(']') - path.IndexOf('[') - 1));
                JArray arr = currToken[path.Substring(0, path.IndexOf('['))] as JArray;
                if (arr == null || arr.Count <= idx)
                {
                    return;
                }
                currToken = arr[idx];
                GetFieldValue(hierachy, layer + 1, currToken, ref values);
            }
            else
            {
                currToken = currToken[path];
                GetFieldValue(hierachy, layer + 1, currToken, ref values);
            }
        }   

    }
}
