using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescallingValue
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\Input.tsv";
                args[1] = @"D:\demo\Output.tsv";
                args[2] = "2";
            }
            string infile = args[0];
            string Outfile = args[1];
            int valueIdx = int.Parse(args[2]);

            LinearRescalling(infile, Outfile, valueIdx);
        }
    }
}
