using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace QAS.PatternEngine
{
    class QASTestOnDoDt
    {
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\QueryText.tsv";
                args[1] = @"D:\demo\QueryProcess.tsv";
            }
            string infile = args[0];
            string outfile = args[1];
        //    QueryTextFormat(infile, outfile);
            Dictionary<string, string> patternIntentDic = new Dictionary<string, string>();
           // LoadPatternIntent(patternQueryFile, queryIntentFile, patternIntentDic); // generate each pattern's intent
        }

        public static void QueryTextFormat(string infile, string outfile)
        {
            StreamWriter sw = new StreamWriter(outfile);
            using (StreamReader sr = new StreamReader(infile))
            {
                string line;
                int curNum = 0;
                HashSet<string> querySet = new HashSet<string>();
                line = sr.ReadLine();
                while((line = sr.ReadLine()) != null)
                {
                    int pos = line.LastIndexOf(']');
                    string query = line.Substring(pos + 1);
                    querySet.Add(query);
                    curNum++;
                    if (curNum == 10000)
                    {
                        foreach (string queryEle in querySet)
                        {
                            sw.WriteLine(queryEle);
                        }
                        querySet.Clear();
                        curNum = 0;
                    }
                }

                foreach(string queryEle in querySet)
                {
                    sw.WriteLine(queryEle);
                }
            }
            sw.Close();
        }
    }
}
