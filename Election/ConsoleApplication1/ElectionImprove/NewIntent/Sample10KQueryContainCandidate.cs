using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.NewIntent
{
    class Sample10KQueryContainCandidate
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\Project\Election\Improve\ElectionCandidateEntityTriggeredQuerySet.tsv";
                args[1] = @"D:\Project\Election\Improve\Sample10KElectionCandidateEntityTriggeredQuerySet.tsv";
                args[2] = "10000";
            }
            string originalFile = args[0];
            string sampleFile = args[1];
            int sampleNum = int.Parse(args[2]);
            RandomSample(originalFile, sampleFile, sampleNum);
        }
        
        public static void RandomSample(string originalFile, string sampleFile, int sampleNum = 10000)
        {
            List<string> rows = new List<string>();
            StreamReader sr = new StreamReader(originalFile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string query = arr[0];
                rows.Add(query);
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(sampleFile);
            Random random = new Random();
            for(int i = 0; i < sampleNum; i++)
            {
                int idx = random.Next(0, rows.Count);
                string query = rows[idx];
                sw.WriteLine(query);
                rows.RemoveAt(idx);
            }
            sw.Close();

        }
    }
}
