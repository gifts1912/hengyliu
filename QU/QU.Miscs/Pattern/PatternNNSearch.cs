using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs.Pattern
{
    public class PatternNNSearch
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "slot")]
            public string SlotParsingFile;

            [Argument(ArgumentType.Required, ShortName = "pattern")]
            public string PatternFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "min")]
            public int MinReformulationCount = 5;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.SlotParsingFile) || !File.Exists(arguments.PatternFile))
            {
                Console.WriteLine("No Pattern File!");
                return;
            }
        }
    }
}
