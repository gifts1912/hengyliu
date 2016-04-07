using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace IntentSlotGenerate
{
    class Program
    {
        public static Dictionary<string, string> slotRemapDic = new Dictionary<string, string>();
        public static int patCol = 1;
        public static void IntentAndSlot(string infile, string slotReMapFile, string outfile)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= patCol)
                    continue;
                string [] patArr = (arr[patCol]).Split();
                StringBuilder sb = new StringBuilder();
                foreach (string ele in patArr)
                {
                    if (ele.StartsWith("#") && ele.EndsWith("#"))
                    {
                        sb.Append(ele);
                    }
                    if()
                }
                     
            }
            sr.Close();


        }
        static void Main(string[] args)
        {
            string infile = @"D:\Project\Election\TokenAndRules\domainSlot.tsv";
            string slotReMapFile = @"D:\Project\Election\TokenAndRules\slotReMap.tsv";
            string outfile = @"D:\Project\Election\TokenAndRules\SPPatternIntentSlotPattern.tsv";

            IntentAndSlot(infile, slotReMapFile, outfile);
        }
    }
}
