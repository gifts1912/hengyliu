using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ElectionImprove.PE
{
    class PatternToPEId
    {
        private static int startIdx = 142;
        private static int colIdx = 0;
        
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\Project\Election\Improve\PEMining\PatternQueryTriggerNoQLF.tsv";
                args[1] = @"D:\Project\Election\Improve\PEMining\PatternToPEIdx_Dict.tsv";
                args[2] = "142";
                args[3] = "0";
            }
            string patQueryFile = args[0];
            string pat2PeIdFile = args[1];
            startIdx = int.Parse(args[2]);
            colIdx = int.Parse(args[3]);
            PatternToPEIdx(patQueryFile, pat2PeIdFile);
        }
        
        public static void PatternToPEIdx(string patQueryFile, string pat2PeIdFile)
        {
            HashSet<string> patternSet = new HashSet<string>();
            using (StreamReader sr = new StreamReader(patQueryFile))
            {
                string line, pat;
                line = sr.ReadLine();
                while((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    if (arr.Length <= colIdx)
                        continue;
                    pat = arr[colIdx].Trim();
                   // query = arr[1];
                    patternSet.Add(pat);
                }
            }

            using (StreamWriter sw = new StreamWriter(pat2PeIdFile))
            {
                int curIdx = startIdx;
                foreach(string pat in patternSet)
                {
                    sw.WriteLine(string.Format("{0}\t{1}", pat, curIdx++));
                }
            }
        }
    }
}