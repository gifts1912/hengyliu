using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using QU.Utility;
using System.IO;

namespace QU.Miscs
{
    public static class Extensions
    {
        public static double GetDoubleFeature(this TSVLine line, string featureName, int optionalIdx)
        {
            try
            {
                string str = line.GetFeatureValueString(featureName);
                return double.Parse(str);
            }
            catch
            {
                try
                {
                    string str = line[optionalIdx];
                    return double.Parse(str);
                }
                catch
                {
                    return 0.0;
                }
            }
        }

        public static double GetDoubleFeature(this TSVLine line, bool withHeader, string featureName, int optionalIdx)
        {
            try
            {
                if (withHeader)
                {
                    return double.Parse(line.GetFeatureValueString(featureName));
                }
                else
                {
                    return double.Parse(line[optionalIdx]);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static string GetString(this TSVLine line, string featureName, int optionalIdx)
        {
            try
            {
                string str = line.GetFeatureValueString(featureName);
                return str;
            }
            catch
            {
                try
                {
                    string str = line[optionalIdx];
                    return str;
                }
                catch
                {
                    return "";
                }
            }
        }

        public static string GetString(this TSVLine line, bool withHeader, string featureName, int optionalIdx)
        {
            try
            {
                if (withHeader)
                {
                    return line.GetFeatureValueString(featureName);
                }
                else
                {
                    return line[optionalIdx];
                }
            }
            catch
            {
                return "";
            }
        }

        public static void ReadFeatureFile(string file, bool withHeader, ref Dictionary<string, ReformulationFeatures> dictPatterns2Prec)
        {
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(file)))
            {
                TSVReader tsvReader = new TSVReader(sr, withHeader);

                TSVLine line;
                while ((line = tsvReader.ReadLine()) != null)
                {
                    ReformulationFeatures features = new ReformulationFeatures();
                    features.BinaryClickCoverage = line.GetDoubleFeature(withHeader, "BinaryClickCoverage", 2);
                    features.FloatClickCoverage = line.GetDoubleFeature(withHeader, "FloatClickCoverage", 3);
                    features.BinarySatClickCoverage = line.GetDoubleFeature(withHeader, "BinarySatClickCoverage", 4);
                    features.FloatSatClickCoverage = line.GetDoubleFeature(withHeader, "FloatSatClickCoverage", 5);
                    features.ClickRatio = line.GetDoubleFeature(withHeader, "ClickRatio", 6);
                    features.SatClickRatio = line.GetDoubleFeature(withHeader, "SatClickRatio", 7);
                    features.ClickYield = line.GetDoubleFeature(withHeader, "ClickYield", 8);
                    features.SatClickYield = line.GetDoubleFeature(withHeader, "SatClickYield", 9);

                    dictPatterns2Prec[ReformulationPatternMatch.MakePair(line.GetString(withHeader, "Query", 0), line.GetString(withHeader, "AlteredQuery", 1))] = features;
                }
            }
        }

        public static List<ReformulationPattern> ReadPatterns(string file, int itemCnt = 5)
        {
            List<ReformulationPattern> patterns
                = new List<ReformulationPattern>();

            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    ReformulationPattern p = null; 
                    if (itemCnt == 3)
                        p = ReformulationPattern.ReadFromLineWith3Items(line);
                    else
                        p = ReformulationPattern.ReadFromLineWith5Items(line);
                    if (p == null)
                        continue;

                    // remove all of the wild-match.
                    if (p.Left.Split(new char[] { ' ', '*' }, StringSplitOptions.RemoveEmptyEntries).Length < 1)
                        continue;

                    patterns.Add(p);
                }
            }

            return patterns;
        }

        public static void GetFeatures(this List<ReformulationPattern> patterns, string featureFile, bool hasHeader)
        {
            Dictionary<string, ReformulationFeatures> dictPatterns2Features
                = new Dictionary<string, ReformulationFeatures>();
            if (File.Exists(featureFile))
            {
                Console.WriteLine("Read Features");
                ReadFeatureFile(featureFile, hasHeader, ref dictPatterns2Features);
            }

            Dictionary<string, double> dictLeftP2Occ = new Dictionary<string, double>();

            char[] sep = new char[] { ' ', '*' };
            foreach (var p in patterns)
            {
                if (p == null)
                    continue;

                ReformulationFeatures features;
                if (!dictPatterns2Features.TryGetValue(ReformulationPatternMatch.MakePair(p.Left, p.Right), out features))
                {
                    continue;
                }

                ReformulationPatternMatch.ExtractReformulationFeatures(p.Left, p, dictPatterns2Features);

                if (dictLeftP2Occ.ContainsKey(p.Left))
                    dictLeftP2Occ[p.Left] += p.L2R;
                else
                    dictLeftP2Occ[p.Left] = p.L2R;

                p.Features = features;
            }

            foreach (var p in patterns)
            {
                if (p.Features != null)
                    p.Features.L2RPercent = p.L2R / dictLeftP2Occ[p.Left];
            }
        }

        static char[] WildAndSpaceSeperator = new char[] { ' ', '*' };
        public static void UpdateFeature(this ReformulationPattern pattern, string queryWithSlots)
        {
            double querySlotTermCnt = queryWithSlots.Split(WildAndSpaceSeperator, StringSplitOptions.RemoveEmptyEntries).Length;
            double matchedTerms = pattern.Left.Split(WildAndSpaceSeperator, StringSplitOptions.RemoveEmptyEntries).Length;
            if (pattern.Features != null)
                pattern.Features.WildcardMatchtedTerms = Math.Max(0, (int)(querySlotTermCnt - matchedTerms));
        }
    }
}
