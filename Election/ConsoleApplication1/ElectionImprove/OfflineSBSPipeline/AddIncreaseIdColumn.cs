using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.OfflineSBSPipeline
{
    class AddIncreaseIdColumn
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\demo\input.tsv";
                args[1] = "1000";
                args[2] = "1";
                args[3] = @"D:\demo\output.tsv";
            }
            string infile = args[0];
            string outfile = args[3];
            int startIdx = int.Parse(args[1]);
            int step = int.Parse(args[2]);
            addColumn(infile, outfile, startIdx, step);  
        }

        public static void addColumn(string infile, string outfile, int startidx, int step)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                sw.WriteLine(string.Format("{0}\t{1}", line, startidx));
                startidx += step;
            }
            sr.Close();
            sw.Close();
        }
    }
}
