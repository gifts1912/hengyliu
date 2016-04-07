using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        public static void ProcessToLower(string infile, string outfile)
        {
            StreamWriter sw = new StreamWriter(outfile);
            using (StreamReader sr = new StreamReader(infile))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    line = line.Trim().ToLower();
                    sw.WriteLine(line);
                }
            }
            sw.Close();
        }
        static void Main(string[] args)
        {
            string infile = @"D:\Project\Election\QuerySet\us_states.tsv";
            string outfile = @"D:\Project\Election\QuerySet\us_statesNew.tsv";
            ProcessToLower(infile, outfile);
        }
    }
}
