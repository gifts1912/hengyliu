using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TopSiteMining.TopSite
{
    class TopUrlsFormat
    {
        public static void RankingFormat(string infile, string outfile)
        {
            Dictionary<string, Dictionary<string, List<string>>> slotPatternUrlList = new Dictionary<string, Dictionary<string,List<string>>>();
            int mergeUrlCol = 3, slotPatCol = 1, intentCol = 0;
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            string line, intent, slotPat, mergeUrl;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split(new string[] {"\t"}, StringSplitOptions.RemoveEmptyEntries);
                if(arr.Length <= mergeUrlCol || arr.Length <= slotPatCol || arr.Length <= intentCol)
                {
                    continue;
                }
                intent = arr[intentCol];
                slotPat = arr[slotPatCol];
                mergeUrl = arr[mergeUrlCol];
                if(string.IsNullOrEmpty(mergeUrl))
                    continue;
                string [] mergeUrlArr =  mergeUrl.Split(',');
                foreach(string urlEle in mergeUrlArr)
                {
                    if (string.IsNullOrEmpty(urlEle))
                        continue;
                    string row = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", intent, slotPat, urlEle, "/[^/]+", 70);
                    sw.WriteLine(row);
                }
            }

            sr.Close();
            sw.Close();
        }
        public static void Run(string []args)
        {
            string infile = @"D:\Project\Election\TokenAndRules\IntentPatternLayerTopDomainUrlScoreMerge.tsv";
            string outfile = @"D:\Project\Election\TokenAndRules\IntentPatternLayerTopDomainUrlScoreMergeRankingFormat.tsv";
            RankingFormat(infile, outfile);
        }
    }
}
