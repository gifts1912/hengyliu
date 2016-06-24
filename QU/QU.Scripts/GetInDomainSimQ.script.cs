using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;

public class Normalizer
{
    public static string NormalizeQuery(string str_in)
    {
        string fcsNormalized = FrontEndUtil.CQueryParser.GetFcsNormalizedQueryUTF8(str_in);

        string nq1 = Regex.Replace(fcsNormalized, "[\"]", " ");
        nq1 = nq1.Replace(";", " ");
        nq1 = nq1.ToLower();
        nq1 = Regex.Replace(nq1, "[ ]+", " ");
        nq1 = nq1.Trim();
        return nq1;
    }
}

