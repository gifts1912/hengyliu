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
    public class MeasureProdMovieCandidates
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
            List<QueryBlock> blocks;
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.Extraction)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                QueryBlockReader qbReader = new QueryBlockReader(tsvReader);
                blocks = qbReader.ReadQueryBlocks().ToList();
                headerLine = tsvReader.HeaderTSVLine;
            }

            using (StreamWriter swFeature = new StreamWriter(arguments.FeatureOutput))
            {
                swFeature.WriteLine("m:Query\tm:Url\tDocumentPosition\tAdvancedPreferFeature_1194\tProdPathScore\tm:QAFact\tImdbScore\tApfScore\tQAScore\tWeightedScore");
                int total = 0, right = 0;
                foreach (var block in blocks)
                {
                    string query = block.Lines.First()["m:Query"];
                    HashSet<long> validApfs = new HashSet<long>();
                    Dictionary<long, info> movie2info = new Dictionary<long, info>();
                    foreach (var line in block.Lines)
                    {
                        try
                        {
                            string url = line["m:Url"];
                            int position = (int)line.GetFeatureValue("DocumentPosition");
                            if (position > arguments.TopN)
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
                                    movie2info.Add(apf1194, new info());
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
                                        movie2info.Add(f, new info());
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

                    var sorted = from p in movie2info
                                 //where validApfs.Contains(p.Key) && (p.Value.totalScore >= 100 || p.Value.imdbPathScore >= 100 || p.Value.apfPathScore >= 100)
                                 //orderby p.Value.totalScore descending, (p.Value.imdbPathScore + p.Value.apfPathScore) descending, p.Value.highestPos ascending
                                 //where validApfs.Contains(p.Key) && (p.Value.dictUrl2Score.Values.Sum() >= 90)
                                 //orderby p.Value.dictUrl2Score.Values.Sum() descending, p.Value.totalScore descending
                                 where validApfs.Contains(p.Key) && (p.Value.Score() > 80)
                                 orderby (p.Value.Score()) descending
                                 select p;
                    int id = 1;
                    foreach (var p in sorted)
                    {
                        swFeature.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                            query, p.Key, id++, p.Key, p.Value.prodPathScore, "",
                            p.Value.imdbPathScore, p.Value.apfPathScore, p.Value.qaScore, p.Value.Score());
                    }
                }
            }
        }

        class info
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
