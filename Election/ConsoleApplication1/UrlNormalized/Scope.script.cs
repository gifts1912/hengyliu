using Microsoft.SCOPE.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
/*
private static Indexgen.UrlNormalizer normalizer = new Indexgen.UrlNormalizer();
private static bool initNormalizer = false;

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

    string normalizedUrl = string.Empty; normalizer.NormalizeUrl(url, out normalizedUrl);

    return normalizedUrl;
}
*/
