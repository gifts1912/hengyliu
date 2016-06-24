using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace queryFormat
{
    class Program
    {
        public static void FormatFile(string infile, string outfile)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if(arr.Length == 1)
                {
                    sw.WriteLine("{0}\t\t", arr[0]);
                }
                else if(arr.Length == 3)
                {
                    sw.WriteLine(line);
                }
                else
                {
                    Console.WriteLine("error of line, {0}", line);
                    Console.ReadKey();
                }
            }
            sw.Close();
            sr.Close();
        }
        static void Main(string[] args)
        {
            string infile = @"D:\demo\BaseQuerySet.tsv";
            string outfile = @"D:\demo\BaseQuerySetFormat.tsv";
            FormatFile(infile, outfile);
        }
    }
}
