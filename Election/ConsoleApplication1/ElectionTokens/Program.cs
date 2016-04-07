using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ElectionTokens
{
    class Program
    {
        public static void ProcessTokens(string infile, string outfile)
        {
            string line;
            StreamReader sr = new StreamReader(infile);
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t'); 
            }
            sr.Close();
        }
        static void Main(string[] args)
        {
            string infile = @"D:\Project\Election\TokenAndRules\ElectionTokens.tsv";
            string outfile = @"D:\Project\Election\TokenAndRules\TokensWatch.tsv";
            ProcessTokens(infile, outfile);
        }
    }
}
