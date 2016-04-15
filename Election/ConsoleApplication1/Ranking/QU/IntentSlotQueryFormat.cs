using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.QU
{
    public class IntentSlotQueryFormat
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

    
        public static void RankingFormat(string infile, string outfile, Dictionary<string,string> queryIntent)
        {
            /*
             * Change original infomation to this format: "10 republican candidates 2016	[election.candidateword] [election.date] [election.party]	[10]:10|||[election.party]:republican|||[election.candidateword]:candidates|||[election.date]:2016"
             * 
             */
            int slotKeyCol = 4, slotValueCol = 5, queryCol = 0, slotCol = 2;
            string QueryPatternGroupId = "31", QueryForMatching , QueryForScraping,  MatchingConditionId = "3", EntitySlotMapping = "";
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
                if(!queryIntent.ContainsKey(query))
                {
                    continue;
                }
                string slotStr = arr[slotCol];
                string[] slotKeyArr = arr[slotKeyCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] slotValueArr = arr[slotValueCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                //slotStr = NormalizePattern(slotStr);
                string intentFlag = queryIntent[query];
                slotStr = Utility.Utility.NormalizationPatternSlot(slotStr, slotKeyArr, slotValueArr, intentFlag, slotIdealSlot);

               // string slotQuery = SlotQueryGen(slotKeyArr, slotValueArr);

                string slotQuery = Utility.Utility.SlotQueryNormalizationGen(slotKeyArr, slotValueArr, intentFlag, slotIdealSlot, ref EntitySlotMapping, ref MatchingConditionId);

                //EntitySlotMapping = EntitySlotMappingGen(slotKeyArr, slotValueArr);

                QueryForMatching = query;
                QueryForScraping = query;

                string result = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}", query, slotStr, QueryPatternGroupId, QueryForMatching, QueryForScraping, QueryId, slotQuery, MatchingConditionId, EntitySlotMapping, intentFlag);
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

        public static void LoadQueryIntent(string infile, Dictionary<string, string> queryIntent)
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
                string intent = arr[intentCol];
                queryIntent[query] = intent;
            }
            sr.Close();
        }


        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\Project\Election\TokenAndRules\pbxmlParse.tsv";
                args[1] = @"D:\Project\Election\TokenAndRules\candidatePartyPoliticalViewMappingDic.tsv";
                args[2] = @"D:\Project\Election\TokenAndRules\pbxmlParseRankingFormat.tsv";
                args[3] = @"D:\Project\Election\TokenAndRules\electionv1.3.tsv";
                
            }
            string infile = args[0];
            string idealSotFile = args[1];
            string outfile = args[2];
            string PatternQueryFile = args[3];

            int queryCol = 0, patCol = 1, intentCol = 3;

            //HashSet<string> querySet = new HashSet<string>();
            Dictionary<string, string> queryIntent = new Dictionary<string, string>();
            LoadQueryIntent(PatternQueryFile, queryIntent);
            ReadIdealSlotExpress(idealSotFile);          
            RankingFormat(infile, outfile, queryIntent);
        }
    }
}
