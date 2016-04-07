using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace LossQuerySelect
{
    class Program
    {
        public static void SelectLossQuerySet(string infile, string outfile)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            string line;
            line = sr.ReadLine();
            int num = 0 ;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split(',');
                Console.WriteLine(arr.Length);
                int score = Int32.Parse(arr[5]);
                if(score < 0)
                {
                    num++;
                    sw.WriteLine(line);
                }
            }
            sr.Close();
            sw.Close();
            Console.WriteLine("Negative number is: {0}", num);
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            string infile = @"D:\Project\Election\QuerySet\EntityV1.1\ElectionQuerySet.csv";
            string outfile = @"D:\Project\Election\QuerySet\EntityV1.1\ElectionLossQuery.tsv";
            SelectLossQuerySet(infile, outfile);
        }
    }
}
