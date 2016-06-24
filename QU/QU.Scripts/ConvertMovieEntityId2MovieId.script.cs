using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

class ImdbUtil
{
    public static long GetImdbId(string url)
    {
        long id = -1;
        string part = string.Empty;
        if (url.StartsWith("http://imdb.com/title/"))
        {
            string temp = url.Substring("http://imdb.com/title/tt".Length);
            int slash = temp.IndexOf('/');
            if (slash < 0)
                part = temp;
            else
                part = temp.Substring(0, slash);
        }

        if (!long.TryParse(part, out id))
            return -1;

        return id;
    }

    public static string CanonicalFilmUrl(string url)
    {
        if (url.StartsWith("http://imdb.com/title/"))
        {
            string temp = url.Substring("http://imdb.com/title/".Length);
            int slash = temp.IndexOf('/');
            if (slash < 0)
                return url;
            else
                return url.Substring(0, "http://imdb.com/title/".Length + slash);
        }

        return url;
    }
}

