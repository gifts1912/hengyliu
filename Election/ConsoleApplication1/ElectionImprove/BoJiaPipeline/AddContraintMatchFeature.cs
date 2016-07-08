using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using QU.Utility;
using QueryUtility;
using System.Text.RegularExpressions;
using System.IO;

//AddContraintMatchFeature.exe {in:ExtractionTSV|ExtractionGZ:ExtractionInput} {in:GenericTSV:ConstraintData} {out:ExtractionGZ:ExtractionOutput} (SlotType) (FeatureName)

namespace ElectionImprove.BoJiaPipeline
{
    class AddContraintMatchFeature
    {
        private const int c_officialSiteScore = 1000;
        private const int c_constraintMatchScoreOpposed = 1;
        private const int c_constraintMatchScore_1st = 800;
        private const int c_constraintMatchScore_2nd = 600;
        private const int c_constraintMatchScore_3rd = 400;
        private const int c_constraintMatchScore_4th = 200;

        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[5];
                args[0] = @"D:\demo\ExtractionInput.tsv.gz";
                args[1] = @"D:\demo\constraintData.tsv";
                args[2] = @"D:\demo\out.tsv.gz";
                args[3] = "Cons_Cons";
                args[4] = "ConstraintMatch";
            }
            string input = args[0];
            string constraintData = args[1];
            string output = args[2];
            string slotType = args[3];
            string featureName = args[4];

            StreamReader constraintDataReader = new StreamReader(constraintData);
            Dictionary<string, ConstraintDataClass> consDataDict = new Dictionary<string, ConstraintDataClass>();
            while (!constraintDataReader.EndOfStream)
            {
                string[] lineArray = constraintDataReader.ReadLine().Split('\t');
                if (lineArray.Length != 4)
                {
                    continue;
                }
                string[] keys = lineArray[0].ToLower().Split(new string[] { "|||" }, StringSplitOptions.None);
                List<string> synoList = lineArray[1].ToLower().Split(new string[] { "|||" }, StringSplitOptions.None).ToList();
                List<string> opposedList = lineArray[2].ToLower().Split(new string[] { "|||" }, StringSplitOptions.None).ToList();
                List<string> excludeList = lineArray[3].ToLower().Split(new string[] { "|||" }, StringSplitOptions.None).ToList();

                foreach (string key in keys)
                {
                    if (!consDataDict.ContainsKey(key))
                    {
                        consDataDict.Add(key, new ConstraintDataClass(key, synoList, opposedList, excludeList));
                    }
                }
            }

            using (StreamReader extractionFileStreamReader = new StreamReader(TSVFile.OpenInputTSVStream(input)))
            using (StreamWriter outputExtraction = new StreamWriter(TSVFile.OpenOutputTSVStream(output, true)))
            {
                TSVReader extractionReader = new TSVReader(extractionFileStreamReader, true);

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

                    uint authorityScore = line.GetFeatureValue("AuthorityScore");
                    uint constraintMatchUrlScore = line.GetFeatureValue("UrlPatternForConstraintMatch");
                    uint documentPosition = line.GetFeatureValue("DocumentPosition");

                    uint[] constraintMatchConditionArray = new uint[5];
                    constraintMatchConditionArray[0] = line.GetFeatureValue("ConstraintMatchingLevel_Opposed");
                    for (int i = 1; i < constraintMatchConditionArray.Length; i++)
                    {
                        constraintMatchConditionArray[i] = line.GetFeatureValue("ConstraintMatchingLevel_" + i);
                    }

                    int consMatch = 0;
                    bool hasOpposed = false;
                    foreach (string consOri in entityDict.Keys)
                    {
                        int consMatchTemp = GenerateConstraintMatchingScore(constraintMatchConditionArray, consOri, consDataDict, url, title, snippet, calQueryDict, queryTermIndexDict,
                        numberOfOcc_url, numberOfOcc_title, numberOfOcc_body, authorityScore, constraintMatchUrlScore, documentPosition);
                        consMatch += consMatchTemp;
                        if (consMatchTemp == 1)
                        {
                            hasOpposed = true;
                        }
                    }
                    string outputLine = "";
                    if (entityDict.Keys.Count == 0)
                    {
                        outputLine = "0\t";
                    }
                    else
                    {
                        if (hasOpposed)
                        {
                            outputLine = "1\t";
                        }
                        else
                        {
                            outputLine = (int)(consMatch / entityDict.Keys.Count) + "\t";
                        }
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

        static void entityArrayMatchingExactPhrase(List<string> entityList, string url, string title, string snippet, out int matchEntityUrl, out int matchEntityTitle, out int matchEntitySnippet)
        {
            matchEntityUrl = 0;
            matchEntityTitle = 0;
            matchEntitySnippet = 0;

            if (entityList == null || entityList.Count == 0)
            {
                return;
            }

            foreach (string entity in entityList)
            {
                if (matchEntityUrl == 0 && url.Split(new string[] { entity }, StringSplitOptions.None).Length - 1 > 0)
                {
                    matchEntityUrl = 1000;
                }
                if (matchEntityTitle == 0 && getMatchCount(entity, title) > 0)
                {
                    matchEntityTitle = 1000;
                }
                if (matchEntitySnippet == 0 && getMatchCount(entity, snippet) > 0)
                {
                    matchEntitySnippet = 1000;
                }
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

        static int GenerateConstraintMatchingScore(uint[] constraintMatchConditionArray, string consOri, Dictionary<string, ConstraintDataClass> consDataDict, string url, string title, string snippet,
            Dictionary<string, List<QueryWord>> calQueryDict, Dictionary<string, int> queryTermIndexDict,
            uint[] numberOfOcc_url, uint[] numberOfOcc_title, uint[] numberOfOcc_body, uint authorityScore, uint constraintMatchUrlScore, uint documentPosition)
        {
            if (constraintMatchConditionArray == null || constraintMatchConditionArray.Length != 5)
            {
                return 0;
            }

            int consOriMatchUrl = 0;
            int consOriMatchTitle = 0;
            int consOriMatchBody = 0;
            entityMatchingWordsFound(consOri, url, title, snippet, calQueryDict, queryTermIndexDict, numberOfOcc_url, numberOfOcc_title, numberOfOcc_body, out consOriMatchUrl, out consOriMatchTitle, out consOriMatchBody);
            bool isConsOriMatchUrl = (consOriMatchUrl == 1000);
            bool isConsOriMatchTitle = (consOriMatchTitle == 1000);
            bool isConsOriMatchBody = (consOriMatchBody == 1000);

            bool isConsSynoMatchUrl = false;
            bool isConsSynoMatchTitle = false;
            bool isConsSynoMatchBody = false;
            bool isConsOpposedMatchUrl = false;
            bool isConsOpposedMatchTitle = false;
            bool isConsOpposedMatchBody = false;
            bool isConsExcludeMatchUrl = false;
            bool isConsExcludeMatchTitle = false;
            bool isConsExcludeMatchBody = false;

            if (consDataDict != null && consDataDict.ContainsKey(consOri))
            {
                int consSynoMatchUrl = 0;
                int consSynoMatchTitle = 0;
                int consSynoMatchBody = 0;
                entityArrayMatchingExactPhrase(consDataDict[consOri].synoList, url, title, snippet, out consSynoMatchUrl, out consSynoMatchTitle, out consSynoMatchBody);
                isConsSynoMatchUrl = (consSynoMatchUrl == 1000);
                isConsSynoMatchTitle = (consSynoMatchTitle == 1000);
                isConsSynoMatchBody = (consSynoMatchBody == 1000);

                int consOpposedMatchUrl = 0;
                int consOpposedMatchTitle = 0;
                int consOpposedMatchBody = 0;
                entityArrayMatchingExactPhrase(consDataDict[consOri].opposedList, url, title, snippet, out consOpposedMatchUrl, out consOpposedMatchTitle, out consOpposedMatchBody);
                isConsOpposedMatchUrl = (consOpposedMatchUrl == 1000);
                isConsOpposedMatchTitle = (consOpposedMatchTitle == 1000);
                isConsOpposedMatchBody = (consOpposedMatchBody == 1000);

                int consExcludeMatchUrl = 0;
                int consExcludeMatchTitle = 0;
                int consExcludeMatchBody = 0;
                entityArrayMatchingExactPhrase(consDataDict[consOri].excludeList, url, title, snippet, out consExcludeMatchUrl, out consExcludeMatchTitle, out consExcludeMatchBody);
                isConsExcludeMatchUrl = (consExcludeMatchUrl == 1000);
                isConsExcludeMatchTitle = (consExcludeMatchTitle == 1000);
                isConsExcludeMatchBody = (consExcludeMatchBody == 1000);
            }

            //Verify whether is condition0 match
            var isCondition0Match = false;     //Opposed match
            switch (constraintMatchConditionArray[0])
            {
                case 0:
                    isCondition0Match = false;
                    break;
                case 1:
                    isCondition0Match = true;
                    break;
                case 2:
                    if (isConsOpposedMatchTitle && !isConsOriMatchTitle)
                    {
                        isCondition0Match = true;
                    }
                    break;
                case 3:
                    if ((isConsOpposedMatchTitle || isConsOpposedMatchUrl) && !isConsOriMatchTitle && !isConsOriMatchUrl)
                    {
                        isCondition0Match = true;
                    }
                    break;
                default:
                    isCondition0Match = false;
                    break;
            }
            if (isCondition0Match)
            {
                return c_constraintMatchScoreOpposed;
            }

            //Verify whether is condition1 match
            var isCondition1Match = false;
            switch (constraintMatchConditionArray[1])
            {
                case 0:
                    isCondition1Match = false;
                    break;
                case 1:
                    isCondition1Match = true;
                    break;
                case 2:
                    if ((isConsOriMatchTitle || isConsSynoMatchTitle) && (constraintMatchUrlScore == 1 || authorityScore == c_officialSiteScore))
                    {
                        isCondition1Match = true;
                    }
                    break;
                case 3:
                    if ((isConsOriMatchTitle || isConsSynoMatchTitle || isConsOriMatchUrl || isConsSynoMatchUrl) && (constraintMatchUrlScore == 1 || authorityScore == c_officialSiteScore))
                    {
                        isCondition1Match = true;
                    }
                    break;
                default:
                    isCondition1Match = false;
                    break;
            }
            if (isCondition1Match)
            {
                return c_constraintMatchScore_1st;
            }

            //Verify whether is condition2 match
            var isCondition2Match = false;
            switch (constraintMatchConditionArray[2])
            {
                case 0:
                    isCondition2Match = false;
                    break;
                case 1:
                    isCondition2Match = true;
                    break;
                case 2:
                    if (((isConsOriMatchBody || isConsSynoMatchBody) && !isConsOpposedMatchTitle && !isConsExcludeMatchBody && (constraintMatchUrlScore == 1 || authorityScore == c_officialSiteScore)) || ((isConsOriMatchTitle || isConsSynoMatchTitle) && constraintMatchUrlScore == 2))
                    {
                        isCondition2Match = true;
                    }
                    break;
                case 3:
                    if (((isConsOriMatchBody || isConsSynoMatchBody) && !isConsOpposedMatchTitle && !isConsOpposedMatchUrl && !isConsExcludeMatchBody && (constraintMatchUrlScore == 1 || authorityScore == c_officialSiteScore)) || ((isConsOriMatchTitle || isConsSynoMatchTitle || isConsOriMatchUrl || isConsSynoMatchUrl) && constraintMatchUrlScore == 2))
                    {
                        isCondition2Match = true;
                    }
                    break;
                default:
                    isCondition2Match = false;
                    break;
            }
            if (isCondition2Match)
            {
                return c_constraintMatchScore_2nd;
            }

            //Verify whether is condition3 match
            var isCondition3Match = false;
            switch (constraintMatchConditionArray[3])
            {
                case 0:
                    isCondition3Match = false;
                    break;
                case 1:
                    isCondition3Match = true;
                    break;
                case 2:
                    if ((isConsOriMatchTitle || isConsSynoMatchTitle) && documentPosition < 5)
                    {
                        isCondition3Match = true;
                    }
                    break;
                case 3:
                    if ((isConsOriMatchTitle || isConsSynoMatchTitle || isConsOriMatchUrl || isConsSynoMatchUrl) && documentPosition < 5)
                    {
                        isCondition3Match = true;
                    }
                    break;
                default:
                    isCondition3Match = false;
                    break;
            }
            if (isCondition3Match)
            {
                return c_constraintMatchScore_3rd;
            }

            //Verify whether is condition4 match
            var isCondition4Match = false;
            switch (constraintMatchConditionArray[4])
            {
                case 0:
                    isCondition4Match = false;
                    break;
                case 1:
                    isCondition4Match = true;
                    break;
                case 2:
                    if ((isConsOriMatchBody || isConsSynoMatchBody) && !isConsOpposedMatchTitle && !isConsExcludeMatchBody && documentPosition < 5)
                    {
                        isCondition4Match = true;
                    }
                    break;
                case 3:
                    if ((isConsOriMatchBody || isConsSynoMatchBody) && !isConsOpposedMatchTitle && !isConsOpposedMatchUrl && !isConsExcludeMatchBody && documentPosition < 5)
                    {
                        isCondition4Match = true;
                    }
                    break;
                default:
                    isCondition4Match = false;
                    break;
            }
            if (isCondition4Match)
            {
                return c_constraintMatchScore_4th;
            }
            return 0;
        }
    }

    class ConstraintDataClass
    {
        public string key;
        public List<string> synoList;
        public List<string> opposedList;
        public List<string> excludeList;

        public ConstraintDataClass(string key, List<string> synoList, List<string> opposedList, List<string> excludeList)
        {
            this.key = key;
            this.synoList = synoList;
            this.opposedList = opposedList;
            this.excludeList = excludeList;
        }
    }
}
