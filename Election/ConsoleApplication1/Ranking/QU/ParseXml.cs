using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.QU.ParseXml
{
    class ParseXml
    {
        static Dictionary<string, List<string>> resultA = new Dictionary<string, List<string>>();
        

        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
               // args[0] = @"D:\demo\WPElectionGoogleSBSScrap.xml";
                args[0] = @"D:\demo\DirectoryRemainIntentsV1.2\google.xml";
                args[1] = @"D:\demo\queryUrlListb.tsv";
            }
            string xmlFile = args[0];
            string outFile = args[1];

            ParseQueryUrlList(xmlFile, outFile, ref resultA);
        }

        public static void ParseQueryUrlList(string infile, string outfile, ref Dictionary<string, List<string>> result)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(infile);

            XmlNode dataNode = xmlDoc.GetElementsByTagName("Batch")[0].SelectSingleNode("Data");
            XmlNodeList queryList = dataNode.SelectNodes("Query");
            foreach(XmlNode queryNode in queryList)
            {
                string id = queryNode.Attributes["id"].Value;
                string rawQuery = queryNode.SelectSingleNode("RawText").InnerText.Trim();
                XmlNodeList resultList = queryNode.SelectNodes("Results/Result");

                Dictionary<int, string> posUrl = new Dictionary<int, string>();
                foreach(XmlNode resultNode in resultList)
                {
                    string url = resultNode.SelectSingleNode("URL").InnerText.Trim();
                    int position = int.Parse(resultNode.SelectSingleNode("Position").InnerText.Trim());
                    posUrl[position] = url;
                }
                if(!result.ContainsKey(id))
                {
                    result[id] = new List<string>();
                    result[id].Add(rawQuery);
                }
                foreach (int key in posUrl.Keys)
                {
                    result[id].Add(posUrl[key]);
                }
            }
            
            using (StreamWriter sw = new StreamWriter(outfile))
            {
                foreach(KeyValuePair<string, List<string>> pair in result)
                {
                    sw.WriteLine("{0}", pair.Value[0]);
                }
            }
            
        }         
    }
}