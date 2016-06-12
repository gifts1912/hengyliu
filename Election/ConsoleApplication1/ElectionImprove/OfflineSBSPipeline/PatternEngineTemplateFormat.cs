using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.OfflineSBSPipeline
{
    class PatternEngineTemplateFormat
    {
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\petest.tsv";
                args[1] = @"D:\demo\watch.tsv";
            }
            string infile = args[0];
            string outfile = args[1];
            FormatPE(infile, outfile);
        }

        public static void FormatPE(string infile, string outfile)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                StringBuilder sb = new StringBuilder();
                string[] arr = line.Split('\t');
                sb.Append(string.Format("{0}", int.Parse(arr[0])));
                sb.Append('\t');
                sb.Append(arr[1]);
                sb.Append("\t");
                sb.Append(@"/[^/]+");
                sb.Append('\t');
                sb.Append(arr[2]);
                sw.WriteLine(sb.ToString());
            }
            sw.Close();
            sr.Close();

        }
    }
}
