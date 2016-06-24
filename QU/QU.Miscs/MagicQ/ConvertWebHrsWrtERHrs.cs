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
    public class ConvertWebHrsWrtERHrs
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "web")]
            public string WebHrsFile;

            [Argument(ArgumentType.Required, ShortName = "er")]
            public string EntityHrsFile;

            [Argument(ArgumentType.Required, ShortName = "map")]
            public string Mapping;

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

            // read hrs data for entity ranking
            MyMisc.HRSReader erHrs = new MyMisc.HRSReader();
            erHrs.ReadHRSFile(arguments.EntityHrsFile);

            // read mapping file
            Dictionary<string, string> url2satoriIdMapping = MyMisc.CommonUtils.ReadPairs(arguments.Mapping);

            // update judgement in web hrs
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.WebHrsFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        MyMisc.HRSReader.HRSData data = MyMisc.HRSReader.HRSData.FromLine(line);
                        if (null == data)
                            continue;
                        var qErHrs = erHrs.GetHRSOfQuery(data.query);
                        if (null == qErHrs)
                            continue;

                        // get satori url
                        data.url = MyMisc.Normalizer.NormalizeUrl(data.url);
                        string satoriUrl;
                        if (!url2satoriIdMapping.TryGetValue(data.url, out satoriUrl))
                            continue;

                        // get judgement in entity rank hrs
                        var quErHrs = erHrs.GetHRSOfQUPair(qErHrs, satoriUrl);
                        string erJudgement = quErHrs == null ? "Unjudged" : quErHrs.judgement;
                        data.judgement = erJudgement;

                        sw.WriteLine(data.ToString());
                    }
                }
            }
        }
    }
}
