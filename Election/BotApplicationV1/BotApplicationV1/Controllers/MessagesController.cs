using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;
using Utility.Crawler;
using System.Xml.XPath;
using PBXMLUtil;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;


namespace BotApplicationV1
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                // calculate something for us to return
                int length = (message.Text ?? string.Empty).Length;

                // return our reply to the user
                //  return message.CreateReplyMessage($"You sent {length} characters");
                Dictionary<string, string> serviceScenario = new Dictionary<string, string>();
                serviceScenario.Add("Dolphin", "Fetch");
                serviceScenario.Add("EntityWebAnswer", "EntityWebPerson");
                serviceScenario.Add("PreWebEntityAnswer", "QueryToEntityLookUp");
              //  serviceScenario.Add("WebAnswer", "queryrequest");
                int counter = message.GetBotPerUserInConversationData<int>("counter");
                string query = message.Text;
                string seResult = null;
                string structQueryLabel = null;
                foreach (KeyValuePair<string, string> pair in serviceScenario)
                {
                    string srviceName = pair.Key, scenaro = pair.Value;
                    string URL2 = "http://www.bing.com/search?q=" + string.Join("+", query.Split(' ')) + string.Format("&filter={0}%7C{1}&format=pbxml&p1=%5bAnswerReducer%20Mode=%22Disabled%22%5d", srviceName, scenaro);
                    PageContentCrawlerByWebrequest crawler2 = new PageContentCrawlerByWebrequest();
                    string PbxmlString = crawler2.crawl(URL2);

                    try
                    {
                        if (srviceName.Equals("Dolphin", StringComparison.OrdinalIgnoreCase))
                        {
                            seResult = ParsePbxml(ToStream(PbxmlString), @"Result:Results[*]\ResultEntityList[*]\Id", srviceName, scenaro);
                            structQueryLabel = ParsePbxml(ToStream(PbxmlString), @"StructQueryLabel:ParserOutputV3\Rules[*]\Result\EntityIndexQueryTriggerHints[0]", srviceName, scenaro);
                            if(!string.IsNullOrEmpty(seResult))
                                seResult = string.Format("{0}\t{1}", seResult, structQueryLabel);
                        }
                        else if (srviceName.Equals("EntityWebAnswer", StringComparison.OrdinalIgnoreCase))
                        {
                            // seResult = ParsePbxml(ToStream(PbxmlString), @"SatoriId:results[*]\Containers[*]\EntityContent\RelatedEntities[*]\RelatedEntity\SatoriId;Name:results[*]\Containers[*]\EntityContent\RelatedEntities[*]\RelatedEntity\Name;Relaption:results[*]\Containers[*]\EntityContent\RelatedEntities[*]\RelatedEntity\Relationship", srviceName, scenaro);
                            // seResult = ParsePbxml(ToStream(PbxmlString), @"SatoriId:results[0]\Containers[0]\EntityContent\RelatedEntities[*]\RelatedEntity\SatoriId;Relationship:results[0]\Containers[0]\EntityContent\RelatedEntities[*]\RelatedEntity\Relationship", srviceName, scenaro);
                            seResult = ParsePbxmlRelateEntity(ToStream(PbxmlString), @"SatoriId:results[*]\Containers[*]\EntityContent\RelatedEntities[*]\RelatedEntity\SatoriId;Relationship:results[*]\Containers[*]\EntityContent\RelatedEntities[*]\Relationship", srviceName, scenaro);
                        }
                        else if (srviceName.Equals("PreWebEntityAnswer", StringComparison.OrdinalIgnoreCase))
                        {
                            seResult = ParsePbxml(ToStream(PbxmlString), @"Result:results[*]\EntityFeatures[*]\Id", srviceName, scenaro);
                        }
                        else if (srviceName.Equals("WebAnswer", StringComparison.OrdinalIgnoreCase))
                        {
                            //                seResult = ParsePbxml(ToStream(PbxmlString), @"Types:FcsHostCollapseResults\Kif.Value[*]\SatoriEntityListV1\Entities[*]\Types[*]", srviceName, scenaro);
                            seResult = WebAnserParsePbxml(ToStream(PbxmlString), @"SatoryId:FcsHostCollapseResults\Kif.Value[*]\SatoriEntityListV1\Entities[*]\Id;Types:FcsHostCollapseResults\Kif.Value[*]\SatoriEntityListV1\Entities[*]\Types[0]", "WebAnswer", "queryrequest", true);
                            if(string.IsNullOrEmpty(seResult))
                            {
                                seResult = WebAnserParsePbxml(ToStream(PbxmlString), @"SatoryId:FcsHostCollapseResults\Kif.Value[*]\SatoriEntityListV1\Entities[0]\Id;Types:FcsHostCollapseResults\Kif.Value[*]\SatoriEntityListV1\Entities[0]\Types[*]", "WebAnswer", "queryrequest", false);
                            }
                            //   seResult = ParsePbxml(ToStream(PbxmlString), @"SatoryId:FcsHostCollapseResults\Kif.Value[*]\SatoriEntityListV1\Entities[*]\Id;Types:FcsHostCollapseResults\Kif.Value[*]\SatoriEntityListV1\Entities[*]\Types[*]", srviceName, scenaro);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    if (!string.IsNullOrEmpty(seResult))
                    {
                        break;
                    }
                }
                if (string.IsNullOrEmpty(seResult))
                {
                    seResult = "Out of our service!";
                }
                Message replyMessage;


                replyMessage = message.CreateReplyMessage($"{seResult}");

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

        public static string ParsePbxml(Stream pbxmlStream, string FieldName2PathMapping, string service, string scenario)
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

            // string service = "Dolphin", scenario = "Fetch";
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

        public static string WebAnserParsePbxml(Stream pbxmlStream, string FieldName2PathMapping, string service = "WebAnswer", string scenario = "queryrequest", bool flag = true)
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

            // string service = "Dolphin", scenario = "Fetch";
            /* XPathDocument pbxml = new XPathDocument(pbxmlStream);
             XPathExpression answerExpression = Util.PBXMLUtil.GetKif(service, scenario);
             JObject json = Util.PBXMLUtil.GetKifJson(pbxml, answerExpression);
             */
            List<string> jsonList = new List<string>();
            PositionJson(pbxmlStream, jsonList);
            foreach (string jsonEle in jsonList)
            {
                JObject json = JObject.Parse(jsonEle);
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
                    if(flag)
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
                        if(condArr.Contains("film.film"))
                        {
                            conditionValueList.Add(fieldValues[0]);
                        }
                    }
                    
                    seRes = string.Join("|||", conditionValueList.ToArray());
                }

                if (!string.IsNullOrEmpty(seRes))
                    break;
            }

            return seRes;
        }

        public static string ParsePbxmlRelateEntity(Stream pbxmlStream, string FieldName2PathMapping, string service, string scenario)
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

            // string service = "Dolphin", scenario = "Fetch";
            /* XPathDocument pbxml = new XPathDocument(pbxmlStream);
             XPathExpression answerExpression = Util.PBXMLUtil.GetKif(service, scenario);
             JObject json = Util.PBXMLUtil.GetKifJson(pbxml, answerExpression);
             */
            List<string> jsonList = new List<string>();
            PositionJson(pbxmlStream, jsonList);
            foreach (string jsonEle in jsonList)
            {
                JObject json = JObject.Parse(jsonEle);
                string[] fieldValues = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    string field = fields[i];
                    string[] hierachy = field.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> values = new List<string>();
                    try
                    {
                        GetFieldValue(hierachy, 0, json as JToken, ref values);
                        fieldValues[i] = string.Join("|||", values.ToArray());
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
                    if(string.IsNullOrEmpty(fieldValues[0]) || string.IsNullOrEmpty(fieldValues[1]))
                    {
                        continue;
                    }
                    string[] valueArr = fieldValues[0].Split(new string[] { "|||" }, StringSplitOptions.None);
                    string[] condArr = fieldValues[1].Split(new string[] { "|||" }, StringSplitOptions.None);
                    if (valueArr.Length != condArr.Length)
                        continue;
                    for (int i = 0; i < condArr.Length; i++)
                    {
                        if (Regex.IsMatch(condArr[i], @"^\d{4}$"))
                        {
                            conditionValueList.Add(valueArr[i]);
                        }
                    }
                    seRes = string.Join("|||", conditionValueList.ToArray());
                }
                if (!string.IsNullOrEmpty(seRes))
                    break;
            }

            return seRes;
        }

        public static void PositionJson(Stream pbxmlStream, List<string> jsonList)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pbxmlStream);
            XmlElement root = doc.DocumentElement;
            // List<string> jsonStr = new List<string>();
            XmlNodeList listNodes = root.SelectNodes(string.Format("/PropertyBag/s_AnswerResponseCommand/s_AnswerQueryResponse/a_AnswerDataArray/s_AnswerData[c_AnswerServiceName=\"{0}\"][c_AnswerDataScenario=\"{1}\"]/k_AnswerDataKifResponse", "EntityWebAnswer", "EntityWebPerson"));
            foreach (XmlNode node in listNodes)
            {
                jsonList.Add(node.InnerText);
            }
        }
        private static void GetFieldValue(string[] hierachy, int layer, JToken json, ref List<string> values)
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

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}