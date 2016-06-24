using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;
using System.Linq;

public class SimpleTextNormalizer
{
    public static string Denormalize(string cleanquery)
    {
        string[] words = cleanquery.Split(' ');
        string outstring = "";
        Regex r1 = new Regex("^nn[0-9]+$");
        Regex r2 = new Regex("^nn[0-9]+d[0-9]+$");
        foreach (string w in words)
        {
            outstring += " ";
            if (r1.IsMatch(w) || r2.IsMatch(w))
            {
                outstring += w.Replace("nn", "").Replace("d", ".");
            }
            else
            {
                outstring += w;
            }
        }
        return outstring.Trim();

    }

    public static string RemoveMarketWord(string pattern)
    {
        string[] marketwords = { "cheap", "cheapest", "best", "great", "greatest", "superior", "buy", "deal", "deals", "for", "dealers", "dealer", "a", "an", "in", "price", "prices", "discount", "sale", "find", "near", "www", "com", "close", "wwww", "con", "net", "org", "edu", "gov", "ratings", "rating", "reviews", "review", "", "of", "good" };
        List<string> patternwords = pattern.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        foreach (string w in marketwords)
        {
            if (patternwords.Contains(w))
                patternwords.Remove(w);
        }

        string newpattern = string.Join(" ", patternwords).Trim();
        string[] sentences = { "highest quality", "high quality", "for sale", "how to", "how do i", "how do you", "how do", "how does", "how much is", "how much does", "how much is", "how much are", "how can i" };
        foreach (string s in sentences)
        {
            if (newpattern == s.Trim())
                return "";
            if (newpattern.Contains(s + " "))
                return newpattern.Replace(s + " ", "").Trim();
            if (newpattern.Contains(" " + s))
                return newpattern.Replace(" " + s, "").Trim();
        }

        return newpattern;
    }
}

