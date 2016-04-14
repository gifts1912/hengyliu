using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.TestGram.Calculater
{
    class Calculater
    {
        public static void Run(string[] args)
        {
            string infile = @"D:\demo\watch.tsv";
            int sum = 0;
            string line;
            StreamReader sr = new StreamReader(infile);
           /* while((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                string[] arr = line.Split();
                int curNum = int.Parse(arr[0]);
                sum += curNum;
            }
            Console.WriteLine(sum);
            
            Console.ReadKey();
            */
            Dictionary<double, double> proScore = new Dictionary<double, double>();
            /*
            proScore.Add(0.0847, -1.034);
            proScore.Add(0.4004, -6.117);
            proScore.Add(0.1001, 1.122);
            proScore.Add(0.1034, -1.133);
            proScore.Add(0.0836, -1.804);
            proScore.Add(0.0737, 1.23);
            proScore.Add(0.0462, 0.022);
            proScore.Add(0.0429, -0.9);
            proScore.Add(0.0341, -0.12);
            proScore.Add(0.0143, 0.011);
            proScore.Add(0.0165, -0.209);
            */
            proScore.Add(0.0847, -0.88);
            proScore.Add(0.4004, -6.601);
            proScore.Add(0.1001, 1.65);
            proScore.Add(0.1034, -0.88);
            proScore.Add(0.0836, -1.98);
            proScore.Add(0.0737, 1.76);
            proScore.Add(0.0462, -0.11);
            proScore.Add(0.0429, -1.1);
            proScore.Add(0.0341, -0.275);
            proScore.Add(0.0143, -0.055);
            proScore.Add(0.0165, -0.275);
            double scoreSum = 0.0;
            foreach (KeyValuePair<double, double> pair in proScore)
            {
                double pro = pair.Key;
                double score = pair.Value;
                scoreSum += pro * score;
            }
            Console.WriteLine(scoreSum);
            Console.ReadKey();
            sr.Close();
        }
        
    }
}
