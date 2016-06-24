using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyMisc = Utility;

namespace QU.Miscs.MagicQ
{
    public class MusicUtility
    {
        private static Regex regexStopPhrases = new Regex(
            "song talk[s|ing]? about|who sings?", RegexOptions.Compiled
            );

        public static string Normalize(string query)
        {
            string nq1 = Regex.Replace(query, "[\"\\+\\-_/\\.,\\?{}\\[\\]]", " ");
            nq1 = nq1.Replace(";", " ");
            nq1 = nq1.ToLower();
            nq1 = Regex.Replace(nq1, "[ ]+", " ");
            nq1 = nq1.Trim();
            return nq1;
        }

        public static int QueryLength(string query)
        {
            return query.Split(' ').Length;
        }

        public static string Process(string query)
        {
            return regexStopPhrases.Replace(query, "");
        }

    }
}
