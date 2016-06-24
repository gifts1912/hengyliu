using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs.Misc
{
    public class AdjustMLGFeatureWeight
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "in")]
            public string InFile;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string OutFile;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            //System.Text.RegularExpressions.Regex.IsMatch(args[0], "MAGIC_E_drug.*MAGIC_E_drug")
            System.Text.RegularExpressions.Regex.Replace(args[0], @"((^|\s)\d+){1,3}\s?(mg|g|ml|mcg)|\d+\s\d+", "SLOT_Dosage");
            "test:0,test2:1".Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split(':')[0]);
       }
    }
}
