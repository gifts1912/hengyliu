using QU.Miscs.MagicQ;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs.Common
{
    /// <summary>
    /// Decision tree for maximizing the Homo Average
    /// </summary>
    class HomoAvgDecisionTree
    {
        public class TreeNode
        {
            public Dictionary<string, MovieCandidateFeature> data 
                = new Dictionary<string, MovieCandidateFeature>();
            public string rule;
            public TreeNode next;
            public TreeNode prev;
            public double homoAvg;
        }

        public TreeNode Root = null;

        public void BuildTree(Dictionary<string, MovieCandidateFeature> data, 
            HashSet<string> truth, double splitRatio, 
            List<string> selectedFeatures, 
            int seed, 
            double minPrecision,
            double minRecall)
        {
            Dictionary<string, MovieCandidateFeature> training = new Dictionary<string, MovieCandidateFeature>();
            Dictionary<string, MovieCandidateFeature> testing = new Dictionary<string, MovieCandidateFeature>();
            Random random = new Random(seed);
            foreach (var f in data)
            {
                if (random.NextDouble() <= splitRatio)
                    training[f.Key] = f.Value;
                else
                    testing[f.Key] = f.Value;
            }

            minRecall *= splitRatio;

            List<string[]> rules = EnumerateRules(selectedFeatures);
            MovieExpressionEvaluator evaluator = null;
            int allTruthCnt = truth.Count;

            TreeNode root = new TreeNode() { data = training, prev = null };
            TreeNode curr = root;
            int layer = 0;

            double maxHomoAvg = 1;
            while (maxHomoAvg > 0)
            {
                maxHomoAvg = -1;

                int currFeatureIdx = -1, currRuleIdx = -1;
                for (int i = 0; i < rules.Count; i++)
                {
                    for (int j = 0; j < rules[i].Length; j++)
                    {
                        RuleResult result = new RuleResult();
                        string expression = string.Format("({0}) ? 1 : 0", rules[i][j]);
                        evaluator = MovieExpressionEvaluator.ParseExpression(expression);
                        Dictionary<string, double> dictMovie2Score
                            = MovieRuleTuning.EvaluateMovieCandidates(evaluator, curr.data);

                        result.predRight = (from p in dictMovie2Score where p.Value > 0 select p).Count();
                        result.actualRight = (from p in dictMovie2Score where (p.Value > 0 && truth.Contains(p.Key)) select p).Count();
                        result.precision = (double)result.actualRight / result.predRight;
                        result.recall = (double)result.actualRight / allTruthCnt;
                        result.allTruthCnt = allTruthCnt;
                        result.homoAverage = 2.0 / (1.0 / result.precision + 1.0 / result.recall);
                        result.rule = rules[i][j];

                        if (result.precision < (1.0 + 0.1 * layer) * minPrecision 
                            || result.recall < minRecall)
                        {
                            continue;
                        }

                        if (result.recall > maxHomoAvg)
                        {
                            maxHomoAvg = result.recall;
                            currFeatureIdx = i;
                            currRuleIdx = j;
                            curr.rule = result.rule;
                            curr.next = new TreeNode();
                            curr.next.prev = curr;
                            //curr.data = (from p in training where dictMovie2Score.ContainsKey(p.Key) && dictMovie2Score[p.Key] > 0 select p);
                            curr.next.data = new Dictionary<string, MovieCandidateFeature>();
                            foreach (var p in curr.data)
                            {
                                if (dictMovie2Score.ContainsKey(p.Key) && dictMovie2Score[p.Key] > 0)
                                {
                                    curr.next.data[p.Key] = p.Value;
                                }
                            }
                            curr.next.homoAvg = result.homoAverage;
                        }
                    }
                }

                if (currFeatureIdx >= 0 && currRuleIdx >= 0)
                {
                    rules.RemoveAt(currFeatureIdx);
                    curr = curr.next;
                    layer++;
                }
            }

            this.Root = root;
        }


        static string[] BuildFeatureArr(string featureName, int range, bool asc)
        {
            string oper = asc ? ">=" : "<=";
            string[] arr = new string[range];
            for (int i = 0; i < range; i++)
            {
                arr[i] = string.Format("f.{0}{1}{2}", featureName, oper, i + 1);
            }
            return arr;
        }

        static List<string[]> EnumerateRules(IEnumerable<string> selectedFeatures)
        {
            // valid features:
            //   f.ProdTopWebPos:-, f.ProdWebOcc:+, f.ProdHasQAFact:+, f.ProdTopQAPos:-
            //   f.ProdQAOcc:+, f.ImdbTopPos:-, f.ImdbOcc:+, f.ApfTopPos:-, f.ApfOcc:+, f.GSTopPos:-

            HashSet<string> setFeatures = new HashSet<string>(selectedFeatures);

            List<string[]> andFeatures = new List<string[]>();
            if (setFeatures.Contains("ProdTopWebPos"))
                andFeatures.Add(BuildFeatureArr("ProdTopWebPos", 20, false));
            if (setFeatures.Contains("ProdWebOcc"))
                andFeatures.Add(BuildFeatureArr("ProdWebOcc", 5, true));
            if (setFeatures.Contains("ProdTopQAPos"))
                andFeatures.Add(BuildFeatureArr("ProdTopQAPos", 20, false));
            if (setFeatures.Contains("ProdQAOcc"))
                andFeatures.Add(BuildFeatureArr("ProdQAOcc", 5, true));
            if (setFeatures.Contains("ImdbTopPos"))
                andFeatures.Add(BuildFeatureArr("ImdbTopPos", 20, false));
            if (setFeatures.Contains("ImdbOcc"))
                andFeatures.Add(BuildFeatureArr("ImdbOcc", 5, true));
            if (setFeatures.Contains("ApfTopPos"))
                andFeatures.Add(BuildFeatureArr("ApfTopPos", 20, false));
            if (setFeatures.Contains("ApfOcc"))
                andFeatures.Add(BuildFeatureArr("ApfOcc", 5, true));
            if (setFeatures.Contains("GSTopPos"))
                andFeatures.Add(BuildFeatureArr("GSTopPos", 5, false));

            return andFeatures;
        }
    }
}
