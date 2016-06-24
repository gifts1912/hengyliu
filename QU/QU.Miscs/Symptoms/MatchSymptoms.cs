using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyMisc = Utility;

namespace QU.Miscs
{
    class MatchSymptoms
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "q")]
            public string QueryFile;

            [Argument(ArgumentType.Required, ShortName = "symp")]
            public string SymptomFile;

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output;
        }

        class InvertedIndexValue
        {
            public HashSet<string> symptoms = new HashSet<string>();
            public int totalDocs = 0;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.QueryFile) || !File.Exists(arguments.SymptomFile))
            {
                Console.WriteLine("No File!");
                return;
            }

            // manually add some stopwords
            stopwords.Add("symptom");
            stopwords.Add("symptoms");
            stopwords.Add("over");
            stopwords.Add("after");
            stopwords.Add("before");

            Dictionary<string, int> dictSymptom2DeseaseCnt = ReadSymptomFile(arguments.SymptomFile);
            Dictionary<string, InvertedIndexValue> dictTerm2Docs = new Dictionary<string, InvertedIndexValue>();
            int TotalDoc = 0;
            foreach (var pair in dictSymptom2DeseaseCnt)
            {
                TotalDoc += pair.Value;
                HashSet<string> terms 
                    = new HashSet<string>(pair.Key.Split(Seperators, StringSplitOptions.RemoveEmptyEntries).Select(t => stemmer.Stem(t)));
                foreach (var t in terms)
                {
                    if (!dictTerm2Docs.ContainsKey(t))
                        dictTerm2Docs.Add(t, new InvertedIndexValue());
                    dictTerm2Docs[t].totalDocs += pair.Value;
                    dictTerm2Docs[t].symptoms.Add(pair.Key);
                }
            }

            using (StreamWriter sw = new StreamWriter(arguments.Output))
            {
                using (StreamReader sr = new StreamReader(arguments.QueryFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('\t');

                        string query = items[0];
                        if (string.IsNullOrEmpty(query))
                            continue;

                        string[] terms = query.Split(Seperators, StringSplitOptions.RemoveEmptyEntries);
                        var sympScore = GetSymptomScore(terms, dictTerm2Docs, TotalDoc);
                        if (null == sympScore || sympScore.Count == 0)
                            continue;
                        string[] selected = 
                            (from s in sympScore
                             where s.Value > 20000
                             orderby (s.Value - ((int)(s.Value) / 10000) * 10000) descending, s.Key.Length ascending
                             select s.Key + ", " + (s.Value - ((int)(s.Value) / 10000) * 10000)).Take(5).ToArray();
                        if (selected.Length == 0)
                            continue;
                        var str = string.Join("##", selected);
                        sw.WriteLine(query + "\t" + str);
                    }
                }
            }
        }

        static char[] Seperators = new char[] { '|', '-', ' ', '(', ')', '[', ']', '{', '}' };
        static HashSet<string> stopwords
            = MyMisc.StopWordUtil.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "stopwords.txt"));
        static MyMisc.MachineLearning.PorterStemmer stemmer = new MyMisc.MachineLearning.PorterStemmer();

        static Dictionary<string, double> GetSymptomScore(
            string[] terms, 
            Dictionary<string, InvertedIndexValue> invertedIndex, 
            int totalDocs)
        {
            Dictionary<string, double> dictSymptom2Score = new Dictionary<string, double>();
            foreach (var t in terms)
            {
                if (stopwords.Contains(t))
                {
                    continue;
                }

                string stemmed = stemmer.Stem(t);

                InvertedIndexValue index;
                if (!invertedIndex.TryGetValue(stemmed, out index))
                {
                    continue;
                }

                foreach (var symp in index.symptoms)
                {
                    if (!dictSymptom2Score.ContainsKey(symp))
                    {
                        dictSymptom2Score.Add(symp, 0);
                    }

                    dictSymptom2Score[symp] += (Math.Log((double)totalDocs / index.totalDocs) + 10000);
                }
            }

            return dictSymptom2Score;
        }

        static Dictionary<string, int> ReadSymptomFile(string file)
        {
            Dictionary<string, int> dictSymptom2DeseaseCnt = new Dictionary<string, int>();
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split('\t');

                    string symptom = items[0];
                    if (string.IsNullOrEmpty(symptom))
                        continue;

                    if (!dictSymptom2DeseaseCnt.ContainsKey(symptom))
                        dictSymptom2DeseaseCnt.Add(symptom, 0);

                    ++dictSymptom2DeseaseCnt[symptom];
                }
            }

            return dictSymptom2DeseaseCnt;
        }


    }
}
