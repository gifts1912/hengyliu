using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QU.Miscs
{
    class DropWordsInQas
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "query")]
            public string QueryFile;

            [Argument(ArgumentType.Required, ShortName = "meta")]
            public string QasMetadataFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "replacement")]
            public string Replacement;

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

            if (!File.Exists(arguments.QasMetadataFile) || !File.Exists(arguments.QueryFile))
            {
                Console.WriteLine("No Pattern File!");
                return;
            }

            Regex regexReplacement = null;
            if (!string.IsNullOrEmpty(arguments.Replacement))
                regexReplacement = new Regex("(" + string.Join("|", arguments.Replacement.ToArray()) + ")+", RegexOptions.Compiled);
            Regex regexMultiSpaces = new Regex(" +", RegexOptions.Compiled);
            Regex regexDropTerm = new Regex(@"mdTypeCALInstructions:Drop\((?<term>.+?)\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Dictionary<string, HashSet<string>> dictQuery2Stopwords = new Dictionary<string, HashSet<string>>();
            using (StreamReader sr = new StreamReader(arguments.QasMetadataFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split('\t');
                    if (items.Length < 1)
                        continue;

                    HashSet<string> stopwords = new HashSet<string>();
                    string origQ = items[0];
                    
                    for (int i = 1; i < items.Length; i++)
                    {
                        if (string.IsNullOrEmpty(items[i]))
                            continue;
                        string term = regexDropTerm.Match(items[i]).Groups["term"].Value;
                        stopwords.Add(term.ToLower());
                    }

                    dictQuery2Stopwords[origQ] = stopwords;
                }
            }

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.QueryFile))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string origQ = line;
                        string q = origQ.ToLower();
                        q = regexMultiSpaces.Replace(q, " ");
                        if (regexReplacement != null)
                            q = regexReplacement.Replace(q, "");

                        HashSet<string> stopwords;
                        if (!dictQuery2Stopwords.TryGetValue(origQ, out stopwords))
                            stopwords = new HashSet<string>();

                        StringBuilder sb = new StringBuilder();
                        string[] qTerms = q.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var t in qTerms)
                        {
                            if (stopwords.Contains(t))
                                continue;
                            sb.Append(t);
                            sb.Append(' ');
                        }

                        sw.WriteLine("{0}\t{1}", sb.ToString(0, sb.Length - 1), origQ);
                    }
                }
            }
        }
    }
}
