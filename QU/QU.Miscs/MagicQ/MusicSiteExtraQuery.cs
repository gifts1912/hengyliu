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
    class MusicSiteExtraQuery
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "i")]
            public string Input = "";

            [Argument(ArgumentType.Required, ShortName = "o")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "s")]
            public string Sites = "metrolyrics.com;azlyrics.com";

            [Argument(ArgumentType.AtMostOnce, ShortName = "len")]
            public int MinQueryLength = 8;

            [Argument(ArgumentType.AtMostOnce, ShortName = "r")]
            public int RelaxCountThreshold = 9;

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

            string[] sites = arguments.Sites.Split(';');

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.Input))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        string query = MusicUtility.Normalize(line);
                        int queryLen = MusicUtility.QueryLength(query);
                        if (queryLen < arguments.MinQueryLength)
                            continue;

                        query = MusicUtility.Process(query);
                        StringBuilder sb = new StringBuilder();
                        sb.Append(line);
                        sb.Append("\t");
                        foreach (string site in sites)
                        {
                            string newq = query + " " + "site:" + site;
                            if (queryLen >= arguments.RelaxCountThreshold)
                            {
                                newq += " [relaxcount=1]";
                            }
                            newq = MyMisc.MultiQueryUtils.EscapeString(newq);
                            sb.Append(newq + "\t");
                        }
                        sw.WriteLine(sb.ToString(0, sb.Length - 1));
                    }
                }
            }
        }
    }
}
