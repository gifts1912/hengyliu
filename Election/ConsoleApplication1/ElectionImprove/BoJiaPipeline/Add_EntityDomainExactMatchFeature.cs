using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using BojiaUtilities;
using QU.Utility;
using System.IO;

//Add_EntityDomainExactMatchFeature.exe {in:ExtractionTSV|ExtractionGZ:ExtractionInput} {out:ExtractionGZ:ExtractionOutput} (SlotType) (FeatureName)

namespace ElectionImprove.BoJiaPipeline
{
    class Add_EntityDomainExactMatchFeature
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\demo\ExtractionInput.tsv.gz";
                args[1] = @"D:\demo\ExtractionOutput.tsv.gz";
                args[2] = "Ent_Ent";
                args[3] = "EntityExactMatch_UrlDomain";
            }
            string input = args[0];
            string output = args[1];
            string slotType = args[2];
            string featureName = args[3];

            using (StreamReader extractionFileStreamReader = new StreamReader(TSVFile.OpenInputTSVStream(input)))
            using (StreamWriter outputExtraction = new StreamWriter(TSVFile.OpenOutputTSVStream(output, true)))
            {
                TSVReader extractionReader = new TSVReader(extractionFileStreamReader, true);

                outputExtraction.WriteLine(featureName + "\t" + extractionReader.GetHeaderLine());

                while (!extractionReader.EndOfTSV)
                {
                    TSVLine line = extractionReader.ReadLine();

                    string tags = line.GetFeatureValueString("m:Tags");
                    var results = CRFOutputParser.ParseResults(tags);
                    List<string> entityList = new List<string>();
                    foreach (var result in results)
                    {
                        if (result.Type.Split(',').Contains(slotType))
                        {
                            entityList.Add(result.Span);
                        }
                    }

                    bool isMatch = false;
                    string url = line.GetFeatureValueString("m:Url");
                    string domain = BojiaUtilities.UrlUtility.GetDomain(BojiaUtilities.UrlUtility.GetNormalizeUrl(url)).Replace(".", " ");
                    foreach (string entity in entityList)
                    {
                        if ((" " + domain + " ").Contains(" " + entity + " "))
                        {
                            isMatch = true;
                            break;
                        }
                        if ((" " + domain + " ").Contains(" " + entity.Replace(" ", "") + " "))
                        {
                            isMatch = true;
                            break;
                        }
                    }
                    string outputLine = "";
                    if (isMatch)
                    {
                        outputLine = "1000\t";
                    }
                    else
                    {
                        outputLine = "0\t";
                    }
                    outputLine += line.GetWholeLineString();
                    outputExtraction.WriteLine(outputLine);
                }
            }
        }
    }
}
