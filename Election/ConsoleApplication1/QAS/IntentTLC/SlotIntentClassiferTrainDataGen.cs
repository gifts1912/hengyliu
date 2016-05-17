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
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new String[3];
                args[0] = @"D:\demo\queryPattern.tsv";// queryCol = 0, patternCol = 2
                args[1] = @"D:\demo\ElectionQueryIntent.tsv"; //queryCol = 0, intentCol = 3;
                args[2] = @"D:\demo\ruleToIntentTrain.tsv";
            }
            string queryPatFile = args[0];
            string queryIntentFile =args[1];
            string ruleToIntentFile = args[2];

            GenRuleToIntentTrainData(queryPatFile, queryIntentFile, ruleToIntentFile);
        }

        public static void  GenRuleToIntentTrainData(string queryPatFile, string queryIntentFile, string ruleToIntentFile)
        {
            StreamReader sr = new StreamReader(queryPatFile);
            string line;
            Dictionary<string, List<string>> patQueryList  = new Dictionary<string, List<string>>();
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= 2)
                    continue;
                string query = arr[0], pat = arr[2];
                if(!patQueryList.ContainsKey(pat))
                {
                    patQueryList[pat] = new List<string>();
                }
                patQueryList[pat].Add(query);
            }
            sr.Close();

            Dictionary<string, string> queryToInt = new Dictionary<string, string>();
            sr = new StreamReader(queryIntentFile);
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string query = arr[0], intent = arr[3];
                queryToInt[query] = intent;
            }
            sr.Close();

            foreach(KeyValuePair<string, List<string>> pair in patQueryList)
            {
                if(pair.Value.Count != 1)
                {
                    Console.WriteLine("{0}\t{1}", pair.Key, string.Join("\t", pair.Value));
                }
            }
            Console.ReadKey();
        }
    }
}
