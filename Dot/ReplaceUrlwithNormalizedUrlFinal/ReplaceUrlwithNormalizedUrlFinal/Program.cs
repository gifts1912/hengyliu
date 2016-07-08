using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FrontEndUtil;

namespace ReplaceUrlwithNormalizedUrlFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\test.tsv";
                args[1] = @"D:\demo\watch.tsv";
                args[2] = "9";
            }
             string inputTsv = args[0];
             string outputTsv = args[1];

            int urlColumn = int.Parse(args[2]);

            using (StreamReader sr = new StreamReader(inputTsv))
            using (StreamWriter sw = new StreamWriter(outputTsv))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split('\t');
                    string url = tokens[urlColumn];
                    string normalizedUrl = FrontEndUtil.CURLUtilities.GetHutNormalizeUrl(url);
                    tokens[urlColumn] = normalizedUrl;

                    sw.WriteLine(string.Join("\t", tokens));
                }
            }
        }
    }
}
