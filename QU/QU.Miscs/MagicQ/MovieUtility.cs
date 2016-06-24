using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TSVUtility;

namespace QU.Miscs.MagicQ
{
    public class MovieUtility
    {
        private static string[] MagicMovieAnchor = new string[] 
        {
            "movie where",
            "movie about",
            "movie in which",
            "movie in what",
            "movies where",
            "movies in which",
            "movies in what",
            "film where",
            "film about",
            "film in which",
            "film in what",
            "films where",
            "films in which",
            "films in what",
        };

        private static Regex MagicMovieAnchorRegex = new Regex(
            "(in what|in which|what is the name of the|which is the name of|what is the|which is the)\\s+(movie|movies|movi|imovie|moview|movis|movei|themovie|moviw|moveis|moviee|mmovie|moviie|movvie|film|films|filmovi|filme|filmy)\\s+(quote|quotes|plot|plot|name)?\\s*(where|in which|that|about|with|when)?|(in what|in which|what is the name of the|which is the name of|what is the|which is the)?\\s*(movie|movies|movi|imovie|moview|movis|movei|themovie|moviw|moveis|moviee|mmovie|moviie|movvie|film|films|filmovi|filme|filmy)\\s+(quote|quotes|plot|plot|name)?\\s*(where|in which|that|about|with|when)\\s+",
            RegexOptions.Compiled);

        private static Regex MagicMovieRegex = new Regex(
            @"(\s|^)(in what|in which|what is the name of the|which is the name of|what is the|which is the|whats|what s|whats the|what s the)\s+(movie|movies|movi|imovie|moview|movis|movei|themovie|moviw|moveis|moviee|mmovie|moviie|movvie|film|films|filmovi|filme|filmy)(\s|$)|(\s|^)(movie|movies|movi|imovie|moview|movis|movei|themovie|moviw|moveis|moviee|mmovie|moviie|movvie|film|films|filmovi|filme|filmy)\s+(quote\s+|quotes\s+|plot\s+|plot\s+|name\s+|)(where|in which|that|about|with|when)(\s|$)",
            RegexOptions.Compiled);

        private static HashSet<string> MagicMovieRegexStopwords = new HashSet<string>(
            "movie|movies|movi|imovie|moview|movis|movei|themovie|moviw|moveis|moviee|mmovie|moviie|movvie|film|films|filmovi|filme|filmy|quote|quotes|plot|plot|name|where|in which|that|about|with|when|in what|in which|what is the name of the|which is the name of|what is the|which is the|whats|what s|whats the|what s the|a|an".Split('|', ' ')
            );

        public static bool IsMagicMovie(string query)
        {
            return MagicMovieRegex.IsMatch(query);
        }

        public static string RemoveRegexStopwords(string query)
        {
            return string.Join(" ", query.Split(' ').Where(q => !MagicMovieRegexStopwords.Contains(q)));
        }

        private static string[] Separator = new string[] { "###" };

        private static HashSet<string> StopwordsInAttribute = new HashSet<string>(new string[] 
            {
                "what", "what's", "whats", "is", "isnt", "was", "are", "were",
                "in", "the", "name", "of", "names", "that", "those", "a", "why",
                "one", "called", "it", "at", "his", "her", "its", "it's", "it s",
                "there"
            }
        );

        private static HashSet<string> StopwordsInDescription = new HashSet<string>(new string[] 
            {
                "they", "he", "she", "it", "him", "her", "him", "his", "its", "their",
                //"man", "woman", "men", "women", "lady", "guy",
                "these", "those", "this", "that", "the",
                "is", "are", "am", "was", "were", "have", "has", "had", 
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
                "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "and", "but", "or", "in", "if", "on", "to", "of", "by", "for", "then", "before", "after"
            }
        );

        public static void SetMagicMovieAnchor(string anchorRegex)
        {
            MagicMovieAnchorRegex = new Regex(anchorRegex, RegexOptions.Compiled);
        }

        public static void SetStopwordsInAttribute(string[] stopwords)
        {
            StopwordsInAttribute = new HashSet<string>(stopwords);
        }

        public static MagicMovieContext UnderstandMagicMovie(string query)
        {
            MagicMovieContext context = new MagicMovieContext();
            string q = MagicMovieAnchorRegex.Replace(query.ToLower(), Separator[0]);
            string[] items = q.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

            if (items == null || items.Length == 0)
                return null;

            if (items.Length < 2)
            {
                context.OrigDescription = items[0].Trim();
                context.Description = string.Join(" ", items[0].Split(' ').Where(i => !StopwordsInDescription.Contains(i))).Trim();
            }
            else
            {
                context.OrigDescription = items[1].Trim();
                context.OrigAttribute = items[0].Trim();
                context.Attribute = string.Join(" ", items[0].Split(' ').Where(i => !StopwordsInAttribute.Contains(i))).Trim();
                context.Description = string.Join(" ", items[1].Split(' ').Where(i => !StopwordsInDescription.Contains(i))).Trim();
            }

            return context;
        }

        public static bool IsFilmUrl(string url)
        {
            if (url.StartsWith("http://imdb.com/title/")
                || url.Contains("netflix.com/movie/")
                || url.StartsWith("http://en.wikipedia.org/wiki/"))
                return true;
            return false;
        }

        public static bool IsImdbFilmUrl(string url)
        {
            if (url.StartsWith("http://imdb.com/title/"))
                return true;
            return false;
        }

        public static bool IsWikiUrl(string url)
        {
            if (url.StartsWith("http://en.wikipedia.org/wiki/"))
                return true;
            return false;
        }

        public static bool IsNetflixUrl(string url)
        {
            if (url.Contains("netflix.com/movie/"))
                return true;
            return false;
        }

        public static string CanonicalFilmUrl(string url)
        {
            if (url.StartsWith("http://imdb.com/title/"))
            {
                string temp = url.Substring("http://imdb.com/title/".Length);
                int slash = temp.IndexOf('/');
                if (slash < 0)
                    return url;
                else
                    return url.Substring(0, "http://imdb.com/title/".Length + slash);
            }

            return url;
        }

        public static void ReadMappingFile(string mappingFile,
            out Dictionary<string, string> url2nameMapping,
            out Dictionary<string, HashSet<string>> url2urlsMapping)
        {
            url2nameMapping = new Dictionary<string, string>();
            url2urlsMapping = new Dictionary<string, HashSet<string>>();

            Dictionary<string, HashSet<string>> sid2urlsMapping = new Dictionary<string, HashSet<string>>();

            using (StreamReader sr = new StreamReader(mappingFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length < 4)
                        continue;

                    string name = items[1];
                    string url = MovieUtility.CanonicalFilmUrl(items[3]);

                    // Get the most confident name for a url.
                    if (url2nameMapping.ContainsKey(url))
                        continue;

                    if (!sid2urlsMapping.ContainsKey(items[0]))
                    {
                        sid2urlsMapping.Add(items[0], new HashSet<string>());
                    }
                    sid2urlsMapping[items[0]].Add(url);

                    url2nameMapping[url] = name.ToLower();
                }
            }

            foreach (var pair in sid2urlsMapping)
            {
                foreach (var u in pair.Value)
                {
                    if (!url2urlsMapping.ContainsKey(u))
                        url2urlsMapping.Add(u, pair.Value);
                    else
                    {
                        foreach (var v in pair.Value)
                        {
                            url2urlsMapping[u].Add(v);
                        }
                    }
                }
            }
        }
    }

    public class MagicMovieContext
    {
        public string Attribute;
        public string OrigAttribute;
        public string Description;
        public string OrigDescription;
    }

    public class MovieRankingUtility
    {
        public static void ReadFeatureFile(string featureFile,
            out Dictionary<string, Dictionary<long, MovieCandidateFeature>> dictQuery2CandidateFeature,
            out Dictionary<string, Dictionary<long, double>> dictQuery2CandidateScore)
        {
            dictQuery2CandidateFeature = new Dictionary<string, Dictionary<long, MovieCandidateFeature>>();
            dictQuery2CandidateScore = new Dictionary<string, Dictionary<long, double>>();

            using (StreamReader sr = new StreamReader(featureFile))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    string[] items = line.Split('\t');
                    try
                    {
                        if (!dictQuery2CandidateFeature.ContainsKey(items[0]))
                            dictQuery2CandidateFeature[items[0]] = new Dictionary<long, MovieCandidateFeature>();

                        if (!dictQuery2CandidateScore.ContainsKey(items[0]))
                            dictQuery2CandidateScore[items[0]] = new Dictionary<long, double>();

                        long cand = long.Parse(items[1]);
                        dictQuery2CandidateFeature[items[0]][cand] = MovieCandidateFeature.FromLongString(items, 2);
                        dictQuery2CandidateScore[items[0]][cand] = double.Parse(items[2 + MovieCandidateFeature.LongHeader.Split('\t').Length]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: {0}, Exception: {1}", line, ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Extract features for candidates
        /// </summary>
        /// <param name="block"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public static Dictionary<long, MovieCandidateFeature> ExtractMovieCandidateFeature(
            QueryBlock block,
            int topN)
        {
            Dictionary<long, MovieCandidateFeature> dictMovie2Features = new Dictionary<long, MovieCandidateFeature>();

            foreach (var line in block.Lines)
            {
                try
                {
                    string query = line["m:Query"];
                    string url = line["m:Url"];
                    int position = (int)line.GetFeatureValue("DocumentPosition");
                    if (position > topN)
                        continue;

                    MoviePath type = (MoviePath)Enum.Parse(typeof(MoviePath), line["m:Type"]);
                    uint apf1194 = line.GetFeatureValue("AdvancedPreferFeature_1194");
                    string qaFact = line["m:QAFact"];
                    double l2score = double.Parse(line["AdjustedRank"]);

                    if (apf1194 > 0)
                    {
                        if (!dictMovie2Features.ContainsKey(apf1194))
                        {
                            dictMovie2Features.Add(apf1194, new MovieCandidateFeature(topN));
                        }

                        if (type == MoviePath.Prod)
                        {
                            ++dictMovie2Features[apf1194].ProdWebOcc;
                            dictMovie2Features[apf1194].ProdTopWebPos = Math.Min(topN + 2, Math.Min(dictMovie2Features[apf1194].ProdTopWebPos, position));
                            dictMovie2Features[apf1194].ProdTopWebL2Score = Math.Max(dictMovie2Features[apf1194].ProdTopWebL2Score, 100 + l2score);
                            dictMovie2Features[apf1194].ProdHasWeb = true;
                            dictMovie2Features[apf1194].ProdWebScore += (1.0 / position);
                        }
                        else if (type == MoviePath.Imdb)
                        {
                            ++dictMovie2Features[apf1194].ImdbOcc;
                            dictMovie2Features[apf1194].ImdbTopPos = Math.Min(topN + 2, Math.Min(dictMovie2Features[apf1194].ImdbTopPos, position));
                            dictMovie2Features[apf1194].ImdbTopL2Score = Math.Max(dictMovie2Features[apf1194].ImdbTopL2Score, 80 + l2score);
                            dictMovie2Features[apf1194].ImdbScore += (1.0 / position);
                        }
                        else if (type == MoviePath.Apf)
                        {
                            ++dictMovie2Features[apf1194].ApfOcc;
                            dictMovie2Features[apf1194].ApfTopPos = Math.Min(topN + 2, Math.Min(dictMovie2Features[apf1194].ApfTopPos, position));
                            dictMovie2Features[apf1194].ApfTopL2Score = Math.Max(dictMovie2Features[apf1194].ApfTopL2Score, l2score);
                            dictMovie2Features[apf1194].ApfScore += (1.0 / position);
                        }
                        else if (type == MoviePath.GS)
                        {
                            dictMovie2Features[apf1194].GSTopPos = Math.Min(topN + 2, Math.Min(dictMovie2Features[apf1194].GSTopPos, position));
                            dictMovie2Features[apf1194].GSTopL2Score = Math.Max(dictMovie2Features[apf1194].GSTopL2Score, 80 + l2score);
                            ++dictMovie2Features[apf1194].GSOcc;
                            dictMovie2Features[apf1194].GSScore += (1.0 / position);
                        }
                    }

                    Dictionary<long, int> QAFacts = null;
                    if (!string.IsNullOrEmpty(qaFact) && type == MoviePath.Prod)
                    {
                        QAFacts = ExtractQAFacts(qaFact);

                        foreach (var f in QAFacts)
                        {
                            if (!dictMovie2Features.ContainsKey(f.Key))
                            {
                                dictMovie2Features.Add(f.Key, new MovieCandidateFeature(topN));
                            }

                            ++dictMovie2Features[f.Key].ProdQAOcc;
                            dictMovie2Features[f.Key].ProdTopQAL2Score = Math.Max(dictMovie2Features[f.Key].ProdTopQAL2Score, 100 + l2score);
                            dictMovie2Features[f.Key].ProdTopQAPos = Math.Min(dictMovie2Features[f.Key].ProdTopQAPos, position);
                            dictMovie2Features[f.Key].ProdHasQAFact = true;
                            dictMovie2Features[f.Key].ProdQAScore += (1.0 / position);
                        }
                    }
                }
                catch { }
            }

            return dictMovie2Features;
        }

        /// <summary>
        /// Extract (id, score) pair for QA url.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Dictionary<long, int> ExtractQAFacts(string str)
        {
            Dictionary<long, int> QAFacts = new Dictionary<long, int>();
            foreach (var f in str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] items = f.Split('|');
                int score = 1;
                if (items.Length > 1)
                    score = int.Parse(items[1]);
                QAFacts[long.Parse(items[0])] = score;
            }

            return QAFacts;
        }


        /// <summary>
        /// Evaluate movie candidate's score.
        /// </summary>
        /// <param name="evaluator"></param>
        /// <param name="dictMovie2Features"></param>
        /// <param name="shouldHaveQA"></param>
        public static Dictionary<long, double> EvaluateMovieCandidates(MovieExpressionEvaluator evaluator,
            Dictionary<long, MovieCandidateFeature> dictMovie2Features)
        {
            Dictionary<long, double> dictMovie2Score = new Dictionary<long, double>();
            foreach (var cand in dictMovie2Features)
            {
                if (!cand.Value.ProdHasWeb
                    && cand.Value.ImdbOcc == 0
                    && cand.Value.ApfOcc == 0
                    && cand.Value.GSOcc == 0)
                    continue;

                if (evaluator != null)
                {
                    dictMovie2Score[cand.Key] = evaluator.Evaluate(cand.Value);
                }
                else
                {
                    dictMovie2Score[cand.Key] = cand.Value.Scoring();
                }
            }

            return dictMovie2Score;
        }

        /// <summary>
        /// Read truth file.
        /// </summary>
        /// <param name="truthFile"></param>
        /// <param name="queryCol"></param>
        /// <param name="candCol"></param>
        /// <param name="scoreCol"></param>
        /// <returns></returns>
        public static Dictionary<string, int> ReadTruth(string truthFile, int queryCol = 1, int candCol = 0, int scoreCol = 2)
        {
            Dictionary<string, int> truth = new Dictionary<string, int>();
            using (StreamReader sr = new StreamReader(truthFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string[] items = line.Split('\t');
                    if (string.IsNullOrEmpty(items[candCol]))
                        continue;

                    string key = BuildKey(items[queryCol], items[candCol]);
                    //truth.Add(key);
                    if (!truth.ContainsKey(key))
                    {
                        int temp;
                        if (int.TryParse(items[scoreCol], out temp))
                        {
                            truth.Add(key, temp);
                        }
                        else
                        {
                            if (items.Equals("Bad"))
                            {
                                //truth.Add(key, 0);
                            }
                            else if (items.Equals("Fair"))
                            {
                                //truth.Add(key, 0);
                            }
                            else if (items.Equals("Good"))
                            {
                                //truth.Add(key, 0);
                            }
                            else if (items.Equals("Excellent") || items.Equals("Perfect"))
                            {
                                truth.Add(key, 1);
                            }
                        }
                    }
                }
            }

            return truth;
        }

        /// <summary>
        /// Build key for feature retrieval.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cand"></param>
        /// <returns></returns>
        public static string BuildKey(string query, string cand)
        {
            return query + "|" + cand;
        }

        /// <summary>
        /// Read Satori Id to List of representative urls
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Dictionary<string, HashSet<string>> ReadSatoriId2RepUrlsMapping(string file)
        {
            Dictionary<string, HashSet<string>> dict = new Dictionary<string, HashSet<string>>();
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(file)))
            {
                TSVReader tsvReader = new TSVReader(sr, true);
                while (!tsvReader.EndOfTSV)
                {
                    var line = tsvReader.ReadLine();
                    string url = line["m:Url"];
                    string repUrl = line["m:RepUrl"];
                    if (!dict.ContainsKey(url))
                        dict[url] = new HashSet<string>();
                    dict[url].Add(repUrl);
                }
            }

            return dict;
        }

        public static HashSet<string> ReadDemotionHosts(string file)
        {
            HashSet<string> dict = new HashSet<string>();
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    string[] items = line.Split('\t');
                    dict.Add(items[0]);
                }
            }

            return dict;
        }
    }
}
