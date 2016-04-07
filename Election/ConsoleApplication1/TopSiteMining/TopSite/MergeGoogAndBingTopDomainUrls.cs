using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TopSiteMining.TopSite
{
    class MergeGoogAndBingTopDomainUrls
    {

        public static void MergeTopDomain(string infileBing, string infileGoogle, string outfile)
        {
            Dictionary<string, Dictionary<string, Dictionary<string, int>>> intentSlotUrlScore = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

            StreamReader srG = new StreamReader(infileGoogle);
            string line;
            int gDomainCol = 2, bDomainCol = 2;

            while((line = srG.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string intent = arr[0], slot = arr[1];
                if(!intentSlotUrlScore.ContainsKey(intent))
                {
                    intentSlotUrlScore[intent] = new Dictionary<string,Dictionary<string,int>>();
                }
                if(!intentSlotUrlScore[intent].ContainsKey(slot))
                {
                    intentSlotUrlScore[intent][slot] = new Dictionary<string, int>();
                }
                if(!intentSlotUrlScore[intent][slot].ContainsKey(in)
                intentSlotUrlScore[intent][slot][0] = arr[gDomainCol];
            }
            srG.Close();

            StreamReader srB = new StreamReader(infileBing);
            while ((line = srB.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string intent = arr[0], slot = arr[1];
                if (!intentSlotUrlScore.ContainsKey(intent))
                {
                    intentSlotUrlScore[intent] = new Dictionary<string, List<string>>();
                }
                if (!intentSlotUrlScore[intent].ContainsKey(slot))
                {
                    intentSlotUrlScore[intent][slot] = new List<string>(new string[3] { "", "", ""});
                }
                intentSlotUrlScore[intent][slot][1] = arr[bDomainCol];
            }
            srB.Close();

            StreamWriter sw = new StreamWriter(outfile);
            foreach(KeyValuePair<string, Dictionary<string, List<string>>> pair in intentSlotUrlScore)
            {
                string intent = pair.Key, slot;
                string BestQueryPattern = "", BestQuery = "", GTopUrls = "", BTopUrls = "", ManuallySelected = "", GTopDomains = "", BTopDomains = "";
                foreach (KeyValuePair<string, List<string>> pairEle in pair.Value)
                {
                    slot = pairEle.Key;
                    GTopDomains = pairEle.Value[0];
                    BTopDomains = pairEle.Value[1];
                    HashSet<string> hsTopDomain = new HashSet<string>(pairEle.Value[0].Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries));
                    hsTopDomain.UnionWith(new HashSet<string> (BTopDomains.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries)));
                    string mergeTopUrl = string.Join(",", hsTopDomain.ToArray()).Trim(',');
                    string result = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", intent, slot, GTopDomains.Trim(','), BTopDomains.Trim(','), mergeTopUrl);
                    sw.WriteLine("{0}",result);                  
                }
            }
            sw.Close();
        }
        public static void Run(string[] args)
        {
            string infileBing = @"D:\Project\Election\TokenAndRules\IntentPatternLayerTopDomainUrlScoreBing.tsv";
            string infileGoogle = @"D:\Project\Election\TokenAndRules\IntentPatternLayerTopDomainUrlScoreGoogle.tsv";
            string outfileMerge = @"D:\Project\Election\TokenAndRules\IntentPatternLayerTopDomainUrlScoreMerge.tsv";
            MergeTopDomain(infileBing, infileGoogle, outfileMerge);
        }
    }
}
