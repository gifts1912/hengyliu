using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ElectionImprove.PE
{
    class TrimColumn
    {
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\demo\PatUrlScore.tsv";
                args[1] = @"D:\demo\PatTrimUrlScore.tsv";
                args[2] = "0";
                args[3] = "\\b";
            }
            string infile = args[0];
            string outfile = args[1];
            int colIdx = int.Parse(args[2]);
            string trimStr = args[3];
            TrimColumnRun(infile, outfile, colIdx, trimStr);
        }
        public static void TrimColumnRun(string infile, string outfile, int colIdx, string trimStr)
        {
            using (StreamReader sr = new StreamReader(infile))
            {
                using (StreamWriter sw = new StreamWriter(outfile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] arr = line.Split('\t');
                        if (arr.Length <= colIdx)
                            continue;
                        string pat = arr[colIdx];
                        if (pat.StartsWith(trimStr))
                        {
                            pat = pat.Substring(trimStr.Length);
                        }
                        if (pat.EndsWith(trimStr))
                        {
                            pat = pat.Substring(0, pat.Length - trimStr.Length);
                        }
                        arr[colIdx] = pat;
                        sw.WriteLine(string.Join("\t", arr));
                    }

                }
            }
        }
    }
}
