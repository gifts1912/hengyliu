using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.TopSite
{
    public class SimSlotMining
    {
        public static Dictionary<string, string> queryIntent = new Dictionary<string, string>();
        public static HashSet<string> stayWords = new HashSet<string>(new string[] { "vice", "female", "male", "black", "white", "marriage", "top", "vs", "and", "or", "history" });
        public static HashSet<string> NeedDetailSlot = new HashSet<string>(new string[] { "[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]" });
        public static Dictionary<string, Dictionary<string, int>> slotWordsTimes = new Dictionary<string, Dictionary<string, int>>();
        public static Dictionary<string, Dictionary<string, List<string>>> intentPatternQuery = new Dictionary<string, Dictionary<string, List<string>>>();
        public static void LoadQueryIntent(string infile, string filterFlag = "no")
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
                if (filterFlag.Equals("no", StringComparison.OrdinalIgnoreCase))
                {
                    queryIntent[query] = arr[intentCol];
                }
                else if (filterFlag.Equals(intent, StringComparison.OrdinalIgnoreCase))
                {
                    queryIntent[query] = arr[intentCol];
                }
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
                    }
                }
                slotPattern.Add(slotValue);
            }
            slotPattern.Sort();
            result = string.Join(" ", slotPattern.ToArray());
            return result;
        }

        public static void ReadPatternSlotQuery(string infile)
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
                if (!queryIntent.ContainsKey(query))
                {
                    continue;
                }
                string intent = queryIntent[query];
                string slotStr = arr[slotCol];
                string[] slotKeyArr = arr[slotKeyCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] slotValueArr = arr[slotValueCol].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                slotStr = NormalizationPatternSlot(slotStr, slotKeyArr, slotValueArr);
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

        public static void GenSpecifiedIntentSlotValueSet(string infile, Dictionary<string, int> slotValueFreq)
        {
              /*
              * (1): Gen specified intent pbxmlParser value. (2): Gen words:times of sim slot name. 
             */
            StreamReader sr = new StreamReader(infile);
            string line, query, slotKey, slotValue;
            int queryCol = 0, slotKeyCol = 4, slotValueCol = 5;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                query = arr[queryCol];
                slotKey = arr[slotKeyCol];
                slotValue = arr[slotValueCol];
                string[] slotKeyArr = slotKey.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] slotValueArr = slotValue.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < slotKeyArr.Length; i++)
                {
                    string slotEle = slotKeyArr[i];
                    string slotValueEle = slotValueArr[i];
                    if (!NeedDetailSlot.Contains(slotEle))
                        continue;
                    if(!slotValueFreq.ContainsKey(slotValueEle))
                    {
                        slotValueFreq[slotValueEle] = 0;
                    }
                    slotValueFreq[slotValueEle] += 1;

                    if (!slotWordsTimes.ContainsKey(slotEle))
                    {
                        slotWordsTimes[slotEle] = new Dictionary<string, int>();
                    }
                    if (!slotWordsTimes[slotEle].ContainsKey(slotValueEle))
                    {
                        slotWordsTimes[slotEle][slotValueEle] = 0;
                    }
                    slotWordsTimes[slotEle][slotValueEle] += 1;
                }
            }
            sr.Close();
        }

        public static double SimBasedOnEditDistance(string wordsA, string wordsB)
        {
            int al = wordsA.Length, bl = wordsB.Length;
            int[,] dis = new int[al + 1, bl + 1];
            if (al == 0)
            {
                return 0.0; //bl;
            }
            if (bl == 0)
                return 0.0; //al;
            for (int i = 0; i <= al; i++)
            {
                dis[i, 0] = i;
            }

            for (int i = 0; i <= bl; i++)
            {
                dis[0, i] = i;
            }

            for (int i = 1; i <= al; i++)
            {
                for (int j = 1; j <= bl; j++)
                {
                    int cost = wordsA[i-1] == wordsB[j-1] ? 0 : 1;
                    dis[i, j] = Math.Min(Math.Min(dis[i-1, j] +1, dis[i, j-1] + 1), dis[i-1, j-1] + cost);
                }
            }

            return 1.0 - Convert.ToDouble(dis[al, bl]) / Math.Max(al, bl) ;
        }
        public static double JacardSim(string wordsA, string wordsB)
        {
            double sim1 = 0.0;
            HashSet<string> hsA = new HashSet<string>(wordsA.Split());
            HashSet<string> hsB = new HashSet<string>(wordsB.Split());
            foreach (string ele in hsA)
            {
                if (hsB.Contains(ele))
                {
                    sim1 += 1;
                }
            }
            hsA.UnionWith(hsB);
            sim1 = sim1 / hsA.Count;
            return sim1;
        }
        public static double SimCompute(string wordsA, string wordsB)
        {
            double sim1 = 0.0, sim2 = 0.0;
            wordsA = Regex.Replace(wordsA, "\\bon\\b", "");
            wordsB = Regex.Replace(wordsB, "\\bon\\b", "");
            wordsA = wordsA.Trim();
            wordsB = wordsB.Trim();
            wordsA = Regex.Replace(wordsA, "\\s+", " ");
            wordsB = Regex.Replace(wordsB, "\\s+", " ");
            sim1 = JacardSim(wordsA, wordsB);        
            sim2 = SimBasedOnEditDistance(wordsA, wordsB);

            return sim2+sim1;
        }
        public static void ComputeSim(Dictionary<string, int> wordsNum, Dictionary<string, Dictionary<string, double>> wordsSimMatrix)
        {
            foreach (KeyValuePair<string, int> pairOut in wordsNum)
            {
                string wordsOut = pairOut.Key;

                if (!wordsSimMatrix.ContainsKey(wordsOut))
                {
                    wordsSimMatrix[wordsOut] = new Dictionary<string, double>();
                }

                double sim = 0.0;
                foreach (KeyValuePair<string, int> pairIn in wordsNum)
                {
                    string wordsIn = pairIn.Key;
                    if (wordsOut.ToLower().Equals(wordsIn, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    sim = SimCompute(wordsOut, wordsIn);
                    wordsSimMatrix[wordsOut][wordsIn] = sim;
                }
            }
        }

        public static int Cmp(KeyValuePair<string, double> pairA, KeyValuePair<string, double> pairB)
        {
            return pairB.Value.CompareTo(pairA.Value);
        }
        public static void ComputeSimSameSlotName(string outfile, Dictionary<string, double> slotConfDic, Dictionary<string, int> slotValueFreq)
        {
            StreamWriter sw = new StreamWriter(outfile);
            foreach (KeyValuePair<string, Dictionary<string, int>> pair in slotWordsTimes)
            {
                string slotName = pair.Key;
                double lowConf = 0.0;
                if(slotConfDic.ContainsKey(slotName))
                {
                    lowConf = slotConfDic[slotName];
                }

                Dictionary<string, Dictionary<string, double>> wordsSimMatrix = new Dictionary<string, Dictionary<string, double>>();
                ComputeSim(pair.Value, wordsSimMatrix);
                foreach(KeyValuePair<string, Dictionary<string, double>> pairSim in wordsSimMatrix)
                {
                    string wordsBase = pairSim.Key ;
                    List<KeyValuePair<string, double>> wordsSimList = pairSim.Value.ToList();
                    wordsSimList.Sort(Cmp);

                    StringBuilder sb = new StringBuilder();
                    //sb.Append(string.Format("{0}\t{1}", slotName, wordsBase));
                    sb.Append(wordsBase);
                    double maxConf = 0.0;
                    string maxConfWord = wordsBase;
                    if(slotValueFreq.ContainsKey(wordsBase))
                    {
                        maxConf = slotValueFreq[wordsBase];
                    }

                    foreach(KeyValuePair<string, double> wordsSimValue in wordsSimList)
                    {
                        double curConf = 0.0;
                        if(slotValueFreq.ContainsKey(wordsSimValue.Key))
                        {
                            curConf = slotValueFreq[wordsSimValue.Key];
                        }
                        if(wordsSimValue.Value >= lowConf)
                        {
                            if(curConf > maxConf)
                            {
                                maxConf = curConf;
                                maxConfWord = wordsSimValue.Key;
                            }
                            //sb.Append(string.Format("\t{0}:{1}", wordsSimValue.Key, wordsSimValue.Value));
                            //num++;
                        }                           
                    }
                    sb.Append(string.Format("\t{0}", maxConfWord));
                    sw.WriteLine(sb.ToString());
                }
            }

            sw.Close();
        }
        public static void Run(string[] args)
        {
         
            string PatternQueryFile = @"D:\Project\Election\TokenAndRules\electionV1.2.tsv";
            string intentFilter = "candidateview"; // "no" will store all intent query set.
            LoadQueryIntent(PatternQueryFile, intentFilter);

            int queryCol = 0, patCol = 1, intentCol = 3;
            string patternSlotQueryFile = @"D:\Project\Election\TokenAndRules\pbxmlParse.tsv";
            Dictionary<string, int> slotValueFrequency = new Dictionary<string, int>();

            GenSpecifiedIntentSlotValueSet(patternSlotQueryFile, slotValueFrequency);//(1): Gen specified intent pbxmlParser value. (2): Gen words:times of sim slot name. 

            string simOutFile = @"D:\Project\Election\TokenAndRules\candidatePartyPoliticalViewMappingDic.tsv";

            Dictionary<string, double> slotConfDic = new Dictionary<string, double> { { "[election.candidate]", 0.5 }, { "[election.candidate.highconf]", 0.5 }, { "[election.bpiissue]", 0.9}, {"[election.party]", 0.8} };
            ComputeSimSameSlotName(simOutFile, slotConfDic, slotValueFrequency); // Gen sim value of slot value that belong to same slot name.

        }
    }
}
