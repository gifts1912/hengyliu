using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class PatternPruningProcessor : Processor
{
    static char[] seperators = new char[] { ' ', '*' };
    static char[] space = new char[] { ' ' };

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
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        int minOcc = int.Parse(args[0]);
        HashSet<string> stopwords = Utility.StopWordUtil.LoadFromFile(args[1]);
        foreach (Row row in input.Rows)
        {
            long l2r = row["l2r"].Long;
            long r2l = row["r2l"].Long;
            string left = row["leftP"].String;
            string right = row["rightP"].String;

            if ((l2r < minOcc && r2l < minOcc)
                || IsSimpleReformulation(left, right, stopwords)
                || SlotMismatch(left, right)
                || WildMismatch(left, right)
                )
            {
                continue;
            }

            row.CopyTo(output);
            yield return output;
        }
    }

    private static bool SlotMismatch(string left, string right)
    {
        string[] lArr = left.Split(space, StringSplitOptions.RemoveEmptyEntries).Where(t => t.StartsWith("Slot^")).ToArray();
        string[] rArr = right.Split(space, StringSplitOptions.RemoveEmptyEntries).Where(t => t.StartsWith("Slot^")).ToArray();
        if (lArr.Length != rArr.Length)
            return true;

        var leftItems = new HashSet<string>(lArr);
        var rightItems = new HashSet<string>(rArr);
        if (leftItems.Count != rightItems.Count)
            return true;
        HashSet<string> overlapped;
        int overlap = Utility.CommonUtils.Overlap(leftItems, rightItems, out overlapped);
        if (overlap != leftItems.Count)
            return true;
        return false;
    }

    private static bool WildMismatch(string left, string right)
    {
        int leftWild = left.Count(c => c == '*');
        int rightWild = right.Count(c => c == '*');
        if (leftWild != rightWild || rightWild > 2)
            return true;
        return false;
    }

    /// <summary>
    /// Simple Reform: just some stopwords in patterns
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="stopwords"></param>
    /// <returns></returns>
    private static bool IsSimpleReformulation(string left, string right, HashSet<string> stopwords)
    {
        // left should not be just "*".
        var items = left.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
        if (items.Length == 0)
        {
            return true;
        }

        foreach (var item in items)
        {
            if (!stopwords.Contains(item))
            {
                return false;
            }
        }

        items = right.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in items)
        {
            if (!stopwords.Contains(item))
            {
                return false;
            }
        }

        return true;
    }
}

