using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ElectionImprove.PE
{
    class PEReplacePatternWithPEId
    {
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\Project\Election\Improve\PEMining\PatternToPEIdx_Dict.tsv";
                args[1] = @"D:\Project\Election\Improve\PEMining\PENew.tsv";
                args[2] = @"D:\Project\Election\Improve\PEMining\PEIdUrlScore.tsv";
            }
            string pat2PeidxFile = args[0];
            string patUrlScoreFile = args[1];
            string peidUrlScoreFile = args[2];
            Pat2PEid(pat2PeidxFile, patUrlScoreFile, peidUrlScoreFile);
        }
        public static void Pat2PEid(string pat2PeidxFile, string patUrlScoreFile, string peidUrlScoreFile)
        {
            Dictionary<string, string> pat2PeidDic = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(pat2PeidxFile))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string [] arr = line.Split('\t');
                    if (arr.Length != 2)
                        continue;
                    pat2PeidDic[arr[0]] = arr[1];                    
                }
            }

            bool flag = true;
            using (StreamReader sr = new StreamReader(patUrlScoreFile))
            {
                using (StreamWriter sw = new StreamWriter(peidUrlScoreFile))
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        string[] arr = line.Split('\t');
                        string pat = arr[0];
                        if(!pat2PeidDic.ContainsKey(pat))
                        {
                            Console.WriteLine("No mapping: {0}", pat);
                            flag = false;
                        }
                        sw.WriteLine("{0}\t{1}\t{2}", pat2PeidDic[pat], arr[1], arr[2]);
                    }
                }
            }
            if(!flag)
            {
                Console.ReadKey();
            }
        }
    }
}