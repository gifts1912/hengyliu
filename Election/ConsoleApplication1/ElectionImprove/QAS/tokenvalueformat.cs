using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.QAS
{
    class tokenvalueformat
    {
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\demo\ElectionTokens.tsv";
                args[1] = @"D:\sumStoneTemplate\electionqas\lexicon\candidatelexicon.txt";
                args[2] = @"D:\sumStoneTemplate\electionqas\lexicon\politicalviewlexicon.txt" ;
            }
            string electiontokenfile = args[0];
            string candidatelexiconfile = args[1];
            string politicallexiconfile = args[2];
            // GenerateCandidatePoliticalToken(electiontokenfile, candidatelexiconfile, politicallexiconfile);
            TokenizerSlotIdeal(electiontokenfile);
        }

        public static void TokenizerSlotIdeal(string infile)
        {
            string idealSlotFile = @"D:\Project\Election\idealSlotMapManuallyProcess.tsv";
            Dictionary<string, string> candNormal = new Dictionary<string, string>();
            candidateNameNormalized(idealSlotFile, candNormal);

            HashSet<string> needSlot = new HashSet<string>(new string[2] { "<election.candidate.highconf>", "<election.bpiissue>" });
           // Dictionary<string, List<string>> autoProc = new Dictionary<string, List<string>>();
            Dictionary<string, string> slotToIdeal = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(infile);
            string line;
            int bpos = "qpv2tkn-".Length;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string key = arr[0], value = arr[1];
                string[] valueArr = value.Split(';');
               
                if(needSlot.Contains(valueArr[0]))
                {
                    key = key.Substring(bpos);
                    string slot = valueArr[1];
                    if(slot.Contains("-"))
                    {
                        if(!candNormal.ContainsKey(slot))
                        {
                            Console.WriteLine(line) ;
                        }
                        else
                        {
                            slot = candNormal[slot];
                        }
                    }
                    slotToIdeal[key] = slot;
                }
            }

            StreamWriter sw = new StreamWriter(@"D:\sumStoneTemplate\electionqas\lexicon\tokenizer.txt");
            foreach(KeyValuePair<string, string> slotMapPair in slotToIdeal)
            {
                sw.WriteLine(string.Format("^{0}$\t{1}", slotMapPair.Key, slotMapPair.Value));
            }
            sw.Close();
            Console.ReadKey();
        }
        
        public static void candidateNameNormalized(string idealSlotFile, Dictionary<string, string> candNormal)
        {
            StreamReader sr = new StreamReader(idealSlotFile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                {
                    Console.WriteLine(line);
                }
                candNormal[arr[0]] = arr[1];
            }
            sr.Close();
        }

        public static void GenerateCandidatePoliticalToken(string alltokenfile, string candidatelexiconfile, string politicallexiconfile)
        {
            //read candidate and political view into set
            StreamReader sr = new StreamReader(alltokenfile);
            string line;
            HashSet<string> candidateSet = new HashSet<string>();
            HashSet<string> bpiSet = new HashSet<string>();
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string[] tags = arr[1].Split(';');
                if(tags[0] == "<election.candidate.highconf>")
                {
                    string value = arr[0].Substring("qpv2tkn-".Length);
                    candidateSet.Add(value);
                }
                else if(tags[0] == "<election.bpiissue>")
                {
                    string value = arr[0].Substring("qpv2tkn-".Length);
                    bpiSet.Add(value);
                }
            }
            sr.Close();

            // store tokenvalues into specified lexicon file.
            StreamWriter sw = new StreamWriter(candidatelexiconfile);
            foreach(string cand in candidateSet)
            {
                sw.WriteLine(cand);
            }
            sw.Close();

            sw = new StreamWriter(politicallexiconfile);
            foreach (string poi in bpiSet)
            {
                sw.WriteLine(poi);
            }
            sw.Close();

        }
    }
}
