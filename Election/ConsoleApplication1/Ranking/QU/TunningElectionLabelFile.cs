using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ranking.QU.TunningElectionLabelFile
{
    class TunningElectionLabelFile
    {
        
        public static void LoadTurnPartFile(string turnPartFile, Dictionary<string, string> queryTurnInfo)
        {
            string line;
            using (StreamReader sr = new StreamReader(turnPartFile))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    int pos = line.IndexOf('\t');
                    string query = line.Substring(0, pos);
                    string info = line.Substring(pos + 1);
                    queryTurnInfo[query] = info;
                }               
            }
        }

        public static void  AddPartTurnToOrigFile(string oriFile, Dictionary<string,string> queryTurnInfo, string outfile)
        {
            string line;
            using (StreamWriter sw = new StreamWriter(outfile))
            {
                using (StreamReader sr = new StreamReader(oriFile))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        int pos = line.IndexOf('\t');
                        string key = line.Substring(0, pos);
                        if (queryTurnInfo.ContainsKey(key))
                        {
                            sw.WriteLine("{0}\t{1}", key, queryTurnInfo[key]);
                        }
                        else
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\Project\Election\TokenAndRules\electionV1.2FilterViewAndCandidateListIntent.tsv";
                args[1] = @"D:\Project\Election\TokenAndRules\electionV1.2.tsv";
                args[2] = @"D:\Project\Election\TokenAndRules\electionv1.3.tsv";
            }
            string turnPartFile = args[0];
            string origFile = args[1];
            string newFile = args[2];
            Dictionary<string, string> queryTurnInfo = new Dictionary<string, string>();
            LoadTurnPartFile(turnPartFile, queryTurnInfo);

            AddPartTurnToOrigFile(origFile, queryTurnInfo, newFile);
        }
    }
}
