using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;

public class Utility
{
    public static string SimpleNormalizeQuery(string str_in)
    {
        string nq1 = Regex.Replace(str_in, "[\"'\\.,\\?{}\\[\\]]", " ");
        nq1 = nq1.Replace(";", " ");
        nq1 = nq1.ToLower();
        nq1 = Regex.Replace(nq1, "[ ]+", " ");
        nq1 = nq1.Trim();
        return nq1;
    }
}
