using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.TriggerCoverageAnalysis
{
    class LossIntentAnalysis
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\ElectionQueryIntent.tsv";
                args[1] = @"D:\demo\LossQuerySet.tsv";
                args[2] = @"D:\demo\DolpinNotTriggerQuery.tsv";
                args[3] = @"D:\demo\QueryNotTriggerByPattern.tsv";
            }
            string queryIntent = args[0];
            string lossQuery = args[1];
            string notTriggerFile = args[2];
            string patternNoTrigFile = args[3];
            HashSet<string> spacePatternQuery = new HashSet<string>();
            StaticLossNum(queryIntent, lossQuery, notTriggerFile, ref spacePatternQuery);
            GrammaFileInfluenceNotTrigger(patternNoTrigFile, spacePatternQuery);
        }
        public static void  StaticLossNum(string queryIntentFile, string lossQueryFile, string notTriggerFile, ref HashSet<string> spacePat)
        {
            int num = 0, noTrigNum = 0;
            StreamReader sr = new StreamReader(queryIntentFile);
            StreamWriter sw = new StreamWriter(lossQueryFile);
            StreamWriter swNoTri = new StreamWriter(notTriggerFile);
            HashSet<string> lossIntents = new HashSet<string>(new string[] { "CandidateView", "CandidateList", "Candidate", "ElectionSchedule", "CandidateCampain"});
            string line;
            while((line = sr.ReadLine())!= null)
            {
                string[] arr = line.Split('\t');
                string intent = arr[3];
                if(lossIntents.Contains(intent))
                {
                    num++;
                    sw.WriteLine(line);
                    if(string.IsNullOrEmpty(arr[1].Trim()))
                    {
                        swNoTri.WriteLine(line);
                        spacePat.Add(arr[0]);
                        noTrigNum++;
                    }
                }

            }

            sw.Close();
            swNoTri.Close();
            sr.Close();
            Console.WriteLine(num);
            Console.WriteLine(noTrigNum);
            Console.ReadKey();
        }

        public static void GrammaFileInfluenceNotTrigger(string patternNotTriggerFile, HashSet<string> spacePatternQuery)
        {
            HashSet<string> noTriggerHS = new HashSet<string>();
            StreamReader sr = new StreamReader(patternNotTriggerFile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                noTriggerHS.Add(line.Trim());
            }
            sr.Close();

            HashSet<string> noTrigByGenerateGramma = new HashSet<string>();

        }
    }
}
