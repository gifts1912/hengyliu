using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.NewIntent
{
    class OtherSlotLexicon
    {
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\ElectionTokens.tsv";
                args[1] = @"D:\Project\Election\Improve\Lexicon";
                args[2] = "atom.mselection.txt";
            }

            string slotfile = args[0];
            string lexiconfilepath = args[1];
            string atomfile = args[2];
            Dictionary<string, List<string>> slotValues = new Dictionary<string, List<string>>();
            LoadElectionSlots(slotfile, slotValues);
            SlotPatternLexicon(slotValues, lexiconfilepath, atomfile);
        }
        public static void LoadElectionSlots(string slotsFile, Dictionary<string, List<string>> slotValues)
        {
            HashSet<string> filterSlot = new HashSet<string>();
           // filterSlot.Add("election.candidate.highconf");
            //filterSlot.Add("election.bpiissue");
            StreamReader sr = new StreamReader(slotsFile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                string value = arr[0], slot = arr[1];
                string[] slotTags = slot.Split(';');
                string slotname = slotTags[0].Trim(new char[] { '<', '>' });
                if(filterSlot.Contains(slotname) || !slotname.Contains("."))
                {
                    continue;
                }
                if(!slotValues.ContainsKey(slotname))
                {
                    slotValues[slotname] = new List<string>();
                }
                value = value.Substring("qpv2tkn-".Length);
                slotValues[slotname].Add(value);
            }
            sr.Close();            
        }

        public static void SlotPatternLexicon(Dictionary<string, List<string>> slotValues, string path, string atomfile)
        {
            atomfile = path + '\\' + atomfile;
            StreamWriter atomsw = new StreamWriter(atomfile);
            foreach(KeyValuePair<string, List<string>> pair in slotValues)
            {
                string slotname = pair.Key;
                atomsw.WriteLine(string.Format("{0}\t\tLexicon:lexicon.mselection.{1}.txt\t\tFunc.Filter(name.Contains(\"__orig__\"))", slotname, slotname));
                List<string> slotvalues = pair.Value;
                string lexiconfile = string.Format("{0}\\lexicon.mselection.{1}.txt", path, slotname);
                StreamWriter lexiconsw = new StreamWriter(lexiconfile);
                foreach(string value in slotvalues)
                {
                    lexiconsw.WriteLine(value);
                }
                lexiconsw.Close();
            }
            atomsw.Close();
        }
    }
}
