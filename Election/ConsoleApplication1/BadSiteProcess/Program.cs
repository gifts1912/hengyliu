using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace BadSiteProcess
{
    class Program
    {
        public static Dictionary<string, List<int>> UrlLossWinDic = new Dictionary<string, List<int>>();
        public static Dictionary<string, int> UrlFreq = new Dictionary<string, int>();
        public static void ReadUrlFreq(string infile)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string url = arr[0];
                int num = 0;
                try
                {
                    num = int.Parse(arr[1]);
                }
                catch(Exception e)
                {
                    continue;
                }
                
                if (!UrlFreq.ContainsKey(url))
                {
                    UrlFreq[url] = num;
                }
            }
            sr.Close();

        }

        public static void ProcessBadSite(string infile, string outfile)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Contains(".org") || line.Contains(".us")||line.Contains("wikia") || line.Contains("yahoo") || line.Contains(".uk")|| line.Contains("us"))
                {
                    sw.WriteLine("{0}\t1", line);
                    continue;
                }
                int num = 0;
                if(UrlFreq.ContainsKey(line))
                {
                    num = UrlFreq[line];
                }
                if(num <= 1)
                {
                    sw.WriteLine("{0}\t-1", line);
                }
                else if(num >= 10)
                {
                    sw.WriteLine("{0}\t1", line);
                }
                else
                {
                    sw.WriteLine("{0}\t0", line);
                }
            }

            sr.Close();
            sw.Close();

        }
        static void Main(string[] args)
        {
            /*string infileOccure = @"D:\demo\FilterUrlAndColumnList.tsv";
            string infile = @"D:\Project\Election\badSite.tsv";
            string outfile = @"D:\Project\Election\badSiteManualLabel.tsv";
            ReadUrlFreq(infileOccure);
            ProcessBadSite(infile, outfile);
             */
            string lossQueryFile = @"D:\Project\Election\ElectionLossQuery.tsv";
            string winQueryFile = @"D:\Project\Election\winQuerySet.tsv";
            string QueryFirstUrl = @"D:\Project\Election\BingQueryUrlScrap.tsv";
            string badSiteFile = @"D:\Project\Election\badSiteLossQuery.tsv";

            string outfile = @"D:\Project\Election\lossQueryBadSiteLabelV2.tsv";

            HashSet<string> lossQuerySet = new HashSet<string>();
            HashSet<string> winQuerySet = new HashSet<string>();
            ReadQueryFile(winQueryFile, winQuerySet);
            ReadQueryFile(lossQueryFile, lossQuerySet);

            Dictionary<string, string> queryFirstUrlDic = new Dictionary<string,string>();
            ReadUrlWinLoss(QueryFirstUrl, queryFirstUrlDic);

            UrlLabel(lossQuerySet, winQuerySet, queryFirstUrlDic, outfile);

            ScoreBadSite(badSiteFile, outfile);
            
        }
        public static void ScoreBadSite(string badSiteFile, string outfile)
        { //public static Dictionary<string, List<int>> UrlLossWinDic = new Dictionary<string, List<int>>();
            StreamReader sr = new StreamReader(badSiteFile);
            StreamWriter sw = new StreamWriter(outfile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if(UrlLossWinDic.ContainsKey(line))
                {
                    int losNum = UrlLossWinDic[line][0];
                    int winNum = UrlLossWinDic[line][1];
                    if(winNum >= 1 && losNum == 0)
                    {
                        sw.WriteLine("{0}\t1", line); 
                    }
                    else if(winNum == 0 && losNum >= 1)
                    {
                        sw.WriteLine("{0}\t-1", line);
                    }
                    else
                    {
                        sw.WriteLine("{0}\t0", line);
                    }
                }
                else
                {
                    sw.WriteLine("{0}\t0", line);
                }
            }

            sr.Close();
            sw.Close();
        }

        public static void UrlLabel(HashSet<string> lossQuerySet, HashSet<string> winQuerySet, Dictionary<string, string> queryFirstUrlDic, string outfile)
        {
            string outfile2 = @"D:\demo\querUrl.tsv";
            StreamWriter sw2 = new StreamWriter(outfile2);
            StreamWriter sw = new StreamWriter(outfile);
            //public static Dictionary<string, Tuple<int, int>> UrlLossWinDic = new Dictionary<string, Tuple<int, int>>();

            foreach(KeyValuePair<string, string> pair in queryFirstUrlDic)
            {
                string query = pair.Key;
                string url = pair.Value;
                sw2.WriteLine("{0}\t{1}", url, query);
                if(!UrlLossWinDic.ContainsKey(url))
                {
                    UrlLossWinDic[url] = new List<int>(new int[2]{0, 0});
                }
                if (lossQuerySet.Contains(query))
                    UrlLossWinDic[url][0] += 1;
                if (winQuerySet.Contains(query))
                    UrlLossWinDic[url][1] += 1;
            }
            //Display();
            sw.Close();
            sw2.Close();
        }

        public static void Display()
        {
            foreach(KeyValuePair<string, List<int>> pair in UrlLossWinDic)
            {
                Console.WriteLine("{0}\t{1}\t{2}", pair.Key, pair.Value[0], pair.Value[1]);
            }
            Console.ReadKey();
        }
        public static void ReadUrlWinLoss(string infile, Dictionary<string, string> queryUrlDic)
        {
            Regex rgx = new Regex(@"http(s)?://(www\.)?([^/]+)");
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Trim().Split('\t');
                string url = arr[2];
                Match mc = rgx.Match(url);
                url = mc.Groups[3].Value.ToString();
                if(!queryUrlDic.ContainsKey(arr[1]))
                {
                    queryUrlDic[arr[1]] = url;
                }           
            }
            sr.Close();
            // Display(queryUrlDic);
        }

        public static void Display(Dictionary<string, string> dic)
        {
            foreach(KeyValuePair<string, string> pair in dic)
            {
                Console.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            Console.ReadKey();
        }
        public static void ReadQueryFile(string file, HashSet<string> hs)
        {
            StreamReader sr = new StreamReader(file);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if(String.IsNullOrEmpty(line))
                    continue;
                hs.Add(line);
            }
            sr.Close();
        }

       
    }
}
