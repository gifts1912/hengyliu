using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Others
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length  == 0)
            {
                args = new string[2];
            }
            string[] cmdArgs = args.Skip(1).ToArray();
            args[0] = "ParseFormatFile.ParseJsonFile";

            if(args[0].ToLower().Equals("parseformatfile.parsejsonfile"))
            {
                ParseFormatFile.ParseJsonFile.Run(cmdArgs);
            }

            /*if (args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\infile.tsv";
                args[1] = @"D:\demo\outfile.tsv";
            }
            string infile = args[0];
            string outfile = args[1];
            Format(infile, outfile);
            */
        }
        
        public static void Format(string infile, string outfile)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
           // sw.WriteLine("Query\tFrequency\tCity\tState\tCountry\tLat\tLong");
            string line = sr.ReadLine();
          //  sw.WriteLine(line);
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string QueryFormat = string.Format("[Lat:{0}][Long:{1}][Town:", arr[5], arr[6]) + "{" + arr[3] + "},{" + arr[4] + "}][PostCode:1111]" + arr[0];
                arr[0] = QueryFormat;
                if (arr.Length != 7)
                    continue;
                string result = string.Format("{0}\t\t{1}", QueryFormat, arr[1]);
                sw.WriteLine(result);
            }
            sw.Close();
            sr.Close();
        }
    }
}
