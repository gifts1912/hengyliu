using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class Misc
{
    public static string GetUrlDomain(string url)
    {
        return RetroIndex.UrlNormalizer.GetDomain(url);
    }

    public static bool IsDesiredDomain(string url, string domainList)
    {
        HashSet<string> domains = new HashSet<string>(domainList.Split(';'));
        return domains.Contains(GetUrlDomain(url));
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
