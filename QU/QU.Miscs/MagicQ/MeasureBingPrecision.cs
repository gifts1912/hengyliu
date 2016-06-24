using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUtil = Utility;

namespace QU.Miscs.MagicQ
{
    public class MeasureBingPrecision
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "g")]
            public string GoogleScrapeFile;

            [Argument(ArgumentType.Required, ShortName = "b")]
            public string BingScrapeFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "n")]
            public int GoogleTopN = 10;

            [Argument(ArgumentType.AtMostOnce, ShortName = "bn")]
            public int BingTopN = 10;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;
        }

        static char[] Seperators = new char[] { ';', ',' };

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            var gScrape = new Dictionary<string, Dictionary<string, int>>();
            MyUtil.ScrapeUtility.ReadScrape(arguments.GoogleScrapeFile, arguments.GoogleTopN, out gScrape);

            var bScrape = new Dictionary<string, Dictionary<string, int>>();
            MyUtil.ScrapeUtility.ReadScrape(arguments.BingScrapeFile, arguments.BingTopN, out bScrape);

            Stat stat = new Stat();
            stat.HaveWebFilmUrlQueryInTopN = new int[arguments.BingTopN];
            stat.EntityResultInTopN = new int[arguments.BingTopN];
            bool[] inTopN = new bool[arguments.BingTopN], topNHasFilm = new bool[arguments.BingTopN];

            foreach (var b in bScrape)
            {
                Dictionary<string, int> g;
                if (!gScrape.TryGetValue(b.Key, out g))
                    continue;

                string[] gUrls = (from gp in g orderby gp.Value ascending select MyUtil.Normalizer.NormalizeUrl(gp.Key)).ToArray();
                gUrls = (from gu in gUrls where MovieUtility.IsFilmUrl(gu) select MovieUtility.CanonicalFilmUrl(gu)).ToArray();

                if (gUrls.Length > 0)
                {
                    stat.GHaveFilmEntityQuery++;
                }

                string[] bUrls = (from bp in b.Value orderby bp.Value ascending select MyUtil.Normalizer.NormalizeUrl(bp.Key)).ToArray();


                for (int i = 0; i < inTopN.Length; i++)
                {
                    inTopN[i] = false;
                    topNHasFilm[i] = false;
                }

                for (int i = 0; i < bUrls.Length; i++)
                {
                    string bu = bUrls[i];
                    if (MovieUtility.IsFilmUrl(bu))
                    {
                        for (int j = i; j < inTopN.Length; j++)
                        {
                            topNHasFilm[j] = true;
                        }
                    }

                    foreach (string gu in gUrls)
                    {
                        if (bu.Contains(gu))
                        {
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}", b.Key, gu, bu, i + 1);

                            for (int j = i; j < inTopN.Length; j++)
                            {
                                inTopN[j] = true;
                            }

                            break;
                        }
                    }
                }

                for (int i = 0; i < stat.EntityResultInTopN.Length; i++)
                {
                    stat.EntityResultInTopN[i] += inTopN[i] ? 1 : 0;
                    stat.HaveWebFilmUrlQueryInTopN[i] += topNHasFilm[i] ? 1 : 0;
                }
            }

            int[] interestedPosition = "1;3;5;10;20;30;40;50".Split(';').Select(i => int.Parse(i)).Where(i => i <= arguments.BingTopN).ToArray();

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("BingHaveFilmUrlInTop{0}\t{1}", interestedPosition[i], stat.HaveWebFilmUrlQueryInTopN[interestedPosition[i] - 1]);
                }

                sw.WriteLine("------------------------------------");

                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("GoogleGroundTruthInBingTop{0}\t{1}", interestedPosition[i], stat.EntityResultInTopN[interestedPosition[i] - 1]);
                }

                sw.WriteLine("------------------------------------");

                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("Precision@{0}\t{1}", interestedPosition[i],
                        (double)stat.EntityResultInTopN[interestedPosition[i] - 1] / stat.HaveWebFilmUrlQueryInTopN[interestedPosition[i] - 1]);
                }

                sw.WriteLine("------------------------------------");
                sw.WriteLine("Google top{0} contain film: {1}", arguments.GoogleTopN, stat.GHaveFilmEntityQuery);
                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("Recall@{0}\t{1}", interestedPosition[i],
                        (double)stat.EntityResultInTopN[interestedPosition[i] - 1] / stat.GHaveFilmEntityQuery);
                }
            }
        }

        class Stat
        {
            public int TotalQuery = 0;
            public int NoWebResultQuery = 0;
            public int HaveFilmEntityQuery = 0;
            public int GHaveFilmEntityQuery = 0;
            public int[] HaveWebFilmUrlQueryInTopN;
            public int[] EntityResultInTopN;
        }
    }
}
