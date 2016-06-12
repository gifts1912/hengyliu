using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.QAS
{
    class CoverageCompute
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\sumStoneTemplate\electionqas\Query2IntentIdFeatureIds.output.txt";
                args[1] = @"D:\sumStoneTemplate\electionqas\score.tsv";
            }
            string idRangeFile = args[0];
            string coveFile = args[1];
            Coverage(idRangeFile, coveFile);
        }
        
        public static void Coverage(string idRangeFile, string scoreFile)
        {
            StreamReader sr = new StreamReader(idRangeFile);
            string line;
            HashSet<string> idRangeSet = new HashSet<string>();
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string key = arr[0];
                if(key.StartsWith("CandidateView"))
                {
                    idRangeSet.Add(arr[4]);
                }
            }
            sr.Close();

            sr = new StreamReader(scoreFile);
            int num = 0;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if(idRangeSet.Contains(arr[1]))
                {
                    num++;
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine(num);
            Console.ReadKey();
            sr.Close();
        }
    }
}
