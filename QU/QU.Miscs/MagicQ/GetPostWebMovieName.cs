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
    public class GetPostWebMovieName
    {
        class Args : CmdOptions
        {
            //[Argument(ArgumentType.Required, ShortName = "g")]
            //public string GoogleScrapeFile;

            [Argument(ArgumentType.Required, ShortName = "b")]
            public string BingScrapeFile;

            //[Argument(ArgumentType.AtMostOnce, ShortName = "gn")]
            //public int GoogleTopN = 10;

            [Argument(ArgumentType.AtMostOnce, ShortName = "bn")]
            public int BingTopN = 10;

            [Argument(ArgumentType.Required, ShortName = "m")]
            public string MappingFile;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;

            [Argument(ArgumentType.Required, ShortName = "imdb")]
            public string ImdbOutput;

            [Argument(ArgumentType.Required, ShortName = "wiki")]
            public string WikiOutput;

            [Argument(ArgumentType.Required, ShortName = "netflix")]
            public string NetflixOutput;
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

            //var gScrape = new Dictionary<string, Dictionary<string, int>>();
            //Util.ScrapeUtility.ReadScrape(arguments.GoogleScrapeFile, arguments.GoogleTopN, out gScrape);

            var bScrape = new Dictionary<string, Dictionary<string, int>>();
            MyUtil.ScrapeUtility.ReadScrape(arguments.BingScrapeFile, arguments.BingTopN, out bScrape);

            Dictionary<string, string> url2NameMapping;
            Dictionary<string, HashSet<string>> url2UrlsMapping;
            MovieUtility.ReadMappingFile(arguments.MappingFile, out url2NameMapping, out url2UrlsMapping);

            int triggered = 0;
            int atLeast2Movies = 0;

            StreamWriter swImdb = new StreamWriter(arguments.ImdbOutput);
            StreamWriter swWiki = new StreamWriter(arguments.WikiOutput);
            StreamWriter swNetflix = new StreamWriter(arguments.NetflixOutput);
            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                foreach (var b in bScrape)
                {
                    List<Tuple<string, string, int>> filmUrls = GetFilmsInTopResults(b.Value, url2NameMapping);

                    triggered += filmUrls.Count > 0 ? 1 : 0;

                    Dictionary<string, MovieInfo> dictMovie2Info = new Dictionary<string, MovieInfo>();
                    foreach (var f in filmUrls)
                    {
                        if (dictMovie2Info.ContainsKey(f.Item2))
                        {
                            continue;
                        }

                        MovieInfo info = new MovieInfo();
                        info.movieName = f.Item2;
                        info.representativUrls.AddRange(url2UrlsMapping[f.Item1]);
                        dictMovie2Info.Add(f.Item2, info);
                    }

                    if (dictMovie2Info.Count > 1)
                    {
                        atLeast2Movies++;
                    }
                    else
                    {
                        foreach (var p in dictMovie2Info)
                        {
                            sw.WriteLine(b.Key + "\t" + p.Key + "\t" + string.Join("||", p.Value.representativUrls));
                            bool hasImdb = false, hasWiki = false, hasNetflix = false;
                            foreach (string u in p.Value.representativUrls)
                            {
                                if (MovieUtility.IsImdbFilmUrl(u) && !hasImdb)
                                {
                                    hasImdb = true;
                                    swImdb.WriteLine(b.Key + "\t" + string.Format("{0} url:{1}", p.Key, u.Replace("(", "%28").Replace(")", "%29")));
                                }
                                else if (MovieUtility.IsWikiUrl(u) && !hasWiki)
                                {
                                    hasWiki = true;
                                    swWiki.WriteLine(b.Key + "\t" + string.Format("{0} url:{1}", p.Key, u.Replace("(", "%28").Replace(")", "%29")));
                                }
                                else if (MovieUtility.IsNetflixUrl(u) && !hasNetflix)
                                {
                                    hasNetflix = true;
                                    swNetflix.WriteLine(b.Key + "\t" + string.Format("{0} url:{1}", p.Key, u.Replace("(", "%28").Replace(")", "%29")));
                                }
                            }
                        }
                    }
                }
            }

            swImdb.Flush(); swImdb.Close();
            swWiki.Flush(); swWiki.Close();
            swNetflix.Flush(); swNetflix.Close();

            Console.WriteLine("Triggered: {0}", triggered);
            Console.WriteLine("Only one movie: {0}", triggered - atLeast2Movies);
        }

        class MovieInfo
        {
            public string movieName;
            public List<string> representativUrls = new List<string>();
        }

        static List<Tuple<string, string, int>> GetFilmsInTopResults(
            Dictionary<string, int> results, Dictionary<string, string> mapping)
        {
            var films = new List<Tuple<string, string, int>>();
            foreach (var r in results)
            {
                string u = MyUtil.Normalizer.NormalizeUrl(r.Key);
                u = MovieUtility.CanonicalFilmUrl(u);

                // Url is a representative url.
                string name;
                if (!mapping.TryGetValue(u, out name))
                {
                    continue;
                }

                //if (!MovieUtility.IsImdbFilmUrl(u))
                //{
                //    continue;
                //}

                films.Add(new Tuple<string, string, int>(u, name, r.Value));
            }

            return films;
        }
        //class 
    }
}
