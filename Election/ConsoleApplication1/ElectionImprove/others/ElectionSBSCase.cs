using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.others
{
    class ElectionSBSCase
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                //args[0] = @"D:\demo\LogSBSGVSBB.tsv";
                //args[1] = @"D:\demo\LogSBSGVSBA.tsv";
                args[0] = @"D:\demo\Base909GVSBB.tsv";
                args[1] = @"D:\demo\Base909GVSBA.tsv";
                args[2] = @"D:\demo\ImproveQuerySet.tsv";
            }
            string gvsbbFile = args[0];
            string gvsbaFile = args[1];
            string improveFile = args[2];
            GenerateImporveQuerySet(gvsbbFile, gvsbaFile, improveFile);
        }
        public static void GenerateImporveQuerySet(string gvsbbFile, string gvsbaFile, string improveFile)
        {
            HashSet<string> beforeLoss = new HashSet<string>();
            HashSet<string> afterWin = new HashSet<string>();
            StreamReader sr = new StreamReader(gvsbbFile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                int score = int.Parse(arr[5]);
                if(score <= -1)
                {
                    beforeLoss.Add(arr[1]);
                }
            }
            sr.Close();


            sr = new StreamReader(gvsbaFile);
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                int score = int.Parse(arr[5]);
                if(score >= 1)
                {
                    afterWin.Add(arr[1]);
                }
            }
            sr.Close();

            HashSet<string> impHs = new HashSet<string>(beforeLoss);
            impHs.IntersectWith(afterWin);

            StreamWriter sw = new StreamWriter(improveFile);
            foreach (string ele in impHs)
            {
                sw.WriteLine(ele);
            }
            sw.Close();
        }
    }
}
