namespace QU.Miscs.Common
{
    using Microsoft.WindowsAzure.StorageClient;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml.XPath;
    using Util;

    public class PbxmlScraperResultsProcessor
    {
        private string azureFolder;
        private int currentSegmentNum;
        private Dictionary<string, int> headerMapping;
        private const string ScrapeLogFileNameFormat = "Log_{0}.log";
        private int totalSegmentsCount;

        public PbxmlScraperResultsProcessor(string iazureFolder, int itotalSegmentsCount, int icurrentSegmentNum)
        {
            this.azureFolder = iazureFolder;
            this.totalSegmentsCount = itotalSegmentsCount;
            this.currentSegmentNum = icurrentSegmentNum;
        }

        public List<ScrapeQueryResult> ExtractAugmentedEntityIndexQuery()
        {
            CloudBlobContainer blobContainer = AzureUtil.GetBlobContainer("DefaultEndpointsProtocol=http;AccountName=warp;AccountKey=5Ya/dmc+kT36CBkhJn6ey+hL/txEIwSpmxTW85vthVLCewSj0utg6EsLTv52ajMlGX9VccY+m90QTC0Lv7c39A==", this.azureFolder);
            string logFileName = string.Format("Log_{0}.log", this.azureFolder);
            string outFile = null;
            if (!AzureUtil.DownloadFile(logFileName, blobContainer, out outFile))
            {
                throw new Exception("Cannot read log file " + logFileName);
            }
            List<string> elementstoProcess = this.InitializeHeaderAndInstances(outFile.Trim());
            AzureConnectionMap connectionMap = new AzureConnectionMap();
            return (from curelement in elementstoProcess select GetScrapeQueryresult(curelement, this.azureFolder, this.headerMapping, connectionMap)).ToList<ScrapeQueryResult>();
        }

        public List<ScrapeQueryResult> ExtractFields(string service, string scenario, string[] fields)
        {
            CloudBlobContainer blobContainer = AzureUtil.GetBlobContainer("DefaultEndpointsProtocol=http;AccountName=warp;AccountKey=5Ya/dmc+kT36CBkhJn6ey+hL/txEIwSpmxTW85vthVLCewSj0utg6EsLTv52ajMlGX9VccY+m90QTC0Lv7c39A==", this.azureFolder);
            string logFileName = string.Format("Log_{0}.log", this.azureFolder);
            string outFile = null;
            if (!AzureUtil.DownloadFile(logFileName, blobContainer, out outFile))
            {
                throw new Exception("Cannot read log file " + logFileName);
            }
            List<string> elementstoProcess = this.InitializeHeaderAndInstances(outFile.Trim());
            AzureConnectionMap connectionMap = new AzureConnectionMap();

            return (from curelement in elementstoProcess select ExtractFields(curelement, this.azureFolder, this.headerMapping, connectionMap, service, scenario, fields)).ToList();
        }

        private static ScrapeQueryResult ExtractFields(
            string logElement,
            string azureFolder,
            Dictionary<string, int> headerMapping,
            AzureConnectionMap connectionMap,
            string service, string scenario, 
            string[] fields)
        {
            CloudBlobContainer blobContainer = AzureUtil.GetBlobContainer("DefaultEndpointsProtocol=http;AccountName=warp;AccountKey=5Ya/dmc+kT36CBkhJn6ey+hL/txEIwSpmxTW85vthVLCewSj0utg6EsLTv52ajMlGX9VccY+m90QTC0Lv7c39A==", azureFolder);
            string logFileName = string.Format("Log_{0}.log", azureFolder);
            string outFile = null;
            if (!AzureUtil.DownloadFile(logFileName, blobContainer, out outFile))
            {
                throw new Exception("Cannot read log file " + logFileName);
            }

            string[] splitLine = logElement.Split(new char[] { '\t' });
            string query = ExtractQuery(splitLine[headerMapping[StringConstants.UrlHeader]]);
            string pbxmlURI = string.Format("http://warp.blob.core.windows.net/{0}/{1}", azureFolder, splitLine[headerMapping[StringConstants.PBXMLHeader]]);
            Console.WriteLine("Query: {0}", query);

            try
            {
                XPathDocument pbxml = connectionMap.GetPBXML(pbxmlURI);
                XPathExpression answerExpression = PBXMLUtil.GetKif(service, scenario);
                JObject json = PBXMLUtil.GetKifJson(pbxml, answerExpression);

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
                        return null;
                    }
                }

                return new ScrapeQueryResult() { Query = query, InterestedFields = fieldValues };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return null;
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

        private static string ExtractQuery(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                return HttpUtility.ParseQueryString(uri.Query)["q"];
            }
            catch
            {
                return null;
            }
        }

        private static ScrapeQueryResult GetScrapeQueryresult(string logElement, string azureFolder, Dictionary<string, int> headerMapping, AzureConnectionMap connectionMap)
        {
            string entityIndexAugmentedQuery;
            string mcEntityId;
            bool hasEntityIndexAnswer;
            string[] splitLine = logElement.Split(new char[] { '\t' });
            string query = ExtractQuery(splitLine[headerMapping[StringConstants.UrlHeader]]);
            string pbxmlURI = string.Format("http://warp.blob.core.windows.net/{0}/{1}", azureFolder, splitLine[headerMapping[StringConstants.PBXMLHeader]]);
            Console.WriteLine("Processing PBXML for " + pbxmlURI);
            try
            {
                XPathDocument pbxml;
                JObject kif;
                string satoriId;
                connectionMap.GetRankerDebugComponents(pbxmlURI, EntityDomains.Other, out pbxml, out kif, out satoriId, null);
                entityIndexAugmentedQuery = SatoriIDExtractor.GetGraphSearchMonteCarloQueryAugmentedEntityIndexQuery(pbxml);
                mcEntityId = SatoriIDExtractor.GetGraphSearchMonteCarloQueryResultEntityId(pbxml);
                hasEntityIndexAnswer = SatoriIDExtractor.GetKifJson(pbxml, SatoriIDExtractor.GetKifXPathExpression("EntityIndexAnswer")) != null;
            }
            catch
            {
                entityIndexAugmentedQuery = null;
                mcEntityId = null;
                hasEntityIndexAnswer = false;
            }
            ScrapeQueryResult result = new ScrapeQueryResult
            {
                PbxmlURI = pbxmlURI,
                ErrorMessage = "success"
            };
            if (query == null)
            {
                result.Query = string.Empty;
                result.ErrorMessage = "Error while processing query";
            }
            else
            {
                result.Query = query;
            }
            if (string.IsNullOrEmpty(entityIndexAugmentedQuery))
            {
                result.ErrorMessage = "Error while retrieving entityIndexAugmentedQuery";
            }
            else
            {
                result.EntityIndexAugmentedQuery = entityIndexAugmentedQuery;
            }
            result.MCResultEntityId = mcEntityId;
            result.HasEntityIndexAnswer = hasEntityIndexAnswer;
            return result;
        }

        private List<string> InitializeHeaderAndInstances(string logContents)
        {
            string[] logInstances = logContents.Split(new char[] { '\n' });
            this.headerMapping = FileUtil.GetHeaderColumnMap(logInstances[0], '\t');
            List<string> logElements = logInstances.Skip<string>(1).ToList<string>();
            int elementspersegment = logElements.Count / this.totalSegmentsCount;
            int readsofar = this.currentSegmentNum * elementspersegment;
            IEnumerable<string> remaininginstances = logElements.Skip<string>(readsofar);
            return ((this.currentSegmentNum == (this.totalSegmentsCount - 1)) ? remaininginstances : remaininginstances.Take<string>(elementspersegment)).ToList<string>();
        }
    }
}

