using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace QU.Miscs
{
    class GetSymptoms
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.AtMostOnce, ShortName = "update")]
            public int ForceUpdate = 0;

            [Argument(ArgumentType.Required, ShortName = "dir")]
            public string OutputDir = "";
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            SymptomCrawler crawler = new SymptomCrawler(arguments.OutputDir);
            crawler.Start(arguments.ForceUpdate > 0);
        }
    }

    class SingleSymptomInfo
    {
        public string symptom;
        public string url;
        public string file;
        public List<Tuple<string, string>> otherPageUrlFiles;
    }

    class MultiSymptomInfo
    {
        public string combinedSymptoms;
        public string url;
        public string file;
        public List<DeseaseInfo> deseases = new List<DeseaseInfo>();

        public override int GetHashCode()
        {
            return this.combinedSymptoms.GetHashCode();
        }
    }

    class DeseaseInfo
    {
        public string desease;
        public string url;
        public string description;

        public override int GetHashCode()
        {
            return (desease+description).GetHashCode();
        }
    }

    class SymptomCrawler
    {
        string cacheDir = "";

        public SymptomCrawler(string cacheDir)
        {
            this.cacheDir = cacheDir;
            if (!Directory.Exists(cacheDir))
            {
                Directory.CreateDirectory(cacheDir);
            }

            this.singleSympFile = Path.Combine(cacheDir, "single.txt");
            this.multiSympFile = Path.Combine(cacheDir, "multi.txt");
        }

        const string SymptomCheckHost = @"http://symptomchecker.webmd.com/";
        const string SymptomCheckUrl = @"http://symptomchecker.webmd.com/symptoms-a-z";
        static Regex regexUrlSymptom
            = new Regex("<li.*<a onclick=\"return sl.*?href=\"(?<url>.+?)\">(?<symptom>.+?)</a></li>", RegexOptions.Compiled);
        static Regex regexMultiSymptomUrl
            = new Regex("<td class=\"col(1|2).*<a onclick=\"return sl.*?href=\"(?<url>.+?)\">(?<symptom>.+?)</a></td>", RegexOptions.Compiled);
        static Regex regexOtherPages
            = new Regex("<li><a onclick=.+?sc-pgnum_.+?href=\"(?<url>.+?)\"", RegexOptions.Compiled);
        static Regex regexCheckDesease
            = new Regex("<a onclick=.+?sc-symindex-cmb_.+?href=\"(?<url>.+?)\">(?<desease>.+?)</a><p>(?<description>.+?)</p>", RegexOptions.Compiled);
        List<SingleSymptomInfo> singleSymptoms = new List<SingleSymptomInfo>();
        HashSet<MultiSymptomInfo> multiSymptoms = new HashSet<MultiSymptomInfo>();
        HashSet<DeseaseInfo> deseases = new HashSet<DeseaseInfo>();
        string singleSympFile = "";
        string multiSympFile = "";

        public void Start(bool forceUpdate)
        {
            // Step 1: get all symptoms (from a to z)
            Console.WriteLine("[info] get all symptoms (from a to z)");
            string symptomCheckAtoZFile = GetCacheFileName(SymptomCheckUrl);
            if (forceUpdate || !CacheExists(SymptomCheckUrl))
            {
                Console.WriteLine("Download {0}", SymptomCheckUrl);
                PageDownloader.Download(SymptomCheckUrl, symptomCheckAtoZFile);
                Thread.Sleep(1000);
            }

            if (!CacheExists(SymptomCheckUrl))
            {
                Console.Error.WriteLine("Failed to get cache file!");
                return;
            }

            if (!ParseSymptomAtoZFile(symptomCheckAtoZFile))
            {
                Console.Error.WriteLine("No Symptoms Parsed!");
                return;
            }

            // Step 2: get single symptoms
            Console.WriteLine("[info] get single symptoms: {0}", this.singleSymptoms.Count);
            DumpSingle();
            foreach (var symp in this.singleSymptoms)
            {
                if (forceUpdate || !CacheExists(symp.url))
                {
                    Console.WriteLine("Download {0}", symp.url);
                    PageDownloader.Download(symp.url, symp.file);
                    Thread.Sleep(1000);
                }

                if (!CacheExists(symp.url))
                {
                    Console.Error.WriteLine("No single symptom file for {0}!", symp.symptom);
                    continue;
                }

                if (!ParseSingleSymptomPage1(symp))
                {
                    Console.Error.WriteLine("Parse single symptom Page1 error for {0}!", symp.symptom);
                    continue;
                }

                if (null == symp.otherPageUrlFiles || symp.otherPageUrlFiles.Count == 0)
                {
                    continue;
                }

                foreach (var tuple in symp.otherPageUrlFiles)
                {
                    if (forceUpdate || !CacheExists(tuple.Item1))
                    {
                        Console.WriteLine("Download {0}", tuple.Item1);
                        PageDownloader.Download(tuple.Item1, tuple.Item2);
                        Thread.Sleep(1000);
                    }

                    if (!CacheExists(tuple.Item1))
                    {
                        Console.Error.WriteLine("No single symptom other page file for {0}!", symp.symptom);
                        continue;
                    }

                    ParseSingleSymptomOtherPage(tuple.Item2);
                }
            }

            Console.WriteLine("[info] dump single symptoms");

            // Step 3: get multi-symptoms.
            Console.WriteLine("[info] get multi symptoms: {0}", this.multiSymptoms.Count);
            foreach (var multiSymptom in this.multiSymptoms)
            {
                if (forceUpdate || !CacheExists(multiSymptom.url))
                {
                    Console.WriteLine("Download {0}", multiSymptom.url);
                    PageDownloader.Download(multiSymptom.url, multiSymptom.file);
                    Thread.Sleep(1000);
                }

                if (!CacheExists(multiSymptom.url))
                {
                    Console.Error.WriteLine("No multi symptom page file for {0}!", multiSymptom.combinedSymptoms);
                    continue;
                }

                ParseMultiSymptomFile(multiSymptom);
            }

            Console.WriteLine("[info] dump multi symptoms");
            DumpMulti();
        }

        public string GetCacheFileName(string url)
        {
            return Path.Combine(this.cacheDir, url.GetHashCode32().ToString());
        }

        public bool CacheExists(string url)
        {
            string cacheFile = this.GetCacheFileName(url);
            return File.Exists(cacheFile);
        }

        /// <summary>
        /// Parse symptom a-to-z html.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool ParseSymptomAtoZFile(string file)
        {
            this.singleSymptoms.Clear();
            string html = File.ReadAllText(file);
            if (string.IsNullOrEmpty(html))
                return false;

            var matches = regexUrlSymptom.Matches(html);
            if (null == matches || matches.Count == 0)
            {
                return false;
            }

            foreach (Match m in matches)
            {
                Group gUrl = m.Groups["url"];
                Group gSymptom = m.Groups["symptom"];
                if (!gUrl.Success || !gSymptom.Success)
                {
                    continue;
                }

                string strUrl = SymptomCheckHost + gUrl.Value.Replace("&amp;", "&");
                this.singleSymptoms.Add(new SingleSymptomInfo
                    {
                        symptom = gSymptom.Value,
                        url = strUrl,
                        file = GetCacheFileName(strUrl)
                    }
                );
            }

            return singleSymptoms.Count != 0;
        }

        /// <summary>
        /// Parse single symptom html.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool ParseSingleSymptomPage1(SingleSymptomInfo symp)
        {
            string html = File.ReadAllText(symp.file);
            if (string.IsNullOrEmpty(html))
                return false;

            ParseSingleSymptomHtml(html);

            var matches = regexOtherPages.Matches(html);
            if (null == matches || matches.Count == 0)
            {
                return true;
            }

            HashSet<string> otherPages = new HashSet<string>();
            foreach (Match m in matches)
            {
                Group gUrl = m.Groups["url"];
                if (!gUrl.Success)
                {
                    continue;
                }

                otherPages.Add(SymptomCheckHost + gUrl.Value.Replace("&amp;", "&"));
            }

            symp.otherPageUrlFiles = new List<Tuple<string, string>>();
            foreach (string page in otherPages)
            {
                symp.otherPageUrlFiles.Add(new Tuple<string, string>(page, GetCacheFileName(page)));
            }

            return true;
        }

        /// <summary>
        /// Other pages w/o page parsing.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool ParseSingleSymptomOtherPage(string file)
        {
            string html = File.ReadAllText(file);
            if (string.IsNullOrEmpty(html))
                return false;

            return ParseSingleSymptomHtml(html);
        }

        /// <summary>
        /// Parse single symptom html.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool ParseSingleSymptomHtml(string html)
        {
            var matches = regexMultiSymptomUrl.Matches(html);
            if (null == matches || matches.Count == 0)
            {
                return false;
            }

            foreach (Match m in matches)
            {
                Group gUrl = m.Groups["url"];
                Group gSymptom = m.Groups["symptom"];
                if (!gUrl.Success || !gSymptom.Success)
                {
                    continue;
                }

                string strUrl = SymptomCheckHost + gUrl.Value.Replace("&amp;", "&");
                this.multiSymptoms.Add(new MultiSymptomInfo
                {
                    combinedSymptoms = gSymptom.Value,
                    url = strUrl,
                    file = GetCacheFileName(strUrl)
                }
                );
            }

            return true;
        }

        /// <summary>
        /// Parse multi-symptom check file.
        /// </summary>
        /// <param name="multiSymp"></param>
        /// <returns></returns>
        bool ParseMultiSymptomFile(MultiSymptomInfo multiSymp)
        {
            string html = File.ReadAllText(multiSymp.file);
            if (string.IsNullOrEmpty(html))
                return false;

            var matches = regexCheckDesease.Matches(html);
            if (null == matches || matches.Count == 0)
            {
                return false;
            }

            foreach (Match m in matches)
            {
                Group gUrl = m.Groups["url"];
                Group gDesease = m.Groups["desease"];
                Group gDescription = m.Groups["description"];
                if (!gUrl.Success || !gDesease.Success || !gDescription.Success)
                {
                    continue;
                }

                multiSymp.deseases.Add(new DeseaseInfo
                {
                    url = SymptomCheckHost + gUrl.Value.Replace("&amp;", "&"),
                    desease = gDesease.Value,
                    description = gDescription.Value
                }
                );
            }

            return true;
        }

        void DumpSingle()
        {
            using (StreamWriter sw = new StreamWriter(this.singleSympFile))
            {
                foreach (var single in this.singleSymptoms)
                {
                    sw.WriteLine("{0}\t{1}\t{2}",
                        single.symptom,
                        single.url, 
                        new FileInfo(single.file).Name);
                }
            }
        }

        void DumpMulti()
        {
            using (StreamWriter sw = new StreamWriter(this.multiSympFile))
            {
                foreach (var multi in this.multiSymptoms)
                {
                    foreach (var desease in multi.deseases)
                    {
                        sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
                            multi.combinedSymptoms,
                            multi.url,
                            desease.desease,
                            desease.url,
                            desease.description);
                    }
                }
            }
        }
    }
}
