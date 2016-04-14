using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.RankingPro.QueryPatternChangeModule
{

    class QueryPatternChangeModule
    {
        public static void LoadQueryPattern(string queryPatternFile, Dictionary<string, Tuple<string, string>> queryIntentPattern)
        {
            StreamReader sr = new StreamReader(queryPatternFile);
            string line, query, pattern, intent;
            while((line = sr.ReadLine())!= null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= 2)
                    continue;
                query = arr[0];
                intent = arr[1];
                pattern = arr[2] ;             
                queryIntentPattern[query] = new Tuple<string, string>(intent, pattern);
            }
            sr.Close();
        }

       public static void ChangeQueryPattern(string queryProcessFile, string outfile, Dictionary<string, Tuple<string, string>> queryIntentPattern)
       {
           string line, query, pattern, intent;
            int queryCol= -1, patternCol = -1;
           StreamReader sr = new StreamReader(queryProcessFile);
           StreamWriter sw = new StreamWriter(outfile);
           line = sr.ReadLine();
           List<string> feaList = new List<string>(line.Split('\t'));
           feaList.Add("m:QueryIntent");
           line = string.Join("\t", feaList.ToArray());
           sw.WriteLine(line);

           string[] arr = line.Split('\t');
           for(int i = 0; i < arr.Length; i++)
           {
               string fea = arr[i];
               if(fea.Equals("m:Query"))
               {
                   queryCol = i;
               }
               if(fea.Equals("m:QueryPattern"))
               {
                   patternCol = i;
               }
           }
           while((line = sr.ReadLine()) != null)
           {
               arr = line.Split('\t');
               if (arr.Length <= queryCol || arr.Length <= patternCol)
                   continue;
               query = arr[queryCol];
               intent = "";
               if (queryIntentPattern.ContainsKey(query))
               {
                   Tuple<string, string> intentPatternPair = queryIntentPattern[query];
                   pattern = intentPatternPair.Item2;
                   intent = intentPatternPair.Item1;
                   arr[patternCol] = pattern;
               }
               string result = string.Join("\t", arr);
               sw.WriteLine("{0}\t{1}", result, intent);
           }
           sr.Close();
           sw.Close();
       }
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\QueryPatternTest.tsv";
                args[1] = @"D:\demo\Compressed_Extraction.tsv";
                args[2] = @"D:\demo\queryChangePatternFile.tsv";
            }
            
            string queryPatternFile = args[0];
            string queryProcessFile = args[1];
            string outfile = args[2];
            //Dictionary<string, string> queryPattern = new Dictionary<string, string>();
            Dictionary<string, Tuple<string, string>> queryIntentPattern = new Dictionary<string, Tuple<string, string>>();
            LoadQueryPattern(queryPatternFile, queryIntentPattern);
            ChangeQueryPattern(queryProcessFile, outfile, queryIntentPattern);
        }
    }
}
