using Microsoft.TMSN.CommandLine;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using MyMisc = Utility;
using ML = Utility.MachineLearning;

namespace QU.Miscs
{
    class ClusterPatterns
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "p")]
            public string PatternFile = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "f")]
            public string FeatureFile = "";

            [Argument(ArgumentType.Required, ShortName = "o")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "h")]
            public int WithHeader = 0;

            [Argument(ArgumentType.AtMostOnce, ShortName = "s")]
            public double MinSim = 20;

            public bool InputValid { get { return File.Exists(PatternFile); } }
        }

        static double ScoringPattern(ReformulationPattern pattern, ReformulationFeatures features)
        {
            if (null == features)
            {
                return 0;
            }

            return features.FloatClickCoverage
                //* pattern.L2R
                ;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments) || !arguments.InputValid)
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            Dictionary<string, ReformulationFeatures> dictPatterns2Features
                = new Dictionary<string, ReformulationFeatures>();
            if (File.Exists(arguments.FeatureFile))
            {
                Console.WriteLine("Read Features");
                Extensions.ReadFeatureFile(arguments.FeatureFile, arguments.WithHeader > 0, ref dictPatterns2Features);
            }

            Dictionary<string, int> index = new Dictionary<string, int>();
            var listLeftPatterns = new List<string>();
            List<Tuple<ReformulationPattern, ReformulationFeatures>> patterns = new List<Tuple<ReformulationPattern, ReformulationFeatures>>();

            char[] sep = new char[] {' ', '*'};
            using (StreamReader sr = new StreamReader(arguments.PatternFile))
            {
                int id = 0;
                string line;

                Console.WriteLine("Read Patterns");
                while ((line = sr.ReadLine()) != null)
                {
                    ReformulationPattern p = ReformulationPattern.ReadFromLineWith5Items(line);
                    if (p == null)
                        continue;

                    ReformulationFeatures features;
                    if (!dictPatterns2Features.TryGetValue(ReformulationPatternMatch.MakePair(p.Left, p.Right), out features)
                    || features.FloatClickCoverage < 0.2)
                    {
                        continue;
                    }

                    if (p.Right.Split(sep, StringSplitOptions.RemoveEmptyEntries).Length == 0)
                    {
                        continue;
                    }

                    if (!index.ContainsKey(p.Left))
                    {
                        index.Add(p.Left, id++);
                        listLeftPatterns.Add(p.Left);
                    }

                    if (!index.ContainsKey(p.Right))
                    {
                        index.Add(p.Right, id++);
                        listLeftPatterns.Add(p.Right);
                    }

                    patterns.Add(new Tuple<ReformulationPattern, ReformulationFeatures>(p, features));
                }
            }

            string[] rIndex = listLeftPatterns.ToArray();

            Console.WriteLine("Build matrix: {0}x{1}", index.Count, index.Count);
            ML.SparseSquareMatrix<double> matrix = new ML.SparseSquareMatrix<double>(index.Count);
            foreach (var p in patterns)
            {
                ReformulationFeatures features = p.Item2;
                matrix[index[p.Item1.Left]][index[p.Item1.Right]] = ScoringPattern(p.Item1, features);
            }

            Console.WriteLine("Cluster");
            var results = Clustering.AgglomerativeCluster(matrix, index.Count, arguments.MinSim);
            //ML.AffinityPropagationClustering apCluster = new ML.AffinityPropagationClustering(0.7, 10, 300);
            //int[] assignments = apCluster.RunSparse(matrix);
            Console.WriteLine("Dump");

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                //for (int i = 0; i < assignments.Length; i++)
                //{
                //    sw.WriteLine(rIndex[i] + "\t" + rIndex[assignments[i]]);
                //}

                foreach (var r in results)
                {
                    sw.WriteLine(string.Join("\t", r.indexes.Select(i => rIndex[i])));
                }
            }
        }
    }
}
