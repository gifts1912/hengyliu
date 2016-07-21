using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using QU.Utility;
using TSVUtility;
using QueryUtility;
using System.Text.RegularExpressions;

//AddEntityMatchFeature.exe {in:ExtractionTSV|ExtractionGZ:ExtractionInput} {out:ExtractionGZ:ExtractionOutput} (SlotType) (FeatureName)

namespace ElectionImprove.BoJiaPipeline
{
    class AddEntityMatchFeatureV0_2
    {
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\demo\ExtractionInput.tsv.gz";
                args[1] = @"D:\demo\wath.tsv.gz";
                args[2] = "Ent_Ent";
                args[3] = "matchEntityUrlFea;matchEntityTitleFea;matchEntitySnippetFea";
            }
            string input = args[0];
            string output = args[1];
            string slotType = args[2];
            string featureName = args[3];

            using (StreamReader extractionFileStreamReader = new StreamReader(TSVFile.OpenInputTSVStream(input)))
            using (StreamWriter outputExtraction = new StreamWriter(TSVFile.OpenOutputTSVStream(output, true)))
            {
                TSVReader extractionReader = new TSVReader(extractionFileStreamReader, true);

                string[] feaArr = featureName.Split(';');
                featureName = string.Join("\t", feaArr);
                outputExtraction.WriteLine(featureName + "\t" + extractionReader.GetHeaderLine());

                while (!extractionReader.EndOfTSV)
                {
                    TSVLine line = extractionReader.ReadLine();
                    string tags = line.GetFeatureValueString("m:Tags");
                    var results = CRFOutputParser.ParseResults(tags);

                    Dictionary<string, double> entityDict = new Dictionary<string, double>();
                    foreach (var result in results)
                    {
                        if (result.Type.Split(',').Contains(slotType))
                        {
                            if (entityDict.ContainsKey(result.Span))
                            {
                                if (result.Score > entityDict[result.Span])
                                {
                                    entityDict[result.Span] = result.Score;
                                }
                            }
                            else
                            {
                                entityDict.Add(result.Span, result.Score);
                            }
                        }
                    }

                    string url = WordBreaker.BreakText(line.GetFeatureValueString("m:Url"), "en-us");
                    string title = WordBreaker.BreakText(line.GetFeatureValueString("m:Title"), "en-us");
                    string snippet = WordBreaker.BreakText(line.GetFeatureValueString("m:Snippet"), "en-us");

                    string canonicalQuery = line.GetFeatureValueString("m:CanonicalQuery");
                    canonicalQuery = Regex.Replace(canonicalQuery, @"AddQuery:(.*?){.*?}", "", RegexOptions.IgnoreCase);
                    CALQuery calQuery = new CALQuery(canonicalQuery, "en-us");

                    Dictionary<string, int> queryTermIndexDict = new Dictionary<string, int>();
                    Dictionary<string, List<QueryWord>> calQueryDict = new Dictionary<string, List<QueryWord>>();
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

                    uint[] numberOfOcc_url = new uint[10];
                    uint[] numberOfOcc_body = new uint[10];
                    uint[] numberOfOcc_title = new uint[10];
                    for (int i = 0; i < 10; i++)
                    {
                        numberOfOcc_url[i] = line.GetFeatureValue("NumberOfOccurrences_Url_" + i);
                        numberOfOcc_body[i] = line.GetFeatureValue("NumberOfOccurrences_Body_" + i);
                        numberOfOcc_title[i] = line.GetFeatureValue("NumberOfOccurrences_MultiInstanceTitle_" + i);
                    }

                    int matchEntity = 0;

                    int matchEntityUrl_ep_fea = 0;
                    int matchEntityTitle_ep_fea = 0;
                    int matchEntitySnippet_ep_fea = 0;

                    foreach (string entity in entityDict.Keys)
                    {
                        if (entityDict[entity] >= 1.0)
                        {
                            // exact phrase match
                            int matchEntityUrl_ep = 0;
                            int matchEntityTitle_ep = 0;
                            int matchEntitySnippet_ep = 0;
                            int matchEntityUrl_wf = 0;
                            int matchEntityTitle_wf = 0;
                            int matchEntitySnippet_wf = 0;
                            entityMatchingExactPhrase(entity, url, title, snippet, out matchEntityUrl_ep, out matchEntityTitle_ep, out matchEntitySnippet_ep);
                            entityMatchingWordsFound(entity, url, title, snippet, calQueryDict, queryTermIndexDict, numberOfOcc_url, numberOfOcc_title, numberOfOcc_body, out matchEntityUrl_wf, out matchEntityTitle_wf, out matchEntitySnippet_wf);

                            if (entity.Split(' ').Length == 1)
                            {
                                matchEntityUrl_ep = Math.Max(matchEntityUrl_ep, matchEntityUrl_wf);
                                matchEntityTitle_ep = Math.Max(matchEntityTitle_ep, matchEntityTitle_wf);
                                matchEntitySnippet_ep = Math.Max(matchEntitySnippet_ep, matchEntitySnippet_wf);

                                matchEntityUrl_ep_fea += matchEntityUrl_ep;
                                matchEntityTitle_ep_fea += matchEntityTitle_ep;
                                matchEntitySnippet_ep_fea += matchEntitySnippet_ep;
                            }
                            else
                            {
                                matchEntityUrl_ep_fea += Math.Max(matchEntityUrl_ep, matchEntityUrl_wf);
                                matchEntityTitle_ep_fea += Math.Max(matchEntityTitle_ep, matchEntityTitle_wf);
                                matchEntitySnippet_ep_fea += Math.Max(matchEntitySnippet_ep, matchEntitySnippet_wf) ;
                            }

                            if (matchEntityTitle_ep == 1000)
                            {
                                matchEntity += 1000;
                            }
                            else if (matchEntitySnippet_ep == 1000)
                            {
                                matchEntity += 750;
                            }
                            else if (matchEntityTitle_wf == 1000)
                            {
                                matchEntity += 500;
                            }
                            else if (matchEntitySnippet_wf == 1000)
                            {
                                matchEntity += 250;
                            }
                            else
                            {
                                matchEntity += 0;
                            }

                        }
                        else
                        {
                            // words found match
                            int matchEntityUrl_wf = 0;
                            int matchEntityTitle_wf = 0;
                            int matchEntitySnippet_wf = 0;
                            entityMatchingWordsFound(entity, url, title, snippet, calQueryDict, queryTermIndexDict, numberOfOcc_url, numberOfOcc_title, numberOfOcc_body, out matchEntityUrl_wf, out matchEntityTitle_wf, out matchEntitySnippet_wf);
                            matchEntity += matchEntityTitle_wf;

                            matchEntityTitle_ep_fea += matchEntityTitle_wf;
                            matchEntityUrl_ep_fea += matchEntityUrl_wf;
                            matchEntitySnippet_ep_fea += matchEntitySnippet_wf;
                        }
                    }
                    string outputLine = "";
                    if (entityDict.Keys.Count == 0)
                    {
                        outputLine = "0\t0\t0\t";
                    }
                    else
                    {
                        int urlScore = (int)(matchEntityUrl_ep_fea / entityDict.Keys.Count);
                        int titleScore = (int)(matchEntityTitle_ep_fea / entityDict.Keys.Count);
                        int snippetScore = (int)(matchEntitySnippet_ep_fea / entityDict.Keys.Count);
                        outputLine = string.Format("{0}\t{1}\t{2}\t", urlScore, titleScore, snippetScore);
                    }
                    outputLine += line.GetWholeLineString();
                    outputExtraction.WriteLine(outputLine);
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

        static void entityMatchingExactPhrase(string entity, string url, string title, string snippet, out int matchEntityUrl, out int matchEntityTitle, out int matchEntitySnippet)
        {
            matchEntityUrl = 0;
            matchEntityTitle = 0;
            matchEntitySnippet = 0;
            if (url.Split(new string[] { entity }, StringSplitOptions.None).Length - 1 > 0)
            {
                matchEntityUrl += 1000;
            }

            if (getMatchCount(entity, title) > 0)
            {
                matchEntityTitle += 1000;
            }
            if (getMatchCount(entity, snippet) > 0)
            {
                matchEntitySnippet += 1000;
            }

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
