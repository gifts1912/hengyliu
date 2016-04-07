using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Ranking.TopSite;

namespace Ranking.QU.IntentSlotAnalysis
{
    class IntentSlotAnalysis
    {
        public static int patternCol = 1, intentCol = 3;
       
        public static void  SlotPatternAnalysis(string infile, string outfile, string intentFlag)
        {
            Dictionary<string, int> slotNum = new Dictionary<string, int>();
            StreamReader sr = new StreamReader(infile);
            string line, pattern, intent;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 5)
                    continue;
                pattern = arr[patternCol];
                intent = arr[intentCol];
                if (intent != intentFlag)
                    continue;
                pattern = Utility.Utility.NormalizationPatternSlot(pattern, intent);
                if(!slotNum.ContainsKey(pattern))
                {
                    slotNum[pattern] = 0;
                }
                slotNum[pattern] += 1;
            }
            sr.Close();
            List<KeyValuePair<string, int>> slotNumSortList = Utility.Utility.SortByValue(slotNum);
            //Display(slotNumSortList);
            //Console.ReadKey();
        }

        public static void Display(List<KeyValuePair<string, int>> slotNumList)
        {
            foreach(KeyValuePair<string, int> pair in slotNumList)
            {
                Console.WriteLine("{0}:{1}", pair.Key, pair.Value);
            }
        }
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\Project\Election\TokenAndRules\electionV1.2.tsv";
                args[1] = @"D:\Project\Election\TokenAndRules\candidateListPatternAnalysis.tsv";
                args[2] = "CandidateList";
            }
            string electionLabel = args[0];
            string outfile = args[1];
            string intent = args[2];
            SlotPatternAnalysis(electionLabel, outfile, intent);
        }

        
    }
}
