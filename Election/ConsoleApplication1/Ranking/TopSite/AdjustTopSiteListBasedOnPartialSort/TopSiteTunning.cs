using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace Ranking.TopSite.AdjustTopSiteListBasedOnPartialSort.TopSiteTunning
{
    /*
     * Change top site list when contradiction if add on partial url pair.
     */
    public class CycleException : ApplicationException
    {
        public CycleException(string message)
            : base(message)
        {

        }
    }
    class TopSiteTunning
    {
        public static void Visit(string node, Dictionary<string, List<string>> graph, List<string> sorted, Dictionary<string, bool> visited, bool ignoreCycles)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(node, out inProcess);
            if (alreadyVisited)
            {
                if (inProcess && !ignoreCycles)
                {
                    //throw new ArgumentException("Cyclic dependency found.");
                    throw (new CycleException("Cyclic dependency found!"));
                }
            }
            else
            {
                visited[node] = true;
                List<string> dependencies = new List<string>();
                if (graph.ContainsKey(node))
                {
                    dependencies = graph[node];
                    foreach (string depNode in dependencies)
                    {
                        Visit(depNode, graph, sorted, visited, ignoreCycles);
                    }
                }
                visited[node] = false;
                sorted.Add(node);
            }

        }
        public static List<string> TopoSort(Dictionary<string, List<string>> topoList)
        {
            List<string> sorted = new List<string>();
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            List<string> source = (new HashSet<string>(topoList.Keys)).ToList();

            foreach (string node in source)
            {
                Visit(node, topoList, sorted, visited, false);
            }

            return sorted;
        }
        public static void TopoLogistic(string infile, string outfile, double confMinThread = 0.0)
        {
            Dictionary<string, List<string>> UrlDependencies = new Dictionary<string, List<string>>();

            StreamReader sr = new StreamReader(infile);
            StreamWriter sw = new StreamWriter(outfile);
            int queryCol = 0, preCol = 1, posCol = 2, confCol = 3;
            string line, queryPre = null, query, preUrl, posUrl;
            double conf;
            List<string> sortResult = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                query = arr[queryCol];
                preUrl = arr[preCol];
                posUrl = arr[posCol];
                conf = double.Parse(arr[confCol]);
                if (conf < confMinThread)
                {
                    continue;
                }
                if (query != queryPre)
                {

                    if (!string.IsNullOrEmpty(queryPre))
                    {
                        sortResult = TopoSort(UrlDependencies);
                        sw.WriteLine("{0}\t{1}", queryPre, string.Join("\t", sortResult.ToArray()));
                    }
                    queryPre = query;
                    UrlDependencies.Clear();
                }
                if (!UrlDependencies.ContainsKey(preUrl))
                {
                    UrlDependencies[preUrl] = new List<string>();
                }
                UrlDependencies[preUrl].Add(posUrl);

                if (!UrlDependencies.ContainsKey(posUrl))
                {
                    UrlDependencies[posUrl] = new List<string>();
                }
            }
            if (!string.IsNullOrEmpty(queryPre))
            {
                sortResult = TopoSort(UrlDependencies);
                sw.WriteLine("{0}\t{1}", queryPre, string.Join("\t", sortResult.ToArray()));
            }
            sr.Close();
            sw.Close();
        }

        public static void LoadEffectiveQuery(string sbsFile, Dictionary<string,int> effectiveQueryScore, HashSet<string> effectiveQuery, int scoreThread)
        {
            StreamReader sr = new StreamReader(sbsFile);
            string line, query;
            line = sr.ReadLine();
            int queryCol = 1, scoreCol = 5, score;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= queryCol || arr.Length <= scoreCol)
                {
                    continue;
                }
                query = arr[queryCol];
                try
                {
                    score = int.Parse(arr[scoreCol]);
                }
                catch (Exception e)
                {
                    continue;
                }
                /*
                if (score > scoreThread)
                    continue;
                lossQuery.Add(query);
                 */
                if(Math.Abs(score) >= scoreThread)
                {
                    effectiveQueryScore[query] = score;
                    effectiveQuery.Add(query);
                }
            }
            sr.Close();

        }

        public static void LoadQuerySlotPattern(string slotFile, Dictionary<string, string> querySlotPattern, HashSet<string> effectiveQuery)
        {
            StreamReader sr = new StreamReader(slotFile);
            string line, query, slotPattern;
            int queryCol = 0, slotPatternCol = 1;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length <= queryCol || arr.Length <= slotPatternCol)
                    continue;
                query = arr[queryCol];
                if (!effectiveQuery.Contains(query))
                    continue;
                slotPattern = arr[slotPatternCol];
                querySlotPattern[query] = slotPattern;
            }

            sr.Close();
        }

        public static void LoadQueryPartialSort(string partialSortFile, double confThread, Dictionary<string, List<KeyValuePair<string, string>>> queryPartialList, bool filterUrlStart = true)
        {
            StreamReader sr = new StreamReader(partialSortFile);
            HashSet<string> queryHaveStar = new HashSet<string>();
            string line, query, preUrl, posUrl;
            double conf;
            int queryCol = 0, preUrlCol = 1, posUrlCol = 2, confCol = 3;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 4)
                    continue;
                query = arr[queryCol];
                preUrl = arr[preUrlCol];
                posUrl = arr[posUrlCol];
                if (preUrl.Trim().Equals("*"))
                {
                    queryHaveStar.Add(query);
                    continue;
                }
                conf = double.Parse(arr[confCol]);
                if (conf < confThread)
                    continue;
                if (!queryPartialList.ContainsKey(query))
                {
                    queryPartialList[query] = new List<KeyValuePair<string, string>>();
                }
                queryPartialList[query].Add(new KeyValuePair<string, string>(preUrl, posUrl));
            }
            if (filterUrlStart)
            {
                foreach (string queryStar in queryHaveStar)
                {
                    if (queryPartialList.ContainsKey(queryStar))
                    {
                        queryPartialList.Remove(queryStar);
                    }
                }
            }

            sr.Close();
        }

        public static bool HaveCycle(List<KeyValuePair<string, string>> partialOrig)
        {
            bool have = false;
            Dictionary<string, List<string>> UrlDependencies = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, string> pair in partialOrig)
            {
                string preUrl = pair.Key;
                string posUrl = pair.Value;
                if (!UrlDependencies.ContainsKey(preUrl))
                {
                    UrlDependencies[preUrl] = new List<string>();
                }
                UrlDependencies[preUrl].Add(posUrl);
                if (!UrlDependencies.ContainsKey(posUrl))
                {
                    UrlDependencies[posUrl] = new List<string>();
                }
            }

            List<string> sorted = new List<string>();
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            List<string> source = (new HashSet<string>(UrlDependencies.Keys)).ToList();

            foreach (string node in source)
            {
                try
                {
                    Visit(node, UrlDependencies, sorted, visited, false);
                }
                catch (CycleException e)
                {
                    have = true;
                    break;
                }
            }
            return have;
        }
        public static void AddSiteListToPartial(List<KeyValuePair<string, string>> partialOrig, List<KeyValuePair<string, string>> partialAdd)
        {
            for (int i = 0; i < partialAdd.Count; i++)
            {
                KeyValuePair<string, string> partialEle = partialAdd[i];
                if (partialOrig.IndexOf(partialEle) != -1)
                    continue;
                partialOrig.Add(partialEle);
                if (HaveCycle(partialOrig))
                {
                    partialOrig.RemoveAt(partialOrig.Count - 1);
                }
            }
        }
        
        public static int partialScoreCmp(KeyValuePair<KeyValuePair<string, string>, int> pairA, KeyValuePair<KeyValuePair<string, string>, int> pairB)
        {
            return pairA.Value.CompareTo(pairB.Value);
        }

        public static void Display(List<KeyValuePair<KeyValuePair<string,string>, int>> partialScoreList)
        {
            foreach(KeyValuePair<KeyValuePair<string,string>, int> pair in partialScoreList)
            {
                Console.WriteLine("{0}\t{1}\t{2}", pair.Key.Key, pair.Key.Value, pair.Value);
            }
            Console.ReadKey();
        }
        public static void MergePartialSortOfSamePattern(Dictionary<string, List<KeyValuePair<string, string>>> queryPatial, Dictionary<string, string> querySlotPattern, Dictionary<string, int> effectiveQueryScore, Dictionary<string, List<KeyValuePair<string, string>>> patternPartial)
        {
            /*
             * (1): Get the pattern partial score through add the score of relative query.
             * (2): Sort the partial url based on score ascending. (In order to strong win/loss query have final influence on the top site list)
             */
            Dictionary<string, Dictionary<KeyValuePair<string, string>, int>> patternPartialScore = new Dictionary<string, Dictionary<KeyValuePair<string, string>, int>>();
            foreach (KeyValuePair<string, string> queryPatternEle in querySlotPattern)
            {
                string query = queryPatternEle.Key;
                string slotPattern = queryPatternEle.Value;
                int score = 0;
                if(effectiveQueryScore.ContainsKey(query))
                {
                    score = Math.Abs(effectiveQueryScore[query]);
                }
                
                if (!queryPatial.ContainsKey(query))
                    continue;
                List<KeyValuePair<string, string>> partialList = queryPatial[query];

                if(!patternPartialScore.ContainsKey(slotPattern))
                {
                    patternPartialScore[slotPattern] = new Dictionary<KeyValuePair<string, string>, int>();
                }
                
                foreach(KeyValuePair<string,string> partialPair in partialList)
                {
                    if(!patternPartialScore[slotPattern].ContainsKey(partialPair))
                    {
                        patternPartialScore[slotPattern][partialPair] = 0;
                    }
                    patternPartialScore[slotPattern][partialPair] += score;
                }
            }

            List<string> patternList = patternPartialScore.Keys.ToList();
            foreach(string pattern in patternList)
            {
                Dictionary<KeyValuePair<string, string>, int> partialScore = patternPartialScore[pattern];
                List<KeyValuePair<KeyValuePair<string, string>, int>> partialScoreList = partialScore.ToList();
                partialScoreList.Sort(partialScoreCmp); //Dictionary<string, List<KeyValuePair<string, string>>> patternPartial
                if(!patternPartial.ContainsKey(pattern))
                {
                    patternPartial[pattern] = new List<KeyValuePair<string, string>>();
                }
                foreach(KeyValuePair<KeyValuePair<string, string>, int> pairIn in partialScoreList)
                {
                    patternPartial[pattern].Add(pairIn.Key);
                }
              
            }
                
        }
        public static void PrintQueryPartialSort(Dictionary<string, List<KeyValuePair<string, string>>> slotPatternPartialSort)
        {
            StreamWriter sw = new StreamWriter(@"D:\demo\watch.tsv");
            foreach (KeyValuePair<string, List<KeyValuePair<string, string>>> pair in slotPatternPartialSort)
            {
                string pattern = pair.Key;
                List<KeyValuePair<string, string>> partialList = pair.Value;
                foreach (KeyValuePair<string, string> urlPair in partialList)
                {
                    sw.WriteLine("{0}\t{1}\t{2}", pattern, urlPair.Key, urlPair.Value);
                }
            }
           // Console.ReadKey();
            sw.Close();
        }

        public static int MyKeyCmp(KeyValuePair<string, int> pairA, KeyValuePair<string, int> pairB)
        {
            return pairB.Value.CompareTo(pairA.Value);
        }

        public static void OutIntentPatternUrlList(Dictionary<string, Dictionary<string, List<KeyValuePair<string, int>>>> intentPatternUrlList)
        {
            foreach(KeyValuePair<string, Dictionary<string,List<KeyValuePair<string, int>>>> intentPatternUrlListPair in intentPatternUrlList)
            {
                string intent = intentPatternUrlListPair.Key;
                Dictionary<string, List<KeyValuePair<string, int>>> patternUrlList = intentPatternUrlListPair.Value;
                foreach(KeyValuePair<string, List<KeyValuePair<string, int>>> pair in patternUrlList)
                {
                    string pattern = pair.Key;
                    foreach(KeyValuePair<string, int> urlScore in pair.Value)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", intent, pattern, urlScore.Key, urlScore.Value);
                    }
                }
            }
            Console.ReadKey();
        }
        public static void SortedByUrlScore(ref Dictionary<string, Dictionary<string, List<KeyValuePair<string, int>>>> intentPatternUrlList)
        {
            List<string> intentList = intentPatternUrlList.Keys.ToList();
            foreach(string intent in intentList)
            {
                Dictionary<string, List<KeyValuePair<string, int>>> patternUrlList = intentPatternUrlList[intent];
                List<string> patternList = patternUrlList.Keys.ToList();
                foreach(string pattern in patternList)
                {
                    List<KeyValuePair<string, int>> urlScoreList = patternUrlList[pattern];
                    urlScoreList.Sort(MyKeyCmp);
                    //intentPatternUrlList[intent][pattern] = urlScoreList;
                }             
            }
           // OutIntentPatternUrlList(intentPatternUrlList);
        }
        public static void LoadPatternTopUrlList(string topUrlFile, Dictionary<string, Dictionary<string, List<KeyValuePair<string, int>>>> intentPatternUrlList)
        {
            StreamReader sr = new StreamReader(topUrlFile);
            string line, intent, pattern, url;
            int score;
            int intentCol = 0, patternCol = 1, urlCol = 2, scoreCol = 4;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 5)
                    continue;
                intent = arr[intentCol];
                pattern = arr[patternCol];
                url = arr[urlCol];
                score = int.Parse(arr[scoreCol]);
                if (!intentPatternUrlList.ContainsKey(intent))
                {
                    intentPatternUrlList[intent] = new Dictionary<string, List<KeyValuePair<string, int>>>();
                }
                if (!intentPatternUrlList[intent].ContainsKey(pattern))
                {
                    intentPatternUrlList[intent][pattern] = new List<KeyValuePair<string, int>>();
                }
                intentPatternUrlList[intent][pattern].Add(new KeyValuePair<string, int>(url, score));
            }
            SortedByUrlScore(ref intentPatternUrlList);
            sr.Close();
        }

        public static List<string> PartialSorted(List<KeyValuePair<string, string>> partialSet)
        {

            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
            string preUrl, posUrl;
            foreach (KeyValuePair<string, string> pair in partialSet)
            {
                preUrl = pair.Key;
                if (!graph.ContainsKey(preUrl))
                {
                    graph[preUrl] = new List<string>();
                }

                posUrl = pair.Value;
                if (!graph.ContainsKey(posUrl))
                {
                    graph[posUrl] = new List<string>();
                }
                graph[preUrl].Add(posUrl);
            }
            List<string> sorted = TopoSort(graph);
            sorted.Reverse();
            return sorted;
        }
        public static void AdjustTopSite(Dictionary<string, Dictionary<string, List<string>>> intentPatternUrlList, Dictionary<string, List<KeyValuePair<string, string>>> slotPatternPartialSort)
        {
            List<string> intentList = intentPatternUrlList.Keys.ToList();
            foreach (string intent in intentList)
            {
                Dictionary<string, List<string>> patternUrl = intentPatternUrlList[intent];
                List<string> patternList = patternUrl.Keys.ToList();
                foreach (string pattern in patternList)
                {
                    if (!slotPatternPartialSort.ContainsKey(pattern))
                    {
                        continue;
                    }
                    List<KeyValuePair<string, string>> partialOrig = slotPatternPartialSort[pattern];
                    List<string> urlList = patternUrl[pattern];//patternUrlPair.Value;
                    if (urlList.Count >= 2)
                    {
                        string preUrl = urlList[0], posUrl;// = urlList[1];
                        int num = urlList.Count, i = 1;

                        while (i < num)
                        {
                            posUrl = urlList[i];
                            KeyValuePair<string, string> pairUrl = new KeyValuePair<string, string>(preUrl, posUrl);
                            partialOrig.Add(pairUrl);
                            if (HaveCycle(partialOrig))
                            {
                                partialOrig.RemoveAt(partialOrig.Count - 1);
                                i++;
                            }
                            else
                            {
                                preUrl = posUrl;
                                i++;
                            }
                        }

                    }

                    List<string> sortUrlList = PartialSorted(partialOrig);
                    intentPatternUrlList[intent][pattern] = sortUrlList;
                }
            }
        }

        public static void StoreAdjustedTopSite(Dictionary<string, Dictionary<string, List<KeyValuePair<string, int>>>> intentPatternUrlList, string outfile)
        {
            StreamWriter sw = new StreamWriter(outfile);
            foreach (KeyValuePair<string, Dictionary<string, List<KeyValuePair<string, int>>>> pair in intentPatternUrlList)
            {
                string intent = pair.Key;
                Dictionary<string, List<KeyValuePair<string, int>>> patternUrl = pair.Value;
                foreach (KeyValuePair<string, List<KeyValuePair<string, int>>> patternUrlPair in patternUrl)
                {
                    string pattern = patternUrlPair.Key;
                    List<KeyValuePair<string, int>> urlList = patternUrlPair.Value;       
                    foreach (KeyValuePair<string, int> urlScore in urlList)
                    {
                        sw.WriteLine("{0}\t{1}\t{2}\t/[^/]+\t{3}", intent, pattern, urlScore.Key, urlScore.Value);
                    }
                }
            }
            sw.Close();
        }

        public static void PrintQuerySlotPattern(Dictionary<string, string> querySlotPattern)
        {
            foreach (KeyValuePair<string, string> pair in querySlotPattern)
            {
                Console.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            Console.ReadKey();
        }

     
        public static void AdjustTopSiteThrougChangeList(Dictionary<string, Dictionary<string, List<KeyValuePair<string, int>>>> intentPatternUrlList, Dictionary<string, List<KeyValuePair<string, string>>> slotPatternPartialSort)
        {
            List<string> intentList = intentPatternUrlList.Keys.ToList();
            foreach(string intent in intentList)
            {
                Dictionary<string, List<KeyValuePair<string, int>>> patternUrlList = intentPatternUrlList[intent];
                List<string> patternList = patternUrlList.Keys.ToList();
                foreach(string pattern in patternList)
                {
                    if (!slotPatternPartialSort.ContainsKey(pattern))
                        continue;
                    List<KeyValuePair<string, int>> urlList = patternUrlList[pattern];
                    List<KeyValuePair<string, string>> partialList = slotPatternPartialSort[pattern];
                    foreach(KeyValuePair<string, string> partialPair in partialList)
                    {
                        string preUrl = partialPair.Key;
                        string posUrl = partialPair.Value;
                        if (preUrl == "*")
                            continue;
                        if(posUrl == "*" && preUrl != "*")
                        {
                            urlList.RemoveAll(x => x.Key == preUrl);
                            int tmpScore = urlList[0].Value;
                            tmpScore = tmpScore + 2;
                            /*
                            if(tmpScore + 3 <= 100)
                            {
                                tmpScore = tmpScore + 3;
                            }
                            else if (tmpScore <= 100)
                            {
                                tmpScore = (tmpScore + 100)/2 + 1;
                            }
                            else
                            {
                                tmpScore = tmpScore + 2;
                            }
                             */
                            urlList.Insert(0, new KeyValuePair<string, int> (preUrl, tmpScore));
                            continue;
                        }
                        
                        int preIdx = urlList.FindIndex(x => x.Key == preUrl);
                        int posIdx = urlList.FindIndex(x => x.Key == posUrl);
                        if(preIdx != -1 && posIdx != -1 && preIdx > posIdx)
                        {
                            int eScore = urlList[posIdx].Value;
                            int bScore ;//= 100;
                            int curScore ;//= eScore + 1;
                            if(posIdx - 1 >= 0)
                            {
                                bScore = urlList[posIdx - 1].Value;
                                int remainNum = (bScore + eScore) % 2 == 0 ? 0 : 1;
                                curScore = (bScore + eScore) / 2 + remainNum;
                            }
                            else
                            {
                                curScore = eScore + 2;
                            }
                            urlList.RemoveAt(preIdx);
                            urlList.RemoveAll(x => x.Key == preUrl);

                            urlList.Insert(posIdx, new KeyValuePair<string, int> (preUrl, curScore));
                        }
                    }
                    intentPatternUrlList[intent][pattern] = urlList;
                }
            }
        }
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new String[8];
                args[0] = @"D:\demo\PartialSortUrl.tsv";
                args[1] = @"D:\demo\TopSiteTuningBasedOnV1.8BAVSG.tsv";
                args[2] = "0.8";
                args[3] = @"D:\demo\v1.8BAVSG.tsv";
                args[4] = @"D:\demo\querySlotPatternV1.3.tsv";
                args[5] = @"D:\demo\queryPartial.tsv";
                args[6] = @"D:\demo\TopSite.tsv";
                args[7] = "1";
            }
            string infile = args[0];
            string outfile = args[1];
            string sbsfile = args[3];
            string slotfile = args[4];
            string partialSortfile = args[5];
            string topSitefile = args[6];
            double confThread = double.Parse(args[2]);
            int scoreThread = int.Parse(args[7]);
         
            Dictionary<string, int> effectiveQueryScore = new Dictionary<string,int>();
            HashSet<string> effectiveQuery = new HashSet<string>();
            LoadEffectiveQuery(sbsfile, effectiveQueryScore, effectiveQuery, scoreThread);
            // Dictionary<string, List<string>> slotPatternQueries = new Dictionary<string, List<string>>();
            Dictionary<string, string> querySlotPattern = new Dictionary<string, string>();
            LoadQuerySlotPattern(slotfile, querySlotPattern, effectiveQuery);

            //  PrintQuerySlotPattern(querySlotPattern);
            Dictionary<string, List<KeyValuePair<string, string>>> queryPartial = new Dictionary<string, List<KeyValuePair<string, string>>>();
            LoadQueryPartialSort(partialSortfile, confThread, queryPartial, false);


            Dictionary<string, List<KeyValuePair<string, string>>> slotPatternPartialSort = new Dictionary<string, List<KeyValuePair<string, string>>>();
            MergePartialSortOfSamePattern(queryPartial, querySlotPattern, effectiveQueryScore, slotPatternPartialSort);
             //PrintQueryPartialSort(slotPatternPartialSort);
                
            Dictionary<string, Dictionary<string, List<KeyValuePair<string, int>>>> intentPatternUrlList = new Dictionary<string, Dictionary<string, List<KeyValuePair<string, int>>>>();
            LoadPatternTopUrlList(topSitefile, intentPatternUrlList);

            AdjustTopSiteThrougChangeList(intentPatternUrlList, slotPatternPartialSort);

            // PrintQueryPartialSort(slotPatternPartialSort);
            StoreAdjustedTopSite(intentPatternUrlList, outfile);          
        }
    }
}
