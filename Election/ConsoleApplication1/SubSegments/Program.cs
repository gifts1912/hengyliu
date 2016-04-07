using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SubSegments
{
    class Program
    {
        
        public static void StaticSlotCount(string infile, string slotCountOutFile)
        {
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(slotCountOutFile);
            Dictionary<string, int> slotNumDic = new Dictionary<string, int>();
            string line;
            int allNum = 0;
            while((line = sr.ReadLine()) != null)
            {
                allNum += 1;
                string[] arr = line.Split('\t');
                string slot = arr[0];
                slot = slot.Replace("qpv2rule-<", "");
                string[] slotArr = slot.Split(' ');
                foreach(string ele in slotArr)
                {
                    if (!ele.Contains("."))
                        continue;
                    if(!slotNumDic.ContainsKey(ele))
                    {
                        slotNumDic[ele] = 0; 
                    }
                    slotNumDic[ele] += 1;
                }
            }

            var Items = from pair in slotNumDic
                        orderby pair.Value descending
                        select pair;

            foreach(KeyValuePair<string, int> pair in Items)
            {
                sw.WriteLine("{0}\t{1}\t{2}", pair.Key, pair.Value, pair.Value * 1.0 / allNum);
            }

            sw.Close();
            sr.Close();
        }
        static void Main(string[] args)
        {
            string infile = @"D:\Project\Election\TokenAndRules\ElectionRules.tsv";
            string outfile = @"D:\Project\Election\TokenAndRules\subSegMentStatic.tsv";
            string outfileIntend = @"D:\Project\Election\TokenAndRules\subSegMentNum.tsv";
            StaticSubSegment(infile, outfile);

            string slotCountOutFile = @"D:\Project\Election\TokenAndRules\slotCountStatic.tsv";
            StaticSlotCount(infile, slotCountOutFile);
        }

        public static void StaticSubSegment(string infile, string outfile)
        {
            Dictionary<string, Dictionary<string, int>> intendSlotNum = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, int> subSegNum = new Dictionary<string, int>();
            Dictionary<string, List<string>> sumSlotRawSlot = new Dictionary<string, List<string>>();
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string [] subStrArr = arr[1].Split(';');
                string subSeg = subStrArr[9];
                string slotStr = arr[0];
                string[] slotStrArrTmp = slotStr.Split(' ');
                List<string> slotStrArr = new List<string>();
                foreach(string ele in slotStrArrTmp)
                {
                    if(ele.Contains("."))
                    {
                        slotStrArr.Add(ele);
                    }
                }

                slotStrArr.Sort();
                slotStr = string.Join(" ", slotStrArr.ToArray());
                if (!sumSlotRawSlot.ContainsKey(slotStr))
                {
                    sumSlotRawSlot[slotStr] = new List<string>();
                }
                sumSlotRawSlot[slotStr].Add(arr[0]);
                if(!subSegNum.ContainsKey(subSeg))
                {
                    subSegNum[subSeg] = 0;
                }
                subSegNum[subSeg] += 1;
                if(!intendSlotNum.ContainsKey(subSeg))
                {
                    intendSlotNum[subSeg] = new Dictionary<string, int>();
                }
                
                if(!intendSlotNum[subSeg].ContainsKey(slotStr))
                {
                    intendSlotNum[subSeg][slotStr] = 0;
                }
                intendSlotNum[subSeg][slotStr] += 1;

             /*   Console.WriteLine("{0}\t{1}", line, subStrArr[9]);
                Console.ReadKey();*/
            }
            string outfileIntend = @"D:\Project\Election\TokenAndRules\subSegMentNum.tsv";
       

            StreamWriter sw = new StreamWriter(outfile);
            foreach(KeyValuePair<string, Dictionary<string, int>> pairSeg in intendSlotNum)
            {
                string key = pairSeg.Key;
                var items = from pair in pairSeg.Value
                            orderby pair.Value descending
                            select pair;
                int num = 0;
                sw.WriteLine("{0}\t{1}", key, subSegNum[key]);
                foreach(KeyValuePair<string, int> pairSlot in items)
                {
                    num++;
                    string slotStr = pairSlot.Key;
                    slotStr = slotStr.Replace("qpv2rule-", "");
                    StringBuilder sb = new StringBuilder();

                    if (!sumSlotRawSlot.ContainsKey(slotStr))
                        continue;
                    List<string> rawSlotList = sumSlotRawSlot[slotStr];
                    int i = 0;
                    
                    foreach (string ele in rawSlotList)
                    {
                        sb.Append(ele);
                        sb.Append("\t");
                        i++;
                        if (i == 5)
                            break;
                    }
                        sw.WriteLine("\t{1}\t{2}\t->\t{3}", key, slotStr, pairSlot.Value, sb.ToString());
                    if (num == 10)
                        break;
                }
            }

            sw.Close();       
            sr.Close();
        }
    }
}
