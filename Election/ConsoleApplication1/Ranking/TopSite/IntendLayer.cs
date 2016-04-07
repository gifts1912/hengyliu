using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TopSiteMining.TopSite
{
    public class IntendLayer
    {
        public static int QueryCol = 0, UrlCol = 1, SortPCol = 3;
        public static Dictionary<string, int> urlScoreDic = new Dictionary<string, int>();
        public static Dictionary<string, int> urlDomainScoreDic = new Dictionary<string, int>();
        public static Dictionary<string, int> queryScoreDic = new Dictionary<string, int>();
        public static HashSet<string> patternQuerySet = new HashSet<string>();

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

        public static void TopSiteScoreGoogle(string infile, string outfile, string urlDomainOutfile)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);

            string line, query, url, urlDomain;
            int SortPos;
            string pattern = "http(s)?://(www.)?([0-9a-zA-Z-.]+)/";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled);
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("<Text>"))
                {
                    string res = line;
                    query = line.Substring(6, line.Length - 13);
                    query = query.Trim();
                    if (!patternQuerySet.Contains(query))
                        continue;
                    line = sr.ReadLine();
                    line = sr.ReadLine();
                    res = res + "\t" + line;
                    //Console.WriteLine("{0}", res);
                    //Console.ReadKey();
                    if (line == null)
                        break;
                    line = line.Trim();

                    int posB = 0;
                    if ((posB = line.IndexOf("<URL>")) == -1)
                        continue;

                    url = line.Substring(5, line.Length - 11);
                    urlDomain = GenUrlDomain(url, rgx);
                    int score = 0;
                    if (queryScoreDic.ContainsKey(query))
                    {
                        score = queryScoreDic[query];
                        if (score < 0)
                            score = 0;
                        else
                            score = 1;
                    }
                    if (!urlScoreDic.ContainsKey(url))
                    {
                        urlScoreDic[url] = 0;
                    }
                    urlScoreDic[url] += score;

                    if (!urlDomainScoreDic.ContainsKey(urlDomain))
                    {
                        urlDomainScoreDic[urlDomain] = 0;
                    }
                    urlDomainScoreDic[urlDomain] += score;
                }
            }

            sr.Close();

            List<KeyValuePair<string, int>> queryScoreList = new List<KeyValuePair<string, int>>();
            queryScoreList = urlScoreDic.ToList();
            queryScoreList.Sort(MyCmp);

            foreach (KeyValuePair<string, int> pair in queryScoreList)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);

                //Console.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            //  Console.ReadKey();
            sw.Close();

            sw = new StreamWriter(urlDomainOutfile);
            queryScoreList = urlDomainScoreDic.ToList();
            queryScoreList.Sort(MyCmp);
            foreach (KeyValuePair<string, int> pair in queryScoreList)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);

                //Console.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();

        }
        public static void TopSiteScore(string infile, string outfile, string urlDomainOutfile)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);

            string line, query, url, urlDomain;
            int SortPos;
            string pattern = "http(s)?://(www.)?([0-9a-zA-Z-.]+)/";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled);
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                query = arr[QueryCol];
                url = arr[UrlCol];
                urlDomain = GenUrlDomain(url, rgx);
                try
                {
                    SortPos = int.Parse(arr[SortPCol]);
                }
                catch (Exception ce)
                {
                    continue;
                }

                if (SortPos == 0)
                {
                    int score = 0;
                    if (queryScoreDic.ContainsKey(query))
                    {
                        score = queryScoreDic[query];
                        if (score < 0)
                            score = 0;
                        else
                            score = 1;
                    }
                    if (!urlScoreDic.ContainsKey(url))
                    {
                        urlScoreDic[url] = 0;
                    }
                    urlScoreDic[url] += score;

                    if (!urlDomainScoreDic.ContainsKey(urlDomain))
                    {
                        urlDomainScoreDic[urlDomain] = 0;
                    }
                    urlDomainScoreDic[urlDomain] += score;
                }
            }

            sr.Close();

            List<KeyValuePair<string, int>> queryScoreList = new List<KeyValuePair<string, int>>();
            queryScoreList = urlScoreDic.ToList();
            queryScoreList.Sort(MyCmp);

            foreach (KeyValuePair<string, int> pair in queryScoreList)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);

                //Console.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            //  Console.ReadKey();
            sw.Close();

            sw = new StreamWriter(urlDomainOutfile);
            queryScoreList = urlDomainScoreDic.ToList();
            queryScoreList.Sort(MyCmp);
            foreach (KeyValuePair<string, int> pair in queryScoreList)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);

                //Console.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();

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

        public static void ReadPatternQuery(string infile)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string rawQuery = arr[1];
                patternQuerySet.Add(rawQuery);
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
        public static void Run(string[] args)
        {
            string infile = @"D:\Project\Election\TokenAndRules\PartyCandidateListScrap.tsv";
            string outfile = @"D:\Project\Election\TokenAndRules\PartyCandidateListTopUrlScore.tsv";
            string outDomainUrlfile = @"D:\Project\Election\TokenAndRules\PartyCandidateListTopDomainUrlScore.tsv";
            string scoreFile = @"D:\Project\Election\QuerySet\queyJudge.tsv";
            ReadQueryScore(scoreFile);

            //  TopSiteScore(infile, outfile, outDomainUrlfile);
            infile = @"D:\Project\Election\TokenAndRules\WPElectionQueryGoogleScrapeGrep.tsv";
            string PatternQueryFile = @"D:\Project\Election\TokenAndRules\QueryDetailIntend.tsv";
            ReadPatternQuery(PatternQueryFile, "[election.candidate.highconf] [election.views]");
            TopSiteScoreGoogle(infile, outfile, outDomainUrlfile);
        }
    }
}
