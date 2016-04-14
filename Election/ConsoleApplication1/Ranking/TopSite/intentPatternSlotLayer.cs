using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.TopSite.intentPatternSlotLayer
{
    public class intentPatternSlotLayer
    {

        public static int QueryCol = 0, UrlCol = 1, SortPCol = 3;
        public static Dictionary<string, int> urlScoreDic = new Dictionary<string, int>();
        public static Dictionary<string, int> urlDomainScoreDic = new Dictionary<string, int>();
        public static Dictionary<string, int> queryScoreDic = new Dictionary<string, int>();
        public static HashSet<string> patternQuerySet = new HashSet<string>();
        public static HashSet<string> NeedDetailSlot = new HashSet<string>(new string[] { "[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]", "[election.Gender.Female]", "[election.howmany]", "[election.when]", "[election.timelineword]" });
        public static HashSet<string> stayWords = new HashSet<string>(new string[] { "vice", "female", "male", "black", "white", "marriage", "top", "vs", "and", "or", "history" });
        public static Dictionary<string, Dictionary<string, List<string>>> intentPatternQuery = new Dictionary<string, Dictionary<string, List<string>>>();
        // public static Dictionary<string, Dictionary<string, List<string>>> intentPatternSlotQuery = new Dictionary<string, Dictionary<string, List<string>>>();
        public static Dictionary<string, List<string>> queryUrlList = new Dictionary<string, List<string>>();
        public static Dictionary<string, Dictionary<string, Dictionary<string, int>>> intentSlotUrlScore = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

        public static Dictionary<string, string> slotIdealSlot = new Dictionary<string, string>();
        public static string GenUrlDomain(string url, Regex rgx)
        {
            string result = "";
            Match mc = rgx.Match(url);
            if (mc.Success)
            {
                result = mc.Groups[3].Value.ToString();
            }
            //  Console.WriteLine("{0}\t{1}", url, result);
            // Console.ReadKey();
            return result;
        }

        public static void ReadQueryTopUrl(string infile, int n)
        {
            /*
             * Just get first position url of query. Stored them in Dictornary<string, List<string>> queryUrlList;
             */
            int queryCol = 1, urlCol = 4, posCol = 7;
            StreamReader sr = new StreamReader(infile);
            string line, query, url;
            int pos;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= queryCol || arr.Length <= urlCol || arr.Length <= posCol)
                    continue;
                query = arr[queryCol];
                url = arr[urlCol];
                pos = int.Parse(arr[posCol]);
                if(!queryUrlList.ContainsKey(query))
                {
                    queryUrlList[query] = new List<string>();
                }
                if(pos <= n)
                {
                    url = string.Format("{0}\t{1}", url, pos);
                    queryUrlList[query].Add(url);
                }         
            }
            /*
            while (true)
            {
                line = sr.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
                line = line.Trim();
                string query, url;
                if (line.StartsWith("<Text>"))
                {
                    query = line.Substring(6, line.Length - 13);
                    if (!queryUrlList.ContainsKey(query))
                    {
                        queryUrlList[query] = new List<string>();
                    }
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;
                    line = line.Trim();
                    url = line.Substring(5, line.Length - 11);
                    queryUrlList[query].Add(url);
                }
            }
             */
            sr.Close();
        }

        public static void TopSiteScoreCompute(string flag , bool topDomainFlag)
        {
            /*
             * Compute the url socre of specified intent + slot layer.
             */
            //Dictionary<string, Dictionary<string, Dictionary<string, int>>> intentSlotUrlScore = new Dictionary<string,Dictionary<string,Dictionary<string,int>>>();

            string pattern = "http(s)?://(www.)?([0-9a-zA-Z-.]+)/";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled);

            foreach (KeyValuePair<string, Dictionary<string, List<string>>> pair in intentPatternQuery)
            {
                string intent = pair.Key;
                if (!intentSlotUrlScore.ContainsKey(intent))
                {
                    intentSlotUrlScore[intent] = new Dictionary<string, Dictionary<string, int>>();
                }
                Dictionary<string, List<string>> slotQuery = pair.Value;
                foreach (KeyValuePair<string, List<string>> pairEle in slotQuery)
                {
                    string slot = pairEle.Key;
                    if (!intentSlotUrlScore[intent].ContainsKey(slot))
                    {
                        intentSlotUrlScore[intent][slot] = new Dictionary<string, int>();
                    }
                    List<string> queryList = pairEle.Value;
                    foreach (string query in queryList)
                    {
                        /*if (intent == "CandidateView" && slot == "immigration") //CandidateView	immigration
                        {
                            Console.WriteLine("{0}", query);
                            Console.ReadKey();
                        }*/
                        int score = 0;
                        if (!queryScoreDic.ContainsKey(query))
                            continue;
                        score = queryScoreDic[query];
                        if (flag.ToLower().Contains("google"))
                        {
                            if (score <= 0)
                            {
                                score = -1 * score + 1;
                            }
                            else
                                score = -1 * score;
                           // score = score * 2;
                        }
                        else
                        {
                            if (score >= 0)
                            {
                                score = score + 1;
                            }
                            else
                                score = -1 * score;                          
                        }
                        if (!queryUrlList.ContainsKey(query))
                            continue;
                        List<string> urlList = queryUrlList[query];
                        foreach (string urlPos in urlList)
                        {
                            string[] urlPosArr = urlPos.Split('\t');
                            if (urlPosArr.Length != 2)
                                continue;
                            string url = urlPosArr[0];
                            int posCur = int.Parse(urlPosArr[1]);

                            string urlDomain = url;
                            if (topDomainFlag)
                            {
                                urlDomain = GenUrlDomain(url, rgx);
                            }
                            score = score >= 0 ? 1 : -1; 
                            score = (11 - posCur)*score;

                            if (!intentSlotUrlScore[intent][slot].ContainsKey(urlDomain))
                            {
                                intentSlotUrlScore[intent][slot][urlDomain] = 0;
                            }
                            intentSlotUrlScore[intent][slot][urlDomain] += score;
                        }
                    }
                }
            }

        }

        public static int MyCmp(KeyValuePair<string, int> k1, KeyValuePair<string, int> k2)
        {
            return k2.Value.CompareTo(k1.Value);
        }
        public static void ReadQueryScore(string infile)
        {
            int queryCol = 1, judgeMentCol = 5;

            StreamReader sr = new StreamReader(infile);
            string line;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string query = arr[queryCol];
                int score = 0;
                try
                {
                    score = int.Parse(arr[judgeMentCol]);
                }
                catch (Exception ce)
                {
                    continue;
                }
                queryScoreDic[query] = score;
            }

            sr.Close();
        }

        public static string NormalizePattern(string pattern)
        {
            /*
             * Delete the ele that doesn't belong to slot entity, and construct the normalized pattern with sorted slot element.
             */
            string result = "";
            string[] patArr = pattern.Split(' ');
            List<string> patEleArr = new List<string>();
            foreach (string ele in patArr)
            {
                if (ele.Contains("."))
                {
                    patEleArr.Add(ele);
                }
            }
            patEleArr.Sort();
            result = string.Join(" ", patEleArr.ToArray());

            return result;
        }

        public static void LoadQueryIntent(Dictionary<string, string> queryIntent, string infile)
        {
            int queryCol = 0, intentCol = 3;
            StreamReader sr = new StreamReader(infile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= intentCol || arr.Length <= queryCol)
                {
                    continue;
                }
                string query = arr[queryCol];
                queryIntent[query] = arr[intentCol];
            }
            sr.Close();
        }

        public static string PreProcess(string line)
        {
            /*
             * Process the error slot of pbxml, such as: 
             * "bernard sanders	bernard sanders	[film.film] [people.person]	film.film	[film.film]|||[people.person]	bernie|||sanders"
             */
            int slotCol = 2, slotKeyCol = 4, slotValueCol = 5;
            string result = "";
            if (!line.Contains("[film.film]"))
            {
                result = line;
            }
            else
            {
                string[] arr = line.Split('\t');
                string slotStr = arr[slotCol];
                if (slotStr.Contains("[people.person] [film.film]"))
                {
                    slotStr = slotStr.Replace("[people.person] [film.film]", "[election.candidate.highconf]");
                }
                else if (slotStr.Contains("[film.film] [people.person]"))
                {
                    slotStr = slotStr.Replace("[film.film] [people.person]", "[election.candidate.highconf]");
                }
                arr[slotCol] = slotStr;

                if (arr[slotKeyCol] == "[people.person]|||[film.film]" || arr[slotKeyCol] == "[film.film]|||[people.person]")
                {
                    arr[slotKeyCol] = "[election.candidate.highconf]";
                    arr[slotValueCol].Replace("|||", " ");
                }
                result = string.Join("\t", arr);
            }
            return result;
        }

        public static void LoadSlotIdealSlot(string infile)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                slotIdealSlot[arr[0]] = arr[1];
            }

            sr.Close();
        }
        public static string NormalizationPatternSlot(string slotStr, string[] slotKeyArr, string[] slotValueArr)
        {/*
          * replace the slot name with value;  delete words that not slotted; sort to filter the influence of sort. 
          */
            string result = "";
            List<string> slotPattern = new List<string>();
            string[] slotArr = slotStr.Split();
            for (int i = 0; i < slotArr.Length; i++)
            {
                string ele = slotArr[i];
          
                if (!ele.Contains("."))
                {
                    if (stayWords.Contains(ele))
                    {
                        slotPattern.Add(ele);
                    }
                    continue;
                }

                string slotValue = ele;
                if (NeedDetailSlot.Contains(ele))
                {
                    int pos = Array.IndexOf(slotKeyArr, ele);
                    if (pos != -1)
                    {
                        slotValue = slotValueArr[pos];
                    }
                    if(slotIdealSlot.ContainsKey(slotValue))
                    {
                        slotValue = slotIdealSlot[slotValue];
                    }
                    if(slotValue.StartsWith("on "))
                    {
                        slotValue = slotValue.Substring(3);
                    }
                    if(slotValue.EndsWith(" on"))
                    {
                        slotValue = slotValue.Substring(0, slotValue.Length - 3);
                    }
                    slotValue = slotValue.Replace(" on ", " ");
                    slotPattern.Add(slotValue);
                }        
            }
            slotPattern.Sort();
            result = string.Join(" ", slotPattern.ToArray());
            return result;
        }
        public static void ReadPatternSlotFrequencyQuery(string infile, string queryIntentManualLabelledFile, Dictionary<string, Dictionary<string, Dictionary<string, int>>> intentSlotQueryFreq, params string[] intentFlag)
        {
            /*
             * Read pbxml file result, and slot query list of specsified "intent+patternSlot".
             * (1): Map query to intent. Map<query, intent>;
             * (2): pattern + slot -> pattern.
             * (3): Dictionary<string, Dictionary<string, List<string>>> intentPatternSlotQuery 
             */
            Dictionary<string, string> queryIntent = new Dictionary<string, string>();
            LoadQueryIntent(queryIntent, queryIntentManualLabelledFile);

            int slotKeyCol = 4, slotValueCol = 5, queryCol = 0, slotCol = 2;
            StreamReader sr = new StreamReader(infile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = PreProcess(line);
                string[] arr = line.Split('\t');
                if (arr.Length <= slotKeyCol || arr.Length <= slotValueCol || arr.Length <= queryCol || arr.Length <= slotCol)
                {
                    continue;
                }
                string query = arr[queryCol];
                if (!queryIntent.ContainsKey(query))
                {
                    continue;
                }
                string intent = queryIntent[query];
                string slotStr = arr[slotCol];
                string[] slotKeyArr = arr[slotKeyCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] slotValueArr = arr[slotValueCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
               // slotStr = NormalizationPatternSlot(slotStr, slotKeyArr, slotValueArr);
                slotStr = Utility.Utility.NormalizationPatternSlot(slotStr, slotKeyArr, slotValueArr, intent, slotIdealSlot);
                //  slotStr = slotStr.Replace("on", "");
                /*  string oldStr = slotStr;
                  if (slotStr.EndsWith(" on"))
                      slotStr = slotStr.Substring(0, slotStr.Length - 3);
                  if (slotStr.StartsWith("on "))
                      slotStr = slotStr.Substring(3);
                  slotStr = slotStr.Replace(" on ", "");*/
                if (!intentSlotQueryFreq.ContainsKey(intent))
                {
                    intentSlotQueryFreq[intent] = new Dictionary<string, Dictionary<string, int>>();
                }
                if (!intentSlotQueryFreq[intent].ContainsKey(slotStr))
                {
                    intentSlotQueryFreq[intent][slotStr] = new Dictionary<string, int>();
                }
                if(!intentSlotQueryFreq[intent][slotStr].ContainsKey(query))
                {
                    intentSlotQueryFreq[intent][slotStr][query] = 0;
                }
                intentSlotQueryFreq[intent][slotStr][query] += 1;
            }
            sr.Close();

            foreach(KeyValuePair<string, Dictionary<string, Dictionary<string, int>>> pair in intentSlotQueryFreq)
            {
                string intent = pair.Key;
                if (!intentPatternQuery.ContainsKey(intent))
                {
                    intentPatternQuery[intent] = new Dictionary<string, List<string>>();
                }
                
                Dictionary<string, Dictionary<string, int>> slotQueryFreq = pair.Value;

                foreach(KeyValuePair<string, Dictionary<string, int>> pairSlot in slotQueryFreq)
                {
                    string slotStr = pairSlot.Key;
                    if (!intentPatternQuery[intent].ContainsKey(slotStr))
                    {
                        intentPatternQuery[intent][slotStr] = new List<string>();
                    }
                    List<KeyValuePair<string, int>> queryFreqList = pairSlot.Value.ToList();
                    queryFreqList.Sort(FreqCmp);
                    foreach(KeyValuePair<string, int> ele in queryFreqList)
                    {
                        string query = ele.Key;
                      //  query = string.Format("{0}:{1}", ele.Key, ele.Value);
                        intentPatternQuery[intent][slotStr].Add(query);
                    }
                }
            }
          //  Display(intentPatternQuery);
        }

        public static void Display(Dictionary<string, Dictionary<string, List<string>>> dic)
        {
            foreach(KeyValuePair<string, Dictionary<string, List<string>>> pair in dic)
            {
                string intent = pair.Key;
                foreach(KeyValuePair<string, List<string>> pairEle in pair.Value)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", intent, pairEle.Key, string.Join("\t", pairEle.Value));
                }
            }
            Console.ReadKey();
        }
        public static int FreqCmp(KeyValuePair<string, int> pairA, KeyValuePair<string, int> pairB)
        {
            return pairB.Value.CompareTo(pairA.Value);
        }
        public static void ReadPatternSlotQuery(string infile, string queryIntentManualLabelledFile)
        {
            /*
             * Read pbxml file result, and slot query list of specsified "intent+patternSlot".
             * (1): Map query to intent. Map<query, intent>;
             * (2): pattern + slot -> pattern.
             * (3): Dictionary<string, Dictionary<string, List<string>>> intentPatternSlotQuery 
             */
            Dictionary<string, string> queryIntent = new Dictionary<string, string>();
            LoadQueryIntent(queryIntent, queryIntentManualLabelledFile);

            int slotKeyCol = 4, slotValueCol = 5, queryCol = 0, slotCol = 2;
            StreamReader sr = new StreamReader(infile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = PreProcess(line);
                string[] arr = line.Split('\t');
                if (arr.Length <= slotKeyCol || arr.Length <= slotValueCol || arr.Length <= queryCol || arr.Length <= slotCol)
                {
                    continue;
                }
                string query = arr[queryCol];
                if (!queryIntent.ContainsKey(query))
                {
                    continue;
                }
                string intent = queryIntent[query];
                string slotStr = arr[slotCol];
                string[] slotKeyArr = arr[slotKeyCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] slotValueArr = arr[slotValueCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                slotStr = NormalizationPatternSlot(slotStr, slotKeyArr, slotValueArr);
              //  slotStr = slotStr.Replace("on", "");
              /*  string oldStr = slotStr;
                if (slotStr.EndsWith(" on"))
                    slotStr = slotStr.Substring(0, slotStr.Length - 3);
                if (slotStr.StartsWith("on "))
                    slotStr = slotStr.Substring(3);
                slotStr = slotStr.Replace(" on ", "");*/
                if (!intentPatternQuery.ContainsKey(intent))
                {
                    intentPatternQuery[intent] = new Dictionary<string, List<string>>();
                }
                if (!intentPatternQuery[intent].ContainsKey(slotStr))
                {
                    intentPatternQuery[intent][slotStr] = new List<string>();
                }
                intentPatternQuery[intent][slotStr].Add(query);
            }
            sr.Close();
        }
        public static void ReadPatternQuery(string infile, int qCol, int pCol, int iCol)
        {
            /*
             * Store query set of specified intend + pattern in data structure Dictionary<string, Dictionary<string, List<string>>> intentPatternQuery.
             */
            StreamReader sr = new StreamReader(infile);
            string line, query, pattern, intent;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= qCol || arr.Length <= pCol || arr.Length <= iCol)
                {
                    continue;
                }
                query = arr[qCol];
                pattern = arr[pCol];
                intent = arr[iCol];
                pattern = NormalizePattern(pattern);
                if (!intentPatternQuery.ContainsKey(intent))
                {
                    intentPatternQuery[intent] = new Dictionary<string, List<string>>();
                }
                if (!intentPatternQuery[intent].ContainsKey(pattern))
                {
                    intentPatternQuery[intent][pattern] = new List<string>();
                }
                intentPatternQuery[intent][pattern].Add(query);
            }

            sr.Close();
        }

        public static void ReadPatternQuery(string infile, string intentFlag)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string rawQuery = arr[0];
                string intent = arr[1].Trim();
                if (intent == intentFlag)
                    patternQuerySet.Add(rawQuery);
            }
            sr.Close();
        }

        public static void TopSiteStore(string domainOutFile, string flag)
        {
            /*
             * Store to disk of the url socre of specified intent + slot layer.
             */
            StreamWriter sw = new StreamWriter(domainOutFile);
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, int>>> pair in intentSlotUrlScore)
            {
                string intent = pair.Key;
                if (string.IsNullOrEmpty(intent))
                    continue;
                foreach (KeyValuePair<string, Dictionary<string, int>> pairEle in pair.Value)
                {
                    string slot = pairEle.Key;
                    List<KeyValuePair<string, int>> urlScoreSort = pairEle.Value.ToList();
                    urlScoreSort.Sort(MyCmp);
                    StringBuilder urlComStr = new StringBuilder();
                    foreach (KeyValuePair<string, int> urlScoreEle in urlScoreSort)
                    {
                        if (urlScoreEle.Value > 0)
                        {
                           // urlComStr.Append(string.Format("{0}\t{1}\t{2}\t{3}", intent, slot, urlScoreEle.Key, urlScoreEle.Value));
                            //urlComStr.Append("\n");
                            sw.WriteLine("{0}\t{1}\t{2}\t{3}", intent, slot, urlScoreEle.Key, urlScoreEle.Value);
                        }
                    }
                  //  string BestQueryPattern = "", BestQuery = "", GTopUrls = "", BTopUrls = "", ManuallySelected = "", GTopDomains = "", BTopDomains = "";
                   /* string topUrls = "";
                    if (flag.ToLower().Contains("bing"))
                    {
                        topUrls = urlComStr.ToString().Trim(',');
                        
                    }
                    else if (flag.ToLower().Contains("google"))
                    {
                        topUrls = urlComStr.ToString().Trim(',');                     
                    }*/
                    
                   // string result = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", intent, slot, BestQueryPattern, BestQuery, GTopUrls, GTopDomains, BTopUrls, BTopDomains, ManuallySelected);
                  //  string result = string.Format("{0}\t{1}\t{2}", intent, slot, topUrls);
                    
                  //  sw.WriteLine("{0}",urlComStr.ToString());
                }
            }
            sw.Close();
        }

        public static void RunFlag(string flag, bool domainUrlFlag)
        {

            

            string outfile = @"D:\Project\Election\TokenAndRules\Election";
            if (domainUrlFlag)
            {
                outfile = outfile + flag + "TopDomainUrlScore.tsv";
            }
            else
            {
                outfile = outfile + flag + "TopUrlScore.tsv";
            }

            string scoreFile = @"D:\Project\Election\QuerySet\queyJudge.tsv";

            ReadQueryScore(scoreFile);

            string PatternQueryFile = @"D:\Project\Election\TokenAndRules\electionV1.2FilterViewAndCandidateListIntent.tsv";
            int queryCol = 0, patCol = 1, intentCol = 3;

            string slotValueIdealValueFile = @"D:\Project\Election\TokenAndRules\candidatePartyPoliticalViewMappingDic.tsv";
            LoadSlotIdealSlot(slotValueIdealValueFile); // slot value replace with ideal value.


            string patternSlotQueryFile = @"D:\Project\Election\TokenAndRules\pbxmlParse.tsv";
           // ReadPatternSlotQuery(patternSlotQueryFile, PatternQueryFile); //PatternQueryFile: Get the manual labeled intent of each query.
            Dictionary<string, Dictionary<string, Dictionary<string, int>>> intentSlotQueryFreq = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();
            //ReadPatternSlotFrequencyQuery(patternSlotQueryFile, PatternQueryFile, intentSlotQueryFreq, "CandidateList");
            ReadPatternSlotFrequencyQuery(patternSlotQueryFile, PatternQueryFile, intentSlotQueryFreq, "Candidate", "CandidateGeneral", "ElectionSchedule",  "CandidateNavigational", "ElectionGeneral", "CandidateCampain", "CandidateBio");
            
            string infile = @"D:\Project\Election\TokenAndRules\" + flag + @"ScrapFeaturesTSV.tsv";
            ReadQueryTopUrl(infile, 10); // query, scrap url come from aether experiment : aether://experiments/1f82e2ad-7955-4085-8d60-1bd4c213e578

            TopSiteScoreCompute(flag, domainUrlFlag);

            TopSiteStore(outfile, flag);
        }


        public static void Run(string[] args)
        {
            /*
             * only one row can run once start. As some satic 
             */
            //RunFlag("Bing", true);
           // RunFlag("Bing", false);
           // RunFlag("Google", true);
            RunFlag("Google", false);
        }
    }
}

