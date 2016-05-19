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
                args = new String[3];
                args[0] = @"D:\demo\queryPattern.tsv";// queryCol = 0, patternCol = 2
                args[1] = @"D:\demo\ElectionQueryIntent.tsv"; //queryCol = 0, intentCol = 3;
                args[2] = @"D:\demo\ruleToIntentTrain.tsv";
            }
            lw = new StreamWriter(@"D:\demo\log.tsv");
            string queryPatFile = args[0];
            string queryIntentFile =args[1];
            string ruleToIntentFile = args[2];

            GenRuleToIntentTrainData(queryPatFile, queryIntentFile, ruleToIntentFile);
            lw.Close();
        }

        public static void  GenRuleToIntentTrainData(string queryPatFile, string queryIntentFile, string ruleToIntentFile)
        {
            StreamReader sr = new StreamReader(queryPatFile);
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

            sr = new StreamReader(queryIntentFile);
            Dictionary<string, Dictionary<string, int>> patIntentsDic= new Dictionary<string, Dictionary<string, int>>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= 2)
                    continue;
                string query = arr[0], pat = arr[1];
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
                    LogWrite(string.Format("most intent not exists:{0}", string.Join(" ", pair.Value.Keys.ToArray())));
                    continue;
                }
                pattern = pattern.Replace("[","").Replace("]", "");
                pattern = pattern.Trim();                         
                sw.WriteLine("{0}\t{1}", pattern, intent);              
            }
            sw.Close();           
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
