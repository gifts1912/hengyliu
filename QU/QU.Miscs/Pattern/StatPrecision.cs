using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs
{
    class StatPrecision
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "prec")]
            public string PrecisionFile = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.PrecisionFile))
            {
                Console.WriteLine("No Precision File!");
                return;
            }

            int[] buckets = new int[20];

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.PrecisionFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length < 4)
                            continue;

                        double prec = double.Parse(items[3]);
                        int id = (int)(prec * 100) / 5;
                        id = Math.Min(id, 19);
                        buckets[id]++;
                    }
                }

                sw.WriteLine("Precision\tPatterns");
                for (int i = 0; i < buckets.Length; i++)
                {
                    sw.WriteLine("[{0},{1})\t{2}", i * 5, (i + 1) * 5, buckets[i]);
                }
            }
        }
    }
}
