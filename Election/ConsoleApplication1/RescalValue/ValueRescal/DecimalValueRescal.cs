using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RescalValue.ValueRescal
{
    class DecimalValueRescal
    {
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[5];
                args[0] = @"D:\demo\HostUrlNum.tsv";
                args[1] = @"D:\demo\Output.tsv";
                args[2] = "1";
                args[3] = "1238";
                args[4] = "1";
            }

            string Infile = args[0];
            string Outfile = args[1];
            int valueIdx = int.Parse(args[2]);
            int maxValue = int.Parse(args[3]);
            int minValue = int.Parse(args[4]);

            RescalCall(Infile, Outfile, valueIdx, maxValue, minValue);
        }

        protected static void RescalCall(string Infile, string Outfile, int valueIdx, int maxValue, int minValue)
        {
            int max = 0, min = 10000;
            if(maxValue == -1 && minValue == -1)
            {
                using (StreamReader sr = new StreamReader(Infile))
                {
                    string line;
                    bool first = true;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] arr = line.Split('\t');
                        if (arr.Length <= valueIdx)
                            continue;
                        if (first)
                        {
                            max = int.Parse(arr[valueIdx]);
                            min = max;
                            first = false;
                            continue;
                        }

                        int num = int.Parse(arr[valueIdx]);
                        if (max < num)
                        {
                            max = num;
                        }
                        else if (min > num)
                        {
                            min = num;
                        }
                    }
                }
            }
            else
            {
                max = maxValue;
                min = minValue;
            }
           

            // scale specified num

            using (StreamReader sr = new StreamReader(Infile))
            {
                using (StreamWriter sw = new StreamWriter(Outfile))
                {
                    string line;
                    int num;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] arr = line.Split('\t');
                        if (arr.Length <= valueIdx)
                            continue;
                        num = int.Parse(arr[valueIdx]);
                        double valueScale = 255 * LinearRescal(num, max, min);
                        sw.WriteLine(string.Format("{0}\t{1}", line, (int)valueScale));
                    }
                }
            }
        }

        protected static double LinearRescal(int num, int max, int min)
        {
            return (num - min) * 1.0 / (max - min);
        }
    }
}
