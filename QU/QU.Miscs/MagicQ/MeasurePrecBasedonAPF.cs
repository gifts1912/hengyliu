using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using Util = Utility;

namespace QU.Miscs.MagicQ
{
    public class MeasurePrecBasedonAPF
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

            [Argument(ArgumentType.Required, ShortName = "result")]
            public string ResultOutput;

            [Argument(ArgumentType.Required, ShortName = "feature")]
            public string FeatureOutput;

            [Argument(ArgumentType.AtMostOnce, ShortName = "top1")]
            public string Top1MatchedQueries;
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

            var gScrape = new Dictionary<string, List<DocInfo>>();
            ReadExtraction(arguments.GoogleScrapeFile, out gScrape, arguments.GoogleTopN);
            //Util.ScrapeUtility.ReadScrape(arguments.GoogleScrapeFile, arguments.GoogleTopN, out gScrape);

            var bScrape = new Dictionary<string, List<DocInfo>>();
            ReadExtraction(arguments.BingScrapeFile, out bScrape, arguments.BingTopN);
            //Util.ScrapeUtility.ReadScrape(arguments.BingScrapeFile, arguments.BingTopN, out bScrape);

            StreamWriter swFeature = new StreamWriter(arguments.FeatureOutput);

            Stat stat = new Stat();
            stat.HaveWebFilmUrlQueryInTopN = new int[arguments.BingTopN];
            stat.EntityResultInTopN = new int[arguments.BingTopN];
            stat.HaveQAFilmUrlQueryInTopN = new int[arguments.BingTopN];
            stat.EntityQAResultInTopN = new int[arguments.BingTopN];
            bool[] inTopN = new bool[arguments.BingTopN], topNHasFilm = new bool[arguments.BingTopN];
            bool[] inQATopN = new bool[arguments.BingTopN], topNHasQAFilm = new bool[arguments.BingTopN];
            int gFilmUrls = 0;
            int[] matchGFilmUrls = new int[arguments.BingTopN], matchGQAUrls = new int[arguments.BingTopN];

            swFeature.WriteLine("Query\tCandidate\t" + MovieCandidateFeature.Header + "\tMatchG");
            HashSet<string> top1MatchedQueries = new HashSet<string>();

            foreach (var b in bScrape)
            {
                List<DocInfo> g;
                if (!gScrape.TryGetValue(b.Key, out g))
                    continue;

                long[] gAPFs = (from gp in g where gp.apf1194 > 0 select gp.apf1194).ToArray();

                if (gAPFs.Length > 0)
                {
                    stat.GHaveFilmEntityQuery++;
                    gFilmUrls += gAPFs.Length;
                }

                DocInfo[] bUrls = (from bp in b.Value orderby bp.position ascending select bp).ToArray();

                for (int i = 0; i < inTopN.Length; i++)
                {
                    inTopN[i] = false;
                    topNHasFilm[i] = false;
                    inQATopN[i] = false;
                    topNHasQAFilm[i] = false;
                }

                Dictionary<long, MovieCandidateFeature> dictCandidate2Feature = new Dictionary<long, MovieCandidateFeature>();
                for (int i = 0; i < bUrls.Length; i++)
                {
                    // Movie Url Features
                    DocInfo bu = bUrls[i];
                    if (bu.apf1194 > 0)
                    {
                        for (int j = i; j < inTopN.Length; j++)
                        {
                            topNHasFilm[j] = true;
                        }

                        if (!dictCandidate2Feature.ContainsKey(bu.apf1194))
                        {
                            dictCandidate2Feature.Add(bu.apf1194, new MovieCandidateFeature());
                        }

                        dictCandidate2Feature[bu.apf1194].hasWebAns = true;
                        ++dictCandidate2Feature[bu.apf1194].webOcc;
                        dictCandidate2Feature[bu.apf1194].webPos = Math.Min(dictCandidate2Feature[bu.apf1194].webPos, i + 1);
                        dictCandidate2Feature[bu.apf1194].MaxDRScore = Math.Max(dictCandidate2Feature[bu.apf1194].MaxDRScore, (int)bu.DRScore);
                        dictCandidate2Feature[bu.apf1194].MaxBM25QAPattern = Math.Max(dictCandidate2Feature[bu.apf1194].MaxBM25QAPattern, (int)bu.BM25FQAPattern);
                        dictCandidate2Feature[bu.apf1194].MaxBM25Entity = Math.Max(dictCandidate2Feature[bu.apf1194].MaxBM25Entity, (int)bu.BM25FEntity);
                    }

                    // QA Url Features
                    Dictionary<long, int> QAFacts = null;
                    if (!string.IsNullOrEmpty(bu.qaFact))
                    {
                        for (int j = i; j < inTopN.Length; j++)
                        {
                            topNHasQAFilm[j] = true;
                        }

                        if (!string.IsNullOrEmpty(bu.qaFact))
                        {
                            QAFacts = new Dictionary<long,int>();
                            foreach (var f in bu.qaFact.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                string[] items = f.Split('|');
                                int score = 1;
                                if (items.Length > 1)
                                    score = int.Parse(items[1]);
                                QAFacts[long.Parse(items[0])] = score;
                            }

                            foreach (var f in QAFacts)
                            {
                                if (!dictCandidate2Feature.ContainsKey(f.Key))
                                {
                                    dictCandidate2Feature.Add(f.Key, new MovieCandidateFeature());
                                }

                                dictCandidate2Feature[f.Key].hasQAFact = true;
                                dictCandidate2Feature[f.Key].QAScore += f.Value;
                                ++dictCandidate2Feature[f.Key].qaOcc;
                                dictCandidate2Feature[f.Key].qaPos = Math.Min(dictCandidate2Feature[f.Key].qaPos, i + 1);
                            }
                        }
                    }

                    // Statistics
                    foreach (long gu in gAPFs)
                    {
                        if (bu.apf1194 == gu)
                        {
                            for (int j = i; j < inTopN.Length; j++)
                            {
                                inTopN[j] = true;
                            }

                            for (int j = i; j < inTopN.Length; j++)
                            {
                                matchGFilmUrls[j]++;
                            }

                            if (i == 0)
                            {
                                top1MatchedQueries.Add(b.Key);
                            }
                        }

                        if (QAFacts != null)
                        {
                            if (QAFacts.ContainsKey(gu))
                            {
                                for (int j = i; j < inTopN.Length; j++)
                                {
                                    inQATopN[j] = true;
                                }

                                for (int j = i; j < inTopN.Length; j++)
                                {
                                    matchGQAUrls[j]++;
                                }
                            }
                        }
                    }
                }

                foreach (var pair in dictCandidate2Feature)
                {
                    if (pair.Value.hasWebAns)
                    {
                        long c = pair.Key;
                        bool matchG = false;
                        foreach (long gu in gAPFs)
                        {
                            if (gu == c)
                            {
                                matchG = true;
                            }
                        }

                        swFeature.WriteLine("{0}\t{1}\t{2}\t{3}", b.Key, c,
                            pair.Value.ToString(),
                            matchG ? 1 : 0);
                    }
                }

                for (int i = 0; i < stat.EntityResultInTopN.Length; i++)
                {
                    stat.EntityResultInTopN[i] += inTopN[i] ? 1 : 0;
                    stat.HaveWebFilmUrlQueryInTopN[i] += topNHasFilm[i] ? 1 : 0;
                    stat.EntityQAResultInTopN[i] += inQATopN[i] ? 1 : 0;
                    stat.HaveQAFilmUrlQueryInTopN[i] += topNHasQAFilm[i] ? 1 : 0;
                }
            }

            #region Output Results
            int[] interestedPosition = "1;2;3;5;10;20;30;40;50;60;80;100;120;150;180;200".Split(';').Select(i => int.Parse(i)).Where(i => i <= arguments.BingTopN).ToArray();

            using (StreamWriter sw = new StreamWriter(arguments.ResultOutput))
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
                sw.WriteLine("Google top{0} contain film: {1} (Query Level)", arguments.GoogleTopN, stat.GHaveFilmEntityQuery);
                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("Recall@{0}\t{1}", interestedPosition[i],
                        (double)stat.EntityResultInTopN[interestedPosition[i] - 1] / stat.GHaveFilmEntityQuery);
                }

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------");
                sw.WriteLine("------------------------------------");
                sw.WriteLine("------------------------------------");
                sw.WriteLine();
                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("BingHaveQAFilmUrlInTop{0}\t{1}", interestedPosition[i], stat.HaveQAFilmUrlQueryInTopN[interestedPosition[i] - 1]);
                }

                sw.WriteLine("------------------------------------");

                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("GoogleGroundTruthInBingQATop{0}\t{1}", interestedPosition[i], stat.EntityQAResultInTopN[interestedPosition[i] - 1]);
                }

                sw.WriteLine("------------------------------------");

                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("QA Precision@{0}\t{1}", interestedPosition[i],
                        (double)stat.EntityQAResultInTopN[interestedPosition[i] - 1] / stat.HaveQAFilmUrlQueryInTopN[interestedPosition[i] - 1]);
                }

                sw.WriteLine("------------------------------------");
                sw.WriteLine("Google top{0} contain film (Query Level): {1}", arguments.GoogleTopN, stat.GHaveFilmEntityQuery);
                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("QA Recall@{0}\t{1}", interestedPosition[i],
                        (double)stat.EntityQAResultInTopN[interestedPosition[i] - 1] / stat.GHaveFilmEntityQuery);
                }

                sw.WriteLine("------------------------------------");
                sw.WriteLine("Google top{0} contain film (Url Level): {1}", arguments.GoogleTopN, gFilmUrls);
                for (int i = 0; i < interestedPosition.Length; i++)
                {
                    sw.WriteLine("Web Recalled Urls@{0}\t{1}", interestedPosition[i],
                        (double)matchGFilmUrls[interestedPosition[i] - 1]);
                    sw.WriteLine("QA Recalled Urls@{0}\t{1}", interestedPosition[i],
                        (double)matchGQAUrls[interestedPosition[i] - 1]);
                }
            }

            #endregion

            swFeature.Flush();
            swFeature.Close();

            if (!string.IsNullOrEmpty(arguments.Top1MatchedQueries))
            {
                using (StreamWriter swTop1Queries = new StreamWriter(arguments.Top1MatchedQueries))
                {
                    foreach (var q in top1MatchedQueries)
                    {
                        swTop1Queries.WriteLine(q);
                    }
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

            public int[] HaveQAFilmUrlQueryInTopN;
            public int[] EntityQAResultInTopN;
        }

        class DocInfo
        {
            public uint position;
            public string url;
            public long apf1194;
            public string qaFact;
            public long DRScore;
            public long BM25FQAPattern;
            public long BM25FEntity;
        }

        static void ReadExtraction(string file, out Dictionary<string, List<DocInfo>> dict, int topN)
        {
            dict = new Dictionary<string, List<DocInfo>>();
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(file)))
            {
                TSVReader reader = new TSVReader(sr, true);
                TSVLine line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        string query = line["m:Query"];
                        string url = line["m:Url"];
                        uint position = line.GetFeatureValue("DocumentPosition");
                        if (position > topN)
                            continue;

                        uint apf1194 = line.GetFeatureValue("AdvancedPreferFeature_1194");
                        string qaFact = line["m:QAFact"];

                        long drscore = 0, bm25qa = 0, bm25entity = 0;
                        try
                        {
                            drscore = line.GetFeatureValue("DRScore");
                            bm25qa = line.GetFeatureValue("PerStreamBM25F_QAPattern-AnswersD");
                            bm25entity = line.GetFeatureValue("PerStreamBM25F_DIS-MovieEntityInfo-Jan2015A");
                        }
                        catch { }

                        if (!dict.ContainsKey(query))
                            dict[query] = new List<DocInfo>();

                        dict[query].Add(new DocInfo 
                        { apf1194 = apf1194, position = position, qaFact = qaFact, url = url, 
                            BM25FEntity = bm25entity, BM25FQAPattern = bm25qa, DRScore = drscore }
                        );
                    }
                    catch { }
                }
            }
        }
    }
}
