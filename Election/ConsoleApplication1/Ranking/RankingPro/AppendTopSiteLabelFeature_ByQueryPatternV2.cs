using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEndUtil;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.RankingPro.AppendTopSiteLabelFeature_ByQueryPatternV2
{

    internal struct TopSiteItem
    {
        public string topsite;
        public Regex urlFilter;
        public string score;
        public bool keep;

        public TopSiteItem(string domainname, string filteringstring, string scorestring)
        {
            this.topsite = domainname;
            this.keep = true;
            if (filteringstring.StartsWith("drop:"))
            {
                this.urlFilter = new Regex(filteringstring.Substring(5), RegexOptions.IgnoreCase);
                this.keep = false;
            }
            else
                this.urlFilter = new Regex(filteringstring, RegexOptions.IgnoreCase);
            this.score = scorestring;
        }
    }
    public class AppendTopSiteLabelFeature_ByQueryPatternV2
    {
        private static Dictionary<string, List<TopSiteItem>> m_topSiteTable = new Dictionary<string, List<TopSiteItem>>();
        private static bool m_isMatchingHost = true;

        private static bool KeepUrl(string url, TopSiteItem tsi)
        {
            url = url.Trim(new char[] { ' ', '/' });
            if (url == tsi.topsite)
                return true;
            else
            {
                return false;
            }
        }

        private static string CalTopSiteLabel(string key, string url)
        {
            if (!m_topSiteTable.ContainsKey(key))
            {
                if (!m_topSiteTable.ContainsKey("0"))
                    return "";
                key = "0";
            }
            foreach (TopSiteItem tsi in m_topSiteTable[key])
            {
                if (KeepUrl(url, tsi))
                {
                    return tsi.score;
                }
            }
            return "";
        }

        public static string SortValue(string value)
        {
            List<string> valueList = value.Split().ToList();
            valueList.Sort();
            return string.Join(" ", valueList.ToArray());
        }
        public static string GenerateSortKey(string[] valueArr, List<int> keyCol, List<int> keySortCol)
        {
            StringBuilder key = new StringBuilder();
            bool begin = true;
            foreach(int idx in keyCol)
            {
                
                if (begin)
                {
                    begin = false;
                }
                else
                {
                    key.Append("\t");
                }
                string value = valueArr[idx];
                if(keySortCol.IndexOf(idx) != -1)
                {
                    value = SortValue(value);
                }
                key.Append(value);              
            }
            return key.ToString();
        }
        private static void Preprocessing(string pat2topsite, string keyDef = "0+1", string keySortDef = "1")
        {
            List<int> keyCol = new List<int>();
            List<int> keySortCol = new List<int>();
            int maxCol = 0;

            foreach (string colStr in keyDef.Split('+'))
            {
                int col = -1;
                try
                {
                    col = int.Parse(colStr.Trim());
                }
                catch (Exception e)
                {
                    return;
                }
                keyCol.Add(col);
            }

            foreach (string colSortStr in keySortDef.Split('+'))
            {
                int col = -1;
                try
                {
                    col = int.Parse(colSortStr);
                }
                catch (Exception e)
                {
                    return;
                }
                if (maxCol < col)
                {
                    maxCol = col;
                }
                keySortCol.Add(col);
            }

            using (StreamReader streamReader = new StreamReader(pat2topsite))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] strArray = streamReader.ReadLine().Split("\t".ToCharArray());
                    if (strArray.Length <= maxCol)
                    {
                        continue;
                    }

                    string keySort = GenerateSortKey(strArray, keyCol, keySortCol);
                    if (!m_topSiteTable.ContainsKey(keySort))
                    {
                        m_topSiteTable.Add(keySort, new List<TopSiteItem>());
                    }                  
                    string url = strArray[2];
                    url = url.Trim(new char[] { '/', ' ' });
                    m_topSiteTable[keySort].Add(new TopSiteItem(url, strArray[3], strArray[4]));

                }
            }
        }

        public static void Run(string[] args)
        {
            /*
             * Get the top site list through the combination of "intent + slotPattern";
             */
            if (args.Length != 6 && args.Length != 0)
            {
                Console.WriteLine("Usage: AppendTopSiteLabelFeature_ByQueryPatternV2.exe \r\n    {in:ExtractionGZ|ExtractionTSV|GenericTSV|GenericTSV_GZ:extraction} \r\n    {in:GenericTSV:Pat2DAScore} \r\n    {in:GenericTSV:Pat2Query} \r\n    {out:ExtractionGZ:result} \r\n    (IsMatchingHost:default,true) \r\n    (PatColumn:default,QLF999)");
            }
            else
            {
                if (args.Length == 0)
                {
                    args = new string[6];
                    args[0] = @"D:\demo\QueryInfo.tsv";
                    args[1] = @"D:\demo\TopSite.tsv";
                    args[2] = @"D:\demo\QueryInfoTopSite.tsv";
                    args[3] = "false"; //IsMatchingHost
                    args[4] = "m:QueryIntent+m:QueryPattern"; //PatColumn
                    args[5] = "0+1"; // the key is the value combination of 0th and 1th column.
                }
                string keyColCombination = args[5];
                Preprocessing(args[1], keyColCombination, "1");
                m_isMatchingHost = args[3] == "true";
                string keyFeature = args[4]; //m:QueryPattern
                string keySortFeature = "m:QueryPattern";
                int queryIdx = -1;

                List<int> keyCol = new List<int>();     
                List<int> keySortCol = new List<int>();

                int urlIdx = -1, num = 0;
              
                using (StreamReader streamReader = new StreamReader(args[0]))
                {
                    using (StreamWriter streamWriter = new StreamWriter(args[2]))
                    {
                        string line = streamReader.ReadLine();
                        string[] strArray = line.Split("\t".ToCharArray());
                        urlIdx = Array.IndexOf(strArray, "m:Url");
                        queryIdx = Array.IndexOf(strArray, "m:Query");
                        foreach(string fea in keyFeature.Split('+'))
                        {
                            int midCol = Array.IndexOf(strArray, fea);
                            if (midCol == -1)
                                return;
                            keyCol.Add(midCol);
                        }
                        foreach(string sortFea in keySortFeature.Split('+'))
                        {
                            int midCol = Array.IndexOf(strArray, sortFea);
                            if (midCol == -1)
                                return;
                            keySortCol.Add(midCol);
                        }
                        streamWriter.WriteLine("TopSiteLabel\t" + line);

                        while (!streamReader.EndOfStream)
                        {
                            line = streamReader.ReadLine();
                            if (line != null)
                            {
                                string[] tsvLine = line.Split('\t');
                                string url = tsvLine[urlIdx];
                                string keyStr = GenerateSortKey(tsvLine, keyCol, keySortCol);
                                string str2 = CalTopSiteLabel(keyStr, url);
                                streamWriter.WriteLine(str2 + "\t" + line);

                                if (++num % 10000 == 0)
                                    Console.WriteLine("{0} processed!", (object)num);
                            }
                        }
                    }
                }
            }
        }
    }
}
