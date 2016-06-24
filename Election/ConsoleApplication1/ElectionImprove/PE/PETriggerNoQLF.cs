using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ElectionImprove.PE
{
    class PETriggerNoQLF
    {
        public static double scoreThread = 0.5;
        public static Dictionary<string, List<double>> patternUrlScores = new Dictionary<string, List<double>>();
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\Project\Election\Improve\PEMining\PETriggerWithoutQLF.tsv";
                args[1] =  @"D:\Project\Election\Improve\PEMining\PENew.tsv";
            }
            string QPatternScoreFile = args[0];
            string PEFile = args[1];
            PETriggerNoQLFFormat(QPatternScoreFile, PEFile);
            ScoreDistribute();
        }
        public static void PETriggerNoQLFFormat(string QPatternScoreFile, string PEFile)
        {
            Regex regex = new Regex(@"\\b([\w.]*)\\b$");
            using (StreamReader sr = new StreamReader(QPatternScoreFile))
            {
                using (StreamWriter sw = new StreamWriter(PEFile))
                {
                    string line, qpattern, url;
                    double score;
                    while((line = sr.ReadLine()) != null)
                    {
                        string[] arr = line.Split('\t');
                        score = double.Parse(arr[2]);
                        if (score < scoreThread)
                            continue;

                        qpattern = arr[0];
                        Match mc = regex.Match(qpattern);
                        if (!mc.Success)
                            continue;
                        qpattern = mc.Groups[1].Value;
                        if(!patternUrlScores.ContainsKey(qpattern))
                        {
                            patternUrlScores[qpattern] = new List<double>();
                        }
                        patternUrlScores[qpattern].Add(score);

                        score = Sigmod(score);
                        score *= 20;
                        sw.WriteLine(string.Format("{0}\t{1}\t{2}", qpattern, arr[1], (int)score));
                    }
                }
            }
        }

       public static void ScoreDistribute()
        {
            foreach(KeyValuePair<string, List<double>> pair in patternUrlScores)
            {
                string pat = pair.Key;
                double avg = Avg(pair.Value);
                Console.WriteLine("{0}\t{1}", pat, avg);
            }
            Console.ReadKey();
        }
        
        public static double Avg(List<double> values)
        {
            double res = 0.0;
            int num = 0;
            foreach(double v in values)
            {
                res += v;
                num++;
            }
            res = res / num;
            return res;
        }
        
        public static double Sigmod(double x)
        {
            double y = Math.Exp(-1.0 * x) + 1;
            y = 1 / y;
            return y;
        }
    }
}
