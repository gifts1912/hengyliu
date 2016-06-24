using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs.MagicQ
{
    public class ConvertExtraction4Ranking
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "extraction")]
            public string Extraction = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string outFile;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }
        }
    }
}
