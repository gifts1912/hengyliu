﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.TopSite.AdjustTopSiteListBasedOnPartialSort.TopoLogisticSort
{
    public class CycleException : ApplicationException
    {
        public CycleException(string message) :base(message)
        {

        }
    }
    class TopoLogisticSort
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

        public static void LoadLossQuery(string sbsFile, HashSet<string> lossQuery, int scoreThread = -2)
        {
            StreamReader sr = new StreamReader(sbsFile);
            string line, query;
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
                if (score > scoreThread)
                    continue;
                lossQuery.Add(query);
            }
            sr.Close();

        }

        public static void LoadQuerySlotPattern(string slotFile, Dictionary<string, string> querySlotPattern, HashSet<string> lossQuery)
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
                if (!lossQuery.Contains(query))
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
                if (preUrl.Trim().Equals("*") || posUrl.Trim().Equals("*"))
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
            if(filterUrlStart)
            {
                foreach(string queryStar in queryHaveStar)
                {
                    if(queryPartialList.ContainsKey(queryStar))
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
            foreach(KeyValuePair<string, string> pair in partialOrig)
            {
                string preUrl = pair.Key;
                string posUrl = pair.Value;
                if(!UrlDependencies.ContainsKey(preUrl))
                {
                    UrlDependencies[preUrl] = new List<string>();
                }
                UrlDependencies[preUrl].Add(posUrl);
                if(!UrlDependencies.ContainsKey(posUrl))
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
            for(int i = 0; i < partialAdd.Count; i++)
            {
                KeyValuePair<string, string> partialEle = partialAdd[i];
                if (partialOrig.IndexOf(partialEle) != -1)
                    continue;
                partialOrig.Add(partialEle);
                if(HaveCycle(partialOrig))
                {
                    partialOrig.RemoveAt(partialOrig.Count - 1);
                }
            }
        }
        public static void MergePartialSortOfSamePattern(Dictionary<string, List<KeyValuePair<string, string>>> queryPatial, Dictionary<string, string> querySlotPattern, Dictionary<string, List<KeyValuePair<string, string>>> patternPartial)
        {
            foreach (KeyValuePair<string, string> queryPatternEle in querySlotPattern)
            {
                string query = queryPatternEle.Key;
                string slotPattern = queryPatternEle.Value;
                if (!queryPatial.ContainsKey(query))
                    continue;
                List<KeyValuePair<string, string>> partialList = queryPatial[query];
                if (!patternPartial.ContainsKey(slotPattern))
                {
                    if (!HaveCycle(partialList))
                    {
                        patternPartial[slotPattern] = partialList;
                    }
                }
                else
                {
                    AddSiteListToPartial(patternPartial[slotPattern], partialList);
                }
            }
        }

        public static void PrintQueryPartialSort(Dictionary<string, List<KeyValuePair<string, string>>> slotPatternPartialSort)
        {
            foreach(KeyValuePair<string, List<KeyValuePair<string, string>>> pair in slotPatternPartialSort)
            {
                string pattern = pair.Key;
                List<KeyValuePair<string, string>> partialList = pair.Value;
                foreach(KeyValuePair<string, string> urlPair in partialList)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", pattern, urlPair.Key, urlPair.Value);
                }
            }
            Console.ReadKey();
        }

        public static void LoadPatternTopUrlList(string topUrlFile, Dictionary<string, Dictionary<string,List<string>>> intentPatternUrlList)
        {
            StreamReader sr = new StreamReader(topUrlFile);
            string line, intent, pattern, url, score;
            int intentCol = 0, patternCol = 1, urlCol = 2, scoreCol = 4;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 5)
                    continue;
                intent = arr[intentCol];
                pattern = arr[patternCol];
                url = arr[urlCol];
                score = arr[scoreCol];
                if(!intentPatternUrlList.ContainsKey(intent))
                {
                    intentPatternUrlList[intent] = new Dictionary<string, List<string>>();
                }
                if(!intentPatternUrlList[intent].ContainsKey(pattern))
                {
                    intentPatternUrlList[intent][pattern] = new List<string>();
                }
                intentPatternUrlList[intent][pattern].Add(url);
            }
            sr.Close();
        }

        public static List<string> PartialSorted(List<KeyValuePair<string, string>> partialSet)
        {
             
             Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
             string preUrl, posUrl;
             foreach(KeyValuePair<string, string> pair in partialSet)
             {
                 preUrl = pair.Key;
                 if(!graph.ContainsKey(preUrl))
                 {
                     graph[preUrl] = new List<string>();
                 }
                 
                 posUrl = pair.Value;
                 if(!graph.ContainsKey(posUrl))
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
            foreach(string intent in intentList)
            {
                Dictionary<string, List<string>> patternUrl = intentPatternUrlList[intent];
                List<string> patternList = patternUrl.Keys.ToList();
                 foreach(string pattern in patternList)
                {
                    if(!slotPatternPartialSort.ContainsKey(pattern))
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

        public static void StoreAdjustedTopSite(Dictionary<string, Dictionary<string, List<string>>> intentPatternUrlList, string outfile)
        {
            StreamWriter sw = new StreamWriter(outfile);
            foreach(KeyValuePair<string, Dictionary<string, List<string>>> pair in intentPatternUrlList)
            {
                string intent = pair.Key;
                Dictionary<string, List<string>> patternUrl = pair.Value;
                foreach(KeyValuePair<string, List<string>> patternUrlPair in patternUrl)
                {
                    string pattern = patternUrlPair.Key;
                    List<string> urlList = patternUrlPair.Value;
                    int num = urlList.Count * 3;
                    foreach(string url in urlList)
                    {
                        sw.WriteLine("{0}\t{1}\t{2}\t/[^/]+\t{3}", intent, pattern, url, num);
                        num -= 3;
                    }
                }
            }
            sw.Close();
        }

        public static void PrintQuerySlotPattern(Dictionary<string, string> querySlotPattern)
        {
            foreach(KeyValuePair<string, string> pair in querySlotPattern)
            {
                Console.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            Console.ReadKey();
        }
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new String[7];
                args[0] = @"D:\demo\PartialSortUrl.tsv";
                args[1] = @"D:\demo\SortUrl.tsv";
                args[2] = "0.7";
                args[3] = @"D:\demo\v1.3BAVSBB.tsv";
                args[4] = @"D:\demo\querySlotPatternV1.3.tsv";
                args[5] = @"D:\demo\queryPartial.tsv";
                args[6] = @"D:\demo\TopSite.tsv";
            }
            string infile = args[0];
            string outfile = args[1];
            string sbsfile = args[3];
            string slotfile = args[4];
            string partialSortfile = args[5];
            string topSitefile = args[6];
            double confThread = double.Parse(args[2]);
            HashSet<string> lossQuery = new HashSet<string>();
            LoadLossQuery(sbsfile, lossQuery, -2);
            // Dictionary<string, List<string>> slotPatternQueries = new Dictionary<string, List<string>>();
            Dictionary<string, string> querySlotPattern = new Dictionary<string, string>();
            LoadQuerySlotPattern(slotfile, querySlotPattern, lossQuery);

          //  PrintQuerySlotPattern(querySlotPattern);

            Dictionary<string, List<KeyValuePair<string, string>>> queryPatial = new Dictionary<string, List<KeyValuePair<string, string>>>();
            LoadQueryPartialSort(partialSortfile, confThread, queryPatial);
            
            Dictionary<string, List<KeyValuePair<string, string>>> slotPatternPartialSort = new Dictionary<string, List<KeyValuePair<string, string>>>();
            MergePartialSortOfSamePattern(queryPatial, querySlotPattern, slotPatternPartialSort);

           // PrintQueryPartialSort(slotPatternPartialSort);
           
            Dictionary<string, Dictionary<string, List<string>>> intentPatternUrlList = new Dictionary<string, Dictionary<string, List<string>>>();
            LoadPatternTopUrlList(topSitefile, intentPatternUrlList);

            AdjustTopSite(intentPatternUrlList, slotPatternPartialSort);

           // PrintQueryPartialSort(slotPatternPartialSort);
            StoreAdjustedTopSite(intentPatternUrlList, outfile);
        }
    }
}
