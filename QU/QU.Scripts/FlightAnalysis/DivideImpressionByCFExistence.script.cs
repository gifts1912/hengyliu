using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using MS.Internal.Bing.DataMining.SearchLogApi;

public class Util
{
    public static string EncodeWebResults(IEnumerable<WebResult> source)
    {
        int i = 0;
        StringBuilder sb = new StringBuilder();
        foreach (var item in source)
        {
            sb.Append(Utility.Normalizer.NormalizeUrl(item.TitleUrl));
            sb.Append("#P#");
            sb.Append(i);
            sb.Append("#U#");
        }
        return sb.ToString();
    }

    public static bool HasCFSiteInTop(string results, int topn, string cfsites)
    {
        HashSet<string> sites = new HashSet<string>(cfsites.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
        string[] items = results.Split(new string[] { "#U#" }, StringSplitOptions.RemoveEmptyEntries);
        if (items == null || items.Length == 0)
            return false;

        foreach (var item in items)
        {
            var pair = item.Split(new string[] { "#P#" }, StringSplitOptions.RemoveEmptyEntries);
            if (pair == null || pair.Length != 2)
                continue;
            int pos;
            if (int.TryParse(pair[1], out pos))
            {
                if (pos > topn)
                    continue;
                string domain = Utility.Normalizer.GetUrlDomain(pair[0]);
                if (sites.Contains(domain))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
