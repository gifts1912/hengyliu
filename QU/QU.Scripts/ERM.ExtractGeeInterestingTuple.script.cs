using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;
using MS.Internal.Bing.DataMining.SearchLogApi;

public class Utility
{
    static Regex regex = new Regex("(?<IT>InterestingTuple:5:.+?)(]|\\s)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static string AlterationToString(DataSourceElement cal)
    {
        if (null == cal)
        {
            return string.Empty;
        }

        DataSourceList alterations = cal.FindElementsWithProperty("NODE_NAME", "AlterationQueryList");
        if (null == alterations || alterations.Count == 0)
        {
            return string.Empty;
        }

        StringBuilder sb = new StringBuilder();
        //foreach (var alt in alterations)
        var alt = alterations[0];
        try
        {
            {
                string rawQ = alt.GetDataPropertyOrDefault("RawQuery", "");
                string augmentation = alt.GetDataPropertyOrDefault("CustomAugmentation", "");
                var matches = regex.Matches(augmentation);
                if (matches == null || matches.Count == 0)
                    return string.Empty;

                sb.Append(rawQ);
                sb.Append("|||");

                foreach (var m in matches)
                {
                    var ma = m as Match;
                    if (ma.Success)
                    {
                        sb.Append(ma.Groups["IT"].Value);
                        sb.Append("###");
                    }
                }
            }
        }
        catch
        {

        }

        return sb.ToString();
    }
}

public class FilterHelper
{
    public static string GetScope(string url)
    {
        Match match = Regex.Match(url, "&scope=([^&]*)", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        return "";
    }

    public static bool IsQueryBad(string query, uint maxBytes)
    {
        //maxBytes = 246
        if (String.IsNullOrEmpty(query) == true)
        { // Empty Phrase
            return true;
        }
        if (query.EndsWith(";") == true)
        { // Trailing Semicolon
            return true;
        }
        if (query.Contains(":"))
        { // internal queries
            return true;
        }
        if (query.IndexOf("word:(") >= 0)
        { // word: operator
            return true;
        }
        string[] terms = query.Split(' '); // Long Terms
        uint length;
        foreach (string term in terms)
        {
            length = (uint)(System.Text.Encoding.UTF8.GetByteCount(term));
            if (length > maxBytes)
                return true;
        }
        if (query.Contains("\0"))
        { // null bytes : 22 Aug 2010
            return true;
        }

        return false;
    }
}

