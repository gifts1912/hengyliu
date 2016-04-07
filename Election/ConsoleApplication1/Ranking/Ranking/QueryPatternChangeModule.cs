using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.QueryPatternChangeModule
{

    class QueryPatternChangeModule
    {
        public static void LoadQueryPattern(string queryPatternFile, Dictionary<string, string> queryPattern)
        {
            StreamReader sr = new StreamReader(queryPatternFile);
            string line, query, pattern;
            while((line = sr.ReadLine())!= null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 9)
                    continue;
                query = arr[0];
                pattern = arr[1];
                queryPattern[query] = pattern;
            }
            sr.Close();
        }

       public static void ChangeQueryPattern(string queryProcessFile, string outfile, Dictionary<string, string> queryPattern)
       {
           string line, query, pattern;
            int queryCol= -1, patternCol = -1;
           StreamReader sr = new StreamReader(queryProcessFile);
           StreamWriter sw = new StreamWriter(outfile);
           line = sr.ReadLine();
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
               if(queryPattern.ContainsKey(query))
               {
                   pattern = queryPattern[query];
                   arr[patternCol] = pattern;
               }
               string result = string.Join("\t", arr);
               sw.WriteLine(result);
           }
           sr.Close();
           sw.Close();
       }
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\QueryPattern.tsv";
                args[1] = @"D:\demo\Compressed_Extraction.tsv";
                args[2] = @"D:\demo\queryChangePatternFile.tsv";
            }
            
            string queryPatternFile = args[0];
            string queryProcessFile = args[1];
            string outfile = args[2];
            Dictionary<string, string> queryPattern = new Dictionary<string, string>();
            LoadQueryPattern(queryPatternFile, queryPattern);
            ChangeQueryPattern(queryProcessFile, outfile, queryPattern);
        }
    }
}
