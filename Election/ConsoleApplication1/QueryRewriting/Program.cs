using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace QueryRewriting
{
    class Program
    {
        public static int queryCol = 0, patternCol = 2;
        public static string[] StayWordsArr = new string[] { "vice", "female", "male", "black", "white", "marriage", "top", "vs", "and", "or", "history"};
        public static string[] StayRgxArr = new string[] { "\\d{4}|\\d{2}" };
        public static void QueryRewrite(string infile, string outfile)
        {
            
            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            StreamWriter swNoRewrite = new StreamWriter(@"D:\Project\Election\QueryRewriting\NotRewrite.tsv");
            string line;
            string query, pattern;
            HashSet<string> stayWordsSet = new HashSet<string>(StayWordsArr);
            int queryId = 100;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= patternCol || arr.Length <= queryCol)
                    continue;
                query = arr[queryCol];
                pattern = arr[patternCol];
                string[] queryArr = query.Split(' ');
                string[] slotArr = pattern.Split(' ');
                int candidateCol = Array.IndexOf(slotArr, "E.Party");
                int candidateListCol = Array.IndexOf(slotArr, "I.NPE.CandidatesList.CandidatesList");
                if(candidateCol == -1 || candidateListCol == -1)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", line, candidateCol, candidateListCol);
                    swNoRewrite.WriteLine("{0}", line);
                    continue;
                }
                StringBuilder newQuery = new StringBuilder();
                newQuery.Append("2016 ");
                for (int i = 0; i < queryArr.Length; i++)
                {
                    string word = queryArr[i];
                    if(i == candidateCol)
                    {
                        newQuery.Append(word);
                        newQuery.Append(" ");
                        continue;
                    }
                    if(stayWordsSet.Contains(word))
                    {
                        newQuery.Append(word);
                        newQuery.Append(" ");
                        Console.WriteLine("Staywords: {0}\t : {1}", word, line);
                    }

                }
                newQuery.Append("presidential candidates");
                sw.WriteLine("{2}\t{0}\t{1}", query, newQuery.ToString(), queryId);
                queryId += 1;
                
            }

            sw.Close();
            sr.Close();
            //Console.ReadKey();
        }
        static void Main(string[] args)
        {
            
            string slotInputFile = @"D:\Project\Election\QueryRewriting\SPPattern.tsv";
            string rewriteFile = @"D:\Project\Election\QueryRewriting\QueryRewrite.tsv";
            QueryRewrite(slotInputFile, rewriteFile);
        }
    }
}
