using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUtil = Utility;

namespace QU.Miscs.MagicQ
{
    public class MergeMovieScrapes
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "b")]
            public string BingScrapeFile;

            //[Argument(ArgumentType.Required, ShortName = "m")]
            //public string MappingFile;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;

            [Argument(ArgumentType.Required, ShortName = "imdb")]
            public string ImdbScrape;

            [Argument(ArgumentType.Required, ShortName = "wiki")]
            public string WikiScrape;

            [Argument(ArgumentType.Required, ShortName = "netflix")]
            public string NetflixScrape;

            [Argument(ArgumentType.AtMostOnce, ShortName = "addurl")]
            public int AddUrl = 1;
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

            bool addUrl = arguments.AddUrl > 0 ? true : false;

            var bScrape = new Dictionary<string, Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>>();
            MyUtil.ScrapeUtility.ReadScrapeWithContents(arguments.BingScrapeFile, 10, out bScrape);

            // imdb, wiki, netflix
            var imdbScrape = new Dictionary<string, Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>>();
            MyUtil.ScrapeUtility.ReadScrapeWithContents(arguments.ImdbScrape, 1, out imdbScrape);
            var wikiScrape = new Dictionary<string, Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>>();
            MyUtil.ScrapeUtility.ReadScrapeWithContents(arguments.WikiScrape, 1, out wikiScrape);
            var netflixScrape = new Dictionary<string, Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>>();
            MyUtil.ScrapeUtility.ReadScrapeWithContents(arguments.NetflixScrape, 1, out netflixScrape);

            var newScrape = new Dictionary<string, Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>>();

            foreach (var b in bScrape)
            {
                string q = b.Key;
                Dictionary<string, int> candidates;
                Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent> candidatesInfo;
                if (!GetCandidates(q, imdbScrape, wikiScrape, netflixScrape, out candidates, out candidatesInfo))
                {
                    newScrape.Add(q, b.Value);
                }

                string[] urls = (from u in b.Value orderby u.Value.pos ascending select u.Key).ToArray();
                HashSet<string> serpUrls = new HashSet<string>();
                Dictionary<string, int> url2Score = new Dictionary<string, int>(urls.Length);
                for (int i = 0; i < urls.Length; i++)
                {
                    string nu = MyUtil.Normalizer.NormalizeUrl(urls[i]);
                    serpUrls.Add(nu);
                    nu = MovieUtility.CanonicalFilmUrl(nu);
                    if (!candidates.ContainsKey(nu))
                    {
                        url2Score[urls[i]] = 1000 - i;
                    }
                    else
                    {
                        url2Score[urls[i]] = 2000 - i;
                    }
                }

                if (addUrl)
                {
                    foreach (var c in candidates)
                    {
                        if (!serpUrls.Contains(c.Key))
                            url2Score[c.Key] = c.Value;
                    }
                }

                var sorted = (from us in url2Score orderby us.Value descending select us.Key).Take(10).ToArray();
                Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent> newScrapeContent =
                    new Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>();
                for (int i = 0; i < sorted.Length; i++)
                {
                    if (b.Value.ContainsKey(sorted[i]))
                    {
                        MyUtil.ScrapeUtility.ScrapeContent c = b.Value[sorted[i]];
                        c.pos = i + 1;
                        newScrapeContent[sorted[i]] = c;
                    }
                    else if (candidatesInfo.ContainsKey(sorted[i]))
                    {
                        MyUtil.ScrapeUtility.ScrapeContent c = candidatesInfo[sorted[i]];
                        c.pos = i + 1;
                        newScrapeContent[sorted[i]] = c;
                    }
                }

                newScrape[q] = newScrapeContent;
            }

            MyUtil.ScrapeUtility.WriteScrape(newScrape, arguments.Output, arguments.BingScrapeFile);
        }

        static bool GetCandidates(string q,
            Dictionary<string, Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>> imdb,
            Dictionary<string, Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>> wiki,
            Dictionary<string, Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>> netflix, 
            out Dictionary<string, int> candidates,
            out Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent> candidatesInfo)
        {
            candidates = new Dictionary<string, int>(3);
            candidatesInfo = new Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent>();
            Dictionary<string, MyUtil.ScrapeUtility.ScrapeContent> temp;
            try
            {
                if (imdb.TryGetValue(q, out temp))
                {
                    candidates.Add(MyUtil.Normalizer.NormalizeUrl(temp.Keys.First()), 1003);
                    candidatesInfo.Add(MyUtil.Normalizer.NormalizeUrl(temp.Keys.First()), temp.Values.First());
                }

                if (wiki.TryGetValue(q, out temp))
                {
                    candidates.Add(MyUtil.Normalizer.NormalizeUrl(temp.Keys.First()), 1002);
                    candidatesInfo.Add(MyUtil.Normalizer.NormalizeUrl(temp.Keys.First()), temp.Values.First());
                }

                if (netflix.TryGetValue(q, out temp))
                {
                    candidates.Add(MyUtil.Normalizer.NormalizeUrl(temp.Keys.First()), 1001);
                    candidatesInfo.Add(MyUtil.Normalizer.NormalizeUrl(temp.Keys.First()), temp.Values.First());
                }
            }
            catch { }

            return candidates.Count > 0;
        }
    }
}
