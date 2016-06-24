using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using MyUtil = Utility;

namespace QU.Miscs.MagicQ
{
    public class MovieRerank
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "extraction")]
            public string Extraction = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.Required, ShortName = "feature")]
            public string FeatureFile = "";

            [Argument(ArgumentType.Required, ShortName = "map")]
            public string MovieId2RepUrlMappingFile = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "demote")]
            public string DemoteHostFile = "tobedemote.txt";

            [Argument(ArgumentType.AtMostOnce, ShortName = "min")]
            public double MinScore = -10;

            [Argument(ArgumentType.AtMostOnce, ShortName = "topn")]
            public int TopN = 20;

            [Argument(ArgumentType.AtMostOnce, ShortName = "levels")]
            public string Levels = "5;-7;-18";

            [Argument(ArgumentType.AtMostOnce, ShortName = "triggered")]
            public string Triggered = "triggered.txt";
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            double[] levels = (from l in arguments.Levels.Split(';') select double.Parse(l)).ToArray();

            var satoriId2RepUrls = MovieRankingUtility.ReadSatoriId2RepUrlsMapping(arguments.MovieId2RepUrlMappingFile);
            var demotedHosts = MovieRankingUtility.ReadDemotionHosts(arguments.DemoteHostFile);

            Dictionary<string, Dictionary<long, MovieCandidateFeature>> dictQ2CandFeat;
            Dictionary<string, Dictionary<long, double>> dictQ2CandScore;
            MovieRankingUtility.ReadFeatureFile(arguments.FeatureFile, out dictQ2CandFeat, out dictQ2CandScore);

            TSVLine headerLine;
            List<QueryBlock> blocks;
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.Extraction)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                QueryBlockReader qbReader = new QueryBlockReader(tsvReader);
                blocks = qbReader.ReadQueryBlocks().ToList();
                headerLine = tsvReader.HeaderTSVLine;
            }

            StreamWriter swTriggered = new StreamWriter(arguments.Triggered);

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                sw.WriteLine(Line.Header + "\tMovieScore\tAdjustedMovieScore\tRerankScore");
                foreach (var block in blocks)
                {
                    string query = block.Lines.First()["m:Query"];
                    if (!dictQ2CandFeat.ContainsKey(query) || !dictQ2CandScore.ContainsKey(query))
                        continue;

                    Dictionary<long, MovieCandidateFeature> dictMovie2Features = dictQ2CandFeat[query];
                    Dictionary<long, double> dictMovie2Score = dictQ2CandScore[query];

                    bool triggered = false;
                    var lines = Merge(dictMovie2Score, satoriId2RepUrls, block, arguments.TopN, arguments.MinScore, levels, out triggered);

                    if (triggered)
                    {
                        Rerank(ref lines, demotedHosts);

                        var sorted = (from l in lines
                                      orderby l.docScore descending, l.documentPos ascending
                                      select l).ToList();

                        foreach (var s in sorted)
                        {
                            sw.WriteLine(s.ToString() + "\t" + s.movieScore + "\t" + s.adjustedMovieScore + "\t" + s.docScore);
                        }

                        swTriggered.WriteLine(query);
                    }
                }
            }

            swTriggered.Flush();
            swTriggered.Close();
        }

        class Line
        {
            public long apf1194;
            public string queryid;
            public string query;
            public string url;
            public int documentPos;
            public double adjustRank;
            public string normalizedUrl;
            public MoviePath type;
            public string qaFact;
            public double movieScore;
            public double adjustedMovieScore;
            public double docScore;

            public static Line FromTSVLine(TSVLine l)
            {
                return new Line
                {
                    apf1194 = (long)l.GetFeatureValue("AdvancedPreferFeature_1194"),
                    adjustRank = double.Parse(l["AdjustedRank"]),
                    documentPos = (int)l.GetFeatureValue("DocumentPosition"),
                    normalizedUrl = l["m:NormalizedUrl"],
                    query = l["m:Query"],
                    url = l["m:Url"],
                    queryid = l["m:QueryId"],
                    type = (MoviePath)Enum.Parse(typeof(MoviePath), l["m:Type"]),
                    qaFact = l["m:QAFact"]
                };
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                    qaFact, apf1194, queryid, query, url, documentPos, adjustRank, normalizedUrl, type.ToString());
            }

            public static string Header
            {
                get
                {
                    return "m:QAFact\tAdvancedPreferFeature_1194\tm:QueryId\tm:Query\tm:Url\tDocumentPosition\tAdjustedRank\tm:NormalizedUrl\tm:Type";
                }
            }
        }

        const int MaxQAUrlCnt = 3;

        static void Rerank(ref List<Line> lines, HashSet<string> demoteHost)
        {
            int cntQAUrl = 0;
            foreach (var l in lines)
            {
                l.docScore = (50.0 - l.documentPos) * 100;
                string url = l.normalizedUrl;
                if (demoteHost.Contains(MyUtil.Normalizer.GetUrlHost(url)))
                {
                    l.docScore = 50.0 - l.documentPos;
                    continue;
                }

                if (IsQAUrl(url))
                {
                    if (cntQAUrl > MaxQAUrlCnt)
                    {
                        l.docScore = 100.0 - l.documentPos;
                    }

                    cntQAUrl++;
                }

                if (l.apf1194 > 0 && l.adjustedMovieScore > 0)
                {
                    if (l.type == MoviePath.Prod)
                    {
                        l.docScore = Math.Max(l.adjustedMovieScore, l.docScore);
                    }
                    else
                    {
                        l.docScore = l.adjustedMovieScore;
                    }
                }
            }
        }

        static bool IsQAUrl(string url)
        {
            string host = MyUtil.Normalizer.GetUrlHost(url);
            if (host == "answers.yahoo.com"
                || host == "chacha.com"
                || host == "answers.com"
                || host == "wiki.answers.com")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Re-rank web results based on movie's score and url information
        /// </summary>
        /// <param name="dictMovie2Score"></param>
        /// <param name="block"></param>
        /// <param name="topN"></param>
        /// <param name="minScore"></param>
        /// <param name="sw"></param>
        /// <returns></returns>
        static bool Rerank(Dictionary<long, double> dictMovie2Score,
            Dictionary<string, HashSet<string>> dictSatorId2RepUrls,
            QueryBlock block, int topN, double minScore, StreamWriter sw)
        {
            // step 1: inference
            bool triggered = false;
            Dictionary<long, double> top3Movies = new Dictionary<long, double>();
            if (dictMovie2Score != null && dictMovie2Score.Count > 0)
            {
                var temp = (from p in dictMovie2Score
                            where p.Value >= minScore
                            orderby p.Value descending
                            select p).Take(3);
                foreach (var t in temp)
                {
                    top3Movies[t.Key] = t.Value;
                }
            }

            // if no movie candidate is found, then do not re-rank.
            triggered = top3Movies.Count > 0;
            if (!triggered)
                return triggered;

            List<Line> gsLines = new List<Line>(),
                prodLines = new List<Line>(),
                imdbLines = new List<Line>(),
                apfLines = new List<Line>();
            var sorted = (from l in block.Lines orderby l.GetFeatureValue("DocumentPosition") ascending select l);
            foreach (var l in sorted)
            {
                long apf = (long)l.GetFeatureValue("AdvancedPreferFeature_1194");
                if (!top3Movies.ContainsKey(apf))
                    continue;

                var type = (MoviePath)Enum.Parse(typeof(MoviePath), l["m:Type"]);
                Line line = Line.FromTSVLine(l);
                switch (type)
                {
                    case MoviePath.GS:
                        gsLines.Add(line);
                        break;
                    case MoviePath.Prod:
                        prodLines.Add(line);
                        break;
                    case MoviePath.Imdb:
                        imdbLines.Add(line);
                        break;
                    case MoviePath.Apf:
                        apfLines.Add(line);
                        break;
                    default:
                        break;
                }
            }

            Dictionary<string, double> dictUrl2Score = new Dictionary<string, double>();
            // if score cannot differentiate the movies, then re-scoring them
            //  for now, force re-scoring
            //if (top3Movies.Count > 1 && top3Movies.Values.Max() == top3Movies.Values.Min())
            //{
            foreach (var l in prodLines)
            {
                double score = 100.0 - l.documentPos;
                long apf1194 = l.apf1194;
                string url = l.normalizedUrl;
                top3Movies[apf1194] = Math.Max(score, top3Movies[apf1194]);
                dictUrl2Score[url] = dictUrl2Score.ContainsKey(url) ? Math.Max(dictUrl2Score[url], score) : score;
            }

            foreach (var l in imdbLines)
            {
                double score = 80.0 - l.documentPos;
                long apf1194 = l.apf1194;
                string url = l.normalizedUrl;
                top3Movies[apf1194] = Math.Max(score, top3Movies[apf1194]);
                dictUrl2Score[url] = dictUrl2Score.ContainsKey(url) ? Math.Max(dictUrl2Score[url], score) : score;
            }

            foreach (var l in apfLines)
            {
                double score = 60.0 - l.documentPos;
                long apf1194 = l.apf1194;
                string url = l.normalizedUrl;
                top3Movies[apf1194] = Math.Max(score, top3Movies[apf1194]);
                dictUrl2Score[url] = dictUrl2Score.ContainsKey(url) ? Math.Max(dictUrl2Score[url], score) : score;
            }

            var adjustedGSLines = new List<Line>();
            foreach (var l in gsLines)
            {
                double score = 40.0 - l.documentPos;
                long apf1194 = l.apf1194;
                string url = l.normalizedUrl;
                top3Movies[apf1194] = Math.Max(score, top3Movies[apf1194]);
                HashSet<string> repUrls;
                if (!dictSatorId2RepUrls.TryGetValue(url, out repUrls))
                    continue;
                foreach (var repU in repUrls)
                {
                    if (repU.StartsWith("http://imdb.com/title/tt") && repU.IndexOf('/', "http://imdb.com/title/tt".Length) < 0)
                        dictUrl2Score[repU] = dictUrl2Score.ContainsKey(repU) ? Math.Max(dictUrl2Score[repU], score + 5) : score + 5;
                    else if (repU.StartsWith("http://en.wikipedia.org/wiki/"))
                        dictUrl2Score[repU] = dictUrl2Score.ContainsKey(repU) ? Math.Max(dictUrl2Score[repU], score + 4) : score + 4;
                    else if (repU.StartsWith("http://rottentomatoes.com/m/"))
                        dictUrl2Score[repU] = dictUrl2Score.ContainsKey(repU) ? Math.Max(dictUrl2Score[repU], score + 3) : score + 3;
                    else if (repU.StartsWith("http://netflix.com/movie/"))
                        dictUrl2Score[repU] = dictUrl2Score.ContainsKey(repU) ? Math.Max(dictUrl2Score[repU], score + 2) : score + 2;
                    else
                        dictUrl2Score[repU] = dictUrl2Score.ContainsKey(repU) ? Math.Max(dictUrl2Score[repU], score) : score;

                    Line newline = new Line
                    {
                        adjustRank = l.adjustRank,
                        apf1194 = l.apf1194,
                        documentPos = l.documentPos,
                        normalizedUrl = repU,
                        type = l.type,
                        queryid = l.queryid,
                        url = repU,
                        query = l.query,
                        qaFact = l.qaFact
                    };
                    adjustedGSLines.Add(newline);
                }
            }
            //}

            // step 2: merge
            List<Line> allProdLines = new List<Line>();
            foreach (var l in sorted)
            {
                var type = (MoviePath)Enum.Parse(typeof(MoviePath), l["m:Type"]);
                if (type != MoviePath.Prod)
                    continue;

                Line line = Line.FromTSVLine(l);
                allProdLines.Add(line);
            }

            var allValidLines = allProdLines
                             .Union(imdbLines)
                             .Union(apfLines)
                             .Union(adjustedGSLines);

            // step 3: re-rank
            HashSet<string> processedUrls = new HashSet<string>();
            HashSet<string> processedQAUrls = new HashSet<string>();
            foreach (var line in allValidLines)
            {
                int score;
                try
                {
                    int position = line.documentPos;
                    if (position > topN)
                        continue;

                    long apf1194 = line.apf1194;
                    string qaFact = line.qaFact;
                    string url = line.normalizedUrl;
                    if (processedUrls.Contains(url))
                    {
                        continue;
                    }

                    processedUrls.Add(url);
                    MoviePath type = line.type;

                    score = 100 - position;
                    double movieScore = 0;
                    if (apf1194 > 0)
                    {
                        if (top3Movies.TryGetValue(apf1194, out movieScore))
                        {
                            score = (int)(10 * movieScore + dictUrl2Score[url]);
                            if (type == MoviePath.Prod)
                            {
                                score += 10000 - position;
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(qaFact) && type == MoviePath.Prod)
                    {
                        bool foundMatch = false;
                        foreach (var f in qaFact.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            long cand = long.Parse(f.Split('|')[0]);
                            if (top3Movies.TryGetValue(cand, out movieScore))
                            {
                                //maxMovieScore = Math.Max(movieScore, maxMovieScore);
                                foundMatch = true;
                                break;
                            }
                        }

                        if (foundMatch && processedQAUrls.Count < 2)
                        {
                            score = 105 - position;
                            processedQAUrls.Add(url);
                        }
                    }

                    sw.WriteLine(line.ToString() + "\t" + movieScore + "\t" + score);
                }
                catch { }
            }

            return triggered;
        }

        /// <summary>
        /// Merge query lines, 1st roung scoring based on movie score
        /// </summary>
        /// <param name="top3Movies"></param>
        /// <param name="dictSatorId2RepUrls"></param>
        /// <param name="topN"></param>
        /// <param name="levelThresholds"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        static List<Line> Merge(Dictionary<long, double> dictMovie2Score,
            Dictionary<string, HashSet<string>> dictSatorId2RepUrls,
            QueryBlock block,
            int topN,
            double minScore,
            double[] levelThresholds, 
            out bool triggered)
        {
            bool hasDefinitive = false;
            Dictionary<long, double> top3Movies = new Dictionary<long, double>();
            if (dictMovie2Score != null && dictMovie2Score.Count > 0)
            {
                var temp = (from p in dictMovie2Score
                            where p.Value >= minScore
                            orderby p.Value descending
                            select p).Take(3).ToArray();

                if (temp.Length == 1 && temp[0].Value >= levelThresholds[0])
                {
                    hasDefinitive = true;
                }
                else if (temp.Length > 1 && temp[0].Value >= levelThresholds[0] && temp[0].Value >= 2.0 + temp[1].Value)
                {
                    hasDefinitive = true;
                }

                if (hasDefinitive)
                {
                    top3Movies[temp[0].Key] = temp[0].Value;
                    Console.WriteLine("{0}\t{1}", block.Lines[0]["m:Query"], temp[0].Value);
                }
                else
                {
                    foreach (var t in temp)
                    {
                        top3Movies[t.Key] = t.Value;
                    }
                }
            }

            triggered = (top3Movies.Count != 0);

            // raw merge
            List<Line> lines = (from l in block.Lines select Line.FromTSVLine(l)).ToList();
            List<Line> gsLines = (from l in lines
                                  where l.type == MoviePath.GS && top3Movies.ContainsKey(l.apf1194)
                                  orderby l.documentPos ascending
                                  select l).ToList();
            lines = (from l in lines 
                     where l.type != MoviePath.GS && (top3Movies.ContainsKey(l.apf1194) || (l.type == MoviePath.Prod && l.documentPos <= topN))
                     orderby l.type ascending, l.documentPos ascending
                     select l)
                     .ToList();
            gsLines = ConvertGSUrl2RepUrl(gsLines, dictSatorId2RepUrls);
            if (null != gsLines)
            {
                lines.AddRange(gsLines);
            }

            // de-dup
            lines = Dedup(lines);

            // movie re-scoring
            Dictionary<long, int> processedMovieUrls = new Dictionary<long, int>();
            foreach (var m in top3Movies)
            {
                processedMovieUrls.Add(m.Key, 0);
            }

            int firstNonTopMovieUrlPos = 20;
            foreach (var l in lines)
            {
                if (l.apf1194 <= 0)
                    continue;

                if (l.type == MoviePath.Prod && l.apf1194 > 0 && !top3Movies.ContainsKey(l.apf1194))
                {
                    firstNonTopMovieUrlPos = Math.Min(l.documentPos, firstNonTopMovieUrlPos);
                    continue;
                }

                double origScore = top3Movies[l.apf1194];
                l.adjustedMovieScore = 3900;
                l.movieScore = origScore;

                int processedUrls = processedMovieUrls[l.apf1194];
                // Level 1 movie
                if (origScore >= levelThresholds[0])
                {
                    if (processedUrls < 2)
                    {
                        l.adjustedMovieScore = 10000 - processedUrls;
                    }
                    else if (processedUrls == 2)
                    {
                        l.adjustedMovieScore = 9997;
                    }
                    else if (processedUrls == 3)
                    {
                        l.adjustedMovieScore = hasDefinitive ? 9996 : 4198;
                    }
                    else if (processedUrls == 4)
                    {
                        l.adjustedMovieScore = hasDefinitive ? 9995 : 3994;
                    }
                }
                // Level 2
                else if (origScore >= levelThresholds[1])
                {
                    if (processedUrls == 0)
                    {
                        l.adjustedMovieScore = 9998;
                    }
                    else if (processedUrls == 1)
                    {
                        l.adjustedMovieScore = 9996;
                    }
                    else if (processedUrls == 2)
                    {
                        l.adjustedMovieScore = 4197;
                    }
                    else if (processedUrls == 3)
                    {
                        l.adjustedMovieScore = 4095;
                    }
                    else if (processedUrls == 4)
                    {
                        l.adjustedMovieScore = 3993;
                    }
                }
                // Level 3
                else if (origScore >= levelThresholds[2])
                {
                    if (processedUrls == 0)
                    {
                        l.adjustedMovieScore = Math.Max(50.0 + (50.0 - firstNonTopMovieUrlPos) * 100, 4201);
                    }
                    if (processedUrls == 1)
                    {
                        l.adjustedMovieScore = 4199;
                    }
                    else if (processedUrls == 2)
                    {
                        l.adjustedMovieScore = 4096;
                    }
                    else if (processedUrls == 3)
                    {
                        l.adjustedMovieScore = 3992;
                    }
                    else if (processedUrls == 4)
                    {
                        l.adjustedMovieScore = 3991;
                    }
                }

                ++processedMovieUrls[l.apf1194];
            }

            return lines;
        }

        static List<Line> Dedup(List<Line> lines)
        {
            List<Line> deduped = new List<Line>();
            HashSet<string> urls = new HashSet<string>();
            int pos = 1;
            foreach (var l in lines)
            {
                if (urls.Contains(l.normalizedUrl))
                    continue;
                urls.Add(l.normalizedUrl);
                l.documentPos = pos++;
                deduped.Add(l);
            }

            return deduped;
        }

        static List<Line> ConvertGSUrl2RepUrl(List<Line> gsLines, Dictionary<string, HashSet<string>> dictSatorId2RepUrls)
        {
            Dictionary<string, double> dictHost2Score = new Dictionary<string, double>();
            dictHost2Score.Add("imdb.com", 5);
            dictHost2Score.Add("en.wikipedia.org", 4);
            dictHost2Score.Add("rottentomatoes.com", 3);

            List<Line> repLines = new List<Line>();
            foreach (var l in gsLines)
            {
                string url = l.normalizedUrl;
                HashSet<string> repUrls;
                if (!dictSatorId2RepUrls.TryGetValue(url, out repUrls))
                    continue;

                var sorted = from u in repUrls
                             orderby (dictHost2Score.ContainsKey(MyUtil.Normalizer.GetUrlHost(u)) ? dictHost2Score[MyUtil.Normalizer.GetUrlHost(u)] : 0) descending
                             select u;

                foreach (var repU in sorted)
                {
                    Line newline = new Line
                    {
                        adjustRank = l.adjustRank,
                        apf1194 = l.apf1194,
                        documentPos = l.documentPos,
                        normalizedUrl = repU,
                        type = l.type,
                        queryid = l.queryid,
                        url = repU,
                        query = l.query,
                        qaFact = l.qaFact
                    };
                    repLines.Add(newline);
                }
            }

            return repLines;
        }
    }
}
