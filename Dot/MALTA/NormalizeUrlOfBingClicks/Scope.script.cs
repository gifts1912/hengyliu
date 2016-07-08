using Microsoft.SCOPE.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using FrontEndUtil;

public class UrlProcess
{
    public static string GetNormalizedUrl(string url)
    {
        return FrontEndUtil.CURLUtilities.GetHutNormalizeUrl(url);
    }
    public static string GetUrlHash(string url)
    {
        return FrontEndUtil.CURLUtilities.GetCBUrlHash(url);
    }

}