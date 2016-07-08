using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using TSVUtility;

//AddRegexMatchFeature.exe {in:ExtractionTSV|ExtractionGZ:ExtractionInput} {in:GenericTSV:RegexInput} {out:ExtractionGZ:ExtractionOutput} (TargetColumn) (FeatureName)

namespace ElectionImprove.BoJiaPipeline
{
    class AddRegexMatchFeature
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[5];
                args[0] = @"D:\demo\ExtractionInput.tsv.gz";
                args[1] = @"D:\demo\regexInput.tsv";
                args[2] = @"D:\demo\output.tsv";
                args[3] = "m:Title";
                args[4] = "GuardingScore_Title";
            }
            string extractionInput = args[0];
            string regexInput = args[1];
            string output = args[2];
            string targetColumn = args[3];
            string featureName = args[4];

            Dictionary<string, int> regexDict = new Dictionary<string, int>();
            StreamReader regexReader = new StreamReader(regexInput);
            while (!regexReader.EndOfStream)
            {
                string[] lineArray = regexReader.ReadLine().Split('\t');
                string key;
                int value;
                if (lineArray.Length == 1)
                {
                    key = lineArray[0];
                    value = 1;
                }
                else if (lineArray.Length == 2)
                {
                    key = lineArray[0];
                    value = Convert.ToInt32(lineArray[1]);
                }
                else
                {
                    continue;
                }
                regexDict[key] = Convert.ToInt32(value);
            }
            regexReader.Close();

            using (StreamReader extractionFileStreamReader = new StreamReader(TSVFile.OpenInputTSVStream(extractionInput)))
            using (StreamWriter outputExtraction = new StreamWriter(TSVFile.OpenOutputTSVStream(output, true)))
            {
                TSVReader extractionReader = new TSVReader(extractionFileStreamReader, true);

                outputExtraction.WriteLine(extractionReader.GetHeaderLine() + "\t" + featureName);

                while (!extractionReader.EndOfTSV)
                {
                    TSVLine line = extractionReader.ReadLine();
                    string text = line.GetFeatureValueString(targetColumn);

                    int newFeatureValue = 0;
                    foreach (string regex in regexDict.Keys)
                    {
                        if (Regex.IsMatch(text, regex, RegexOptions.IgnoreCase))
                        {
                            newFeatureValue = regexDict[regex];
                            break;
                        }
                    }
                    outputExtraction.WriteLine(line.GetWholeLineString() + "\t" + newFeatureValue);
                }
            }
        }
    }
}