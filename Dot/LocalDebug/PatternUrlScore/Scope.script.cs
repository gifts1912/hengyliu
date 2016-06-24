using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScopeRuntime;
using FrontEndUtil;

class HelperFunction
{
    static public string GetUrlHost(string url)
    {
        return CURLUtilities.GetHostNameFromUrl(url);
    }
}

public class UrlPatternReducer : Reducer
{
    List<Tuple<string, double>> patternlist = new List<Tuple<string, double>>();
    string oldqp = "";
    string oldhost = "";
    double oldscore = 0;
    double oldsum = 0;

    string MergePattern(string s1, string s2)
    {
        string ss = s1.Replace("[^/]+", "$$$");
        string[] sp1 = s1.Trim('/').Replace("[^/]+", "$$$").Split('/');
        string[] sp2 = s2.Trim('/').Replace("[^/]+", "$$$").Split('/');

        if (sp1.Length != sp2.Length)
            return "";

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < sp1.Length; i++)
        {
            sb.Append("/");
            if (sp1[i] == sp2[i])
                sb.Append(sp1[i].Replace("$$$", "[^/]+"));
            else if (sp1[i] != "$$$" && sp2[i] == "$$$")
                sb.Append(sp1[i].Replace("$$$", "[^/]+"));
            else if (sp1[i] == "$$$" && sp2[i] != "$$$")
                sb.Append(sp2[i].Replace("$$$", "[^/]+"));
            else
                return ""; //inconsistent patterns
        }

        string s = sb.ToString();
        if (s.IndexOf("[^/]+") == -1)
            return "";

        return s;
    }

    void ReduceUrlPattern()
    {
        string p = "";
        foreach (var item in patternlist)
        {
            if (p == "")
                p = item.Item1;
            else
            {
                p = MergePattern(p, item.Item1);
                if (p == "")
                    break;
            }
        }

        if (p != "")
        {
            Tuple<string, double> t = new Tuple<string, double>(p, patternlist.Max(n => n.Item2));
            patternlist.Clear();
            patternlist.Add(t);
        }
    }

    public override Schema Produces(string[] requestedColumns, string[] args, Schema input)
    {
        return input.Clone();
    }

    public override IEnumerable<Row> Reduce(RowSet input, Row outputRow, string[] args)
    {
        oldhost = "";
        oldqp = "";
        oldscore = 0;
        oldsum = 0;

        foreach (Row row in input.Rows)
        {
            string qp = row["qpattern"].String;
            string up = row["upattern"].String;
            double score = double.Parse(row["clickProb"].String);
            double sum = double.Parse(row["ClickSum"].String);

            int idx = up.IndexOf('/');
            string host = up;
            string path = "";
            if (idx != -1)
            {
                host = up.Substring(0, up.IndexOf('/'));
                path = up.Substring(up.IndexOf('/'));
            }

            if (oldqp != qp || oldhost != host || oldscore != score)
            {
                if (oldhost != "")
                {
                    ReduceUrlPattern();
                    foreach (var i in patternlist)
                    {
                        outputRow["upattern"].Set(oldhost + i.Item1);
                        outputRow["qpattern"].Set(oldqp);
                        outputRow["clickProb"].Set(oldscore);
                        outputRow["ClickSum"].Set(oldsum);
                        yield return outputRow;
                    }
                    patternlist.Clear();
                }

                oldqp = qp;
                oldhost = host;
                oldscore = score;
                oldsum = sum;
            }

            patternlist.Add(new Tuple<string, double>(path, score));
        }
        ReduceUrlPattern();
        foreach (var i in patternlist)
        {
            outputRow["upattern"].Set(oldhost + i.Item1);
            outputRow["qpattern"].Set(oldqp);
            outputRow["clickProb"].Set(oldscore);
            outputRow["ClickSum"].Set(oldsum);
            yield return outputRow;
        }
        patternlist.Clear();
    }
}

public class TopNReducer : Reducer
{
    public override Schema Produces(string[] requestedColumns, string[] args, Schema input)
    {
        return input.Clone();
    }

    public override IEnumerable<Row> Reduce(RowSet input, Row outputRow, string[] args)
    {
        int topn = int.Parse(args[0]);
        int num = 0;

        foreach (Row row in input.Rows)
        {
            for (int col = 0; col < input.Schema.Count; col++)
            {
                row[col].CopyTo(outputRow[col]);
            }

            yield return outputRow;

            num++;
            if (num == topn)
            {
                break;
            }
        }
    }
}

public class UrlPatternProcessor : Processor
{
    int m_UrlSegKeepParts;
    bool m_DropSubDomain;
    HashSet<string> m_countryCode;

    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return new Schema(columns);
    }

    static private bool isUrl(string str)
    {
        if (str.StartsWith("http", StringComparison.OrdinalIgnoreCase) ||
            str.StartsWith("https", StringComparison.OrdinalIgnoreCase) ||
            str.StartsWith("www", StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    void GetPathPatterns(List<string> pathpattern, List<string> tmp, string[] pathitems, int keepcount, int allcount)
    {
        if (allcount == pathitems.Length)
        {
            if (keepcount == pathitems.Length || keepcount > m_UrlSegKeepParts)
                return;

            if ((keepcount == m_UrlSegKeepParts) || 
                (pathitems.Length - keepcount <= m_UrlSegKeepParts) ||
                (pathitems.Length == 0))
                pathpattern.Add(string.Join("/", tmp));
            return;
        }

        if (keepcount < m_UrlSegKeepParts)
        {
            tmp.Add(pathitems[allcount]);
            GetPathPatterns(pathpattern, tmp, pathitems, keepcount + 1, allcount + 1);
            tmp.RemoveAt(tmp.Count - 1);
        }

        tmp.Add("[^/]+");
        GetPathPatterns(pathpattern, tmp, pathitems, keepcount, allcount + 1);
        tmp.RemoveAt(tmp.Count - 1);

    }

    List<string> GetPathPatterns(string path)
    {
        List<string> pathpattern = new List<string>();
        List<string> tmp = new List<string>();

        string[] segments = path.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        GetPathPatterns(pathpattern, tmp, segments, 0, 0);

        return pathpattern;
    }

    List<string> GetUrlPatterns(string url)
    {
        List<string> ups = new List<string>();

        if (isUrl(url))
        {
            try
            {
                string normurl = CURLUtilities.GetHutNormalizeUrl(url.Trim()) ?? "";
                System.Uri uri = new System.Uri(normurl);

                string host = uri.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
                if (m_DropSubDomain)
                {
                    int idxDot = host.LastIndexOf('.');

                    //handle ending country names
                    string lastDomain = host.Substring(idxDot);
                    if (m_countryCode.Contains(lastDomain))
                    {
                        idxDot = host.LastIndexOf('.', idxDot - 1);
                    }

                    //get domain name
                    int preIdxDot = -1;
                    if (idxDot != -1)
                        preIdxDot = host.LastIndexOf('.', idxDot - 1);

                    if (idxDot != preIdxDot && preIdxDot != -1)
                        host = host.Substring(preIdxDot + 1);
                }

                string path = uri.GetComponents(UriComponents.Path, UriFormat.UriEscaped);
                if (path == "")
                    ups.Add(host);
                else
                {
                    foreach (string p in GetPathPatterns(path))
                    {
                        ups.Add(host + "/" + p);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(url);
                return null;
            }
        }
        return ups;
    }

    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        m_UrlSegKeepParts = int.Parse(args[0]); //2
        m_DropSubDomain = (args[1] == "true"); //false
        m_countryCode = new HashSet<string>();

        //building country code hash
        using (StreamReader inputCountryCodeStream = new StreamReader("url_country_code.txt"))
        {
            string line = inputCountryCodeStream.ReadLine();
            string[] items = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
                m_countryCode.Add(item);
        }

        foreach (Row row in input.Rows)
        {
            string url = row["u"].String;
            List<string> urlpatterns = GetUrlPatterns(url);

            if (urlpatterns != null)
            {
                foreach (string up in urlpatterns)
                {
                    output["u"].Set(url);
                    output["upattern"].Set(up);
                    yield return output;
                }
            }
        }
    }
}

// Generated by ScopeStudio, version 1.8.0000.6!

