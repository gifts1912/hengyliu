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
                int counter = message.GetBotPerUserInConversationData<int>("counter");
                string query = message.Text;
                string URL2 = "http://www.bing.com/search?q=" + string.Join("+", query.Split(' ')) + "&filter=dolphin%7Cfetch&format=pbxml&p1=%5bAnswerReducer%20Mode=%22Disabled%22%5d";
                PageContentCrawlerByWebrequest crawler2 = new PageContentCrawlerByWebrequest();
                string PbxmlString = crawler2.crawl(URL2);
                
                string seResult = ParsePbxml(ToStream(PbxmlString));
                if(String.IsNullOrEmpty(seResult))
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