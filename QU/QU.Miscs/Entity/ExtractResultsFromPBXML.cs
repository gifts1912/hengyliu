using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QU.Miscs.Common;
using System.IO;

namespace QU.Miscs.Entity
{
    public class ExtractResultsFromPBXML
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "azure")]
            public string AzureFolder;

            [Argument(ArgumentType.Required, ShortName = "result")]
            public string ResultFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "service")]
            public string Service = "Dolphin";

            [Argument(ArgumentType.AtMostOnce, ShortName = "scenario")]
            public string Scenario = "Fetch";

            [Argument(ArgumentType.AtMostOnce, ShortName = "field")]
            public string FieldName2PathMapping = "Query:Query;NQuery:NormalizedQuery";
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            string[] mappings = arguments.FieldName2PathMapping.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] names = new string[mappings.Length], paths = new string[mappings.Length];
            for (int i = 0; i < mappings.Length; i++)
            {
                string[] items = mappings[i].Split(':');
                names[i] = items[0];
                paths[i] = items[1];
            }

            string azureFolder = File.ReadAllText(arguments.AzureFolder);
            var processor = new PbxmlScraperResultsProcessor(azureFolder, 1, 0);
            List<ScrapeQueryResult> results = processor.ExtractFields(arguments.Service, arguments.Scenario, paths);

            using (StreamWriter sw = new StreamWriter(arguments.ResultFile))
            {
                foreach (var r in results)
                {
                    if (r != null)
                    {
                        sw.WriteLine(WriteFields(r.Query, r.InterestedFields));
                    }
                }
            }
        }

        static string WriteFields(string query, string[] fields)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(query);
            sb.Append("\t");
            foreach (string f in fields)
            {
                sb.Append(f);
                sb.Append("\t");
            }
            return sb.ToString(0, sb.Length - 1);
        }
    }
}
