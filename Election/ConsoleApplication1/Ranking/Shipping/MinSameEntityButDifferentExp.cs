using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace Ranking.Shipping.MinSameEntityButDifferentExp
{
    public class MinSameEntityButDifferentExp
    {
        public static Dictionary<string, string> queryIntent = new Dictionary<string, string>();
        public static HashSet<string> stayWords = new HashSet<string>(new string[] { "vice", "female", "male", "black", "white", "marriage", "top", "vs", "and", "or", "history" });
        public static HashSet<string> NeedDetailSlot = new HashSet<string>(new string[] { "[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]" });
        public static Dictionary<string, Dictionary<string, int>> slotWordsTimes = new Dictionary<string, Dictionary<string, int>>();
        public static Dictionary<string, Dictionary<string, List<string>>> intentPatternQuery = new Dictionary<string, Dictionary<string, List<string>>>();
        public static double lowConf = 0.5;
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
                    if(!slotValueFreq.ContainsKey(slotValueEle))
                    {
                        slotValueFreq[slotValueEle] = 0;
                    }
                    slotValueFreq[slotValueEle] += 1;
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
            wordsA = Regex.Replace(wordsA, "\\b(on|in|the|policy|policy of|policies|of|s|S|state|plan|plans|reform|problem|problems|issues|issue|public|right|rights)\\b", "");
            wordsB = Regex.Replace(wordsB, "\\b(on|in|the|polich|policy of|policies|of|s|S|state|plan|plans|reform|problem|problems|issues|issue|public|right|rights)\\b", "");
            wordsA = wordsA.Trim();
            wordsB = wordsB.Trim();
            wordsA = Regex.Replace(wordsA, "\\s+", " ");
            wordsB = Regex.Replace(wordsB, "\\s+", " ");
            if (wordsA.Length <= 2 || wordsB.Length <= 2)
                return 0.0;
            sim1 = JacardSim(wordsA, wordsB);        
            sim2 = SimBasedOnEditDistance(wordsA, wordsB);

            return sim2+sim1;
        }
        public static void ComputeSim(List<string> wordsNum, Dictionary<string, Dictionary<string, double>> wordsSimMatrix)
        {
            /*
             * Comput each pair similarity of the same slot name. And store in wordsSimMatrix
             */
            foreach (string wordsOut in wordsNum)
            {
                if (!wordsSimMatrix.ContainsKey(wordsOut))
                {
                    wordsSimMatrix[wordsOut] = new Dictionary<string, double>();
                }

                double sim = 0.0;
                foreach (string wordsIn in wordsNum)
                {
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
        
        public static void ComputeSimSameSlotName(Dictionary<string, List<string>> slotValueList, Dictionary<string, int> slotValueFreq, Dictionary<string, double> slotConfDic, string simOutFile, Dictionary<string, List<string>> idealSlotSlotList)
        {
            StreamWriter sw = new StreamWriter(simOutFile);
            foreach (KeyValuePair<string, List<string>> pair in slotValueList)
            {
                string slotName = pair.Key; // such as election.candidate.highconf
                
                if(slotConfDic.ContainsKey(slotName)) // slot key name like : election.candidate.highconf
                {
                    lowConf = slotConfDic[slotName]; 
                }

                Dictionary<string, Dictionary<string, double>> wordsSimMatrix = new Dictionary<string, Dictionary<string, double>>(); // stay the every pair words sim value of the same slot key.
                ComputeSim(pair.Value, wordsSimMatrix); //  pair.Value : words that belown to the same slot key name. Such as each similarity of pair in election.candidate.highconf

                Dictionary<string, string> slotMapSlot = new Dictionary<string, string>();
                foreach(KeyValuePair<string, Dictionary<string, double>> pairSim in wordsSimMatrix) // wordsSimMatrix stay the similarity of two pair in the same slot name.
                {
                    string wordsBase = pairSim.Key ;  // pariSim.Value saty the similarity words and similarity value of the wordsBase.
                    List<KeyValuePair<string, double>> wordsSimList = pairSim.Value.ToList(); // the words and response similairy value with wordsBase                
                    wordsSimList.Sort(Cmp);

                    StringBuilder sb = new StringBuilder();
                    sb.Append(wordsBase);
                    double maxConf = 0.0;
                    string maxConfWord = wordsBase;
                    if(slotValueFreq.ContainsKey(wordsBase))
                    {
                        maxConf = slotValueFreq[wordsBase]; //wordsBase's frequency 
                    }

                    int satNum = 0;
                    foreach(KeyValuePair<string, double> wordsSimValue in wordsSimList)
                    {
                        double curConf = 0.0;
                        if(slotValueFreq.ContainsKey(wordsSimValue.Key))
                        {
                            curConf = slotValueFreq[wordsSimValue.Key]; // current word's frequency
                        }
                        if(wordsSimValue.Value >= lowConf) //stay the similarity words that larger than similarity threadhold.
                        {
                            if(curConf > maxConf)
                            {
                                satNum++;
                                maxConf = curConf;
                                maxConfWord = wordsSimValue.Key; // catch the word that have the max frequency that belong to the same slot name.
                            }
                        }                           
                    }
                    if (satNum <= 1 && maxConfWord == wordsBase)
                        continue;
                   
                    sb.Append(string.Format("\t{0}", maxConfWord)); // sb stay each words and it's ideal expression .
                    sw.WriteLine(sb.ToString());
                }
                /*
                foreach(KeyValuePair<string, string> pairCur in slotMapSlot)
                {
                    string x = pairCur.Key, y = pairCur.Value;
                    y = FinallyIdeal(y, slotMapSlot);
                    if(!idealSlotSlotList.ContainsKey(y))
                    {
                        idealSlotSlotList[y] = new List<string>();
                    }
                    idealSlotSlotList[y].Add(x);
                    sw.WriteLine("{0}\t{1}", x, y);
                }
                 */
            }

            sw.Close();
        }

        public static string FinallyIdeal(string x, Dictionary<string, string> valueIdeal)
        {
            string y = x;
            int maxNum = 3, n = 0;
            while(true)
            {
                if (n++ >= maxNum)
                    break;
                if (!valueIdeal.ContainsKey(x))
                {
                    break;
                }
                y = valueIdeal[x];
                if (x == y)
                {
                    break;
                }
                x = y;         
            }
            return y;
        }
        public static bool IsIllegal(string value)
        {
            string[] arr = value.Split();
            if (arr.Length >= 4)
                return true;
            foreach(string ele in arr)
            {
                if(ele.Length >= 15)
                {
                    return true;
                }
            }
            return false;
        }

        public static string  StopWordsDelete(string value)
        {
            string result = value;
         //   result = Regex.Replace(value, "\\b(on|in|the|policy|policy of|policies|of|s|S|state|plan|plans|reform|problem|problems|issues|issue|public)\\b", "");
            result = Regex.Replace(value, "\\b(on|in|the|of|s|S|state)\\b", "");
            result = Regex.Replace(result, "\\s+", " ");
            result = result.Trim();
            return result;
        }
        public static void  ReadElectionTokens(string tokenfile, Dictionary<string, List<string>> tokenValues, HashSet<string> needDetailSlot)
        {
            using (StreamReader sr = new StreamReader(tokenfile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    if (arr.Length != 2)
                        continue;
                    string value = arr[0];
                    
                    value = value.Substring("qpv2tkn-".Length);
                    /*if (value == "dr ben carsons")
                    {
                        Console.WriteLine(value);
                        Console.ReadKey();
                    }*/
                    if (IsIllegal(value))
                        continue;
                    value = StopWordsDelete(value);
                    string key = arr[1].Split(';')[0].Trim(new char[] { '<', '>' });
                    if (!key.Contains('.') || !needDetailSlot.Contains(key))
                    {
                        continue;
                    }
                    if (!tokenValues.ContainsKey(key))
                    {
                        tokenValues[key] = new List<string>();
                    }
                    tokenValues[key].Add(value);
                }
            }
        }

        public static void StoreValueListDic(Dictionary<string, List<string>> tokenValueList)
        {
            StreamWriter sw = new StreamWriter(@"D:\demo\watch.tsv");
            foreach(KeyValuePair<string, List<string>> pair in tokenValueList)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, string.Join("\t", pair.Value));
            }
            sw.Close();
        }

        public static void FilterSlotBoundChar(HashSet<string> needDetailSlot)
        {
            foreach(string ele in Utility.Utility.DefaultNeedDetailSlot)
            {
                string slot = ele.Trim(new char[] { '[', ']' });
                needDetailSlot.Add(slot);
            }
        }

        public static void StoreSameSlot(Dictionary<string, List<string>> idealSlotValueList, string idealSlotFile)
        {
            StreamWriter sw = new StreamWriter(idealSlotFile);
            foreach(KeyValuePair<string, List<string>> pair in idealSlotValueList)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, string.Join("\t", pair.Value.ToArray()));
            }
            sw.Close();
        }

        public static void FilterLessFrequencyMakeFinalMap(string rawFile, string filterFile)
        {
            Dictionary<string, string> slotIdealSlot = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(rawFile);
            string line;
            Dictionary<string, HashSet<string>> idealSlotNormalList = new Dictionary<string, HashSet<string>>();
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                if(!idealSlotNormalList.ContainsKey(arr[1]))
                {
                    idealSlotNormalList[arr[1]] = new HashSet<string>();
                }
                idealSlotNormalList[arr[1]].Add(arr[0]);
                slotIdealSlot[arr[0]] = arr[1];
            }
            sr.Close();

            int numThread = 2;
            StreamWriter sw = new StreamWriter(filterFile);
            HashSet<string> slotMap = new HashSet<string>();
            foreach(KeyValuePair<string, HashSet<string>> pair in idealSlotNormalList)
            {
                string y = pair.Key; // represent the ideal expression of the slot
                y = FinallyIdeal(y, slotIdealSlot);
                if (pair.Value.Count() <= numThread && y != "republican" && y != "democratic")
                    continue;
                foreach(string ele in pair.Value)
                {
                    slotMap.Add(string.Format("{0}\t{1}", ele, y));
                }
            }
            
            foreach(string ele in slotMap)
            {
                sw.WriteLine(ele);
            }
            sw.Close();
        }
        public static void Run(string[] args)
        {
            
            string electionTokensFile = @"D:\Project\Election\TokenAndRules\ElectionTokens.tsv";
            Dictionary<string, List<string>> slotValueList = new Dictionary<string, List<string>>();
            HashSet<string> needDetailSlot = new HashSet<string>(); //Utility.Utility.DefaultNeedDetailSlot;
            FilterSlotBoundChar(needDetailSlot); //Solve the problem that needDetialSlot begin with "[" and ElectionTokens starts with "<"
            needDetailSlot = new HashSet<string>(new string[] { "election.bpiissue" });
            needDetailSlot.Remove("location.state");
            ReadElectionTokens(electionTokensFile, slotValueList, needDetailSlot); // Read entity slot and response values.

            string patternSlotQueryFile = @"D:\Project\Election\TokenAndRules\pbxmlParse.tsv";
            Dictionary<string, int> slotValueFrequency = new Dictionary<string, int>();
            GenSpecifiedIntentSlotValueSet(patternSlotQueryFile, slotValueFrequency); //(1): Gen specified intent pbxmlParser value. (2): Gen words:times of sim slot name. 

            string simOutFile = @"D:\Project\Election\TokenAndRules\candidatePartyPoliticalViewMappingDic.tsv";
            Dictionary<string, double> slotConfDic = new Dictionary<string, double> { { "election.candidate", 0.5 }, { "election.candidate.highconf", 0.5 }, { "election.bpiissue", 0.5}, {"election.party", 0.8} };

            Dictionary<string, List<string>> idealSlotValueList = new Dictionary<string, List<string>>();
            ComputeSimSameSlotName(slotValueList, slotValueFrequency, slotConfDic, simOutFile, idealSlotValueList); // Gen sim value of slot value that belong to same slot name.

            // Manually reform simOutFile and then run the following function  
            string simOutFileTurn = @"D:\Project\Election\TokenAndRules\candidatePartyPoliticalViewMappingDicTurn.tsv";
            string slotIdealSlotFile = @"D:\Project\Election\TokenAndRules\slotIdealSlot.tsv";
            FilterLessFrequencyMakeFinalMap(simOutFileTurn, slotIdealSlotFile);
        }
    }
}