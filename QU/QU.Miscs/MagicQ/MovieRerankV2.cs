using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;

namespace QU.Miscs.MagicQ
{
    public class MovieRerankV2
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "extraction")]
            public string Extraction = "";

            [Argument(ArgumentType.Required, ShortName = "feature")]
            public string FeatureOutput = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "topn")]
            public int TopN = 20;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            TSVLine headerLine;
            List<QueryBlock> blocks = null;
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.Extraction)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                QueryBlockReader qbReader = new QueryBlockReader(tsvReader);
                blocks = qbReader.ReadQueryBlocks().ToList();
                headerLine = tsvReader.HeaderTSVLine;
            }

            using (StreamWriter swFeature = new StreamWriter(arguments.FeatureOutput))
            {
                swFeature.WriteLine("m:QueryId\tm:Query\tm:Url\tm:QAFact\tDocumentPosition\tAdvancedPreferFeature_1194\tMovieScore\tDRScore\tRerankScore");
                foreach (var block in blocks)
                {
                    string query = block.Lines.First()["m:Query"];
                    var movie2info = GetMovieInfo(block, arguments.TopN);

                    foreach (var line in block.Lines)
                    {
                        string url = line["m:Url"];
                        int position = (int)line.GetFeatureValue("DocumentPosition");
                        if (position > arguments.TopN)
                            continue;

                        long apf1194 = (long)line.GetFeatureValue("AdvancedPreferFeature_1194");
                        MoviePath type = (MoviePath)Enum.Parse(typeof(MoviePath), line["m:Type"]);
                        string qaFact = line["m:QAFact"];
                        double l2score = double.Parse(line["AdjustedRank"]);
                        int qid = int.Parse(line["m:QueryId"]);

                        Dictionary<long, int> QAFacts = new Dictionary<long, int>();
                        if (!string.IsNullOrEmpty(qaFact) && type == MoviePath.Prod)
                        {
                            QAFacts = MovieRankingUtility.ExtractQAFacts(qaFact);
                        }

                        double movieScore = 0;
                        if (apf1194 > 0)
                        {
                            double s = movie2info[apf1194].Score();
                            if (s > 80)
                                movieScore = s * 0.4;
                        }
                        else if (QAFacts.Count != 0)
                        {
                            foreach (var f in QAFacts)
                            {
                                double s = movie2info.ContainsKey(f.Key) ? movie2info[f.Key].Score() : 0;
                                if (s > 80)
                                    movieScore += s * 0.2;
                            }
                        }

                        swFeature.WriteLine(string.Join("\t", new string[] {qid.ToString(), query, url, qaFact,
                                                                            position.ToString(), 
                                                                            apf1194.ToString(), 
                                                                            ((int)(movieScore * 1000)).ToString(),
                                                                            ((int)(l2score * 1000)).ToString(),
                                                                            ((int)((l2score * 0.6 + movieScore * 0.4) * 1000)).ToString()
                                                                           }
                                                       )
                                            );
                    }
                }
            }
        }

        static Dictionary<long, MovieInfo> GetMovieInfo(QueryBlock block, int topn)
        {
            Dictionary<long, MovieInfo> validMovie2info = new Dictionary<long, MovieInfo>();

            Dictionary<long, MovieInfo> movie2info = new Dictionary<long, MovieInfo>();
            HashSet<long> validApfs = new HashSet<long>();
            foreach (var line in block.Lines)
            {
                try
                {
                    string url = line["m:Url"];
                    int position = (int)line.GetFeatureValue("DocumentPosition");
                    if (position > topn)
                        continue;

                    MoviePath type = (MoviePath)Enum.Parse(typeof(MoviePath), line["m:Type"]);

                    long apf1194 = (long)line.GetFeatureValue("AdvancedPreferFeature_1194");
                    if (apf1194 > 0)
                    {
                        validApfs.Add(apf1194);
                    }
                    double l2score = double.Parse(line["AdjustedRank"]);

                    if (apf1194 > 0)
                    {
                        if (!movie2info.ContainsKey(apf1194))
                        {
                            movie2info.Add(apf1194, new MovieInfo());
                        }

                        double score = l2score;
                        double imdbweight = 0.4, apfweight = 0.4;

                        if (type == MoviePath.Prod)
                        {
                            movie2info[apf1194].prodPathScore += l2score;
                            movie2info[apf1194].bestProdScore = Math.Max(l2score, movie2info[apf1194].bestProdScore);
                            movie2info[apf1194].highestPos = Math.Min(movie2info[apf1194].highestPos, position);
                        }
                        else if (type == MoviePath.Imdb)
                        {
                            movie2info[apf1194].imdbPathScore += l2score;
                            movie2info[apf1194].bestOtherScore = Math.Max(l2score, movie2info[apf1194].bestOtherScore);
                            score *= imdbweight;
                        }
                        else if (type == MoviePath.Apf)
                        {
                            movie2info[apf1194].apfPathScore += l2score;
                            movie2info[apf1194].bestOtherScore = Math.Max(l2score, movie2info[apf1194].bestOtherScore);
                            score *= apfweight;
                        }

                        if (!movie2info[apf1194].dictUrl2Score.ContainsKey(url))
                        {
                            movie2info[apf1194].dictUrl2Score[url] = score;
                        }
                        else
                        {
                            movie2info[apf1194].dictUrl2Score[url] = Math.Max(score, movie2info[apf1194].dictUrl2Score[url]);
                        }

                        continue;
                    }

                    string qaFact = line["m:QAFact"];

                    Dictionary<long, int> QAFacts = null;
                    if (!string.IsNullOrEmpty(qaFact) && type == MoviePath.Prod)
                    {
                        QAFacts = MovieRankingUtility.ExtractQAFacts(qaFact);

                        foreach (var f in QAFacts.Keys)
                        {
                            if (!movie2info.ContainsKey(f))
                            {
                                movie2info.Add(f, new MovieInfo());
                            }

                            //movie2info[f].totalScore += l2score;
                            movie2info[f].highestPos = Math.Min(movie2info[f].highestPos, position);
                            movie2info[f].bestProdScore = Math.Max(l2score, movie2info[f].bestProdScore);
                            if (!movie2info[f].dictUrl2Score.ContainsKey(url))
                            {
                                movie2info[f].dictUrl2Score[url] = l2score;
                            }
                            else
                            {
                                movie2info[f].dictUrl2Score[url] = Math.Max(l2score, movie2info[f].dictUrl2Score[url]);
                            }

                            movie2info[f].qaScore += l2score;
                        }
                    }
                }
                catch { }
            } // lines

            foreach (var p in movie2info)
            {
                if (validApfs.Contains(p.Key))
                    validMovie2info.Add(p.Key, p.Value);
            }

            return validMovie2info;
        }

        class MovieInfo
        {
            public double prodPathScore = 0;
            public int highestPos = 20;
            public double imdbPathScore = 0;
            public double apfPathScore = 0;
            public double bestProdScore = 0;
            public double bestOtherScore = 0;
            public Dictionary<string, double> dictUrl2Score = new Dictionary<string, double>();
            public double qaScore = 0;

            public double Score()
            {
                return qaScore + 0.4 * prodPathScore + 0.3 * imdbPathScore + 0.3 * apfPathScore;
            }
        }
    }
}
