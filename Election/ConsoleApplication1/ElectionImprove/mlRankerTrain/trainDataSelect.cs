using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.mlRankerTrain
{
    class trainDataSelect
    {
        private static int queryIdx = 1, lowIdx = 34, highIdx = 43, scoreIdx = 5;
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\trainData\";
                args[1] = @"D:\demo\trainDataReranking.tsv";
            }

            string filePath = args[0];
            string outfile = args[1];
            GenerateTrainData(filePath, outfile);
        }
        public static void GenerateTrainData(string filePath, string outfile)
        {
           if(!Directory.Exists(filePath))
            {
                return;
            }
            string []fileEntries = Directory.GetFiles(filePath);
            Dictionary<string, List<string>> queryUrlsInstances = new Dictionary<string, List<string>>();
            HashSet<string> contradictionSet = new HashSet<string>();
            foreach(string file in fileEntries)
            {
                StreamReader sr = new StreamReader(file);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    string query = arr[queryIdx];
                    double score = double.Parse(arr[scoreIdx]);
                    if (score < 0)
                        continue;
                    AddNewInstance(arr, ref queryUrlsInstances, ref contradictionSet);
                }
                sr.Close(); 
            }

            StreamWriter sw = new StreamWriter(outfile);
            foreach(KeyValuePair<string, List<string>> pair in queryUrlsInstances)
            {
                string query = pair.Key;
                List<string> urlList = pair.Value;
                for(int i = 0; i < urlList.Count; i++)
                {
                    sw.WriteLine("{0}\t{1}\t{2}", query, urlList[i], 20 - i);
                }
            }
            sw.Close();
        }
        public static void AddNewInstance(string [] arr, ref Dictionary<string, List<string>> queryUrlsInstances, ref HashSet<string> contradictionSet)
        {
            string query = arr[queryIdx];
            if(contradictionSet.Contains(query))
            {
                return;
            }
            if(!queryUrlsInstances.ContainsKey(query))
            {
                queryUrlsInstances[query] = new List<string>();
                for(int i = lowIdx; i <= highIdx; i++)
                {
                    string url = arr[i].Trim();
                    if (string.IsNullOrEmpty(url))
                        continue;
                    queryUrlsInstances[query].Add(NormalizeUrl(url));
                }
            }
            else
            {
                if (Contradiction(arr, queryUrlsInstances[query]))
                {
                    queryUrlsInstances.Remove(query);
                    contradictionSet.Add(query);
                }
            }   
        }

        public static string NormalizeUrl(string url)
        {
            return FrontEndUtil.CURLUtilities.GetHutNormalizeUrl(url) ;
        }

        public static bool Contradiction(string [] arr, List<string> urlList)
        {
            bool result = false;
            for(int i = lowIdx; i <= highIdx; i++)
            {
                string url = arr[i];
                url = FrontEndUtil.CURLUtilities.GetCBUrlHash(url);
                int relevanceIdx = i - lowIdx;
                if (url != urlList[relevanceIdx])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
