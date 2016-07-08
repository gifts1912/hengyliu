using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QU.Utility;
using TSVUtility;
using QueryUtility;
using System.Text.RegularExpressions;
using System.IO;

//DiscoverPRFConstraints.exe {in:ExtractionTSV|ExtractionGZ:ExtractionInput} {out:ExtractionGZ:ExtractionOutput} (TitleThres_Top5:default,3) (TitleThres_Top10:default,5) (BodyThres_Top5:default,4) (BodyThres_Top10:default,8)

namespace ElectionImprove.BoJiaPipeline
{
    class DiscoverPRFConstraints
    {
        public static string[] stopwordList = {"about","an","and","are","as","at","be","but","by","com","for",
                                             "from","how","if","in","is","it","of","on","or","that","the","this",
                                             "to","was","what","when","where","which","who","will","with",
                                             "would","www","a","i", "your", "s"};

        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[6];
                args[0] = @"D:\demo\ExtractionInput.tsv.gz";
                args[1] = @"D:\demo\ExtractionOutput.tsv.gz";
                args[2] = "3";
                args[3] = "5";
                args[4] = "4";
                args[5] = "8";
            }
            string input = args[0];
            string output = args[1];
            int titleTop5Thres = Convert.ToInt32(args[2]);
            int titleTop10Thres = Convert.ToInt32(args[3]);
            int bodyTop5Thres = Convert.ToInt32(args[4]);
            int bodyTop10Thres = Convert.ToInt32(args[5]);

            HashSet<string> stopwordHash = new HashSet<string>();
            foreach (string stopword in stopwordList)
            {
                stopwordHash.Add(stopword);
            }

            using (StreamReader extractionFileStreamReader = new StreamReader(TSVFile.OpenInputTSVStream(input)))
            using (StreamWriter outputExtraction = new StreamWriter(TSVFile.OpenOutputTSVStream(output, true)))
            {
                TSVReader extractionReader = new TSVReader(extractionFileStreamReader, true);

                outputExtraction.WriteLine("m:NewTags\t" + extractionReader.GetHeaderLine());

                QueryBlockReader qbreader = new QueryBlockReader(extractionReader);
                foreach (QueryBlock qb in qbreader.ReadQueryBlocks())
                {
                    string canonicalQuery = qb.Lines[0].GetFeatureValueString("m:CanonicalQuery");
                    canonicalQuery = Regex.Replace(canonicalQuery, @"AddQuery:(.*?){.*?}", "", RegexOptions.IgnoreCase);
                    CALQuery calQuery = new CALQuery(canonicalQuery, "en-us");

                    Dictionary<string, int> queryTermIndexDict = new Dictionary<string, int>();
                    Dictionary<string, List<QueryWord>> calQueryDict = new Dictionary<string, List<QueryWord>>();
                    string originalQuery = qb.Lines[0].GetFeatureValueString("m:Query");
                    for (int i = 0; i < calQuery.Words.Count; i++)
                    {
                        QueryWord queryWord = calQuery.Words[i];
                        if (queryWord.GetTotalAlterationCount() > 1)
                        {
                            calQueryDict[queryWord.AlterWords[0].ToString().Trim('"')] = queryWord.AlterWords;
                        }
                        string queryTermString = getQueryTermString(queryWord);
                        if (!queryTermIndexDict.ContainsKey(queryTermString))
                        {
                            queryTermIndexDict.Add(getQueryTermString(queryWord), i);
                        }
                    }

                    string tags = qb.Lines[0].GetFeatureValueString("m:Tags");
                    var tagResults = CRFOutputParser.ParseResults(tags);
                    for (int i = 0; i < tagResults.Count; i++)
                    {
                        originalQuery = originalQuery.Replace(tagResults[i].Span, "");
                    }
                    string[] unknowQueryParts = originalQuery.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<CandidateTermClass> candidateTermList = new List<CandidateTermClass>();
                    foreach (string term in unknowQueryParts)
                    {
                        if (!stopwordHash.Contains(term))
                        {
                            candidateTermList.Add(new CandidateTermClass(term));
                        }
                    }

                    foreach (TSVLine line in qb.Lines)
                    {
                        uint position = line.GetFeatureValue("DocumentPosition");
                        if (position >= 10)
                        {
                            break;
                        }
                        string url = WordBreaker.BreakText(line.GetFeatureValueString("m:Url"), "en-us");
                        string title = WordBreaker.BreakText(line.GetFeatureValueString("m:Title"), "en-us");
                        string snippet = WordBreaker.BreakText(line.GetFeatureValueString("m:Snippet"), "en-us");

                        uint[] numberOfOcc_url = new uint[10];
                        uint[] numberOfOcc_body = new uint[10];
                        uint[] numberOfOcc_title = new uint[10];
                        for (int i = 0; i < 10; i++)
                        {
                            numberOfOcc_url[i] = line.GetFeatureValue("NumberOfOccurrences_Url_" + i);
                            numberOfOcc_body[i] = line.GetFeatureValue("NumberOfOccurrences_Body_" + i);
                            numberOfOcc_title[i] = line.GetFeatureValue("NumberOfOccurrences_MultiInstanceTitle_" + i);
                        }
                        for (int i = 0; i < candidateTermList.Count; i++)
                        {
                            CandidateTermClass cadidateTerm = candidateTermList[i];
                            string term = cadidateTerm.term;
                            int matchEntityUrl_wf = 0;
                            int matchEntityTitle_wf = 0;
                            int matchEntitySnippet_wf = 0;
                            entityMatchingWordsFound(term, url, title, snippet, calQueryDict, queryTermIndexDict, numberOfOcc_url, numberOfOcc_title, numberOfOcc_body, out matchEntityUrl_wf, out matchEntityTitle_wf, out matchEntitySnippet_wf);

                            if (matchEntityTitle_wf == 1000)
                            {
                                if (position < 5)
                                {
                                    cadidateTerm.titleTop5Count++;
                                }
                                cadidateTerm.titleTop10Count++;
                            }
                            if (matchEntitySnippet_wf == 1000)
                            {
                                if (position < 5)
                                {
                                    cadidateTerm.bodyTop5Count++;
                                }
                                cadidateTerm.bodyTop10Count++;
                            }
                        }
                    }
                    StringBuilder sb = new StringBuilder();
                    foreach (CandidateTermClass candidateTerm in candidateTermList)
                    {
                        if (candidateTerm.titleTop5Count >= titleTop5Thres || candidateTerm.titleTop10Count >= titleTop10Thres || candidateTerm.bodyTop5Count >= bodyTop5Thres || candidateTerm.bodyTop10Count >= bodyTop10Thres)
                        {
                            sb.Append("|{\"Span\":\"" + candidateTerm.term + "\",\"Begin\":0,\"End\":0,\"Score\":0.7,\"Type\":\"Cons_Cons\"}");
                        }
                    }
                    tags += sb.ToString();
                    foreach (TSVLine line in qb.Lines)
                    {
                        outputExtraction.WriteLine(tags + "\t" + line.GetWholeLineString());
                    }
                }
            }
        }

        static void entityMatchingWordsFound(string entity, string url, string title, string snippet, Dictionary<string, List<QueryWord>> calQueryDict, Dictionary<string, int> queryTermIndexDict,
            uint[] numberOfOcc_url, uint[] numberOfOcc_title, uint[] numberOfOcc_body, out int matchEntityUrl, out int matchEntityTitle, out int matchEntitySnippet)
        {
            matchEntityUrl = 0;
            matchEntityTitle = 0;
            matchEntitySnippet = 0;

            int termUrlMatchCount = 0;
            int termTitleMatchCount = 0;
            int termSnippetMatchCount = 0;

            string[] entityTerms = entity.Split(' ');
            foreach (string entityTerm in entityTerms)
            {
                int urlMatchCount = 0;
                int titleMatchCount = 0;
                int snippetMatchCount = 0;

                bool isTermUrlMatch = false;
                bool isTermTitleMatch = false;
                bool isTermSnippetMatch = false;

                if (calQueryDict.ContainsKey(entityTerm))
                {
                    foreach (QueryWord alter in calQueryDict[entityTerm])
                    {
                        string wordString = alter.ToString().Trim('"');
                        urlMatchCount += url.Split(new string[] { wordString }, StringSplitOptions.None).Length - 1;
                        titleMatchCount += getMatchCount(wordString, title);
                        snippetMatchCount += getMatchCount(wordString, snippet);
                    }
                }
                else
                {
                    urlMatchCount = url.Split(new string[] { entityTerm }, StringSplitOptions.None).Length - 1;
                    titleMatchCount = getMatchCount(entityTerm, title);
                    snippetMatchCount = getMatchCount(entityTerm, snippet);
                }
                if (urlMatchCount > 0)
                {
                    isTermUrlMatch = true;
                }
                if (titleMatchCount > 0)
                {
                    isTermTitleMatch = true;
                }
                if (snippetMatchCount > 0)
                {
                    isTermSnippetMatch = true;
                }

                // Use NumberOfOccurrence feature
                if (queryTermIndexDict.ContainsKey(entityTerm))
                {
                    int index = queryTermIndexDict[entityTerm];
                    if (index < 10)
                    {
                        if (numberOfOcc_url[index] > 0)
                        {
                            isTermUrlMatch = true;
                        }
                        if (numberOfOcc_title[index] > 0)
                        {
                            isTermTitleMatch = true;
                        }
                        if (numberOfOcc_body[index] > 0)
                        {
                            isTermSnippetMatch = true;
                        }
                    }
                }

                if (isTermUrlMatch)
                {
                    termUrlMatchCount++;
                }
                if (isTermTitleMatch)
                {
                    termTitleMatchCount++;
                }
                if (isTermSnippetMatch)
                {
                    termSnippetMatchCount++;
                }
            }
            matchEntityUrl += (int)(termUrlMatchCount * 1000 / entityTerms.Length);
            matchEntityTitle += (int)(termTitleMatchCount * 1000 / entityTerms.Length);
            matchEntitySnippet += (int)(termSnippetMatchCount * 1000 / entityTerms.Length);
        }

        static int getMatchCount(string str, string source)
        {
            int count = 0;

            count = source.Split(new string[] { " " + str + " " }, StringSplitOptions.None).Length - 1;

            if (source.StartsWith(str + " "))
            {
                count++;
            }
            if (source.EndsWith(" " + str))
            {
                count++;
            }
            if (source.Equals(str, StringComparison.CurrentCultureIgnoreCase))
            {
                count++;
            }

            return count;
        }

        static string getQueryTermString(QueryWord word)
        {
            string wordString = "";
            if (word.GetTotalAlterationCount() == 1)
            {
                wordString = word.ToString().Trim('"');
                if (word.IsRankOnly())
                {
                    int index = wordString.IndexOf("rankonly:");
                    if (index >= 0 && wordString.Length > (index + 9))
                        wordString = wordString.Substring(index + 9);
                }
                wordString = wordString.ToLower();
            }
            else
            {
                wordString = word.AlterWords[0].ToString().Trim('"');
            }
            return wordString;
        }
    }
}

class CandidateTermClass
{
    public string term;
    public int titleTop5Count { get; set; }
    public int titleTop10Count { get; set; }
    public int bodyTop5Count { get; set; }
    public int bodyTop10Count { get; set; }

    public CandidateTermClass(string term)
    {
        this.term = term;
        titleTop5Count = 0;
        titleTop10Count = 0;
        bodyTop10Count = 0;
        bodyTop5Count = 0;
    }

}
