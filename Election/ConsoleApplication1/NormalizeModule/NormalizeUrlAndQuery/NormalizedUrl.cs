using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//using UrlNormalizer.Managed;
/*
namespace NormalizeModule.NormalizeUrlAndQuery
{
    public class NormalizedUrl
    {
        private static Indexgen.UrlNormalizer normalizer = new Indexgen.UrlNormalizer();
        private static bool initNormalizer = false;
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\urlTest.tsv";
                args[1] = @"D:\demo\urlNormalized.tsv";
            }

            string line, url;

            using (StreamReader sr = new StreamReader(InfileUrl))
            {
                using (StreamWriter sw = new StreamWriter(OutfileUrl))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        url = ExtractUrl(line, 1);
                        if(string.IsNullOrEmpty(url))
                        {
                            continue;
                        }
                        string urlNormalized = Normalize(url);
                        sw.Write(urlNormalized);
                    }
                }
            }
        }

        private static string ExtractUrl(string line, int col)
        {
            string[] arr = line.Split('\t');
            if (arr.Length <= col)
                return null;
            return arr[col];
        }

        static string Normalize(string url)
        {
            if (!initNormalizer)
            {
                lock (normalizer)
                {
                    if (!initNormalizer)
                    {
                        normalizer.Initialize("tld.txt", "UrlNormalizerConfig.NotBlockExtension.ini", "UrlNormalizeRules.txt");
                    }
                    initNormalizer = true;
                }
            }

            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            string normalizedUrl = string.Empty;
            normalizer.NormalizeUrl(url,
                                    out normalizedUrl);

            return normalizedUrl;
        }

    }
}
*/
