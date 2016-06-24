using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.others
{
    class SubStringTest
    {
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[1];
                args[0] = @"D:\demo\test.tsv";
            }
            string infile = args[0];

            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string [] arr = line.Split('\t');
                Console.WriteLine(arr[1].Substring(33));
            }
            Console.ReadKey();
            sr.Close();
        }
    }
}
