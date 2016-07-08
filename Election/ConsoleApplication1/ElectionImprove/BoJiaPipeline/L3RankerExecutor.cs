using ExecutorLibrary;
using ExtractionData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ElectionImprove.BoJiaPipeline
{
    public class L3RankerExecutor
    {
        public static bool CheckFileExistence(string file)
        {
            if (!File.Exists(file))
                throw new IOException("File doesn't exist " + file);
            return true;
        }

        public static void CalculateScore(string extractionData, string l3Ranker, string output, bool checkMissingFeature)
        {
            CheckFileExistence(extractionData);
            CheckFileExistence(l3Ranker);
            using (StreamWriter streamWriter = new StreamWriter((Stream)new FileStream(output, FileMode.Create, FileAccess.ReadWrite)))
            {
                Ranker ranker = new Ranker(l3Ranker);
                ExtractionTSVReader extractionTsvReader = new ExtractionTSVReader(extractionData);
                if (checkMissingFeature)
                    ExtractionTSVReader.AssertMissingColumns(extractionTsvReader.FeatureColumns(), ranker.FeaturesUsed());
                string[] strArray = new string[12]
                {
        "m:QueryId",
        "m:DocId",
        "m:Rating",
        "m:RawQuery",
        "m:Url",
        "m:Market",
        "m:Tier",
        "m:Query",
        "m:OldPosition",
        "m:NewPosition",
        "m:L3Score",
        "m:NewL3Score"
                };
                string str1 = string.Join("\t", strArray);
                streamWriter.WriteLine(str1);
                foreach (QueryBlock queryBlock in extractionTsvReader.GetQueryBlocks())
                {
                    QueryBlock block = queryBlock;
                    ranker.EvaluateAndUpdateQueryBlock(block, false);
                    for (int i = 0; i < block.Documents.Count; ++i)
                    {
                        string str2 = string.Join("\t", Enumerable.Select<string, string>((IEnumerable<string>)strArray, (Func<string, string>)(x =>
                        {
                            string metadata = "";
                            block.Documents[i].GetMetadata(x, out metadata);
                            return metadata;
                        })));
                        streamWriter.WriteLine(str2);
                    }
                }
            }
        }

        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\demo\input1.tsv";
                args[1] = @"D:\demo\input2";
                args[2] = @"D:\demo\ranking.tsv";
                args[3] = "true";
            }
            CalculateScore(args[0], args[1], args[2], bool.Parse(args[3]));
        }
    }
}
