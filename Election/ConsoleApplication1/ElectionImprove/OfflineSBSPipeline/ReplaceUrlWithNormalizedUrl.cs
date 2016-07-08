using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEndUtil;
using System.IO;

namespace ElectionImprove.OfflineSBSPipeline
{
    class ReplaceUrlWithNormalizedUrl
    {
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\test.tsv";
                args[1] = @"D:\demo\wath.tsv";
                args[2] = "9";
            }
            string infile = args[0];
            string outfile = args[1];
            int col = int.Parse(args[2]);
            ReplaceUrlWithNormalizedUrl_Run(infile, outfile, col);
        }
        public static void ReplaceUrlWithNormalizedUrl_Run(string inputTsv, string outputTsv, int urlColumn)
        {
            using (StreamReader sr = new StreamReader(inputTsv))
            using (StreamWriter sw = new StreamWriter(outputTsv))
            {
                string line;
                line = sr.ReadLine();
                sw.WriteLine(line);
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
