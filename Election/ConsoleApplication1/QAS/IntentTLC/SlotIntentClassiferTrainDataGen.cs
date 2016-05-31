using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace QAS.IntentTLC
{
    class SlotIntentClassiferTrainDataGen
    {
        public static StreamWriter lw;
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new String[4];
                args[0] = @"D:\demo\queryPattern.tsv";// queryCol = 0, patternCol = 2
                args[1] = @"D:\demo\ElectionQueryIntent.tsv"; //queryCol = 0, intentCol = 3;
                args[2] = @"D:\Python27\src\dataset\election\ruleToIntentTrain.tsv";
                args[3] = @"D:\Python27\src\dataset\election\ruleToIntentTrainLibSVM.txt";
          
            }
            lw = new StreamWriter(@"D:\demo\log.tsv");
            string queryPatFile = args[0];
            string queryIntentFile =args[1];
            string ruleToIntentFile = args[2];
            
            Dictionary<string, string> patIntent = new Dictionary<string, string>();
            GenRuleToIntentTrainData(queryPatFile, queryIntentFile, ruleToIntentFile, ref patIntent);

            string wordIdxFile = @"D:\Python27\src\dataset\election\wordIndex.tsv";
            string libsvmFile = @"D:\Python27\src\dataset\election\ruleToIntentTrainLibSVM.txt";
            string intentIdxFile = @"D:\Python27\src\dataset\election\intentIndex.tsv";

            LibSvmFileFormat(patIntent, wordIdxFile, intentIdxFile, libsvmFile);
   
            lw.Close();
        }

        public static void  GenRuleToIntentTrainData(string queryPatFile, string queryIntentFile, string ruleToIntentFile, ref Dictionary<string, string> patIntentDic)
        {
            StreamReader sr; //= new StreamReader(queryPatFile);
            string line; 
            Dictionary<string, string> queryToInt = new Dictionary<string, string>();
            sr = new StreamReader(queryIntentFile);
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string query = arr[0], intent = arr[3];
                queryToInt[query] = intent;
            }
            sr.Close();

            sr = new StreamReader(queryPatFile);
            Dictionary<string, Dictionary<string, int>> patIntentsDic= new Dictionary<string, Dictionary<string, int>>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= 2)
                    continue;
                string query = arr[0], pat = arr[2];
                if(!queryToInt.ContainsKey(query))
                {
                    LogWrite(query);
                    continue;
                }
                string intent = queryToInt[query];

                if (!patIntentsDic.ContainsKey(pat))
                {
                    patIntentsDic[pat] = new Dictionary<string, int>();
                }
                if(!patIntentsDic[pat].ContainsKey(intent))
                {
                    patIntentsDic[pat][intent] = 0;
                }
                patIntentsDic[pat][intent] += 1;
            }
            sr.Close();


            StreamWriter sw = new StreamWriter(ruleToIntentFile);
            foreach (KeyValuePair<string, Dictionary<string, int>> pair in patIntentsDic)
            {
                string pattern = pair.Key;
                string intent = MostFreq(pair.Value);
                if (string.IsNullOrEmpty(intent))
                {
                    LogWrite(string.Format("intent not exists:{0}", string.Join(" ", pair.Value.Keys.ToArray())));
                    continue;
                }
                pattern = pattern.Replace("[","").Replace("]", "");
                pattern = pattern.Trim();
                patIntentDic[pattern] = intent;                       
                sw.WriteLine("{0}\t{1}", pattern, intent);
            }
            sw.Close();

                   
        }

        public static void LibSvmFileFormat(Dictionary<string, string> patIntent, string wordIdxFile, string intentIdxFile, string libsvmFile)
        {
            // generate word index and intent index
            int index = 1;
            Dictionary<string, int> wordIdxDic = new Dictionary<string, int>();
            Dictionary<string, int> intentIdxDic = new Dictionary<string, int>();
            int intentIndex = 1;
            foreach(KeyValuePair<string, string> pair in patIntent)
            {
                string pat = pair.Key, intent = pair.Value;
                string[] wordsArr = pat.Split();
                foreach(string word in wordsArr)
                {
                    if(!wordIdxDic.ContainsKey(word))
                    {
                        wordIdxDic[word] = index;
                        index++;
                    }
                }
                if(!intentIdxDic.ContainsKey(intent))
                {
                    intentIdxDic[intent] = intentIndex++;
                }
            }

            // write the word and corresponding index to file.
            StreamWriter sw = new StreamWriter(wordIdxFile);
            foreach (KeyValuePair<string, int> pair in wordIdxDic)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();

            //write intent and corresponding index to file
            sw = new StreamWriter(intentIdxFile);
            foreach(KeyValuePair<string, int> pair in intentIdxDic)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();

            // generate the libsvm format file from train data             
            sw = new StreamWriter(libsvmFile);
            foreach(KeyValuePair<string ,string> pair in patIntent)
            {
                string pat = pair.Key, intent = pair.Value;
                if (!intentIdxDic.ContainsKey(intent))
                {
                    LogWrite(string.Format("intent not occured in intentIdxDic: {0}", intent));
                    continue;
                }
                int curIntIdx = intentIdxDic[intent];
                string[] wordsArr = pat.Split();
                StringBuilder sb = new StringBuilder();
                sb.Append(curIntIdx);
                Dictionary<int, int> wordFreq = new Dictionary<int, int>();
                foreach(string word in wordsArr)
                {
                    int curWordIdx;
                    try
                    {
                        curWordIdx = wordIdxDic[word];
                    }
                    catch(Exception ex)
                    {
                        LogWrite(string.Format("word not occurred in wordIdxDic:{0}", word));
                        continue;
                    }
                    if(!wordFreq.ContainsKey(curWordIdx))
                    {
                        wordFreq[curWordIdx] = 0;
                    }
                    wordFreq[curWordIdx] += 1;
                    //sb.Append(" ");
                    //sb.Append(string.Format("{0}:1", curWordIdx));
                }
                List<KeyValuePair<int, int>> feaValueList = wordFreq.ToList();
                feaValueList.Sort(KeyCmp);
                foreach(KeyValuePair<int, int> pairFea in feaValueList)
                {
                    sb.Append(" ");
                    sb.Append(string.Format("{0}:{1}", pairFea.Key, pairFea.Value));
                }
                sw.WriteLine(sb.ToString());
            }
            sw.Close();
        }

        public static int KeyCmp(KeyValuePair<int, int> pair1, KeyValuePair<int, int> pair2)
        {
            return (pair1.Key).CompareTo(pair2.Key);
        }

        public static void LogWrite(string query)
        {
            lw.WriteLine(query);
        }

        public static string MostFreq(Dictionary<string, int> intentFreq)
        {
            int max = -1;
            string mostFreqInt = null;
            foreach (KeyValuePair<string, int> pair in intentFreq)
            {
                if (max < pair.Value)
                {
                    max = pair.Value;
                    mostFreqInt = pair.Key;
                }
            }
            return mostFreqInt;
        }
    }
}
