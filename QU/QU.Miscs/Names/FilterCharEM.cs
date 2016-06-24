using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMisc = Utility;

namespace QU.Miscs
{
    class FilterCharEM
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "in")]
            public string InFile;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string OutFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "dist")]
            public int MaxEditDist = 2;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            using (StreamWriter sw = new StreamWriter(arguments.OutFile))
            {
                using (StreamReader sr = new StreamReader(arguments.InFile))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length != 6)
                            continue;

                        string orig = items[0];
                        string corr = items[1];
                        if (IsValid(orig, corr, arguments.MaxEditDist))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }

        static bool IsValid(string orig, string corr, int maxEditDist)
        {
            foreach (char ch in orig)
            {
                if (ch < 'a' || ch > 'z')
                    return false;
            }

            foreach (char ch in corr)
            {
                if (ch < 'a' || ch > 'z')
                    return false;
            }

            int ed = MyMisc.SimilarQueryUtils.editDistance(orig, corr);
            if (ed > maxEditDist)
                return false;

            return true;
        }
    }
}
