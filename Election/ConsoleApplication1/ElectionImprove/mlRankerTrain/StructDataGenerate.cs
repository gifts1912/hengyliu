using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectionImprove.mlRankerTrain
{
    public class StructDataGenerate
    {
        
        private static string keyFea = "m:Query";
        private static StreamWriter LogWriter;
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[5];
                args[0] = @"D:\demo\trainDataReranking.tsv";
                args[1] = @"D:\demo\QueryFeatures.tsv";
                args[2] = @"D:\demo\trainStructData.tsv";
                args[3] = "TopSiteLabelAuth;matchConstraintUrlFea;matchConstraintTitleFea;matchConstraintSnippetFea;matchIntentUrlFea;matchIntentTitleFea;matchIntentSnippetFea;matchEntityUrlFea;matchEntityTitleFea;matchEntitySnippetFea;SortedPosition;DRScore;UrlDepth";
                args[4] = "m:Query;m:Url";
            }

            string queryFile = args[0];
            string feasFile = args[1];
            string outPut = args[2];
            string featuresExtract = args[3];
            string featuresKey = args[4]; 
            
            LogWriter = new StreamWriter(@"D:\demo\log.tsv");

            GenerateStructData(queryFile, feasFile, outPut, featuresExtract, featuresKey);

            SVMLightFileFormat(outPut, @"D:\demo\trainStructSVMLight.txt");

            LogWriter.Close();
        }

        public static void SVMLightFileFormat(string input, string output)
        {
            StreamReader sr = new StreamReader(input);
            StreamWriter sw = new StreamWriter(output);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 14)
                    continue;
                int num = arr.Length;
                string classLabel = arr[num - 1];
                StringBuilder sb = new StringBuilder();
                sb.Append(classLabel);
                for(int i = 0; i < num - 1; i++)
                {
                    sb.Append(" ");
                    sb.Append(string.Format("{0}:{1}", i + 1, arr[i]));
                }
                sw.WriteLine(sb.ToString());
            }
            sr.Close();
            sw.Close();
        }

        public static void GenerateStructData(string queryFile, string feasFile, string outPut, string featuresExtract, string featuresKey)
        {
            Dictionary<string, int> queryUrlScore = new Dictionary<string, int>();
            LoadQueryToTrain(queryFile, ref queryUrlScore);

            List<int> feaIdx = new List<int>();
            List<int> keyIdx = new List<int>();
            Dictionary<string, int> featuresTarget = new Dictionary<string, int>();

            StreamReader sr = new StreamReader(feasFile);
            string line, headLine;
            headLine = sr.ReadLine();
            GenerateColumnIdexByColumnName(headLine, featuresExtract, ref feaIdx);
            GenerateColumnIdexByColumnName(headLine, featuresKey, ref keyIdx);
            int normalizeUrlColumn = (new List<string>(headLine.Split('\t'))).IndexOf("m:Url");

            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string feaValue = ExtractColumnsValue(arr, feaIdx, normalizeUrlColumn); 
                string keyValue = ExtractColumnsValue(arr, keyIdx, normalizeUrlColumn); 
                if(queryUrlScore.ContainsKey(keyValue))
                {
                    featuresTarget[feaValue] = queryUrlScore[keyValue];
                }
                else
                {
                    LogWriter.WriteLine(String.Format("Not Exists: {0}", keyValue));
                }
            }
            sr.Close();

            StoreStructTrain(featuresTarget, outPut);
        }

        public static void StoreStructTrain(Dictionary<string, int> featuresTarget, string output)
        {
            StreamWriter sw = new StreamWriter(output);
            foreach(KeyValuePair<string, int> pair in featuresTarget)
            {
                sw.WriteLine(pair.Key + "\t" + pair.Value);
            }
            sw.Close();
        }
        public static string ExtractColumnsValue(string [] arr, List<int> feaIdx, int normalizeUrlColumn)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach(int idx in feaIdx)
            {
                string value = arr[idx];
                if(string.IsNullOrEmpty(value))
                {
                    value = "0";
                }
                if(idx == normalizeUrlColumn)
                {
                    value = FrontEndUtil.CURLUtilities.GetHutNormalizeUrl(value);
                }
                if(first)
                {
                    first = false;
                }
                else
                {
                    sb.Append("\t");
                }
                sb.Append(value);
            }
            return sb.ToString();
        }
        public static void GenerateColumnIdexByColumnName(string headLine, string featuresExtract, ref List<int> feaIdx)
        {
            feaIdx.Clear();
            List<string> feaArr = new List<string>(featuresExtract.Split(';'));
            List<string> arr = new List<string>(headLine.Split('\t'));
            foreach (string fea in feaArr)
            {
                int curIdx = arr.IndexOf(fea);
                if (curIdx == -1)
                {
                    Console.WriteLine(string.Format("Error: feature {0} couldn't found!", fea));
                    Console.ReadKey();
                    return;
                }
                feaIdx.Add(curIdx);
            }
        }

        public static void LoadQueryToTrain(string queryFile, ref Dictionary<string, int> queryUrlScore)
        {
            StreamReader sr = new StreamReader(queryFile);
            string line, query, url;
            int score;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 3)
                    continue;
                query = arr[0];
                url = arr[1];
                url = FrontEndUtil.CURLUtilities.GetHutNormalizeUrl(url);
                score = int.Parse(arr[2]);
                string key = string.Format("{0}\t{1}", query, url);
                queryUrlScore[key] = score;
            }
            sr.Close();
        }
    }
}
