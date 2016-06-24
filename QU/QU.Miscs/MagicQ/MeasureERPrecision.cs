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
    public class MeasureERPrecision
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "web")]
            public string WebScrapeFile;

            [Argument(ArgumentType.Required, ShortName = "ei")]
            public string EIParseFile;

            [Argument(ArgumentType.AtMostOnce, ShortName = "types")]
            public string ExpectedTypes = "film.film";

            [Argument(ArgumentType.AtMostOnce, ShortName = "topn")]
            public int TopN = 10;

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

            HashSet<string> types = new HashSet<string>(arguments.ExpectedTypes.Split(Seperators, StringSplitOptions.RemoveEmptyEntries));

            var webScrape = new Dictionary<string, Dictionary<string, int>>();
            MyUtil.ScrapeUtility.ReadScrape(arguments.WebScrapeFile, 10, out webScrape);

            Stat stat = new Stat();

            using (StreamReader sr = new StreamReader(arguments.EIParseFile))
            {
                string line;
                string currQ = "";
                List<SatoriDocInfo> queryLines
                    = new List<SatoriDocInfo>();

                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split('\t');
                    if (items.Length < 8)
                        continue;

                    string q = items[0];
                    if (!string.IsNullOrEmpty(currQ) && !string.Equals(q, currQ))
                    {
                        ProcessLines(currQ, queryLines, webScrape, ref stat);
                        queryLines.Clear();
                    }

                    AddLine(items, ref queryLines, types, arguments.TopN);
                    currQ = q;
                }

                ProcessLines(currQ, queryLines, webScrape, ref stat);
            }

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                sw.WriteLine("NoWebResultQuery" + "\t" + stat.NoWebResultQuery);
                sw.WriteLine("HaveFilmEntityQuery" + "\t" + stat.HaveFilmEntityQuery);
                sw.WriteLine("HaveWebFilmUrlInTop1" + "\t" + stat.HaveWebFilmUrlQueryInTop1);
                sw.WriteLine("HaveWebFilmUrlInTop3" + "\t" + stat.HaveWebFilmUrlQueryInTop3);
                sw.WriteLine("HaveWebFilmUrlInTop5" + "\t" + stat.HaveWebFilmUrlQueryInTop5);
                sw.WriteLine("HaveWebFilmUrlInTop10" + "\t" + stat.HaveWebFilmUrlQueryInTop10);
                sw.WriteLine("EntityResultInTop1" + "\t" + stat.EntityResultInTop1);
                sw.WriteLine("EntityResultInTop3" + "\t" + stat.EntityResultInTop3);
                sw.WriteLine("EntityResultInTop5" + "\t" + stat.EntityResultInTop5);
                sw.WriteLine("EntityResultInTop10" + "\t" + stat.EntityResultInTop10);

                sw.WriteLine("------------------------------------");
                sw.WriteLine("Precision@1\t{0}", (double)stat.EntityResultInTop1 / stat.HaveFilmEntityQuery);
                sw.WriteLine("Precision@3\t{0}", (double)stat.EntityResultInTop3 / stat.HaveFilmEntityQuery);
                sw.WriteLine("Precision@5\t{0}", (double)stat.EntityResultInTop5 / stat.HaveFilmEntityQuery);
                sw.WriteLine("Precision@10\t{0}", (double)stat.EntityResultInTop10 / stat.HaveFilmEntityQuery);

                sw.WriteLine("Recall@1\t{0}", (double)stat.EntityResultInTop1 / stat.HaveWebFilmUrlQueryInTop1);
                sw.WriteLine("Recall@3\t{0}", (double)stat.EntityResultInTop3 / stat.HaveWebFilmUrlQueryInTop3);
                sw.WriteLine("Recall@5\t{0}", (double)stat.EntityResultInTop5 / stat.HaveWebFilmUrlQueryInTop5);
                sw.WriteLine("Recall@10\t{0}", (double)stat.EntityResultInTop10 / stat.HaveWebFilmUrlQueryInTop10);
            }
        }

        static void AddLine(string[] items, ref List<SatoriDocInfo> lines, HashSet<string> types, int maxPos)
        {
            int pos = int.Parse(items[1]);

            if (pos > maxPos)
                return;

            string doc = items[2];
            double docScore = double.Parse(items[3]);

            string type = items[4];
            if (!types.Contains(type))
                return;

            SatoriDocInfo docInfo = null;
            foreach (var l in lines)
            {
                if (l.doc == doc)
                {
                    docInfo = l;
                    break;
                }
            }

            if (null == docInfo)
                docInfo = new SatoriDocInfo();

            docInfo.pos = pos;
            docInfo.doc = doc;
            docInfo.docScore = docScore;

            if (!docInfo.types.ContainsKey(type))
                docInfo.types[type] = new SatoriType();

            docInfo.types[type].type = type;
            docInfo.types[type].typeScore = double.Parse(items[5]);
            docInfo.types[type].names.Add(items[6].ToLower());
            docInfo.types[type].urls.Add(items[7].ToLower());

            lines.Add(docInfo);
        }

        static void ProcessLines(string query, 
                                    List<SatoriDocInfo> lines, 
                                    Dictionary<string, Dictionary<string, int>> webScrape, 
                                    ref Stat stat)
        {
            ++stat.TotalQuery;

            Dictionary<string, int> webResult;
            if (!webScrape.TryGetValue(query, out webResult))
            {
                ++stat.NoWebResultQuery;
                return;
            }

            string[] results = (from w in webResult orderby w.Value ascending select w.Key).ToArray();
            HashSet<string> entities = new HashSet<string>();
            foreach (var l in lines)
            {
                foreach (var t in l.types.Values)
                {
                    foreach (var u in t.urls)
                        entities.Add(MyUtil.Normalizer.NormalizeUrl(u));
                }
            }

            if (entities.Count > 0)
            {
                ++stat.HaveFilmEntityQuery;
            }

            bool inTop1 = false, inTop3 = false, inTop5 = false, inTop10 = false;
            bool top1HasFilm = false, top3HasFilm = false, top5HasFilm = false, top10HasFilm = false;
            for (int i = 0; i < results.Length; i++)
            {
                string nu = MyUtil.Normalizer.NormalizeUrl(results[i]);
                // web film url
                if (MovieUtility.IsFilmUrl(nu))
                {
                    if (i < 1)
                    {
                        top1HasFilm = true;
                        top3HasFilm = true;
                        top5HasFilm = true;
                        top10HasFilm = true;
                    }
                    else if (i < 3)
                    {
                        top3HasFilm = true;
                        top5HasFilm = true;
                        top10HasFilm = true;
                    }
                    else if (i < 5)
                    {
                        top5HasFilm = true;
                        top10HasFilm = true;
                    }
                    else if (i < 10)
                    {
                        top10HasFilm = true;
                    }
                }

                // entity index url
                foreach (string e in entities)
                {
                    if (nu.Contains(e))
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", query, e, nu, i + 1);

                        if (i < 1)
                        {
                            inTop1 = true;
                            inTop3 = true;
                            inTop5 = true;
                            inTop10 = true;
                        }
                        else if (i < 3)
                        {
                            inTop3 = true;
                            inTop5 = true;
                            inTop10 = true;
                        }
                        else if (i < 5)
                        {
                            inTop5 = true;
                            inTop10 = true;
                        }
                        else if (i < 10)
                        {
                            inTop10 = true;
                        }

                        break;
                    }
                }
            }

            stat.EntityResultInTop1 += (inTop1 ? 1 : 0);
            stat.EntityResultInTop3 += (inTop3 ? 1 : 0);
            stat.EntityResultInTop5 += (inTop5 ? 1 : 0);
            stat.EntityResultInTop10 += (inTop10 ? 1 : 0);

            stat.HaveWebFilmUrlQueryInTop1 += (top1HasFilm ? 1 : 0);
            stat.HaveWebFilmUrlQueryInTop3 += (top3HasFilm ? 1 : 0);
            stat.HaveWebFilmUrlQueryInTop5 += (top5HasFilm ? 1 : 0);
            stat.HaveWebFilmUrlQueryInTop10 += (top10HasFilm ? 1 : 0);
        }

        class SatoriDocInfo
        {
            public int pos;
            public string doc;
            public double docScore;

            public Dictionary<string, SatoriType> types = new Dictionary<string, SatoriType>();
        }

        class SatoriType
        {
            public string type;
            public double typeScore;
            public HashSet<string> names = new HashSet<string>();
            public HashSet<string> urls = new HashSet<string>();
        }

        class Stat
        {
            public int TotalQuery = 0;
            public int NoWebResultQuery = 0;
            public int HaveFilmEntityQuery = 0;
            public int HaveWebFilmUrlQueryInTop1 = 0;
            public int HaveWebFilmUrlQueryInTop3 = 0;
            public int HaveWebFilmUrlQueryInTop5 = 0;
            public int HaveWebFilmUrlQueryInTop10 = 0;
            public int EntityResultInTop1 = 0;
            public int EntityResultInTop3 = 0;
            public int EntityResultInTop5 = 0;
            public int EntityResultInTop10 = 0;
        }
    }
}
