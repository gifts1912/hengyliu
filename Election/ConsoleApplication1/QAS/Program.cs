using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace QAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(args);
        }
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[6];
                args[0] = @"D:\demo\ElectionTokens.tsv";
                args[1] = @"D:\demo\ElectionRules.tsv";
                args[2] = @"D:\demo\watch.tsv";
                args[3] = @"D:\demo\queryPattern.tsv"; // queryCol = 0, patternCol = 2
                args[4] = @"D:\demo\ElectionQueryIntent.tsv"; //queryCol = 0, intentCol = 3;
              //  args[5] = @"D:\Project\Election\TokenAndRules\candidatePartyPoliticalViewMappingDic.tsv";
                args[5] = @"D:\demo\slotIdealSlot.tsv";
            }
            string tokenfile = args[0];
            string tokenRuleFile = args[1];
            string patternQueryFile = args[3];
            string queryIntentFile = args[4];
            string slotIdealFile = args[5];

            Dictionary<string, List<string>> tokenValues = new Dictionary<string, List<string>>();
            loadTokens(tokenfile, tokenValues); //filename ;

            Dictionary<string, string> patternIntentDic = new Dictionary<string, string>();
            LoadPatternIntent(patternQueryFile, queryIntentFile, patternIntentDic);

            HashSet<string> rules = new HashSet<string>();
            LoadTokenRules(tokenRuleFile, rules);

            Dictionary<string, HashSet<string>> idealSlotExpHs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, string> slotIdealSlot = new Dictionary<string, string>();
            LoadSlotIdealExp(slotIdealFile, idealSlotExpHs, slotIdealSlot);

            GenerateXML(tokenValues, rules, patternIntentDic, idealSlotExpHs, slotIdealSlot, @"D:\demo\patternIntentIdex.tsv");
        }
        
        public static void LoadSlotIdealExp(string slotIdealFile, Dictionary<string, HashSet<string>> idealSlotExpList, Dictionary<string, string> slotIdealSlot)
        {
            /*
             * As slotidealSlotFile the expression of slot have make StopWordsDelete(value) process.
             */
            StreamReader sr = new StreamReader(slotIdealFile);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                slotIdealSlot[arr[0]] = arr[1];
                if(!idealSlotExpList.ContainsKey(arr[1]))
                {
                    idealSlotExpList[arr[1]] = new HashSet<string>();
                    idealSlotExpList[arr[1]].Add(arr[1]);
                }
                idealSlotExpList[arr[1]].Add(arr[0]);
            }
            //add KeyValuePair<string, string> idealSlotIdealSlot to dictionary slotIdealSlot
            HashSet<string> vcHS = new HashSet<string>(slotIdealSlot.Values.ToArray());
            foreach(string iv in vcHS)
            {
                slotIdealSlot[iv] = iv;
            }
            sr.Close();
        }
        public static void LoadPatternIntent(string patternQueryFile, string queryIntentFile,  Dictionary<string, string> patternIntents)
        {
            Dictionary<string, string> patternQuery = new Dictionary<string,string>();
            Dictionary<string, string> queryIntents = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(patternQueryFile))
            {
                string line;
                int patternIdx = 2, queryIdx = 0;
                while((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    if (arr.Length <= patternIdx || arr.Length <= queryIdx)
                        continue;
                    string pattern = arr[patternIdx];
                    string query = arr[queryIdx];
                    pattern = pattern.Replace('[', ' ').Replace(']', ' ');
                    pattern = Regex.Replace(pattern, @"\s+", " ");
                    pattern = pattern.Trim();
                  //  pattern = StopWordsDelete(pattern);
                    patternQuery[pattern] = query;
                }
            }

            using (StreamReader sr = new StreamReader(queryIntentFile))
            {
                string line;
                int queryIdx = 0, intentIdx = 3;
                while((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    string query = arr[queryIdx];
                    string intent = arr[intentIdx];
                    queryIntents[query] = intent;
                }
            }
            
            foreach(KeyValuePair<string, string> pair in patternQuery)
            {
                string pattern = pair.Key;
                string query = pair.Value;
                if(queryIntents.ContainsKey(query))
                {
                    string intent = queryIntents[query];
                    patternIntents[pattern] = intent;
                }
            }

            /*
            StreamWriter sw = new StreamWriter(@"D:\demo\watch.tsv");
            Dictionary<string, List<string>> intentPatterns = new Dictionary<string, List<string>>();
            foreach(KeyValuePair<string, string> pair in patternIntents)
            {
                //sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);
                if(!intentPatterns.ContainsKey(pair.Value))
                {
                    intentPatterns[pair.Value] = new List<string>();
                }
                intentPatterns[pair.Value].Add(pair.Key);
            }
            
            foreach(KeyValuePair<string, List<string>> pair in intentPatterns)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, string.Join("\t", pair.Value));
            }
            sw.Close();
             */
        }
        public static void LoadTokenRules(string tokenRuleFile, HashSet<string> rulesSet)
        {
            using (StreamReader sr = new StreamReader(tokenRuleFile))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] args = line.Split('\t');
                    if (args.Length != 2)
                        continue;
                    string rule = args[0];
                    rule = rule.Substring("qpv2rule-".Length);
                    rule = rule.Replace('<', ' ').Replace('>', ' ');
                    rule = Regex.Replace(rule, @"\s+", " ");
                    rule = rule.Trim();
                 //   rule = StopWordsDelete(rule);
                    rulesSet.Add(rule);
                }
            }

           /* StreamWriter sw = new StreamWriter(@"D:\demo\ruleWatch.tsv");
            foreach(string rule in rulesSet)
            {
                sw.WriteLine(rule);
            }
            Console.ReadKey();
            */
        }
        public static string StopWordsDelete(string value)
        {
            string result = value;
            result = Regex.Replace(value, "\\b(on|in|the|of|s|S|state)\\b", "");
            result = Regex.Replace(result, "\\s+", " ");
            result = result.Trim();
            return result;
        }
        public static void GenerateXML(Dictionary<string, List<string>> tokenValues, HashSet<string> rulesHS, Dictionary<string, string> patternIntents, Dictionary<string, HashSet<string>> idealSlotExpHs, Dictionary<string, string> slotIdealSlot, string intentIndexFile)
        {
            Dictionary<string, int> intentCurIdx = new Dictionary<string, int>();
            Dictionary<string, string> intentIdxPattern = new Dictionary<string, string>();
            intentCurIdx.Add("Intent0", 1);

            string pcfgFile = @"D:\demo\watchPcfg.xml";
            string rootId = "MSElection";
            XElement Grammar = new XElement("grammar", new XAttribute("root", rootId));
            /*
             * Add slot value list of response ideal slot expression.
             */
            foreach(KeyValuePair<string, HashSet<string>> pair in idealSlotExpHs)
            {
                XElement atomNode = new XElement("rule", new XAttribute("id", pair.Key));
                XElement oneofNode = new XElement("one-of");
                XElement tagNode = new XElement("tag");
                foreach(string ele in pair.Value)
                {
                    XElement itemNode = new XElement("item");
                    itemNode.SetValue(ele);
                    oneofNode.Add(itemNode);
                }
                atomNode.Add(oneofNode);
                tagNode.SetValue("$=$$");
                atomNode.Add(tagNode);
                Grammar.Add(atomNode);
            }
            
            // Add tokenValus of specified token.
            foreach(KeyValuePair<string, List<string>> pair in tokenValues)
            {
                string tokenName = pair.Key;
                List<string> tokenValueList = pair.Value;
                XElement atomNode = new XElement("rule", new XAttribute("id", tokenName));

                 XElement oneofNode = new XElement("one-of");
                 HashSet<string> refIdealSlot = new HashSet<string>();
                    foreach (var a in tokenValueList)
                    {
                       // string tv = StopWordsDelete(a);
                        if(slotIdealSlot.ContainsKey(a))
                        {
                            refIdealSlot.Add(slotIdealSlot[a]);
                            continue;
                        }
                        XElement itemNode = new XElement("item");
                        itemNode.SetValue(a);
                        oneofNode.Add(itemNode);
                    }
                if(refIdealSlot.Count() > 0)
                {
                    foreach(string idealSlot in refIdealSlot)
                    {
                        XElement itemNode = new XElement("item");
                        XElement refNode = new XElement("ruleref", new XAttribute("uri", "#" + idealSlot));
                        XElement tagNode = new XElement("tag");
                        tagNode.SetValue("$=$$");
                        itemNode.Add(refNode);
                        itemNode.Add(tagNode);
                        oneofNode.Add(itemNode);
                    }
                }
                    atomNode.Add(oneofNode);
                    Grammar.Add(atomNode);
            }

            // Add rule
            HashSet<string> gramaRuleHs = new HashSet<string>();
            foreach(string rule in rulesHS)
            {
                string intent = "Intent0";
                if (patternIntents.ContainsKey(rule))
                {
                    intent = patternIntents[rule];
                }
                else
                {
                    continue;
                }               
                if(!intentCurIdx.ContainsKey(intent))
                {
                    intentCurIdx[intent] = 0;
                }
                intentCurIdx[intent] += 1;
                int cur = intentCurIdx[intent];

                string ruleId = string.Format("{0}_{1}", intent, cur);
                gramaRuleHs.Add(ruleId);
                intentIdxPattern[ruleId] = rule;
                XElement atomNode = new XElement("rule", new XAttribute("id", ruleId));
               // XElement oneofNode = new XElement("one-of");
                string[] arr = rule.Split(' ');
                bool first = true;
                foreach(string ele in arr)
                {
                    if(!ele.Contains("."))
                    {                       
                            XElement itemNode = new XElement("item");
                            itemNode.SetValue(ele);
                            atomNode.Add(itemNode);
                          //  oneofNode.Add(itemNode);              
                    }
                    else
                    {
                        XElement itemNode = new XElement("item");
                        XElement refNode = new XElement("ruleref", new XAttribute("uri", "#" + ele));
                        XElement tagNode = new XElement("tag");
                        if (first)
                        {
                            tagNode.SetValue("$=$$");
                        }                          
                        else
                        {
                            tagNode.SetValue("$ = $+$$");
                        }
                        itemNode.Add(refNode);
                        itemNode.Add(tagNode);
                  //      oneofNode.Add(itemNode);
                        atomNode.Add(itemNode);
                    }
                    first = false;
                }
                //atomNode.Add(oneofNode);
                Grammar.Add(atomNode);
            }
            
            /*
             *Add rule that Election grama that will call. 
             */
            XElement RAtomNode = new XElement("rule", new XAttribute("id", rootId));
            XElement ROneofNode = new XElement("one-of");
            foreach(string ruleGram in gramaRuleHs)
            {
                //rootId = "MSElection"              
                XElement itemNode = new XElement("item");
                XElement refNode = new XElement("ruleref", new XAttribute("uri", "#" + ruleGram));
                XElement tagNode = new XElement("tag");
                tagNode.SetValue("$=$$");
                itemNode.Add(refNode);
                itemNode.Add(tagNode);
                ROneofNode.Add(itemNode);
            }
            RAtomNode.Add(ROneofNode);
            Grammar.Add(RAtomNode);

            Grammar.Save(pcfgFile);

            StreamWriter sw = new StreamWriter(intentIndexFile);
            foreach(KeyValuePair<string, string> pair in intentIdxPattern)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();
        }
     
        public static void loadTokens(string tokenfile, Dictionary<string, List<string>> tokenValues)
        {
            using (StreamReader sr = new StreamReader(tokenfile))
            {
                string line;
                while((line= sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    if (arr.Length != 2)
                        continue;
                    string value = arr[0];
                    value = value.Substring("qpv2tkn-".Length); 
                   // value = StopWordsDelete(value);
                    string key = arr[1].Split(';')[0].Trim(new char[] { '<', '>' });
                    if(!key.Contains('.'))
                    {
                        continue;
                    }
                    if(!tokenValues.ContainsKey(key))
                    {
                        tokenValues[key] = new List<string>();
                    }
                    tokenValues[key].Add(value);
                }
               // StoreWatch(@"D:\demo\watch.tsv", tokenValues);
            }
        }
        public static void StoreWatch(string filename, Dictionary<string, List<string>> tokenValues)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                foreach(KeyValuePair<string, List<string>> pair in tokenValues)
                {
                    sw.WriteLine("{0}\t{1}", pair.Key, string.Join("\t", pair.Value));
                }
            }

        }
    }
}

