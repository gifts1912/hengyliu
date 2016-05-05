using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace QAS.PatternEngine
{
    class PatternEngineFormat
    {
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[7];
                args[0] = @"D:\Project\Election\TokenAndRules\ElectionTopSiteListFinally.tsv";
                args[1] = @"D:\Project\Election\TokenAndRules\ElectionTopSiteListFinallyFormat.tsv";
                args[2] = @"D:\Project\Election\TokenAndRules\IntentSlotPatternIdexFile.tsv";
                args[3] = @"D:\Project\Election\TokenAndRules\candidatePartyPoliticalViewMappingDicOriginal.tsv";
                args[4] = @"D:\demo\ElectionTokens.tsv";
                args[5] = @"D:\demo\IdealSlotToDophineFile.tsv";
                args[6] = @"D:\Project\Election\TokenAndRules\ElectionTopSiteListIndexASKey.tsv";
            }
            string topListFile = args[0];
            string formatTopListFile = args[1];
            string intentSlotPatternIdexFile = args[2];
            string slotIdealSlotFile = args[3];
            string electionTokensFile = args[4];
            string slotIdealToDophineFile = args[5];
            string indexTopSiteFile = args[6];
            FormatTopList(topListFile, formatTopListFile);

            //Generate the dictionary that can mapping ideal slot to expression from 'election tokens';
            Dictionary<string, string> idealSlotToDophine = new Dictionary<string, string>();
            IdealSlotToDophine(slotIdealSlotFile, electionTokensFile, idealSlotToDophine, slotIdealToDophineFile);

            IntentSlotPatternIdex(topListFile, intentSlotPatternIdexFile, slotIdealToDophineFile);

            GenIndexTopUrlList(topListFile, intentSlotPatternIdexFile, indexTopSiteFile);
        }

        public static void GenIndexTopUrlList(string topSiteFile, string slotPatToIdxFile, string indexTopSiteFile)
        {
            Dictionary<string, string> intPatIndex = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(slotPatToIdxFile);
            string line;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 4)
                    continue;
                string key = string.Format("{0}\t{1}", arr[0], arr[1]);
                intPatIndex[key] = arr[3];
            }
            sr.Close();

            Dictionary<string, Dictionary<string, double>> indexUrlsScore = new Dictionary<string, Dictionary<string, double>>();
            sr = new StreamReader(topSiteFile);
            string url, intent, slotPat;
            double score;
            int intCol = 0, patCol = 1, urlCol = 2, scoreCol = 4;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                url = arr[urlCol];
                slotPat = arr[patCol];
                intent = arr[intCol];
                score = double.Parse(arr[scoreCol]);
                string key = string.Format("{0}\t{1}", intent, slotPat);
                if (!intPatIndex.ContainsKey(key))
                {
                    continue;
                }
                string index = intPatIndex[key];
                if (!indexUrlsScore.ContainsKey(index))
                {
                    indexUrlsScore[index] = new Dictionary<string, double>();
                }
                if (!indexUrlsScore[index].ContainsKey(url))
                {
                    indexUrlsScore[index][url] = score;
                }
                else
                {
                    indexUrlsScore[index][url] = (indexUrlsScore[key][url] + score) / 2.0;
                }
            }
            sr.Close();


            StreamWriter sw = new StreamWriter(indexTopSiteFile);
            foreach (KeyValuePair<string, Dictionary<string, double>> pair in indexUrlsScore)
            {
                List<KeyValuePair<string, double>> urlScoreSort = pair.Value.ToList();
                string index = pair.Key;
                urlScoreSort.Sort(CmpByValue);
                foreach (KeyValuePair<string, double> urlScorePair in urlScoreSort)
                {
                    int scoreTmp = (int)urlScorePair.Value;
                    sw.WriteLine("{0}\t{1}\t{2}", index, urlScorePair.Key, scoreTmp);
                }
            }
            sw.Close();
        }

        public static int CmpByValue(KeyValuePair<string, double> pairA, KeyValuePair<string, double> pairB)
        {
            return pairB.Value.CompareTo(pairA.Value);
        }
        public static void IdealSlotToDophine(string slotIdealSlotFile, string electionTokensFile, Dictionary<string, string> idealSlotToDophine, string outfile)
        {
            Dictionary<string, HashSet<string>> idealSlotExpHs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, string> valueToToken = new Dictionary<string, string>();

            StreamReader sr = new StreamReader(slotIdealSlotFile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                string value = arr[0], slot = arr[1];
                if (!idealSlotExpHs.ContainsKey(slot))
                {
                    idealSlotExpHs[slot] = new HashSet<string>();
                }
                idealSlotExpHs[slot].Add(value);
            }
            sr.Close();

            sr = new StreamReader(electionTokensFile);
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                string value = arr[0], slot = arr[1];
                value = value.Substring("qpv2tkn-".Length).Trim();
                slot = slot.Split(';')[1].Trim();
                if (string.IsNullOrWhiteSpace(slot))
                    continue;
                valueToToken[value] = slot;
            }
            sr.Close();

            foreach (KeyValuePair<string, HashSet<string>> idealSlotToVauesEle in idealSlotExpHs)
            {
                string idealSlot = idealSlotToVauesEle.Key, maxDophineExp = idealSlotToVauesEle.Key;
                int maxMapNum = 0;
                Dictionary<string, int> mapSlotAndNum = new Dictionary<string, int>();
                foreach (string value in idealSlotToVauesEle.Value)
                {
                    if (valueToToken.ContainsKey(value))
                    {
                        string token = valueToToken[value];
                        if (!mapSlotAndNum.ContainsKey(token))
                        {
                            mapSlotAndNum[token] = 0;
                        }
                        mapSlotAndNum[token]++;
                    }
                }
                foreach (KeyValuePair<string, int> slotNumEle in mapSlotAndNum)
                {
                    int num = slotNumEle.Value;
                    if (num >= maxMapNum)
                    {
                        maxMapNum = num;
                        maxDophineExp = slotNumEle.Key;
                    }
                }
                idealSlotToDophine[idealSlot] = maxDophineExp;
            }

            // string outfile = @"D:\demo\IdealSlotToDophineFile.tsv";
            StreamWriter sw = new StreamWriter(outfile);
            foreach (KeyValuePair<string, string> slotToDophEle in idealSlotToDophine)
            {
                string dophineValue = slotToDophEle.Value;
                if (dophineValue.IndexOf("-") != -1)
                {
                    sw.WriteLine("{0}\t{1}\t{2}", slotToDophEle.Key, dophineValue, Regex.Replace(slotToDophEle.Key, @"\s+", ""));
                }
                else
                {
                    sw.WriteLine("{0}\t{1}", slotToDophEle.Key, dophineValue);
                }

            }
            sw.Close();
        }

        public static int CmpKeyNum(KeyValuePair<string, string> pairA, KeyValuePair<string, string> pairB)
        {
            int aN = pairA.Key.Split(' ').Length;
            int bN = pairB.Key.Split(' ').Length;
            return bN.CompareTo(aN);
        }
        public static void IntentSlotPatternIdex(string intPatUrlListFile, string slotPatIdxFile, string slotToDophineFile)
        {
            StreamReader sr = new StreamReader(slotToDophineFile);
            Dictionary<string, string> slotToDophineDic = new Dictionary<string, string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length == 2)
                {
                    slotToDophineDic[arr[0]] = arr[1];
                }
                else if (arr.Length == 3)
                {
                    slotToDophineDic[arr[0]] = arr[2];
                }
            }
            sr.Close();


            sr = new StreamReader(intPatUrlListFile);
            List<KeyValuePair<string, string>> slotToDophineSort = slotToDophineDic.ToList();
            slotToDophineSort.Sort(CmpKeyNum); // max number match first
            int intentCol = 0, patCol = 1;
            int curIdex = 1;
            Dictionary<string, int> intPatIdx = new Dictionary<string, int>();
            Dictionary<string, string> intPatToNewPat = new Dictionary<string, string>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string newSlotPat = SlotPatternMapToDophine(arr[patCol], slotToDophineSort);
                string oldKey = string.Format("{0}\t{1}", arr[intentCol], arr[patCol]);
                intPatToNewPat[oldKey] = string.Format("{0}\t{1}", arr[intentCol], newSlotPat);
                string key = string.Format("{0}\t{1}", arr[intentCol], newSlotPat);
                if (!intPatIdx.ContainsKey(key))
                {
                    intPatIdx[key] = curIdex++;
                }
            }
            sr.Close();



            StreamWriter sw = new StreamWriter(slotPatIdxFile);
            sw.WriteLine("{0}\t{1}\t{2}\t{3}", "Intent", "OldSlotPattern", "NewSlotPattern", "Index");
            foreach (KeyValuePair<string, string> pair in intPatToNewPat)
            {
                string newIntPat = pair.Value;
                if (!intPatIdx.ContainsKey(newIntPat))
                {
                    continue;
                }
                string idxStr = intPatIdx[newIntPat].ToString("D3");
                string result = string.Format("{0}\t{1}\t{2}", pair.Key, newIntPat.Split('\t')[1], idxStr);
                sw.WriteLine(result);
            }
            sw.Close();
        }

        public static string SlotPatternMapToDophine(string pat, List<KeyValuePair<string, string>> slotToDophineSort)
        {
            List<string> slotPatList = pat.Split().ToList();
            foreach (KeyValuePair<string, string> slot2NewSlot in slotToDophineSort)
            {
                string[] keyArr = slot2NewSlot.Key.Split();
                bool satisfied = true;
                if (keyArr.Length <= 1)
                {
                    if (slotPatList.Contains(slot2NewSlot.Key))
                    {
                        slotPatList.Remove(slot2NewSlot.Key);
                        slotPatList.Add(slot2NewSlot.Value);
                    }
                }
                else
                {
                    foreach (string slotEle in keyArr)
                    {
                        if (!slotPatList.Contains(slotEle))
                        {
                            satisfied = false;
                            break;
                        }
                    }
                    if (satisfied)
                    {
                        foreach (string slotEle in keyArr)
                        {
                            slotPatList.Remove(slotEle);
                        }
                        foreach (string newSlotEle in slot2NewSlot.Value.Split())
                        {
                            slotPatList.Add(newSlotEle);
                        }                      
                    }
                }
            }
            slotPatList.Sort(CmpList);
            return string.Join(" ", slotPatList).Trim();
        }
        public static int CmpList(string a, string b)
        {
            return a.CompareTo(b);
        }
        public static string HttpsServer(string url)
        {
            string result = null;
            if (url.IndexOf("http:") != -1)
            {
                result = url.Replace("http:", "https:");
            }
            else if (url.IndexOf("https:") != -1)
            {
                result = url.Replace("https:", "http:");
            }
            return result;
        }

        public static string WWWServer(string url)
        {
            string result = null;
            if (url.IndexOf("//www.") != -1)
            {
                result = url.Replace("www.", "");
            }
            else
            {
                string pattern = @"^(http(s)?://)(.*)$";
                Match mc = Regex.Match(url, pattern);
                if (mc.Success)
                {
                    result = mc.Groups[1].ToString() + "www." + mc.Groups[3].ToString();
                }
            }
            return result;
        }
        public static void FormatTopList(string infile, string outfile)
        {
            StreamReader sr = new StreamReader(infile);
            string line, url, score = "100";
            int urlCol = 2, scoreCol = 4;
            HashSet<string> urlHS = new HashSet<string>();
            //       int num = 0;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 5)
                    continue;
                url = arr[urlCol];
                urlHS.Add(url);
                string url3W = WWWServer(url);
                if (!string.IsNullOrWhiteSpace(url3W))
                {
                    urlHS.Add(url3W);
                }
                string urlHttps = HttpsServer(url);
                if (!string.IsNullOrWhiteSpace(urlHttps))
                {
                    urlHS.Add(urlHttps);
                    string url3WIn = WWWServer(urlHttps);
                    if (!string.IsNullOrWhiteSpace(url3WIn))
                    {
                        urlHS.Add(url3WIn);
                    }
                }


            }
            sr.Close();

            StreamWriter sw = new StreamWriter(outfile);
            foreach (string urlCur in urlHS)
            {
                sw.WriteLine("{0}\t{1}\t{2}", urlCur, 100, urlCur);
            }
            sw.Close();
        }
    }
}
