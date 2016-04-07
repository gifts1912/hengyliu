using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace IntentSlotPattern
{
    class Program
    {
        public static int patCol = 1;
        public static Dictionary<string, string> slotIntentDic = new Dictionary<string, string>()
        {
            {"#CandidateName#", "Candidate"},
            {"#CandidateList#", "CandidateList"},
            {"#PersonInfoWebPages#", "CandidateNavigational"},
            {"#Schedule#", "ElectionSchedule"},
            {"#Biography#", "CandidateBio"},
            {"#PersonInfoContact#", "CandidateContact"},
            {"#PersonInfoMarried#", "CandidateMarriage"},
            {"#PersonInfoPoliticalView#", "CandidateView"},
            {"election 2016", "ElectionGeneral"},
            {"presidential election", "ElectionGeneral"},
            {"election day", "ElectionSchedule"},
            {"next president", "CandidateList"}
        };

        public static List<string> orgQueryList = new List<string>();

        public static Dictionary<string, string> slotMappingSlot = new Dictionary<string, string>();
        public static void ProcessAndSlot(string infile, string outfile, string outfileManual)
        {
            StreamWriter swManual = new StreamWriter(outfileManual);
            StreamWriter sw = new StreamWriter(outfile);
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string patStr = arr[patCol];
                string rawQuery = arr[0];
                string[] patArr = patStr.Split(' ');
                StringBuilder sb = new StringBuilder();
                string intent = "";
                List<string> slotList = new List<string>();
                foreach(KeyValuePair<string, string> pair in slotMappingSlot)
                {
                    if(patStr.Contains(pair.Key) && slotList.IndexOf(pair.Value) == -1)
                    {
                        sb.Append(pair.Value);
                        sb.Append(",");
                        slotList.Add(pair.Value);
                    }
                    if(rawQuery.Contains(pair.Key) && slotList.IndexOf(pair.Value) == -1)
                    {
                        sb.Append(pair.Value);
                        sb.Append(",");
                        slotList.Add(pair.Value);
                    }
                }
                /*
                foreach(KeyValuePair<string, string> pair in slotIntentDic)
                {
                    if(patStr.Contains(pair.Key)
                }
                 */
                foreach(string slotEle in patArr)
                {
                    if (slotEle.StartsWith("#") && slotEle.EndsWith("#"))
                    {
                        if (slotIntentDic.ContainsKey(slotEle))
                        {
                            intent = slotIntentDic[slotEle];
                        }
                    }
                }
                if(string.IsNullOrEmpty(intent))
                {
                    foreach(KeyValuePair<string,string> pair in slotIntentDic)
                    {
                        if(rawQuery.Contains(pair.Key))
                        {
                            intent = pair.Value;
                        }
                    }
                }

                string slotStr = sb.ToString().TrimEnd(',');
                string result = string.Format("{0}\t{1}\t{2}", arr[0], intent, slotStr);
                if(!string.IsNullOrEmpty(slotStr) && !string.IsNullOrEmpty(intent))
                    sw.WriteLine(result);
                else
                {
                    swManual.WriteLine(result);
                }
            }
            sr.Close();
            sw.Close();
            swManual.Close();
        }
        
        public static void ReadSlotMapFile(string infile)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                string key = arr[0];
                //key = string.Format("\\b{0}\\b", key);
                //Console.WriteLine("{0}", key);
               
                slotMappingSlot[key] = arr[1];             
            }
            //Console.WriteLine("size : {0}", slotMappingSlot.Count);
            //Console.ReadKey();
            sr.Close();
        }

        public static void AddMaunallyToAutoFile(string manuallLabeFile, string outfile)
        {
            StreamReader sr = new StreamReader(manuallLabeFile);
            StreamWriter sw = new StreamWriter(outfile,true);
            List<string> listStr = new List<string>();

            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split(new char[]{'\t'}, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length != 3)
                {
                    listStr.Add(line);
                    //Console.WriteLine(line);
                }
                else
                {
                    sw.WriteLine(line);
                }
            }
            //Console.ReadKey();
            sr.Close();
            sw.Close();

            sw = new StreamWriter(manuallLabeFile);
            foreach(string ele in listStr)
            {
                sw.WriteLine(ele);
            }
            sw.Close();
        }

        public static void ReadRawQuerySet(string infile)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                orgQueryList.Add(line);
            }
            sr.Close();
        }

        public static void SortAsOrignalQuery(string outfile, string finalQuerySlotFile)
        {
            StreamReader sr = new StreamReader(outfile);
            string line;
            Dictionary<string, string> queryLineDic = new Dictionary<string, string>();
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                queryLineDic[arr[0]] = line;
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(finalQuerySlotFile);
            foreach(string query in orgQueryList)
            {
                if(queryLineDic.ContainsKey(query))
                {
                    sw.WriteLine(queryLineDic[query]);
                }
                else
                {
                    sw.WriteLine("{0}\t\t", query);
                }
            }
            sw.Close();
        }

        public static void  FormatTokenSlotFile(string tokenSlotFile, string tokenSlotOutfile)
        {
            StreamReader sr = new StreamReader(tokenSlotFile);
            StreamWriter sw = new StreamWriter(tokenSlotOutfile);
            Dictionary<string, string> querySlotDic = new Dictionary<string, string>();
            string line;
            while((line = sr.ReadLine())!= null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 4)
                    continue;
                string query = arr[0];
                if(!querySlotDic.ContainsKey(query))
                {
                    querySlotDic[query] = arr[2] + "\t" + arr[3] ;
                }
            }

            int allNum = 0;
            int satisfyNum = 0;
            foreach(string ele in orgQueryList)
            {
                allNum += 1;
                if(querySlotDic.ContainsKey(ele))
                {
                    sw.WriteLine("{0}\t{1}", ele, querySlotDic[ele]);
                    satisfyNum += 1;
                }
                else
                {
                    sw.WriteLine("{0}\t\t", ele);
                }
            }
            sw.Close();
            sr.Close();
            Console.WriteLine("{0}/{1}={2}",satisfyNum, allNum, Convert.ToDouble(satisfyNum)/allNum);
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            string infile = @"D:\Project\Election\TokenAndRules\domainSlot.tsv";
            string outfile = @"D:\Project\Election\TokenAndRules\domainSlotIntentAndSlot.tsv";

            string slotMapFile = @"D:\Project\Election\TokenAndRules\SlotMappingFile.tsv";
            string slotMannualFile = @"D:\Project\Election\TokenAndRules\domainSlotIntentAndSlotMannually.tsv";

            //ReadSlotMapFile(slotMapFile);

            //ProcessAndSlot(infile, outfile, slotMannualFile);

            string manuallLabeFile = @"D:\Project\Election\TokenAndRules\domainSlotIntentAndSlotMannuallyFinally.tsv";
          //  AddMaunallyToAutoFile(manuallLabeFile, outfile);

            string querySetFile = @"D:\Project\Election\TokenAndRules\OriginalQuerySet.tsv";
            ReadRawQuerySet(querySetFile);

            //string finalQuerySlotFile = @"D:\Project\Election\TokenAndRules\FianllyQueryIntendSlot.tsv";
            //SortAsOrignalQuery(outfile, finalQuerySlotFile);

            string tokenSlotFile = @"D:\Project\Election\TokenAndRules\Manipulated TSV File.tsv";
            string tokenSlotOutfile = @"D:\Project\Election\TokenAndRules\tokenSlotFileFormat.tsv";

            FormatTokenSlotFile(tokenSlotFile, tokenSlotOutfile);
        }
    }
}
