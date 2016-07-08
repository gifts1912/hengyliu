using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QueryPartialLabel
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\SelectQueryTriggerMalta.tsv";
           //     args[0] = @"D:\demo\TriggeredMalta200KQuerySet.tsv";
                args[2] = @"D:\demo\QueryNeedScrape200K.tsv";
                args[1] = @"D:\demo\QueryPartialTriggerLabelled.tsv";
            }
            string infile = args[0];
            string outfile = args[1];
            string scrapefile = args[2];

            //GenerateScrapteFile(infile, scrapefile);
            LabelProcess(infile, outfile);
        }
        public static void LabelProcess(string infile, string outfile)
        { 
            StreamReader sr = new StreamReader(infile);
            string line;
            List<string> rawData = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                rawData.Add(line);
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(outfile);
            string url, trigQuery, judgeQuery;
            bool quite = false;
            int i = 0;
            int cur = 1;
            Console.WriteLine("*******************Should trigger(+1)/Not should trigger(-1)/not sure(0)/ q for quite/n for next case*******************");
            for (i = 0; i < rawData.Count; i++)  
            {
                line = rawData[i];
                string[] arr = line.Split('\t');
                url = arr[0];
                trigQuery = arr[1];
                judgeQuery = arr[3];
                Console.WriteLine("*******************{0}*******************", cur);
                cur++;
                Console.WriteLine(url);
                foreach (string query in trigQuery.Split(';'))
                {
                    Console.WriteLine(query);
                }

                Console.WriteLine("*********************Label Query*******************");
                foreach (string jq in judgeQuery.Split(';'))
                {
                    Console.WriteLine("{" +jq +"} :");
                    string score = Console.ReadLine();
                    if(score == "q")
                    {
                        quite = true;
                        break;
                    }
                    else if(score == "n")
                    {
                        break;
                    }
                    else 
                        sw.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", url, trigQuery, jq, score));
                }
                if (quite)
                    break;
            }
            sw.Close();

            sw = new StreamWriter(infile);
            for (; i < rawData.Count; i++)
            {
                sw.WriteLine(rawData[i]);
            }
            sw.Close();
        }
       public static void GenerateScrapteFile(string infile, string scrapefile)
        {
            HashSet<string> queryScrape = new HashSet<string>();
            StreamReader sr = new StreamReader(infile);
            string line, triggered, judgeTriggered;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                triggered = arr[1];
                judgeTriggered = arr[3];
                queryScrape.UnionWith(new HashSet<string>(triggered.Split(';')));
                queryScrape.UnionWith(new HashSet<string>(judgeTriggered.Split(';')));
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(scrapefile);
            foreach(string query in queryScrape)
            {
                sw.WriteLine(query);
            }
            sw.Close();
        }
    }
}
