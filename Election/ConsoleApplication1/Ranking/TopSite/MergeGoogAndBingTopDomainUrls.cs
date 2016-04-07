using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using FrontEndUtil;

namespace Ranking.TopSite
{
    class MergeGoogAndBingTopDomainUrls
    {
        public static bool m_isMatchingHost = true;
        public static int Cmp(KeyValuePair<string, double> pairA, KeyValuePair<string,double> pairB)
        {
            return pairB.Value.CompareTo(pairA.Value);
        }
        public static void MergeTopDomain(string infileBing, string infileGoogle, string outfile)
        {
            Dictionary<string, Dictionary<string, Dictionary<string, double>>> intentSlotUrlScore = new Dictionary<string, Dictionary<string, Dictionary<string, double>>>();

            StreamReader srG = new StreamReader(infileGoogle);
            string line;
            while((line = srG.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if(arr.Length != 4)
                    continue;
                string intent = arr[0], slot = arr[1], url = arr[2];
                int score = int.Parse(arr[3]);
                if(!intentSlotUrlScore.ContainsKey(intent))
                {
                    intentSlotUrlScore[intent] = new Dictionary<string, Dictionary<string, double>>();
                }
                if(!intentSlotUrlScore[intent].ContainsKey(slot))
                {
                    intentSlotUrlScore[intent][slot] = new Dictionary<string, double>();
                }
                if (!intentSlotUrlScore[intent][slot].ContainsKey(url))
                {
                    intentSlotUrlScore[intent][slot][url] = 0;
                }
                intentSlotUrlScore[intent][slot][url] += score;
            }
            srG.Close();
/*
            StreamReader srB = new StreamReader(infileBing);
            while ((line = srB.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 4)
                    continue;
                string intent = arr[0], slot = arr[1], url = arr[2];
                int score = int.Parse(arr[3]);
                if (!intentSlotUrlScore.ContainsKey(intent))
                {
                    intentSlotUrlScore[intent] = new Dictionary<string, Dictionary<string, double>>();
                }
                if (!intentSlotUrlScore[intent].ContainsKey(slot))
                {
                    intentSlotUrlScore[intent][slot] = new Dictionary<string, double>();
                }
                if (!intentSlotUrlScore[intent][slot].ContainsKey(url))
                {
                    intentSlotUrlScore[intent][slot][url] = 0;
                }
                intentSlotUrlScore[intent][slot][url] += score;
            }
            srB.Close();
*/
            StreamWriter sw = new StreamWriter(outfile);
            foreach(KeyValuePair<string, Dictionary<string, Dictionary<string, double>>> pair in intentSlotUrlScore)
            {
                string intent = pair.Key, slot;
                //string BestQueryPattern = "", BestQuery = "", GTopUrls = "", BTopUrls = "", ManuallySelected = "", GTopDomains = "", BTopDomains = "";
                foreach (KeyValuePair<string, Dictionary<string , double>> pairEle in pair.Value)
                {
                    slot = pairEle.Key;
                    List<KeyValuePair<string, double>> urlScoreList = pairEle.Value.ToList();
                    urlScoreList.Sort(Cmp);
                    foreach(KeyValuePair<string, double> pairUrlScore in urlScoreList)
                    {
                        string urlStr = pairUrlScore.Key;
                        string pszUrl = CURLUtilities.GetHutNormalizeUrl(urlStr.Trim()) ?? "";
                        string input = m_isMatchingHost ? CURLUtilities.GetHostNameFromUrl(pszUrl) : CURLUtilities.GetDomainNameFromUrl(pszUrl);
                        string remainUlr = urlStr.Substring(urlStr.IndexOf(input) + input.Length);
                        if(remainUlr == "/")
                        {
                            remainUlr = "/[^/]+/";
                        }
                        remainUlr = "/[^/]+";
                        sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", intent, slot, urlStr, remainUlr, pairUrlScore.Value);
                    }
                }
            }
            sw.Close();
        }
        public static void Run(string[] args)
        {
            string infileBing = @"D:\Project\Election\TokenAndRules\ElectionBingTopUrlScore.tsv";
            string infileGoogle = @"D:\Project\Election\TokenAndRules\ElectionGoogleTopUrlScore.tsv";
            string outfileMerge = @"D:\Project\Election\TokenAndRules\ElectionMergeTopUrlScore.tsv";
            MergeTopDomain(infileBing, infileGoogle, outfileMerge);
        }
    }
}
