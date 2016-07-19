﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;
using System.Linq;
using Utility;

class MovieUtility
{
    private static Regex MagicMovieAnchorRegex = new Regex(
            "(in what|in which|what is the name of the|which is the name of|what is the|which is the)\\s+(movie|movies|movi|imovie|moview|movis|movei|themovie|moviw|moveis|moviee|mmovie|moviie|movvie|film|films|filmovi|filme|filmy)\\s+(quote|quotes|plot|plot|name)?\\s*(where|in which|that|about|with|when)?|(in what|in which|what is the name of the|which is the name of|what is the|which is the)?\\s*(movie|movies|movi|imovie|moview|movis|movei|themovie|moviw|moveis|moviee|mmovie|moviie|movvie|film|films|filmovi|filme|filmy)\\s+(quote|quotes|plot|plot|name)?\\s*(where|in which|that|about|with|when)\\s+",
            RegexOptions.Compiled);

    public static bool IsMagicMovieQuestion(string question)
    {
        return MagicMovieAnchorRegex.IsMatch(question);
    }

    private static IEnumerable<string> Split(string query, int ngram)
    {
        string[] items = query.Split(SpaceSeparator);
        for (int i = 0; i < items.Length; i++)
        {
            for (int j = 1; j <= Math.Min(ngram, items.Length - i); j++)
            {
                yield return string.Join(" ", items.Skip(i).Take(j));
            }
        }
    }

    static char[] SpaceSeparator = new char[] { ' ' };
    static string[] LineSeparators = new string[] { "#R#", "#N#" };
    static string[] SentenceSeparators = new string[] { ",", "." };
    static string[] QuoteSeparators = new string[] { "|||" };
    static Regex QuotedPhrase = new Regex("\"(?<Quoted>.+)?\"", RegexOptions.Compiled);

    static string RemoveQuoted(string str, out List<string> quoted)
    {
        quoted = new List<string>();
        var mc = QuotedPhrase.Matches(str);
        if (mc == null || mc.Count == 0)
            return str;

        foreach (Match m in mc)
        {
            string q = m.Groups["Quoted"].Value;
            if (!string.IsNullOrEmpty(q))
                quoted.Add(q);
        }

        return QuotedPhrase.Replace(str, QuoteSeparators[0]);
    }

    public static List<string> GenerateFactCandidate(string answer, int ng)
    {
        List<string> candidates = new List<string>();
        if (string.IsNullOrEmpty(answer))
            return candidates;

        string[] lines = answer.Split(LineSeparators, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            // the first 8 sentences.
            string[] sentences = line.Split(SentenceSeparators, StringSplitOptions.RemoveEmptyEntries).Take(8).ToArray();
            foreach (string sentence in sentences)
            {
                List<string> quoted;
                string noQuote = RemoveQuoted(sentence, out quoted);
                if (quoted.Count != 0)
                {
                    foreach (var q in quoted)
                        candidates.Add(Utility.Normalizer.NormalizeQuery(q));
                }

                string[] items = noQuote.Split(QuoteSeparators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in items)
                {
                    string normItem = Utility.Normalizer.NormalizeQuery(item);
                    if (string.IsNullOrEmpty(normItem))
                        continue;

                    var ngrams = Split(normItem, ng);
                    foreach (var ngram in ngrams)
                    {
                        candidates.Add(ngram);
                    }
                }
            }
        }

        return candidates;
    }

}

/// <summary>
///  
/// </summary>
public class LongestCandReducer : Reducer
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return input.Clone();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override IEnumerable<Row> Reduce(RowSet input, Row output, string[] args)
    {
        List<string> candidates = new List<string>();
        foreach (Row row in input.Rows)
        {
            bool hasLongerCand = false;
            string cand = row[1].String;
            foreach (var c in candidates)
            {
                if (c.Contains(cand))
                {
                    hasLongerCand = true;
                    break;
                }
            }

            if (!hasLongerCand)
            {
                candidates.Add(cand);
                row.CopyTo(output);
                yield return output;
            }
        }
    }
}




/// <summary>
/// 
/// </summary>
public class CanGenProcessor : Processor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return new Schema("url, question, candidate, score:double");
    }

    static string[] Separators = new string[] { "#TTT#" };
    static HashSet<string> stopwords = StopWordUtil.LoadFromFile("stopword.txt");

    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        int ngram = int.Parse(args[0]);
        foreach (Row row in input.Rows)
        {
            output[0].Set(row[0].String);
            output[1].Set(row[1].String);

            string[] answers = row[2].String.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            double[] ratings = row[3].String.Split(Separators, StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToDouble(i) + 1).ToArray();

            for (int i = 0; i < answers.Length; i++)
            {
                string ans = answers[i];
                double rate = i >= ratings.Length ? 0 : ratings[i];

                var candidates = MovieUtility.GenerateFactCandidate(ans, ngram);
                foreach (var cand in candidates)
                {
                    // filter single stopword
                    if (stopwords.Contains(cand))
                        continue;

                    output[2].Set(cand);
                    output[3].Set(rate);
                    yield return output;
                }
            }
        }
    }
}


/// <summary>
/// 
/// </summary>
public class SplitNameProcessor : Processor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        Schema output = input.Clone();
        output.Add(new ColumnInfo("part", ColumnDataType.String));
        return output;
    }

    static HashSet<string> stopwords = StopWordUtil.LoadFromFile("stopword.txt");

    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        int ngram = int.Parse(args[0]);
        int partCol = input.Schema.Count;
        foreach (Row row in input.Rows)
        {
            row.CopyTo(output);
            string name = row[1].String;
            if (name.Split(' ').Length <= ngram)
            {
                output[partCol].Set(name);
                yield return output;
                continue;
            }

            var parts = Split(name, ngram);
            foreach (var p in parts)
            {
                if (stopwords.Contains(p))
                    continue;

                output[partCol].Set(p);
                yield return output;
            }
        }


    }

    private static IEnumerable<string> Split(string query, int ngram)
    {
        string[] items = query.Split(' ');
        for (int i = 0; i < items.Length - ngram; i++)
        {
            yield return string.Join(" ", items.Skip(i).Take(ngram));
        }
    }
}


