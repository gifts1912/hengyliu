using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace QAS.PatternEngine
{
    class GenerateFormatUrlOfPatterns
    {
        private static Dictionary<string, string> urlToPdi = new Dictionary<string, string>();
        private static Dictionary<string, Dictionary<string, int>> idxToUrlSocre = new Dictionary<string, Dictionary<string, int>>();
        private static StreamWriter logWriter;
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\demo\UrlToPdiDumpFile.tsv";
                args[1] = @"D:\Project\Election\TokenAndRules\ElectionTopSiteListIndexASKey.tsv";
                args[2] = @"D:\demo\KeyTermPattern.tsv";
                args[3] = @"D:\demo\ProcLog.tsv";
            }
            string urlToPdiFile = args[0];
            string topSiteFile = args[1];
            string keyTermPatFile = args[2];
            string logFile = args[3];
            logWriter = new StreamWriter(logFile);

            Loader(urlToPdiFile, 0, urlToPdi, 0);
            LoaderUrlList(topSiteFile);

            GenKeyTermPat(keyTermPatFile);
            logWriter.Close();
        }


        public static void Loader(string infile, int keyCol, Dictionary<string, string> storeDic, int urlCol)
        {
            //load and process the url inoder to match topSite and urlToPdi.
            using (StreamReader sr = new StreamReader(infile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    string key = arr[keyCol];
                    if (keyCol == urlCol)
                    {
                        key = ProcessUrl(key);
                    }
                    StringBuilder sb = new StringBuilder();
                    bool first = true;
                    for (int i = 0; i < keyCol; i++)
                    {
                        if (i == urlCol)
                        {
                            arr[i] = ProcessUrl(arr[i]);
                        }
                        if (first)
                        {
                            sb.Append(arr[i]);
                            first = false;
                        }
                        else
                        {
                            sb.Append("\t");
                            sb.Append(arr[i]);
                        }

                    }
                    for (int i = keyCol + 1; i < arr.Length; i++)
                    {
                        if (i == urlCol)
                        {
                            arr[i] = ProcessUrl(arr[i]);
                        }
                        if (first)
                        {
                            sb.Append(arr[i]);
                            first = false;
                        }
                        else
                        {
                            sb.Append("\t");
                            sb.Append(arr[i]);
                        }
                    }

                    storeDic[key] = sb.ToString();
                }
            }
        }

        public static void LoaderUrlList(string infile)
        {
            using (StreamReader sr = new StreamReader(infile))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    string idx = int.Parse(arr[0]).ToString();
                    if(!idxToUrlSocre.ContainsKey(idx))
                    {
                        idxToUrlSocre[idx] = new Dictionary<string, int>();
                    }
                    string url = ProcessUrl(arr[1]);
                    if(!idxToUrlSocre[idx].ContainsKey(url))
                    {
                        idxToUrlSocre[idx][url] = int.Parse(arr[2]);
                    }
                    else
                    {
                        ProcLog(string.Format("RepUrl in same intent: (1): {0}\t(2):{1}", line, string.Format("{0}\t{1}\t{2}", idx, url, idxToUrlSocre[idx][url])));
                    }
                }
            }
        }
        public static string ProcessUrl(string url)
        {
            //Delete the prefix such as "http://, https://, http://www, https://www." from url 
            string urlPat = @"^(http(s)?://(www\.)?)(.*)$";
            Match mc = Regex.Match(url, urlPat);
            if (mc.Success)
            {
                return mc.Groups[4].ToString();
            }
            else
            {
                ProcLog(string.Format("Not satisfied the url pattern:\t{0}", url));
                return null;
            }
        }
        public static void GenKeyTermPat(string outfile)
        {
            //private static Dictionary<string, string> urlToPdi = new Dictionary<string, string>();
            //private static Dictionary<string, Dictionary<string, int>> idxToUrlSocre = new Dictionary<string, Dictionary<string, int>>();
            using (StreamWriter sw = new StreamWriter(outfile))
            {
                int startnum = 8;       
                foreach (KeyValuePair<string, Dictionary<string, int>> idxUrlScorePair in idxToUrlSocre)
                {
                    sw.WriteLine(string.Format("[KeyTermDict:SIIntentLevelPlatformAuthoritySites:{0}]", startnum));
                    startnum++;
                    sw.WriteLine(string.Format("MatchConstraint=QLF$2950:{0}", idxUrlScorePair.Key));
                    int cur_idx = 0;
                    string preUrl = "KeyTerm_", preScore = "Socre_";
                    foreach (KeyValuePair<string, int> urlScorePair in idxUrlScorePair.Value.ToList())
                    {
                        string url = urlScorePair.Key;
                        if (urlToPdi.ContainsKey(url))
                        {
                            string metaStream = urlToPdi[url];
                            sw.WriteLine(string.Format("{0}{1}={2}", preUrl, cur_idx, metaStream));
                            sw.WriteLine(string.Format("{0}{1}={2}", preScore, cur_idx, urlScorePair.Value));
                            cur_idx++;
                        }
                        else
                        {
                            ProcLog(string.Format("Not in urlToPdi: {0}", url));
                        }
                    }
                }
            }    
        }

        public static void ProcLog(string line)
        {
            logWriter.WriteLine(line);
        }
    }
}
