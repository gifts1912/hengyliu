using FrontEndUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
//using TSVUtility;


namespace Ranking.AppendTopSiteLabelFeature_ByQueryPatternV2
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
    public class Program
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
            /*
            string pszUrl = CURLUtilities.GetHutNormalizeUrl(url.Trim()) ?? "";
            string input = Program.m_isMatchingHost ? CURLUtilities.GetHostNameFromUrl(pszUrl) : CURLUtilities.GetDomainNameFromUrl(pszUrl);
            bool flag = false;
            if (Regex.IsMatch(input, tsi.topsite))
                flag = true;
            if (flag)
                flag = tsi.urlFilter.IsMatch(url);
            return tsi.keep && flag || !tsi.keep && !flag;
             */
        }

        private static string CalTopSiteLabel(string patternstring, string url)
        {

            List<string> patArr = new List<string>(patternstring.Split());
            patArr.Sort();
            string key = string.Join(" ", patArr.ToArray());
            if (!Program.m_topSiteTable.ContainsKey(key))
            {
                if (!Program.m_topSiteTable.ContainsKey("0"))
                    return "";
                key = "0";
            }
            foreach (TopSiteItem tsi in Program.m_topSiteTable[key])
            {
                if (Program.KeepUrl(url, tsi))
                {
                    return tsi.score;
                }
            }
            return "";
        }

        private static void Preprocessing(string pat2topsite, int keyCol)
        {
            using (StreamReader streamReader = new StreamReader(pat2topsite))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] strArray = streamReader.ReadLine().Split("\t".ToCharArray());
                    if (strArray.Length <= keyCol)
                    {
                        continue;
                    }
                    else 
                    {
                        string key = strArray[keyCol];
                        List<string> keyList = new List<string>(key.Split(new string [] {" "}, StringSplitOptions.RemoveEmptyEntries));
                        keyList.Sort();
                        string keySort = string.Join(" ", keyList.ToArray());

                        if (!Program.m_topSiteTable.ContainsKey(keySort))
                            Program.m_topSiteTable.Add(keySort, new List<TopSiteItem>());
                        string url = strArray[2];
                        url = url.Trim(new char[] { '/', ' ' });
                        Program.m_topSiteTable[keySort].Add(new TopSiteItem(url, strArray[3], strArray[4]));
                    }
                }
            }
        }

        public static void Run(string[] args)
        {
            if (args.Length != 6 && args.Length != 0)
            {
                Console.WriteLine("Usage: AppendTopSiteLabelFeature_ByQueryPatternV2.exe \r\n    {in:ExtractionGZ|ExtractionTSV|GenericTSV|GenericTSV_GZ:extraction} \r\n    {in:GenericTSV:Pat2DAScore} \r\n    {in:GenericTSV:Pat2Query} \r\n    {out:ExtractionGZ:result} \r\n    (IsMatchingHost:default,true) \r\n    (PatColumn:default,QLF999)");
            }
            else
            {
                if (args.Length == 0)
                {
                    args = new string[6];
                    args[0] = @"D:\demo\InputQuery.tsv";
                    args[1] = @"D:\demo\topSiteDebut.tsv";
                    args[2] = @"D:\demo\output.tsv";
                    args[3] = "false"; //IsMatchingHost
                    args[4] = "m:QueryPattern"; //PatColumn
                    args[5] = "1";
                }
                int keyCol = int.Parse(args[5]);
                Program.Preprocessing(args[1], keyCol);
                Program.m_isMatchingHost = args[3] == "true";
                string str1 = args[4]; //m:QueryPattern
                int num = 0;
                int index1 = -1;
                int index2 = -1;
                using (StreamReader streamReader = new StreamReader(args[0]))
                {
                    using (StreamWriter streamWriter = new StreamWriter(args[2]))
                    {
                        string line = streamReader.ReadLine();
                        string[] strArray = line.Split("\t".ToCharArray());
                        for (int index3 = 0; index3 < strArray.Length; ++index3)
                        {
                            if (strArray[index3] == "m:Url")
                                index1 = index3;
                            else if (strArray[index3] == str1)
                                index2 = index3;
                        }
                        streamWriter.WriteLine("TopSiteLabel\t" + line);
                        while (!streamReader.EndOfStream)
                        {
                            line = streamReader.ReadLine();
                            if (line != null)
                            {
                                string[] tsvLine = line.Split('\t');
                                string url = tsvLine[index1];
                                string str2 = Program.CalTopSiteLabel(tsvLine[index2], url);
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

