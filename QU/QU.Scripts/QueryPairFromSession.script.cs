using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class BingClicksStep1SessionDataExtractor : Extractor
{
    public override Schema Produces(string[] columns, string[] args)
    {
        return new Schema(columns);
    }

    public override IEnumerable<Row> Extract(StreamReader reader, Row output, string[] args)
    {
        string line;
        string[] strIndexes = args[0].Split(',');
        int[] indexes = new int[strIndexes.Length];
        for (int i = 0; i < strIndexes.Length; i++)
        {
            indexes[i] = int.Parse(strIndexes[i]);
        }

        while ((line = reader.ReadLine()) != null)
        {
            string[] tokens = line.Split('\t');
            for (int i = 0; i < indexes.Length; ++i)
            {
                output[i].Set(tokens[indexes[i]]);
            }
            yield return output;
        }
    }
}


/// <summary>
///  
/// </summary>
public class SessionSimilarQueryReducer : Reducer
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
        return new Schema("leftQuery:string, rightQuery:string, similarity:double");
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
        int count = 0;
        int SessionSize = 1;
        int minSharedPrefix = int.Parse(args[0]);
        int minSharedTerms = int.Parse(args[1]);
        foreach (Row row in input.Rows)
        {
            if (++count == 1)
            {
                SessionSize = row["SessionSize"].Integer;
                if (SessionSize == 1)
                    yield break;
            }
            else
            {
                string currQuery = row["Query"].String;
                string prevQuery = row["PrevQuery"].String;
                prevQuery = FrontEndUtil.CQueryParser.GetFcsNormalizedQuery(prevQuery.ToLower());
                currQuery = FrontEndUtil.CQueryParser.GetFcsNormalizedQuery(currQuery.ToLower());
                int currQueryClick = row["currentQueryClicks"].Integer;
                int prevQueryClick = row["PrevQueryClicks"].Integer;
                double similarity = ComputeSimilarity(prevQuery, currQuery, minSharedPrefix, minSharedTerms);
                if (similarity < 0)
                    continue;
                output[0].Set(prevQuery);
                output[1].Set(currQuery);
                output[2].Set(similarity);
                yield return output;
            }
        }
    }

    /// <summary>
    /// Compute similarity between two queries
    /// </summary>
    /// <param name="leftQuery">Left</param>
    /// <param name="rightQuery">Right</param>
    /// <param name="threshSharedPrefix">sharedPrefix larger than threshold</param>
    /// <returns>-1 if no similarity, Pearson correlation otherwise</returns>
    public static double ComputeSimilarity(string left, string right, int minSharedPrefix, int minSharedTerms)
    {
        if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
            return -1;

        minSharedPrefix = Math.Min(Math.Min(left.Length, right.Length), minSharedPrefix);

        //judge if left and right share the same prefix
        bool sharePrefix = false;
        if (minSharedPrefix <= 0)
            sharePrefix = true;
        else
            sharePrefix = left.Substring(0, minSharedPrefix).Equals(right.Substring(0, minSharedPrefix));

        //count how many overlapped words in two queries
        HashSet<string> termsInLeft = new HashSet<string>(left.Split(' '));
        HashSet<string> allTerms = new HashSet<string>(left.Split(' '));
        string[] itemsInRight = right.Split(' ');
        int countOverlap = 0;
        foreach (string item in itemsInRight)
        {
            if (termsInLeft.Contains(item))
                countOverlap++;
            else
                allTerms.Add(item);
        }

        if (!sharePrefix && (countOverlap < minSharedTerms)) //no prefix and no overlap -> no similarity
            return -1;
        else //define similarity as the Pearson correlation
            return ((double)countOverlap / allTerms.Count);
    }
}
