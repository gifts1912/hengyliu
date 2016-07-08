using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FrontEndUtil;

namespace ElectionImprove.SBSAnalysis
{
    class RepresentiveCaseSelect
    {
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\ElectionImproveV3.0GVSBAJudgeFinally.tsv";
                args[1] = @"D:\demo\Out.tsv";
            }
            string InputTSV = args[0];
            string OutputTSV = args[1];

            SelectRepresentiveCase(InputTSV, OutputTSV);
        }
        public static void SelectRepresentiveCase(string InputTSV, string OutputTSV)
        {
            HashSet<string> querySet = new HashSet<string>();
            StreamReader sr = new StreamReader(InputTSV);
            string line, query;
            line = sr.ReadLine();
            while((line = sr.ReadLine()) != null)
            {
                HashSet<string> GUrlHs = new HashSet<string>();
                HashSet<string> BUrlHs = new HashSet<string>();
                string[] arr = line.Split('\t');
                query = arr[1];
                double score = double.Parse(arr[5]);
                if (score >= -1)
                    continue;
                for(int i = 25; i < 27; i++)
                {
                    string urlL = arr[i];
                    urlL = NormalizeUrl(urlL);
                //    GUrlHs.Add(arr[i].Trim('/'));
                    GUrlHs.Add(urlL);
                }
                for(int i = 35; i < 44; i++)
                {
                    string urlR = arr[i];
                    urlR = NormalizeUrl(urlR);
                   // BUrlHs.Add(arr[i].Trim('/'));
                    BUrlHs.Add(urlR);
                }

                bool repre = QueryRepresentativeJudge(BUrlHs, GUrlHs);
                if(repre)
                {
                    querySet.Add(query);
                }
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(OutputTSV);
            foreach(string q in querySet)
            {
                sw.WriteLine(q);
            }
            sw.Close();
        }

        public static string NormalizeUrl(string url)
        {
            string urlNorm = FrontEndUtil.CURLUtilities.GetHutNormalizeUrl(url);
            return urlNorm;
        }
        public static bool QueryRepresentativeJudge(HashSet<string> BUrlHs, HashSet<string> GUrlHs)
        {
            foreach(string url in GUrlHs)
            {
                if(!BUrlHs.Contains(url))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
