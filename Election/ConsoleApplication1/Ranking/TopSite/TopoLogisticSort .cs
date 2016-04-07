using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Ranking.TopSite.TopoLogisticSort
{
    class TopoLogisticSort
    {
        public static void Visit(string node, Dictionary<string, List<string>> graph, List<string> sorted, Dictionary<string, bool> visited, bool ignoreCycles)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(node, out inProcess);
            if(alreadyVisited)
            {
                if(inProcess && !ignoreCycles)
                {
                    throw new ArgumentException("Cyclic dependency found.");
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
            
            foreach(string node in source)
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
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                query = arr[queryCol];
                preUrl = arr[preCol];
                posUrl = arr[posCol];
                conf = double.Parse(arr[confCol]);
                if(conf < confMinThread)
                {
                    continue;
                }
                if(query != queryPre)
                {
                    
                    if(!string.IsNullOrEmpty(queryPre))
                    {
                        sortResult = TopoSort(UrlDependencies);
                        sw.WriteLine("{0}\t{1}", queryPre, string.Join("\t", sortResult.ToArray()));
                    }
                    queryPre = query;
                    UrlDependencies.Clear();
                }
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
            if(!string.IsNullOrEmpty(queryPre))
            {
                sortResult = TopoSort(UrlDependencies);
                sw.WriteLine("{0}\t{1}", queryPre, string.Join("\t", sortResult.ToArray()));
            }
            sr.Close();
            sw.Close();
        }
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new String[3];
                args[0] = @"D:\demo\PartialSortUrl.tsv";
                args[1] = @"D:\demo\SortUrl.tsv";
                args[2] = "0.5";
                
            }
            string infile = args[0];
            string outfile = args[1];
            double confThread = double.Parse(args[2]);
            TopoLogistic(infile, outfile, confThread);
        }
    }
}
