using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;

public class Misc
{
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}


/// <summary>
/// 
/// </summary>
public class MultiSymptomProcessor : Processor
{
    class DeseaseInfo
    {
        public string desease;
        public string url;
        public string description;

        public override int GetHashCode()
        {
            return (desease + description).GetHashCode();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return new Schema("symptoms, symptomIds, locations, symptomsUrl, desease, deseaseDescription, deseaseUrl");
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        foreach (Row row in input.Rows)
        {
            string url = row["url"].String;
            string html = row["html"].String;
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(html))
            {
                continue;
            }

            string arguments = url.Substring("http://symptomchecker.webmd.com/multiple-symptoms?".Length);
            string symptoms = "", symptomids = "", locations = "";
            string[] items = arguments.Split('&');
            foreach (var item in items)
            {
                if (item.StartsWith("symptoms="))
                {
                    symptoms = item.Substring("symptoms=".Length);
                }
                else if (item.StartsWith("symptomids="))
                {
                    symptomids = item.Substring("symptomids=".Length);
                }
                else if (item.StartsWith("locations="))
                {
                    locations = item.Substring("locations=".Length);
                }
            }

            if (string.IsNullOrEmpty(symptoms))
                continue;

            var deseases = ParseHtml(html);
            if (null == deseases || deseases.Count == 0)
            {
                continue;
            }

            //symptoms, symptomIds, locations, symptomsUrl, desease, deseaseDescription, deseaseUrl
            output["symptoms"].UnsafeSet(symptoms);
            output["symptomIds"].UnsafeSet(symptomids);
            output["locations"].UnsafeSet(locations);
            output["symptomsUrl"].UnsafeSet(url);

            foreach (var d in deseases)
            {
                output["desease"].UnsafeSet(d.desease);
                output["deseaseDescription"].UnsafeSet(d.description);
                output["deseaseUrl"].UnsafeSet(d.url);
                yield return output;
            }
        }
    }

    static Regex regexCheckDesease
            = new Regex("<a onclick=.+?sc-symindex-cmb_.+?href=\"(?<url>.+?)\">(?<desease>.+?)</a><p>(?<description>.+?)</p>", RegexOptions.Compiled);
    const string SymptomCheckHost = @"http://symptomchecker.webmd.com/";

    List<DeseaseInfo> ParseHtml(string html)
    {
        List<DeseaseInfo> deseases = new List<DeseaseInfo>();

        var matches = regexCheckDesease.Matches(html);
        if (null == matches || matches.Count == 0)
        {
            return deseases;
        }

        foreach (Match m in matches)
        {
            Group gUrl = m.Groups["url"];
            Group gDesease = m.Groups["desease"];
            Group gDescription = m.Groups["description"];
            if (!gUrl.Success || !gDesease.Success || !gDescription.Success)
            {
                continue;
            }

            deseases.Add(new DeseaseInfo
            {
                url = SymptomCheckHost + gUrl.Value.Replace("&amp;", "&"),
                desease = gDesease.Value,
                description = gDescription.Value
            }
            );
        }

        return deseases;
    }
}


