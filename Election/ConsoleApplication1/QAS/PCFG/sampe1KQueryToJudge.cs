using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace QAS.PCFG
{
    class sampe1KQueryToJudge
    {

        public static void ParseIntent(string infile, string outfile)
        {
            List<string> dataList = new List<string>();
            StreamWriter Log = new StreamWriter(@"D:\demo\log.tsv");
            StreamWriter sw = new StreamWriter(outfile);
            StreamReader sr = new StreamReader(infile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if(arr.Length != 2)
                {
                    Console.WriteLine(line);
                }
                string query = arr[0], intent = arr[1];
                if(!intent.StartsWith("MSElection{"))
                {
                    Console.WriteLine(line);
                }
                int pos_beg = "MSElection{".Length;
                int pos_end = intent.IndexOf("{", pos_beg + 1);
                intent = intent.Substring("MSElection{".Length, pos_end - pos_beg);
                intent = intent.Split('_')[0];
                dataList.Add(string.Format("{0}\t{1}", query, intent));
             //   sw.WriteLine(string.Format("{0}\t{1}", query, intent));
            }


            RandomSortList(ref dataList);
            for (int i = 0; i < 1000; i++)
            {
                sw.WriteLine(dataList[i]);
            }
            /*
            StreamWriter swRd = new StreamWriter(@"D:\demo\random.txt");         
            Random rnd = new Random();
            for(int i = 0; i < 1000; i++)
            {
                int r = rnd.Next(dataList.Count());
                swRd.WriteLine(r);
            }         
            swRd.Close();
            */

            sr.Close();
            Log.Close();
            sw.Close();
            Console.ReadKey();

        }

        public static void RandomSortList(ref List<string> arr)
        {
            for(int i = arr.Count - 1; i > 0 ; i--)
            {
                Random rand = new Random();
                int p = rand.Next(i);
                string tmp = arr[p];
                arr[p] = arr[i];
                arr[i] = tmp;
            }
        }
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\ElectionPCFG\queryLabel.txt";
                args[1] = @"D:\demo\ElectionPCFG\queryLabelParse.txt"; // query and url id           
            }
            string infile = args[0];
            string outfile = args[1];
            ParseIntent(infile, outfile);
        }
    }
}
