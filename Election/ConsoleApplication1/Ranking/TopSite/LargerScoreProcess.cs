using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.TopSite
{
    class LargerScoreProcess
    {
        public static void Run(string[] args)
        { 
            if (args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\TopSiteList.tsv";
                args[1] = @"D:\demo\TopSiteListNormalized.tsv";
            }
            string infile = args[0];
            string outfile = args[1];
            ProcessScoreLarger100(infile, outfile);
          
            /*
            string[] files = Directory.GetFiles(@"D:\Doctor\HotSite\", "*ForamtScore.tsv");
            foreach(string infile in files)
            {
                
                string outfile = infile.Substring(infile.LastIndexOf("\\") + 1);
                outfile =  @"D:\Doctor\HotSite\normalizedScore\ProcessLarger100" + outfile;
                ProcessScoreLarger100(infile, outfile);
                //Console.WriteLine("{0}\t{1}", infile, outfile);
            }*/
           // Console.ReadKey();
           // ProcessScoreLarger100(infile, outfile);

        }

        public static int MyCmp(Tuple<string, string, double> x, Tuple<string, string, double> y)
        {
            return y.Item3.CompareTo(x.Item3);
        }

        public static double normalize(double curScore, double maxScoreLess100)
        {
            curScore = curScore / 10000.0; 
            return maxScoreLess100 +  (100.0 - maxScoreLess100)*(1.0 / (1 + Math.Tanh(curScore)));
        }
        public static void ProcessScore(List<Tuple<string, string, double>> scoreList)
        {
            double maxScoreLess100 = 0.0;
            foreach(Tuple<string, string, double> ele in scoreList)
            {
                if (ele.Item3 < 100.0)
                {
                    maxScoreLess100 = ele.Item3;
                    break;
                }    
            }
            
            for(int i = 0; i < scoreList.Count; i++)
            {
                double curScore = scoreList[i].Item3;
                string hostUrl = scoreList[i].Item1;
                string rgxUrl = scoreList[i].Item2;
                if (curScore > 100)
                {
                    double normalScore = normalize(curScore, maxScoreLess100);
                   // Console.WriteLine("{0}\t{1}\t{2}", curScore, normalScore, maxScoreLess100);
                   // Console.ReadKey();
                    Tuple<string, string, double> tp = new Tuple<string,string, double>(hostUrl, rgxUrl, normalScore);
                    scoreList[i] = tp;
                }
            }
        }

        public static void Display(List<Tuple<string, string, double>> tupleList)
        {
            foreach (Tuple<string, string, double> ele in tupleList)
            {
                Console.WriteLine("{0}\t{1}\t{2}", ele.Item1, ele.Item2, ele.Item3);
            }
            Console.ReadKey();
        }
        public static void ProcessScoreLarger100(string infile, string outfile)
        {
            Dictionary<string, List<Tuple<string, string, double>>> patternHotSiteScore = new Dictionary<string, List<Tuple<string, string, double>>>();
            using (StreamReader sr = new StreamReader(infile))
            {
                using (StreamWriter sw = new StreamWriter(outfile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] arr = line.Split('\t');
                        if (arr.Length != 5)
                        {
                            sw.Write(line);
                            continue;
                        }

                        string pat = string.Format("{0}\t{1}", arr[0], arr[1]) ;
                        if (!patternHotSiteScore.ContainsKey(pat))
                        {
                            patternHotSiteScore[pat] = new List<Tuple<string, string, double>>();
                        }
                        Tuple<string, string, double> tp = new Tuple<string, string, double>(arr[2], arr[3], Convert.ToDouble(arr[4]));
                        patternHotSiteScore[pat].Add(tp);
                    }

                    foreach(KeyValuePair<string, List<Tuple<string, string, double>>> pair in patternHotSiteScore)
                    {
                        List<Tuple<string, string, double>> SiteValue = pair.Value;
                        SiteValue.Sort(MyCmp);
                      //  Display(SiteValue);
                        ProcessScore(SiteValue);
                        string pat = pair.Key;
                        foreach(Tuple<string, string, double> ele in SiteValue)
                        {
                            int score = Convert.ToInt32(ele.Item3);
                            sw.WriteLine("{0}\t{1}\t{2}\t{3}", pat, ele.Item1, ele.Item2, score);
                        }                      
                    }
                }
            }
        }
    }
}


