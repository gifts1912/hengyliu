using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;

public class JoinQueries_StringString : Aggregate2<string, string, string>
{
    string u;
    int count;
    public override void Initialize() { u = null; count = 0; }
    public override void Add(string s, string t)
    {
        if (count > 100)
        {
            return;
        }

        if (u == null)
            u = s + ", " + t;
        else
        {
            u = u + "|||" + s + ", " + t;
            count++;
        }
    }
    public override string Finalize() { return u; }
}

/// <summary>
/// 
/// </summary>
public class DupProcessor : Processor
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
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {

        foreach (Row row in input.Rows)
        {
            row.CopyTo(output);
            yield return output;

            output["leftP"].Set(row["rightP"].String);
            output["rightP"].Set(row["leftP"].String);
            output["freq"].Set(row["freq"].Float);
            yield return output;
        }
    }
}

/// <summary>
/// 
/// </summary>
public class QueryPatternMiner : Processor
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
        //return input.Clone();
        return new Schema("leftP:string, rightP:string, leftQ, rightQ");
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
            //Process
            List<QueryPattern> patterns = QueryPattern.LoadFromQueryPair(row["leftQ"].String, row["rightQ"].String);
            if (patterns == null || patterns.Count == 0)
                continue;

            foreach (QueryPattern pattern in patterns)
            {
                output["leftP"].Set(pattern.Left);
                output["rightP"].Set(pattern.Right);
                output["leftQ"].Set(row["leftQ"].String);
                output["rightQ"].Set(row["rightQ"].String);
                yield return output;
            }
        }
    }


}

public class StopWordUtil
{
    public static HashSet<string> LoadFromFile(string file)
    {
        HashSet<string> list = new HashSet<string>();
        using (StreamReader reader = new StreamReader(file))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;
                list.Add(line);
            }
        }
        return list;
    }
}


public class Utils
{
    /// <summary>
    /// Get the longest common sub phrases. sub phrases are seperated by space.
    /// </summary>
    /// <param name="phrases1"></param>
    /// <param name="start1"></param>
    /// <param name="end1"></param>
    /// <param name="phrases2"></param>
    /// <param name="start2"></param>
    /// <param name="end2"></param>
    /// <param name="subphrase"></param>
    /// <param name="subphraseStart1"></param>
    /// <param name="subphraseStart2"></param>
    /// <returns></returns>
    public static int LongestCommonSubPhrase(string[] phrases1, int start1, int end1,
        string[] phrases2, int start2, int end2,
        out string subphrase, out int subphraseStart1, out int subphraseStart2)
    {
        subphrase = string.Empty;
        subphraseStart1 = 0;
        subphraseStart2 = 0;

        //terminate the recursion
        if (start1 > end1 || start2 > end2)
            return 0;

        //retrieve target phrases
        string[] targetPhrase1 = new string[end1 - start1 + 1];
        for (int i = 0; i < targetPhrase1.Length; i++)
        {
            targetPhrase1[i] = phrases1[i + start1];
        }
        string[] targetPhrase2 = new string[end2 - start2 + 1];
        for (int i = 0; i < targetPhrase2.Length; i++)
        {
            targetPhrase2[i] = phrases2[i + start2];
        }

        //deal with target phrases
        int[,] num = new int[targetPhrase1.Length, targetPhrase2.Length];
        int maxlen = 0;

        for (int i = 0; i < targetPhrase1.Length; i++)
        {
            for (int j = 0; j < targetPhrase2.Length; j++)
            {
                if (targetPhrase1[i] != targetPhrase2[j])
                    num[i, j] = 0;
                else
                {//dynamic programming
                    if ((i == 0) || (j == 0))
                        num[i, j] = 1;
                    else
                        num[i, j] = 1 + num[i - 1, j - 1];

                    if (num[i, j] > maxlen)
                    {
                        maxlen = num[i, j];
                        subphraseStart1 = i - num[i, j] + 1; //this is where the sub phrase starts
                        subphraseStart2 = j - num[i, j] + 1;
                    }
                }
            }
        }

        //concate the subphrases
        for (int i = subphraseStart1; i < subphraseStart1 + maxlen; i++)
        {
            subphrase += targetPhrase1[i] + " ";
        }
        subphrase = subphrase.TrimEnd(' ');

        return maxlen;
    }

    private static void ReplaceSubphrase(ref string[] items,
        string replaceWith, int sublen, int start)
    {
        string[] modified1 = new string[items.Length - sublen + 1];
        for (int i = 0; i < start; i++)
            modified1[i] = items[i];
        modified1[start] = replaceWith;
        for (int i = start + 1; i < modified1.Length; i++)
            modified1[i] = items[i + sublen - 1];

        items = modified1;
    }

    /// <summary>
    /// get all of the overlapped sub phrases (stopword excluded)
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <param name="stopwords"></param>
    /// <returns></returns>
    public static List<string> GetAllOverlappedSubphrases(string str1, string str2, HashSet<string> stopwords)
    {
        List<string> subphrases = new List<string>();
        string[] items1 = str1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string[] items2 = str2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //int start1 = 0, end1 = items1.Length - 1, start2 = 0, end2 = items2.Length - 1;
        string subphrase;
        int subphraseStart1, subphraseStart2;
        int sublen = LongestCommonSubPhrase(items1, 0, items1.Length - 1, items2, 0, items2.Length - 1,
            out subphrase, out subphraseStart1, out subphraseStart2);
        int orderNonStopWord = 0;
        int orderStopWord = -1;
        while (sublen > 0)
        {
            if (sublen != 1 || !stopwords.Contains(subphrase))
            {
                subphrases.Add(subphrase);
                //replace the sub phrase with non-regular terms
                ReplaceSubphrase(ref items1, "\a" + orderNonStopWord.ToString() + "\a", sublen, subphraseStart1);
                ReplaceSubphrase(ref items2, "\f" + orderNonStopWord.ToString() + "\f", sublen, subphraseStart2);
                orderNonStopWord++;
            }
            else //max substring length is 1, maybe a stopword
            {
                ReplaceSubphrase(ref items1, "\a" + orderStopWord.ToString() + "\a", sublen, subphraseStart1);
                ReplaceSubphrase(ref items2, "\f" + orderStopWord.ToString() + "\f", sublen, subphraseStart2);
                orderStopWord--;
            }
            sublen = LongestCommonSubPhrase(items1, 0, items1.Length - 1, items2, 0, items2.Length - 1,
                                                out subphrase, out subphraseStart1, out subphraseStart2);
        }

        return subphrases;
    }

    /// <summary>
    /// Convert a string to phrases. identified N-gram as a single phrase
    /// </summary>
    /// <param name="str"></param>
    /// <param name="listSubPhrases">the list is sorted descendingly</param>
    /// <returns></returns>
    public static string[] ConvertString2PhraseArray(string str, List<string> listSubPhrases)
    {
        //build a dictionary
        Dictionary<string, List<string>> dictStartword2Phrases = new Dictionary<string, List<string>>();
        foreach (string phrase in listSubPhrases)
        {
            string startWord = phrase.Split(' ')[0];
            if (dictStartword2Phrases.ContainsKey(startWord))
            {
                dictStartword2Phrases[startWord].Add(phrase);
            }
            else
            {
                dictStartword2Phrases.Add(startWord, new List<string>(new string[] { phrase }));
            }
        }

        List<string> phrases = new List<string>();
        //convert based on the dictionary
        string[] items = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < items.Length; )
        {
            List<string> listNgram;
            if (dictStartword2Phrases.TryGetValue(items[i], out listNgram))
            {
                bool isNGram = false;
                foreach (string gram in listNgram) //the list is sorted descendingly
                {
                    string[] gramItems = gram.Split(' ');
                    if (items.Length - i < gramItems.Length)
                        continue;
                    bool isEqualGram = true;
                    for (int j = 1; j < gramItems.Length; j++)
                    {
                        if (!gramItems[j].Equals(items[i + j]))
                        {
                            isEqualGram = false;
                            break;
                        }
                    }
                    if (!isEqualGram)
                        continue;
                    isNGram = isEqualGram; //true here
                    phrases.Add(gram);
                    i += gramItems.Length;
                    break;
                }
                if (!isNGram) //directly output, will not catch by this block, but in case!
                {
                    phrases.Add(items[i++]);
                }
            }
            else //directly output
            {
                phrases.Add(items[i++]);
            }
        }

        return phrases.ToArray();
    }
}


public class QueryPattern
{
    private string left;
    public string Left
    {
        get { return left; }
    }
    private string right;
    public string Right
    {
        get { return right; }
    }

    public static List<QueryPattern> LoadFromQueryPair(string leftQ, string rightQ)
    {
        string normLeftQ = leftQ.Replace('*', ' ');
        string normRightQ = rightQ.Replace('*', ' ');

        if (string.IsNullOrEmpty(normLeftQ) || string.IsNullOrEmpty(rightQ))
            return null;

        HashSet<string> stopwords = StopWordUtil.LoadFromFile("stopword.txt");

        //step 1: filter queries which have no common word or all common words
        string[] itemsInLeft = normLeftQ.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        HashSet<string> termsInLeft = new HashSet<string>();
        foreach (string item in itemsInLeft)
        {
            if (stopwords.Contains(item))
                continue;
            termsInLeft.Add(item);
        }
        HashSet<string> allTerms = new HashSet<string>(termsInLeft);
        string[] itemsInRight = normRightQ.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        HashSet<string> overlappedTerms = new HashSet<string>();
        foreach (string item in itemsInRight)
        {
            if (stopwords.Contains(item))
                continue;
            if (termsInLeft.Contains(item))
                overlappedTerms.Add(item);
            else
                allTerms.Add(item);
        }

        if (overlappedTerms.Count == 0 || overlappedTerms.Count == allTerms.Count)
            return null;

        //Step 2: Sub phrases are searched using LCS algorithm
        List<string> listCommonSubPhrases = Utils.GetAllOverlappedSubphrases(normLeftQ, normRightQ, stopwords);
        if (listCommonSubPhrases.Count > 6) //for efficiency issue
            return null;
        string[] phrasesInLeft = Utils.ConvertString2PhraseArray(normLeftQ, listCommonSubPhrases);
        string[] phrasesInRight = Utils.ConvertString2PhraseArray(normRightQ, listCommonSubPhrases);
        HashSet<string> setCommonSubPhrases = new HashSet<string>(listCommonSubPhrases);


        //step 3: loop all of the subsets of the overlapped terms to form patterns
        List<QueryPattern> patterns = new List<QueryPattern>();
        string[] overlap = new string[setCommonSubPhrases.Count];
        int tmp = 0;
        foreach (string item in setCommonSubPhrases)
            overlap[tmp++] = item;
        int allSubsets = (int)Math.Pow(2, overlap.Length);

        for (int i = 1; i < allSubsets; i++)
        {
            HashSet<string> subset = new HashSet<string>();
            //construct subset
            tmp = i;
            int j = 0;
            while (tmp != 0)
            {
                if (tmp % 2 != 0)
                    subset.Add(overlap[j]);
                tmp /= 2;
                j++;
            }
            //replace words in subset as "*" to form the pattern
            QueryPattern pattern = new QueryPattern();
            pattern.left = "";
            pattern.right = "";
            for (j = 0; j < phrasesInLeft.Length; j++)
            {
                int termCnt = phrasesInLeft[j].Split(' ').Length;
                if ((termCnt == 1 && phrasesInLeft[j].StartsWith("Slot^"))
                    || !subset.Contains(phrasesInLeft[j]))
                {
                    pattern.left = pattern.left + " " + phrasesInLeft[j];
                }
                else
                {
                    pattern.left = pattern.left + " *";
                }
            }
            for (j = 0; j < phrasesInRight.Length; j++)
            {
                int termCnt = phrasesInRight[j].Split(' ').Length;
                if ((termCnt == 1 && phrasesInRight[j].StartsWith("Slot^"))
                    || !subset.Contains(phrasesInRight[j]))
                {
                    pattern.right = pattern.right + " " + phrasesInRight[j];
                }
                else
                {
                    pattern.right = pattern.right + " *";
                }
            }
            pattern.left = pattern.left.Trim();
            pattern.right = pattern.right.Trim();
            patterns.Add(pattern);
        }

        return patterns;
    }
}