using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMisc = Utility;

namespace QU.Miscs.MagicQ
{
    public class MovieQU
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "i")]
            public string Input = "";

            [Argument(ArgumentType.Required, ShortName = "o")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "a")]
            public string MovieAnchor = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "s")]
            public string Stopwords = "";

            public bool InputValid { get { return File.Exists(Input); } }
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!string.IsNullOrEmpty(arguments.MovieAnchor))
            {
                MovieUtility.SetMagicMovieAnchor(arguments.MovieAnchor);
            }

            if (!string.IsNullOrEmpty(arguments.Stopwords))
            {
                MovieUtility.SetStopwordsInAttribute(arguments.Stopwords.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries));
            }

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.Input))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        string query = MyMisc.Normalizer.SimpleNormalizeQuery(line);
                        //MagicMovieContext context = MovieUtility.UnderstandMagicMovie(query);
                        //if (null == context)
                        //    continue;

                        //query = (context.Attribute + " movie where " + context.OrigDescription).Trim();
                        //sw.WriteLine(line + "\t" + context.OrigAttribute + "\t" + context.Attribute + "\t" + context.OrigDescription + "\t" + context.Description + "\t" + query);
                        if (!MovieUtility.IsMagicMovie(query))
                            continue;

                        string nostopwordQ = MovieUtility.RemoveRegexStopwords(query);
                        sw.WriteLine(line + "\t" + nostopwordQ);
                    }
                }
            }
        }
    }
}
