using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using PBXMLUtil;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace pbxmlParse
{
    class Program
    {
        private static void GetFieldValue(string[] hierachy, int layer, JToken json, ref List<string> values)
        {
            if (layer == hierachy.Length)
            {
                values.Add(json.ToString());
                return;
            }
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
        static void Main(string[] args)
        {
            string pbxmlFile = @"D:\demo\pbxmlFile.xml";
            string outFile = @"D:\demo\watch.tsv";
            string FieldName2PathMapping = @"NormQuery:NormalizedQuery;ParserOutput:ParserOutputV3\Rules[0]\Text;Result:Results[*]\ResultEntityList[*]\Id";
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

            string service = "Dolphin", scenario = "Fetch";
            XPathDocument pbxml = new XPathDocument(pbxmlFile);
            XPathExpression answerExpression = Util.PBXMLUtil.GetKif(service, scenario);
            JObject json = Util.PBXMLUtil.GetKifJson(pbxml, answerExpression);

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

            StreamWriter sw = new StreamWriter(outFile);
            foreach(string value in fieldValues)
            {
                sw.WriteLine(value);
            }
            sw.Close();

        }

        public static void Run()
        {          
                string query = message.Text;
                string URL2 = "http://www.bing.com/search?q=" + string.Join("+", query.Split(' ')) + "&filter=dolphin%7Cfetch&format=pbxml&p1=%5bAnswerReducer%20Mode=%22Disabled%22%5d";
                PageContentCrawlerByWebrequest crawler2 = new PageContentCrawlerByWebrequest();
                string PbxmlString = crawler2.crawl(URL2);

                string seResult = ParsePbxml(ToStream(PbxmlString));
                if (String.IsNullOrEmpty(seResult))
                {
                    seResult = "Out of our service!";
                }
                Message replyMessage = message.CreateReplyMessage($"{seResult}");
                return replyMessage;
            }
            else
            {
                return HandleSystemMessage(message);
            }
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

        public static string ParsePbxml(Stream pbxmlStream)
        {
            string seRes = null;
            string FieldName2PathMapping = @"Result:Results[*]\ResultEntityList[*]\Id";
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

            string service = "Dolphin", scenario = "Fetch";
            XPathDocument pbxml = new XPathDocument(pbxmlStream);
            XPathExpression answerExpression = Util.PBXMLUtil.GetKif(service, scenario);
            JObject json = Util.PBXMLUtil.GetKifJson(pbxml, answerExpression);

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
            seRes = string.Join("\t", fieldValues);
            return seRes;
        }

    }
}
