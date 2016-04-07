using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.QU.SlotTriggerPbmxlFile
{
    class SlotTriggerPbmxlFile
    {

        public static void  RemainSlotTrigger(string lexiconFile, string pbxmlFile, string pbxmlOptimizedFile, int patCol)
        {
            Dictionary<string, Regex> slotRegexDic = new Dictionary<string, Regex>();
            string line;
            StreamReader srLexicon = new StreamReader(lexiconFile);
            while((line = srLexicon.ReadLine()) != null)
            {
                string[] arr = line.Split();
                if(arr.Length != 3)
                    continue;
                string key = arr[0];
                if(!string.IsNullOrEmpty(arr[1]))
                    key = string.Format("{0}.{1}", arr[0], arr[1]);
                key = string.Format("[{0}]", key);
                string value = arr[2];
                Regex rgx = new Regex(value);
                slotRegexDic[key] = rgx;
            }
            srLexicon.Close();

            StreamReader srPbxml = new StreamReader(pbxmlFile);
            StreamWriter sw = new StreamWriter(pbxmlOptimizedFile);
            string slotPat = "";
            int slotMatchCol = 4;
            while((line = srPbxml.ReadLine()) != null)
            {
                List<string> slotList = new List<string>();
                string[] arr = line.Split('\t');
                slotPat = arr[patCol];
 
                string[] patArr = slotPat.Split();
                for (int i = 0; i < patArr.Length; i++)
                {
                    string ele = patArr[i];
                    if (!ele.Contains("."))
                    {
                        ele = ele.Trim(new char[] { '[', ']' });
                        foreach(KeyValuePair<string, Regex> pair in slotRegexDic)
                        {                   
                            Regex rgx = pair.Value;
                            if(rgx.IsMatch(ele))
                            {
                                
                                string slotMatchStr = arr[slotMatchCol];
                                slotMatchStr = slotMatchStr.Replace(string.Format("[{0}]", ele), pair.Key);
                                arr[slotMatchCol] = slotMatchStr;
                                ele = pair.Key;
                                break;
                            }
                        }
                    }
                    slotList.Add(ele);
                }
                arr[patCol] = string.Join(" ", slotList.ToArray());
                sw.WriteLine(string.Join("\t", arr));
            }
            srPbxml.Close();
            sw.Close();    
        }

        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\Project\Election\TokenAndRules\ElectionEntityLexiconDirectory\electionSlotPatternConsraintWords.txt";
                args[1] = @"D:\Project\Election\TokenAndRules\pbxmlParseOriginal.tsv";
                args[2] = @"D:\Project\Election\TokenAndRules\pbxmlParse.tsv";
                args[3] = "2";
            }
            string lexiconFile = args[0];
            string pbxmlFile = args[1];
            string pbxmlOptimizedFile = args[2];
            int patCol = int.Parse(args[3]);
            RemainSlotTrigger(lexiconFile, pbxmlFile, pbxmlOptimizedFile, patCol);
        }
    }
}
