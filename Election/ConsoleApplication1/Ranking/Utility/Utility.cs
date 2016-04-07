using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.Utility
{
    class Utility
    {
        public static HashSet<string> DefaultNeedDetailSlot = new HashSet<string>(new string[] { "[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]", "[election.timelineword]", "[location.state]"}); //"[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]", "[location.state]" });
        public static HashSet<string> DefaultNeedSlot = new HashSet<string>(new string[] {"[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]", "[election.Gender.Female]", "[election.howmany]", "[election.when]", "[election.timelineword]", "[election.Speech]", "[election.timelineword]", "[location.state]" });//{ "[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]", "[location.state]" });
        public static HashSet<string> stayWords = new HashSet<string>(new string[] { "woman","women","vice", "female", "male", "black", "white", "marriage", "top", "vs", "and", "or", "history" });
        public static Dictionary<string, HashSet<string>> intentNeedStaySlot= new Dictionary<string, HashSet<string>> {{"CandidateList", new HashSet<string> (new string[] {"[election.party]", "[location.state]", "[election.national]",
        "[location.state]", "[election.candidate.highconf]", "[election.newsword]", "[election.candidate]",
        "[election.primary]", "[election.voting]", "[election.campaign]", "[election.next]", 
        "[election.winlose]", "[election.Gender.Female]", "[election.howmany]", "[election.when]", "[election.timelineword]", "[election.Speech]", "[election.timelineword]" })}};
        
        public static Dictionary<string, HashSet<string>> slotMapSlotsDic = new Dictionary<string, HashSet<string>> { { "E", new HashSet<string>(new string[] { "StayWords", "election.party", "location.state", "election.national", "election.winlose", "election.candidate.highconf" }) }, { "I", new HashSet<string>() { "election.candidateword", "election.newsword", "election.voting" } }, { "C", new HashSet<string>() { "election.campaign", "election.race", "election.primary", "election.presidential" } } };  // { { "election.party", "election.party=E0" }, { "election.primary", "election.primary=E1" }, { "location.state", "location.state=C0" }, { "election.national", "" } };

        public static string SlotMappingGen(List<string> slotMappingList)
        {
            int ENum = 0, INum = 0, CNum = 0;
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<string, HashSet<string>> pair in slotMapSlotsDic)
            {
                string slotFlag = pair.Key;
                int num = 0, maxNum = 0;
                switch(slotFlag)
                {
                    case "E":
                        maxNum = 4;
                        break;
                    case "I":
                        maxNum = 1;
                        break;
                    case "C":
                        maxNum = 2;
                        break;                 
                }
                foreach(string ele in pair.Value)
                {
                    if(slotMappingList.Contains(ele))
                    {
                        if (num >= maxNum)
                            break;
                        sb.Append(string.Format("{0}={1}{2};", ele, slotFlag, num));
                        num++;
                    }
                }
            }

            return sb.ToString().Trim(new char[] {';'});
        }

        public static string SlotQueryNormalizationGen(string[] slotKeyArr, string[] slotValueArr, string intentFlag, Dictionary<string, string> slotIdealSlot, ref string slotMapping, ref string matchingConditionId)
        {   /*
             * Genrage the slot query like "[election.date]:2016||| [election.presidential]:presidential|||[election.election]:election|||[articles]:articles" 
             * from slotKeyArr "[election.date]|||[election.presidential]|||[election.election]|||[articles]" and SlotValueArr "2016|||presidential|||election|||articles"
             * Detail Process: (1): Delete boundary flag. (2): Filter the specified words. (3): Ideal the slot value.
             */
            
            string result = "";
            List<string> slotPattern = new List<string>();
            List<string> slotMappingList = new List<string>();

            HashSet<string> NeedStaySlot = new HashSet<string>();
            if(intentNeedStaySlot.ContainsKey(intentFlag))
            {
                NeedStaySlot = intentNeedStaySlot[intentFlag];
            }
            else
            {
                NeedStaySlot = DefaultNeedSlot;
            }
            // string[] slotArr = slotStr.Split();

            for (int i = 0; i < slotKeyArr.Length; i++)
            {
                string ele = slotKeyArr[i];
                string slotValue = slotValueArr[i];
                if (!ele.Contains("."))
                {
                    ele = ele.Trim(new char[] { '[', ']' });
                    if(stayWords.Contains(ele))
                    {
                        slotPattern.Add(string.Format("StayWords:{0}", ele));
                        slotMappingList.Add("StayWords");
                    }
                    continue;
                }
                if(NeedStaySlot.Contains(ele))
                {
                    if (slotIdealSlot.ContainsKey(slotValue))
                    {
                        slotValue = slotIdealSlot[slotValue];
                    }
                    
                    ele = ele.Trim(new char[] { ']', '[' });
                    slotPattern.Add(string.Format("{0}:{1}", ele, slotValue));
                    slotMappingList.Add(ele);
                }         
            }
            result = string.Join("|||", slotPattern.ToArray());
            
            string sm = "";

            slotMappingList.Sort();
            slotMapping = SlotMappingGen(slotMappingList);

            /*
            slotMapping = string.Join("|||", slotMappingList.ToArray());
            if (slotMapping == "election.candidateword|||election.presidential")
            {
                slotMapping = "election.candidateword=I0;election.presidential=C0"; 
            }
            else if(slotMapping == "election.candidateword|||election.party|||election.presidential")
            {
                slotMapping = "election.candidateword=I0;election.party=E0;election.presidential=C0";
                matchingConditionId = "2";
            }
            else if(slotMapping == "election.candidateword|||election.party")
            {
                slotMapping = "election.candidateword=I0;election.party=E0";
                matchingConditionId = "2";
            }
            else if(slotMapping == "election.presidential|||election.race")
            {
                slotMapping = "election.presidential=C0;election.race=I0";
            }
            else if(slotMapping == "election.party|||election.presidential|||election.race")
            {
                slotMapping = "election.party=E0;election.presidential=C0;election.race=I0";
                matchingConditionId = "4";
            }
            else if(slotMapping == "election.election|||election.primary|||location.state")
            {
                slotMapping = "election.election=I0;election.primary=C0;location.state=E0";
                matchingConditionId = "4";
            }
            else if(slotMapping == "election.candidateword|||election.national|||election.presidential")
            {
                slotMapping = "election.candidateword=I0;election.national=E0;election.presidential=C0";
                matchingConditionId = "2";
            }
            else if(slotMapping == "election.presidential")
            {
                slotMapping = "election.presidential=I0";
            }
            else if(slotMapping.Contains("election.party"))
            {
                sm = sm + "election.party=E0;";
                matchingConditionId = "2";
            }
            else if(slotMapping.Contains("location.state"))
            {
                sm = sm + "location.state=E1;";
                matchingConditionId = "2";
            }
            else if(slotMapping.Contains("election.winlose"))
            {
                sm = sm + "election.winlose=I0;";
            }
            else if(slotMapping.Contains("election.voting"))
            {
                sm = sm+ "election.voting=I0;";
            }
            if(slotMapping.Contains("|||"))
            {
                sm = sm.Trim(new char[] { ';' });
                if (string.IsNullOrEmpty(sm))
                    sm = "election.candidateword=I0";
                slotMapping = sm;
            }
            */
            return result;

        }
        public static string NormalizationPatternSlot(string slotStr, string[] slotKeyArr, string[] slotValueArr, string intentFlag, Dictionary<string, string> slotIdealSlot)
        {/*
          * replace the slot name with value;  delete words that not slotted; sort to filter the influence of sort. 
          */
            HashSet<string> needStaySlot = new HashSet<string>();
            if(intentNeedStaySlot.ContainsKey(intentFlag))
            {
                needStaySlot = intentNeedStaySlot[intentFlag];
            }
            else
            {
                needStaySlot = DefaultNeedSlot;
            }

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
                if (needStaySlot.Contains(ele))
                {
                    if(DefaultNeedDetailSlot.Contains(ele))
                    {
                        int pos = Array.IndexOf(slotKeyArr, ele);
                        if (pos != -1)
                        {
                            slotValue = slotValueArr[pos];
                            if (slotIdealSlot.ContainsKey(slotValue))
                            {
                                slotValue = slotIdealSlot[slotValue];
                            }
                            if (slotValue.StartsWith("on "))
                            {
                                slotValue = slotValue.Substring(3);
                            }
                            if (slotValue.EndsWith(" on"))
                            {
                                slotValue = slotValue.Substring(0, slotValue.Length - 3);
                            }
                            slotValue = slotValue.Replace(" on ", " ");
                        }
                    }                   
                    slotPattern.Add(slotValue);
                }

            }
            slotPattern.Sort();
            result = string.Join(" ", slotPattern.ToArray());
            return result;
        }

        public static string NormalizationPatternSlot(string slotStr, string intent)
        {/*
          * replace the slot name with value;  delete words that not slotted; sort to filter the influence of sort. 
          */
            HashSet<string> needStaySlot = new HashSet<string>();
            if(intentNeedStaySlot.ContainsKey(intent))
            {
                needStaySlot = intentNeedStaySlot[intent];
            }
            else
            {
                needStaySlot = DefaultNeedDetailSlot;
            }
           // Console.WriteLine("{0}", needStaySlot.Count());
            //Console.ReadKey();
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
                else
                {
                    if(needStaySlot.Contains(ele))
                    {
                        slotPattern.Add(ele);
                    }
                   // slotPattern.Add(ele);
                }
            }
            slotPattern.Sort();
            result = string.Join(" ", slotPattern.ToArray());
           // result = string.Format("{0}_{1}", result, slotStr);
            return result;
        }


        public static int CmpValue(KeyValuePair<string, int> pairA, KeyValuePair<string, int> pairB)
        {
            return pairB.Value.CompareTo(pairA.Value);
        }
        public static List<KeyValuePair<string, int>> SortByValue(Dictionary<string, int> dic)
        {
            List<KeyValuePair<string, int>> dicList = dic.ToList();
            dicList.Sort(CmpValue);
            return dicList;
        }


        public static void StaticSlot(string infile, string outfile)
        {
            Dictionary<string, int> slotCountDic = new Dictionary<string, int>();

            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Trim().Split(new string[] {"|||"}, StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0; i < arr.Length; i++)
                {
                    string slotKeyValue = arr[i];
                    int pos = slotKeyValue.IndexOf(":");
                    if (pos == -1)
                        continue;
                    string slotKey = slotKeyValue.Substring(0, pos);
                    string slotValue = slotKeyValue.Substring(pos + 1);
                    if(!slotCountDic.ContainsKey(slotKey))
                    {
                        slotCountDic[slotKey] = 0;
                    }
                    slotCountDic[slotKey] += 1;
                }
            }

            List<KeyValuePair<string, int>> slotCountList = slotCountDic.ToList();
            slotCountList.Sort(CmpValue);
            foreach(KeyValuePair<string, int> pair in slotCountList)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();
            sr.Close();
            
        }
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\slotQuery.tsv";
                args[1] = @"D:\demo\output.tsv";
            }
            string infile = args[0];
            string outfile = args[1];

            StaticSlot(infile, outfile);
        }
    }
}
