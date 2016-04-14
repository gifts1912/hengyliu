using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.PipelineChange.TurnAddFeatureModule
{
    class TurnAddFeatureModule
    {
        public static void ProcessLastNullColDel(string infile, string outfile)
        {
            using (StreamReader sr = new StreamReader(infile))
            {
                using (StreamWriter sw = new StreamWriter(outfile))
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        string[] arr = line.Split('\t');
                        if(arr.Length == 59)
                        {
                            sw.WriteLine("{0}\t", line);
                        }
                        else
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\input.tsv";
                args[1] = @"D:demo\Output.tsv";
            }
            string infile = args[0];
            string outfile = args[1];
            ProcessLastNullColDel(infile, outfile);
        }
    }
}
