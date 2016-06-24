using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs
{
    public class FilterDesiredPatterns
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "p")]
            public string PatternFile = "";

            [Argument(ArgumentType.Required, ShortName = "o")]
            public string Output = "";

            [Argument(ArgumentType.Required, ShortName = "e")]
            public string Expression = "pattern.StartsWith(\"is \")||pattern.StartsWith(\"are \")||pattern.StartsWith(\"can \")||pattern.StartsWith(\"will \")||pattern.StartsWith(\"will \")||pattern.StartsWith(\"would \")||pattern.StartsWith(\"why \")||pattern.StartsWith(\"what \")||pattern.StartsWith(\"when \")||pattern.StartsWith(\"which \")||pattern.StartsWith(\"how \")";

            public bool InputValid { get { return File.Exists(PatternFile); } }
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            BooleanEvaluator evaluator = BooleanEvaluator.ParseExpression(arguments.Expression);

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.PatternFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');
                        if (items.Length == 0)
                            continue;

                        string pattern = items[0];
                        if (Score(pattern, evaluator))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }

        private static bool Score(string pattern, BooleanEvaluator evaluator)
        {
            return evaluator.Evaluate(pattern);
        }
    }
}
