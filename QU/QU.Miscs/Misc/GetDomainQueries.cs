using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs
{
    /// <summary>
    /// Get queries which can be triggered by a binary classifier.
    /// </summary>
    public class GetDomainQueries
    {
        class ClassifierOutput
        {
            public string Query;
            public double Confidence;
            public string Entities;
        }

        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "d")]
            public string Domain;

            [Argument(ArgumentType.AtMostOnce, ShortName = "th")]
            public double Threshold = 0.7;

            [Argument(ArgumentType.Required, ShortName = "in")]
            public string Input;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;
        }

        static char[] seperator = new char[] { '\t' };
        /// <summary>
        /// Get queries which can be triggered by a binary classifier.
        /// </summary>
        /// <param name="args">/in {QAS Output} /d {Domain of Interest} /th {Threshold} /out {Output}</param>
        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.Input))
            {
                Console.WriteLine("No Qas Output File!");
                return;
            }

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.Input))
                {
                    string query = string.Empty;
                    string line;
                    string outputString = string.Empty;

                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Replace("__", "-");
                        string[] tokens = line.Split('\t');
                        if (tokens.Length <= 2)
                        {
                            continue;
                        }

                        query = tokens[0];

                        string[] classifier = tokens[1].Split('_');
                        if (classifier.Length < 2)
                        {
                            continue;
                        }

                        string domain = classifier[0];
                        double confidence = double.Parse(classifier[1]);
                        if (!domain.Equals(arguments.Domain, StringComparison.OrdinalIgnoreCase)
                             || confidence < arguments.Threshold)
                        {
                            continue;
                        }

                        sw.WriteLine("{0}\t{1}\t{2}\t{3}", query, domain, confidence, tokens[2]);
                    }
                }
            }
        }
    }
}
