using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.QU
{
    public class QuerySlotFormat
    {
         public static int QueryCol = 0, UrlCol = 1, SortPCol = 3;
        public static Dictionary<string, int> urlScoreDic = new Dictionary<string, int>();
        public static Dictionary<string, int> urlDomainScoreDic = new Dictionary<string, int>();
        public static Dictionary<string, int> queryScoreDic = new Dictionary<string, int>();
        public static HashSet<string> patternQuerySet = new HashSet<string>();
        public static HashSet<string> stayWords = new HashSet<string>(new string[] { "vice", "female", "male", "black", "white", "marriage", "top", "vs", "and", "or", "history" });
        public static Dictionary<string, Dictionary<string, List<string>>> intentPatternQuery = new Dictionary<string, Dictionary<string, List<string>>>();
        // public static Dictionary<string, Dictionary<string, List<string>>> intentPatternSlotQuery = new Dictionary<string, Dictionary<string, List<string>>>();
        public static Dictionary<string, List<string>> queryUrlList = new Dictionary<string, List<string>>();
        public static Dictionary<string, Dictionary<string, Dictionary<string, int>>> intentSlotUrlScore = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();
        public static HashSet<string> NeedDetailSlot = new HashSet<string>(new string[] { "[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]", "[location.state]" });
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

            StreamReader sr = new StreamReader(infile);
            string line;
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
                        int score = 0;
                        if (!queryScoreDic.ContainsKey(query))
                            continue;
                        score = queryScoreDic[query];
                        if (flag.ToLower().Contains("google"))
                        {
                            score = score <= 0 ? 1 : -1;
                        }
                        else
                        {
                            score = (score >= 0 ? 1 : -1);
                        }

                        if (!queryUrlList.ContainsKey(query))
                            continue;
                        List<string> urlList = queryUrlList[query];
                        foreach (string url in urlList)
                        {
                            string urlDomain = url;
                            if (topDomainFlag)
                            {
                                urlDomain = GenUrlDomain(url, rgx);
                            }

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
                else if(stayWords.Contains(ele))
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
                    }
                    slotPattern.Add(slotValue);
                }
                
            }
            slotPattern.Sort();
            result = string.Join(" ", slotPattern.ToArray());
            return result;
        }

        public static string SlotQueryNormalizationGen(string [] slotKeyArr, string [] slotValueArr)
        {   /*
             * Genrage the slot query like "[election.date]:2016||| [election.presidential]:presidential|||[election.election]:election|||[articles]:articles" 
             * from slotKeyArr "[election.date]|||[election.presidential]|||[election.election]|||[articles]" and SlotValueArr "2016|||presidential|||election|||articles"
             * Detail Process: (1): Delete boundary flag. (2): Filter the specified words. (3): Ideal the slot value.
             */
            string result = "";
            List<string> slotPattern = new List<string>();
          // string[] slotArr = slotStr.Split();

            for (int i = 0; i < slotKeyArr.Length; i++)
            {
                string ele = slotKeyArr[i];
                string slotValue = slotValueArr[i];
                if(!ele.Contains("."))
                {
                    continue;
                }
                if (NeedDetailSlot.Contains(ele))
                {
                        if (slotIdealSlot.ContainsKey(slotValue))
                        {
                            slotValue = slotIdealSlot[slotValue];
                        }               
                }
                ele = ele.Trim(new char[] { ']', '[' });
                slotPattern.Add(string.Format("{0}:{1}", ele, slotValue));
            }
            
            result = string.Join("|||", slotPattern.ToArray());
            return result;
            
        }
        public static string SlotQueryGen(string [] slotKeyArr, string [] slotValueArr)
        {
            /*
             * Genrage the slot query like "[election.date]:2016||| [election.presidential]:presidential|||[election.election]:election|||[articles]:articles" 
             * from slotKeyArr "[election.date]|||[election.presidential]|||[election.election]|||[articles]" and SlotValueArr "2016|||presidential|||election|||articles"
             */
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < slotKeyArr.Length; i++ )
            {
                sb.Append(string.Format("{0}:{1}", slotKeyArr[i], slotValueArr[i]));
                sb.Append("|||");
            }
            string result = sb.ToString();
            result = result.EndsWith("|||") ? result.Substring(0, result.Length - 3) : result;
            return result;
         }
         public static void ReadPatternSlotQuery(string infile, string queryIntentManualLabelledFile)
         {
             /*
              * Read pbxml file result, and slot query list of specsified "intent+patternSlot".
              * (1): Map query to intent. Map<query, intent>;
              * (2): pattern + slot -> pattern.
              * (3): Dictionary<string, Dictionary<string, List<string>>> intentPatternSlotQuery 
              */
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
              
                string slotStr = arr[slotCol];
                string[] slotKeyArr = arr[slotKeyCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] slotValueArr = arr[slotValueCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                slotStr = NormalizePattern(slotStr);

                string slotQuery = SlotQueryGen(slotKeyArr, slotValueArr);

                string result = string.Format("{0}\t{1}\t{2}", query, slotStr, slotQuery);
                
            }
            sr.Close();
        }

         public static string EntitySlotMappingGen(string slotQuery)
         {
             string result = "election.candidate=E0";
             string[] arr = slotQuery.Split(new string [] {"|||"}, StringSplitOptions.RemoveEmptyEntries);
             return result;
         }
        public static void RankingFormat(string infile, string outfile)
        {
            /*
             * Change original infomation to this format: "10 republican candidates 2016	[election.candidateword] [election.date] [election.party]	[10]:10|||[election.party]:republican|||[election.candidateword]:candidates|||[election.date]:2016"
             * 
             */
            int slotKeyCol = 4, slotValueCol = 5, queryCol = 0, slotCol = 2;
            string QueryPatternGroupId = "31", QueryForMatching , QueryForScraping,  MatchingConditionId = "3", EntitySlotMapping = "election.candidate=E0";
            int QueryId = 1000;

            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);

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

                string slotStr = arr[slotCol];
                string[] slotKeyArr = arr[slotKeyCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] slotValueArr = arr[slotValueCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                //slotStr = NormalizePattern(slotStr);
                slotStr = NormalizationPatternSlot(slotStr, slotKeyArr, slotValueArr);

               // string slotQuery = SlotQueryGen(slotKeyArr, slotValueArr);
                string slotQuery = SlotQueryNormalizationGen(slotKeyArr, slotValueArr);
                EntitySlotMapping = EntitySlotMappingGen(slotQuery);

                QueryForMatching = query;
                QueryForScraping = query;
                
                string result = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", query, slotStr,QueryPatternGroupId, QueryForMatching,QueryForScraping,QueryId,slotQuery, MatchingConditionId, EntitySlotMapping);
                QueryId += 1;
                sw.WriteLine("{0}", result);
            }
            sr.Close();
            sw.Close();
        }

        public static void ReadIdealSlotExpress(string idealSlotExpressInfile)
        {
            using (StreamReader sr =new StreamReader(idealSlotExpressInfile))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    if (arr.Length != 2)
                        continue;
                    slotIdealSlot[arr[0]] = arr[1];
                }
            }
        }
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\Project\Election\TokenAndRules\pbxmlParse.tsv";
                args[1] = @"D:\Project\Election\TokenAndRules\candidatePartyPoliticalViewMappingDic.tsv";
                args[2] = @"D:\Project\Election\TokenAndRules\pbxmlParserRankingFormat.tsv";
            }
            string infile = args[0];
            string idealSotFile = args[1];
            string outfile = args[2];

            ReadIdealSlotExpress(idealSotFile);
            
            RankingFormat(infile, outfile);
        }
    }
}
