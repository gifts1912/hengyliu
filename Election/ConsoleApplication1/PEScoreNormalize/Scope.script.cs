using Microsoft.SCOPE.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class ScoreNormalReducer : Reducer
{
    private double scoreThread;
    
    public ScoreNormalReducer(double st = 0.5)
    {
        scoreThread = st;
    }
    public override Schema Produces(string[] requestedColumns, string[] args, Schema input)
    {
        //return input.Clone();
        return new Schema("peId:string, url:string, score:int");
    }

    public override IEnumerable<Row> Reduce(RowSet input, Row outputRow, string[] args)
    {
        Dictionary<string, double> urlScore = new Dictionary<string, double>();
        Dictionary<string, int> urlScoreNorm = new Dictionary<string, int>();
        bool first = true;
        string peId = "";
        foreach (Row row in input.Rows)
        {
            if (first)
            {
                peId = row["peId"].String;
                first = false;
            }
            string url = row["url"].String;
            double score = row["score"].Double;
            if (score < scoreThread)
                continue;
            urlScore[url] = score;
        }
        if (urlScore.Count != 0)
            urlScoreNorm = NormalizeScore(urlScore);
        foreach (KeyValuePair<string, int> pair in urlScoreNorm)
        {
            outputRow["peId"].Set(peId);
            outputRow["url"].Set(pair.Key);
            outputRow["score"].Set(pair.Value);
            yield return outputRow;
        }
    }

    public static Dictionary<string, int> NormalizeScore(Dictionary<string, double> urlScore)
    {
        Dictionary<string, int> urlScoreRes = new Dictionary<string, int>();
        foreach (string key in urlScore.Keys)
        {
            double score = urlScore[key];
            urlScoreRes[key] = (int)SigmodNormalize(score);
        }
        return urlScoreRes;
    }

    public static double SigmodNormalize(double score)
    {
        double res = 1 + Math.Exp(-1.0 * score);
        res = 1.0 / res;
        return res * 20;
    }
}