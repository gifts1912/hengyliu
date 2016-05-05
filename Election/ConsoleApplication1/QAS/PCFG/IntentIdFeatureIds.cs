using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace QAS.PCFG
{
    class IntentIdFeatureIds
    {
        private static List<string> legalList = new List<string>();
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[2];
                args[0] = @"D:\sumStoneTemplate\pcfg\MSElection\Query2IntentIdFeatureIds.output.txt";
                args[1] = @"D:\Project\Election\TokenAndRules\IntentSlotPatternIdexFile.tsv";
            }
            string patIdxFile = args[1];
            string itentFeatureIdFile = args[0];

            IntentIdFeatureIdGen(itentFeatureIdFile, patIdxFile);
           // StayTokeCheck(patIdxFile);
        }

        public static void StayTokeCheck(string patIdxFile)
        {
            HashSet<string> hs = new HashSet<string>();
            using (StreamReader sr = new StreamReader(patIdxFile))
            {
                Regex rgx = new Regex("[\\[\\]]");
                string line;
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    if (arr.Length != 4)
                        continue;
                    string newSlotPat = arr[2].Trim('-');
                    newSlotPat = rgx.Replace(newSlotPat, "");
                    string[] newSlotPatArr = newSlotPat.Split();
                    foreach(string ele in newSlotPatArr)
                    {
                        if(ele.Contains("."))
                        {
                            hs.Add(ele);
                        }
                    }
                }
            }

            foreach(string ele in hs)
            {
                Console.WriteLine(ele);
            }
            Console.ReadKey();
        }
        public static void IntentIdFeatureIdGen(string outfile, string patIdxFile)
        {
            StreamWriter sw = new StreamWriter(outfile);
            using (StreamReader sr = new StreamReader(patIdxFile))
            {
                Regex rgx = new Regex("[\\[\\]]");
                string line;
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    if (arr.Length != 4)
                        continue;
                    string newSlotPat = arr[2];
                    newSlotPat = rgx.Replace(newSlotPat, "");
                    string[] slotPatArr = newSlotPat.Trim().Split();
                    List<string> permutation = new List<string>();
            
                    ExpandSlotPat(slotPatArr, 0, slotPatArr.Length, permutation);
                    foreach (string ele in permutation)
                    {
                        sw.WriteLine("{0}-{1}\tExternalInput3\t0\t1\t{2}\t1\t0\t0", arr[0], ele, arr[3]);
                    }
                }
            }
            sw.Close();
        }

        public static void ExpandSlotPat(string[] arr, int b, int e, List<string> permutation)
        {
            if (b >= e - 1 )
            {
                permutation.Add(string.Join("-", arr));
            }
            else
            {
                for (int i = b; i < e; i++)
                {
                    Swap(ref arr[b], ref arr[i]);
                    ExpandSlotPat(arr, b + 1, e, permutation);
                    Swap(ref arr[b], ref arr[i]);
                }
            }

        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        public static void Swap(string[] arr, int i, int j)
        {
            string str = arr[i];
            arr[i] = arr[j];
            arr[j] = str;
        }
    }
}
