using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ranking.Shipping
{
    class EntityClusterBasedOnElectionTokens
    {
        public static void Processor(string tokenFile, string outfile)
        {
            StreamReader sr = new StreamReader(tokenFile);
            string line;
            Dictionary<string, string> slotIdealSlot = new Dictionary<string, string>();
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                string value = arr[0].Substring("qpv2tkn-".Length);
                string slot = arr[1].Split(';')[1];
                if (string.IsNullOrEmpty(slot))
                    continue;
                slotIdealSlot[value] = slot;
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(outfile);
            foreach(KeyValuePair<string, string> pair in slotIdealSlot)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();
        }
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\Project\Election\TokenAndRules\ElectionTokens.tsv";
                args[1] = @"D:\demo\slotIdealSlot.tsv";
            }
            string eleTokenFile = args[0];
            string slotIdealSlotFile = args[1];
            Processor(eleTokenFile, slotIdealSlotFile);
        }
    }
}
