using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.MachineLearning;

namespace QU.Miscs
{
    class Clustering
    {
        public static List<ClusterResultIndexes> AffinityPropagationCluster(double[,] similarities, int dim)
        {
            double avgSim = AverageSimilarity(similarities);

            return AffinityPropagationCluster(similarities, dim, avgSim);
        }

        public static List<ClusterResultIndexes> AgglomerativeCluster(double[,] similarities, int dim, double threshold)
        {
            List<ClusterResultIndexes> clusters = new List<ClusterResultIndexes>();
            for (int i = 0; i < dim; i++)
            {
                bool hasMatchedCluster = false;
                for (int j = 0; j < clusters.Count; j++)
                {
                    bool hasSimilarUrlInCluster = false;
                    foreach (int index in clusters[j].indexes)
                    {
                        if (index == i)
                            continue;
                        if (similarities[i, index] >= threshold)
                        {
                            hasSimilarUrlInCluster = true;
                            break;
                        }
                    }

                    if (hasSimilarUrlInCluster)
                    {
                        clusters[j].indexes.Add(i);
                        hasMatchedCluster = true;
                        break;
                    }
                }

                if (!hasMatchedCluster)
                {
                    ClusterResultIndexes result = new ClusterResultIndexes();
                    result.indexes.Add(i);
                    clusters.Add(result);
                }
            }

            return clusters;
        }

        public static List<ClusterResultIndexes> AgglomerativeCluster(SparseSquareMatrix<double> similarities, int dim, double threshold)
        {
            List<ClusterResultIndexes> clusters = new List<ClusterResultIndexes>();
            for (int i = 0; i < dim; i++)
            {
                bool hasMatchedCluster = false;
                for (int j = 0; j < clusters.Count; j++)
                {
                    bool hasSimilarUrlInCluster = false;
                    foreach (int index in clusters[j].indexes)
                    {
                        if (index == i)
                            continue;
                        if (similarities[i][index] >= threshold)
                        {
                            hasSimilarUrlInCluster = true;
                            break;
                        }
                    }

                    if (hasSimilarUrlInCluster)
                    {
                        clusters[j].indexes.Add(i);
                        hasMatchedCluster = true;
                        break;
                    }
                }

                if (!hasMatchedCluster)
                {
                    ClusterResultIndexes result = new ClusterResultIndexes();
                    result.indexes.Add(i);
                    clusters.Add(result);
                }
            }

            return clusters;
        }

        public static List<ClusterResultIndexes> AffinityPropagationCluster(double[,] similarities, int dim, double preference)
        {
            APCluster.Net.Cluster algo = new APCluster.Net.Cluster();
            APCluster.Net.APOption option = new APCluster.Net.APOption();
            option.lambda = 0.7;
            option.minimum_iterations = 1;
            option.maximum_iterations = 300;
            option.converge_iterations = 15;
            option.nonoise = 0;

            VisualSummarization.Analysis.SimilarityMatrix matrix = new VisualSummarization.Analysis.SimilarityMatrix(dim);
            matrix.matrix = similarities;

            double[] preferences = new double[matrix.dim];
            for (int i = 0; i < matrix.dim; i++)
                preferences[i] = preference;

            List<APCluster.Net.ClusterResult> results = new List<APCluster.Net.ClusterResult>();
            algo.Clustering(option, matrix, preferences, results);

            var sorted = from r in results
                         orderby r.score descending
                         select r;

            List<ClusterResultIndexes> indexes = new List<ClusterResultIndexes>();
            foreach (APCluster.Net.ClusterResult result in sorted)
            {
                ClusterResultIndexes temp = new ClusterResultIndexes();
                temp.indexes.AddRange(result.children);
                indexes.Add(temp);
            }

            return indexes;
        }

        private static double AverageSimilarity(double[,] simMatrix)
        {
            double avg = 0;
            int cnt = 0;

            for (int i = 0; i < simMatrix.GetLength(0); i++)
                for (int j = 0; j < simMatrix.GetLength(1); j++)
                {
                    if (j != i)
                    {
                        avg += simMatrix[i, j];
                        cnt++;
                    }
                }
            return avg / cnt;
        }

        public class ClusterResultIndexes
        {
            public List<int> indexes;

            public ClusterResultIndexes()
            {
                indexes = new List<int>();
            }
        }

        public class ClusterResult
        {
            public int clusterID;
            public Dictionary<string, int> dictUrl2Clicks = new Dictionary<string, int>();
            public Dictionary<string, int> dictKeyword2Freq = new Dictionary<string, int>();
        }
    }
}
