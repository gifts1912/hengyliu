using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TopSiteMining.TopSite
{
    public class IntendPatternLayer
    {
        public static int QueryCol = 0, UrlCol = 1, SortPCol = 3;
        public static Dictionary<string, int> urlScoreDic = new Dictionary<string, int>();
        public static Dictionary<string, int> urlDomainScoreDic = new Dictionary<string, int>();
        public static Dictionary<string, int> queryScoreDic = new Dictionary<string, int>();
        public static HashSet<string> patternQuerySet = new HashSet<string>();

        public static Dictionary<string, Dictionary<string, List<string>>> intentPatternQuery = new Dictionary<string, Dictionary<string, List<string>>>();
        public static Dictionary<string, List<string>> queryUrlList = new Dictionary<string, List<string>>();
        public static Dictionary<string, Dictionary<string, Dictionary<string, int>>> intentSlotUrlScore = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();
        public static string GenUrlDomain(string url, Regex rgx)
        {
            string result = "";
            Match mc = rgx.Match(url);
            if (mc.Success)
            {
                result = mc.Groups[3].Value.ToString();
            }
            //  Console.WriteLine("{0}\t{1}", url, result);
            // Console.ReadKey();
            return result;
        }

        public static void ReadQueryTopUrlAndScore(string infile, int n)
        {
            /*
             * Just get first position url of query. Stored them in Dictornary<string, List<string>> queryUrlList;
             */
            int queryCol = 1, urlCol = 4, posCol = 7;
            StreamReader sr = new StreamReader(infile);
            string line, query, url, pos;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                query = arr[queryCol];
                url = arr[urlCol];
                pos = arr[posCol];
                if(!queryUrlList.ContainsKey(query))
                {
                    queryUrlList[query] = new List<string>();
                }
                int score = 11 - int.Parse(pos);
                queryUrlList[query].Add(string.Format("%s\t%s", url, score));
            }
            /*
            while (true)
            {
                line = sr.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
                line = line.Trim();

                string query, url;
                if (line.StartsWith("<Text>"))
                {
                    query = line.Substring(6, line.Length - 13);
                    if(!queryUrlList.ContainsKey(query))
                    {
                        queryUrlList[query] = new List<string>();
                    }
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;
                    line = line.Trim();
                    url = line.Substring(5, line.Length - 11);
                    queryUrlList[query].Add(url);
                }
            }*/
            sr.Close();
        }

        public static void TopSiteScoreCompute(string flag = "bing")
        {
            /*
             * Compute the url socre of specified intent + slot layer.
             */
            //Dictionary<string, Dictionary<string, Dictionary<string, int>>> intentSlotUrlScore = new Dictionary<string,Dictionary<string,Dictionary<string,int>>>();
            string pattern = "http(s)?://(www.)?([0-9a-zA-Z-.]+)/";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled);

            foreach(KeyValuePair<string, Dictionary<string, List<string>>> pair in intentPatternQuery)
            {
                string intent = pair.Key;
                if(!intentSlotUrlScore.ContainsKey(intent))
                {
                    intentSlotUrlScore[intent] = new Dictionary<string, Dictionary<string, int>>();
                }
                Dictionary<string, List<string>> slotQuery = pair.Value;
                foreach(KeyValuePair<string, List<string>> pairEle in slotQuery)
                {
                    string slot = pairEle.Key;
                    if(!intentSlotUrlScore[intent].ContainsKey(slot))
                    {
                        intentSlotUrlScore[intent][slot] = new Dictionary<string, int>();
                    }
                    List<string> queryList = pairEle.Value;
                    foreach(string query in queryList)
                    {
                        int score = 0;
                        if(!queryScoreDic.ContainsKey(query))
                            continue;
                        score = queryScoreDic[query];
                        if (flag.ToLower().Contains("google"))
                        {
                            score = score <= 0 ? 1 : -1;
                        }
                        else
                        {
                            score = (score >= 0 ? 1 : -1);
                        }
                    
                        if(!queryUrlList.ContainsKey(query))
                            continue;
                        List<string> urlList = queryUrlList[query];
                        foreach(string urlScore in urlList)
                        {
                            string[] arrUrlScore = urlScore.Split('\t');                        
                            if (arrUrlScore.Length != 2)
                                continue;
                            string url = arrUrlScore[0];
                            int scoreCur = int.Parse(arrUrlScore[1]);
                            score = score * scoreCur;
                            string urlDomain = GenUrlDomain(url, rgx);
                            if(!intentSlotUrlScore[intent][slot].ContainsKey(urlDomain))
                            {
                                intentSlotUrlScore[intent][slot][urlDomain] = 0;
                            }
                            intentSlotUrlScore[intent][slot][urlDomain] += score;
                        }
                    }
                }
            }
           
        }
    
        public static int MyCmp(KeyValuePair<string, int> k1, KeyValuePair<string, int> k2)
        {
            return k2.Value.CompareTo(k1.Value);
        }
        public static void ReadQueryScore(string infile)
        {
            int queryCol = 1, judgeMentCol = 5;

            StreamReader sr = new StreamReader(infile);
            string line;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string query = arr[queryCol];
                int score = 0;
                try
                {
                    score = int.Parse(arr[judgeMentCol]);
                }
                catch (Exception ce)
                {
                    continue;
                }
                queryScoreDic[query] = score;
            }

            sr.Close();
        }

        public static string NormalizePattern(string pattern)
        {
            /*
             * Delete the ele that doesn't belong to slot entity, and construct the normalized pattern with sorted slot element.
             */
            string result = "";
            string[] patArr = pattern.Split(' ');
            List<string> patEleArr = new List<string>();
            foreach (string ele in patArr)
            {
                if (ele.Contains("."))
                {
                    patEleArr.Add(ele);
                }
            }
            patEleArr.Sort();
            result = string.Join(" ", patEleArr.ToArray());

            return result;
        }
        public static void ReadPatternQuery(string infile, int qCol, int pCol, int iCol)
        {
            /*
             * Store query set of specified intend + pattern in data structure Dictionary<string, Dictionary<string, List<string>>> intentPatternQuery.
             */
            StreamReader sr = new StreamReader(infile);
            string line, query, pattern, intent;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= qCol || arr.Length <= pCol || arr.Length <= iCol)
                {
                    continue;
                }
                query = arr[qCol];
                pattern = arr[pCol];
                intent = arr[iCol];
                pattern = NormalizePattern(pattern);
                if (!intentPatternQuery.ContainsKey(intent))
                {
                    intentPatternQuery[intent] = new Dictionary<string, List<string>>();
                }
                if (!intentPatternQuery[intent].ContainsKey(pattern))
                {
                    intentPatternQuery[intent][pattern] = new List<string>();
                }
                intentPatternQuery[intent][pattern].Add(query);
            }

            sr.Close();
        }

        public static void ReadPatternQuery(string infile, string intentFlag)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string rawQuery = arr[0];
                string intent = arr[1].Trim();
                if (intent == intentFlag)
                    patternQuerySet.Add(rawQuery);
            }
            sr.Close();
        }

        public static void TopSiteStore(string outfile, string domainOutFile, string flag)
        {
            /*
             * Store to disk of the url socre of specified intent + slot layer.
             */
            StreamWriter sw = new StreamWriter(domainOutFile);
            foreach(KeyValuePair<string, Dictionary<string, Dictionary<string, int>>> pair in intentSlotUrlScore)
            {
                string intent = pair.Key;
                if (string.IsNullOrEmpty(intent))
                    continue;
                foreach(KeyValuePair<string, Dictionary<string,int>> pairEle in pair.Value)
                {
                    string slot = pairEle.Key;
                    if (string.IsNullOrEmpty(slot))
                        continue;
                    List<KeyValuePair<string, int>> urlScoreSort = pairEle.Value.ToList();
                    urlScoreSort.Sort(MyCmp);
                    StringBuilder urlComStr = new StringBuilder();
                    foreach(KeyValuePair<string, int> urlScoreEle in urlScoreSort)
                    {
                        if(urlScoreEle.Value >= 0)
                        {
                            urlComStr.Append(urlScoreEle.Key);
                            urlComStr.Append(",");
                        }
                    }
                    string BestQueryPattern = "", BestQuery = "", GTopUrls = "", BTopUrls = "", ManuallySelected = "", GTopDomains = "", BTopDomains = "";
                    string topUrl = "";
                    if(flag.ToLower().Contains("bing"))
                    {
                        BTopDomains = urlComStr.ToString().Trim(',');
                        topUrl = BTopDomains;
                    }
                    else if(flag.ToLower().Contains("google"))
                    {
                        GTopDomains = urlComStr.ToString().Trim(',');
                        topUrl = GTopDomains;
                    }
                    //string result = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", intent, slot, BestQueryPattern, BestQuery, GTopUrls, GTopDomains, BTopUrls, BTopDomains, ManuallySelected);
                    string result = string.Format("{0}\t{1}\t{2}", intent, slot, topUrl);
                    sw.WriteLine("{0}",result);
                }
            }
            sw.Close();
        }
        public static void Run(string[] args)
        {
            string infile = @"D:\Project\Election\SBS\GoogleScrapeTSV.tsv";
            string outfile = @"D:\Project\Election\TokenAndRules\PartyCandidateListTopUrlScore.tsv";
            string outDomainUrlfile = @"D:\Project\Election\TokenAndRules\IntentPatternLayerTopDomainUrlScoreGoogle.tsv";
            string scoreFile = @"D:\Project\Election\QuerySet\queyJudge.tsv";

            ReadQueryScore(scoreFile);


            string PatternQueryFile = @"D:\Project\Election\TokenAndRules\electionV1.2.tsv";
            int queryCol = 0, patCol = 1, intentCol = 3;

            ReadPatternQuery(PatternQueryFile, queryCol, patCol, intentCol);

            ReadQueryTopUrlAndScore(infile, 5);

            TopSiteScoreCompute("Google");

            TopSiteStore(outfile, outDomainUrlfile, "GoogleTopDomain");

           // TopSiteScore(outfile, outDomainUrlfile);
        }
    }
}
