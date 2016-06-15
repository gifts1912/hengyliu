using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.NewIntent
{
    class EntityLexicon
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\demo\ElectionTokens.tsv";
                args[1] = @"D:\demo\ElelctionEntityLexicon.txt";
            }
            string tokenfile = args[0];
            string lexiconfile = args[1];
            LexiconGenerate(tokenfile, lexiconfile);
        }

        public static void LexiconGenerate(string tokensfile, string lexiconfile)
        {
            HashSet<string> slotEntitySet = new HashSet<string>();
            Dictionary<string, string> candEntitySet = new Dictionary<string, string>();
            slotEntitySet.Add("<election.candidate.highconf>"); // candidate slot
            slotEntitySet.Add("<election.bpiissue>"); // political view slot

            candEntitySet.Add("1a466af2-ed23-25bd-794d-1ca925e4681b","candidate.donaldtrump"); // candidate donald trump
            candEntitySet.Add("82b92f37-7844-b683-443c-71f0cdf6dcb4","candidate.hillaryclition"); // candidate hillary cliton

            StreamReader sr = new StreamReader(tokensfile);
            string line;
            List<string> rows = new List<string>();
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string[] entityInfo = arr[1].Split(';');
                string slotClass = entityInfo[0].Trim(), slotNormal = entityInfo[1].Trim();
                if(slotEntitySet.Contains(slotClass))
                {
                    string label = slotClass.Trim(new char[] {'<', '>'});
                    if(slotClass == "<election.candidate.highconf>")
                    {
                        if(candEntitySet.ContainsKey(slotNormal))
                        {
                            label = candEntitySet[slotNormal];
                        }
                        else
                        {
                            continue;
                        }
                    }
                    string key = arr[0].Substring("qpv2tkn-".Length);
                    rows.Add(string.Format("{0}\t{1}", key, label));
                }
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(lexiconfile);
            foreach(string row in rows)
            {
                sw.WriteLine(row);
            }
            sw.Close();
               
        }
    }
}
