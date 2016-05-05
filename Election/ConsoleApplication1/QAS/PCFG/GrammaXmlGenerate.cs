using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;


namespace QAS.PCFG
{
    class GrammaXmlGenerate
    {
        public static HashSet<string> DefaultNeedDetailSlot = new HashSet<string>(new string[] { "election.candidate.highconf", "election.candidate", "election.bpiissue", "election.party", "location.state", "I.NPE.Religion", "election.Speech" }); //"[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]", "[location.state]" });
        public static HashSet<string> stayWords = new HashSet<string>(new string[] { "donation", "woman", "women", "vice", "female", "male", "black", "white", "marriage", "top", "vs", "and", "or", "history", "how old", "age", "info", "information", "education", "election", "religious", "story", "life", "voluntee" });
        public static Dictionary<string, HashSet<string>> intentNeedStaySlot = new Dictionary<string, HashSet<string>> {{"CandidateList", new HashSet<string> (new string[] {"[election.party]", "[location.state]", "[election.national]",
        "[location.state]", "[election.candidate.highconf]", "[election.newsword]", "[election.candidate]",
        "[election.primary]", "[election.voting]", "[election.campaign]", "[election.next]", 
        "[election.winlose]", "[election.Gender.Female]", "[election.howmany]", "[election.when]", "[election.timelineword]", "[election.Speech]", "[election.timelineword]" })},
        {"CandidateView", new HashSet<string>(new string[] {"[election.candidate.highconf]", "[election.candidate]", "[election.bpiissue]", "[election.party]", "[election.timelineword]", "[location.state]"})},
        {"Candidate", new HashSet<string> (new string[] {"[election.candidatehistory]", "[election.candidate.highconf]", "[election.infoword]","[election.candidate]", "[election.party]", "[election.candidate]", "[election.race]"})},
        {"ElectionSchedule", new HashSet<string> (new string[] {"[location.state]", "[election.primary]", "[election.voting]", "[election.party]", "[election.race]", "[election.candidate.highconf]"})},
        {"CandidateCampain", new HashSet<string> (new string[] {"[election.candidate.highconf]", "[election.campaign]", "[election.Speech]", "[election.when]", "[election.Site]", "[election.Campaign.Fund]", "[election.Campaign.Manager]", "[election.Campaign.Team]", "[election.Campaign.platform]", "[election.Campaign.Site]", "[election.Campaign.Contack]", "[election.Campaign.Store]", "[election.Campaign.Logo]", "[election.Campaign.Volunteer]"})},
        {"CandidateBio", new HashSet<string> (new string[] {"[election.candidatehistory]", "[election.candidate.highconf]", "[I.NPE.Religion]"})}
        };


        public static HashSet<string> DetailSlots = new HashSet<string>();
        public static HashSet<string> StaySlots = new HashSet<string>();
        public static void Run(string[] args)
        {
           
            if (args.Length == 0)
            {
                args = new string[7];
                args[0] = @"D:\demo\ElectionTokens.tsv";
                args[1] = @"D:\demo\ElectionRules.tsv";
                args[2] = @"D:\demo\watch.tsv";
                args[3] = @"D:\demo\queryPattern.tsv"; // queryCol = 0, patternCol = 2
                args[4] = @"D:\demo\ElectionQueryIntent.tsv"; //queryCol = 0, intentCol = 3;
                args[5] = @"D:\demo\IdealSlotToDophineFile.tsv";

            }
            string tokenfile = args[0];
            string tokenRuleFile = args[1];
            string patternQueryFile = args[3];
            string queryIntentFile = args[4];
            string slotIdealFile = args[5];
            

            Dictionary<string, List<string>> tokenValues = new Dictionary<string, List<string>>();
            loadTokens(tokenfile, tokenValues); // Load tokens and response values and store them into Dictionary<string, List<string>> tokenValues;
            

            Dictionary<string, string> patternIntentDic = new Dictionary<string, string>();
            LoadPatternIntent(patternQueryFile, queryIntentFile, patternIntentDic); // generate each pattern's intent

            HashSet<string> rules = new HashSet<string>();
            LoadTokenRules(tokenRuleFile, rules); // load election rules

            Dictionary<string, HashSet<string>> idealSlotExpHs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, string> slotIdealSlot = new Dictionary<string, string>();
            
            LoadSlotIdealExp(tokenfile, idealSlotExpHs, slotIdealSlot); // Generate ideal slot expression of each slot value.

            
            GenStayAndDetailSlot();

            GenerateXML(tokenValues, rules, patternIntentDic, idealSlotExpHs, slotIdealSlot, slotIdealFile, @"D:\sumStoneTemplate\pcfg\MSElection\MSElection.grammar.xml");
        }

        public static void  GenStayAndDetailSlot()
        {//DefaultNeedDetailSlot
            foreach (string detailSlotEle in DefaultNeedDetailSlot)
            {
                string ele = detailSlotEle.Trim(new char[] { '[', ']' });
                DetailSlots.Add(ele);
            }
            foreach(KeyValuePair<string, HashSet<string>> pair in intentNeedStaySlot)
            {
                foreach(string stayEle in pair.Value)
                {
                    string ele = stayEle.Trim(new char[] { '[', ']' });
                    if(!DetailSlots.Contains(ele))
                        StaySlots.Add(ele);
                }
            }

            /*
           Console.WriteLine("Detail slot: {0}", string.Join("\t", DetailSlots.ToArray()));

            Console.WriteLine("Stay slot: {0}", string.Join("\t", StaySlots.ToArray()));

            Console.ReadKey();
             */
           
        }
        public static void IntentIdx(string inFile)
        {
            Dictionary<string, int> ruleIdx = new Dictionary<string, int>();
            int startIdx = 10000;
            StreamReader sr = new StreamReader(inFile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                int pos = line.IndexOf("_");
                if (pos == -1)
                    continue;
                pos = "<rule id=".Length + 1;
                string label = line.Substring(pos, line.Length - pos - 2);
                ruleIdx[label] = startIdx++;
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(@"D:\demo\watch.tsv");
            foreach (KeyValuePair<string, int> pair in ruleIdx)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();
        }
        
        public static void SlotIndex(string slotIdealFile, string slotIdxMapFile)
        {
            int curIdx = 1;
            Dictionary<string, int> IdealSlotIdx = new Dictionary<string, int>();
            StreamReader sr = new StreamReader(slotIdealFile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                string key = arr[1];
                if (!IdealSlotIdx.ContainsKey(key))
                {
                    IdealSlotIdx[key] = curIdx;
                    curIdx++;
                }
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(slotIdxMapFile);
            foreach (KeyValuePair<string, int> pair in IdealSlotIdx)
            {
                sw.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
            sw.Close();
        }
        public static void LoadSlotIdealExp(string electionTokenFile, Dictionary<string, HashSet<string>> idealSlotExpList, Dictionary<string, string> slotIdealSlot)
        {
            StreamReader sr = new StreamReader(electionTokenFile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 2)
                    continue;
                string tokenValue = arr[0];
                tokenValue = tokenValue.Substring("qpv2tkn-".Length);
                string tokenKey = arr[1].Split(';')[1];
                if(string.IsNullOrWhiteSpace(tokenKey))
                {
                    continue;
                }

                slotIdealSlot[tokenValue] = tokenKey ;
                if (!idealSlotExpList.ContainsKey(tokenKey))
                {
                    idealSlotExpList[tokenKey] = new HashSet<string>();
                    // idealSlotExpList[arr[1]].Add(arr[1]);
                }
                idealSlotExpList[tokenKey].Add(tokenValue);
            }
           /* HashSet<string> vcHS = new HashSet<string>(slotIdealSlot.Values.ToArray());
            foreach (string iv in vcHS)
            {
                if (iv.IndexOf("-") != -1)
                    continue;
                slotIdealSlot[iv] = iv;
            }
            */
            sr.Close();
        }
        public static void LoadPatternIntent(string patternQueryFile, string queryIntentFile, Dictionary<string, string> patternIntents)
        {
            Dictionary<string, string> patternQuery = new Dictionary<string, string>();
            Dictionary<string, string> queryIntents = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(patternQueryFile))
            {
                string line;
                int patternIdx = 2, queryIdx = 0;
                while ((line = sr.ReadLine()) != null)
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
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    string query = arr[queryIdx];
                    string intent = arr[intentIdx];
                    queryIntents[query] = intent;
                }
            }

            foreach (KeyValuePair<string, string> pair in patternQuery)
            {
                string pattern = pair.Key;
                string query = pair.Value;
                if (queryIntents.ContainsKey(query))
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
                while ((line = sr.ReadLine()) != null)
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

        public static void ReadCandIdx(Dictionary<string, string> candIdx, string infile)
        {
            StreamReader sr = new StreamReader(infile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                if (arr.Length != 3)
                    continue;
                string key = arr[1], value = arr[2];
                key = key.Trim();
                value = value.Trim();
                candIdx[key] = value;
            }
            sr.Close();
        }
        public static void GenerateXML(Dictionary<string, List<string>> tokenValues, HashSet<string> rulesHS, Dictionary<string, string> patternIntents, Dictionary<string, HashSet<string>> idealSlotExpHs, Dictionary<string, string> slotIdealSlot, string idealSlotToDophineFile, string grammaFile)
        {
            /*  public static HashSet<string> DetailSlots = new HashSet<string>();
                public static HashSet<string> StaySlots = new HashSet<string>();
            */

            Dictionary<string, string> candIdx = new Dictionary<string, string>();
            ReadCandIdx(candIdx, idealSlotToDophineFile);

            Dictionary<string, int> intentCurIdx = new Dictionary<string, int>();
            Dictionary<string, string> intentIdxPattern = new Dictionary<string, string>();
            intentCurIdx.Add("Intent0", 1);
            string rootId = "MSElection";
            XElement Grammar = new XElement("grammar", new XAttribute("root", rootId));
           
            /*
             * Add slot value list of response ideal slot expression. 
             */
            foreach (KeyValuePair<string, HashSet<string>> pair in idealSlotExpHs)
            {
                string slotIdealExp = pair.Key;
                if(candIdx.ContainsKey(slotIdealExp))
                {
                    slotIdealExp = candIdx[slotIdealExp];
                }
                XElement atomNode = new XElement("rule", new XAttribute("id", slotIdealExp));
                XElement oneofNode = new XElement("one-of");
                XElement tagNode = new XElement("tag");
                foreach (string ele in pair.Value)
                {
                    XElement itemNode = new XElement("item");
                    itemNode.SetValue(ele);
                    oneofNode.Add(itemNode);
                }
                atomNode.Add(oneofNode);
                tagNode.SetValue(string.Format("$=\"-{0}\"", slotIdealExp));
                atomNode.Add(tagNode);
                Grammar.Add(atomNode);
            }

            // Add tokenValus of specified token. if token value doesn't belong to a idealslot cluster, directly add it. Else add them together with other refIdealSlot.
            foreach (KeyValuePair<string, List<string>> pair in tokenValues)
            {
                string tokenName = pair.Key;
                List<string> tokenValueList = pair.Value;
                XElement atomNode = new XElement("rule", new XAttribute("id", tokenName));

                XElement oneofNode = new XElement("one-of");
                XElement tagNodeToken = new XElement("tag");

                HashSet<string> refIdealSlot = new HashSet<string>();
                foreach (var a in tokenValueList)
                {
                    if (slotIdealSlot.ContainsKey(a))
                    {
                        string label = slotIdealSlot[a];
                        if (candIdx.ContainsKey(label))
                        {
                            label = candIdx[label];
                        }
                        refIdealSlot.Add(label);
                        continue;
                    }
                    XElement itemNode = new XElement("item");
                    itemNode.SetValue(a);
                    oneofNode.Add(itemNode);
                }
                if (refIdealSlot.Count() > 0)
                {
                    foreach (string idealSlot in refIdealSlot)
                    {
                        XElement itemNode = new XElement("item");
                        XElement refNode = new XElement("ruleref", new XAttribute("uri", "#" + idealSlot));
                        XElement tagNode = new XElement("tag");
                        tagNode.SetValue("$ = $$");
                        itemNode.Add(refNode);
                        itemNode.Add(tagNode);
                        oneofNode.Add(itemNode);
                    }
                }
                tagNodeToken.SetValue(string.Format("$ = \"-{0}\"", tokenName));
                atomNode.Add(oneofNode);
                if(StaySlots.Contains(tokenName))
                    atomNode.Add(tagNodeToken);
                Grammar.Add(atomNode);
            }

            // Add rule
            HashSet<string> gramaRuleHs = new HashSet<string>();
            foreach (string rule in rulesHS)
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
                if (!intentCurIdx.ContainsKey(intent))
                {
                    intentCurIdx[intent] = 0;
                }
                intentCurIdx[intent] += 1;
                int cur = intentCurIdx[intent];

                string ruleId = string.Format("{0}_{1}", intent, cur);
                gramaRuleHs.Add(ruleId);
               // intentIdxPattern[ruleId] = rule;
                XElement atomNode = new XElement("rule", new XAttribute("id", ruleId));
                // XElement oneofNode = new XElement("one-of");
                string[] arr = rule.Split(' ');
                bool first = true;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    string ele = arr[i];
                    if (!ele.Contains("."))
                    {
                        XElement itemNode = new XElement("item");
                        itemNode.SetValue(ele);
                        /*if(stayWords.Contains(ele))
                        {
                            XElement tagNode = new XElement("tag");
                            tagNode.SetValue("$ = $ + \"-\" + $$");
                            itemNode.Add(tagNode);
                        }
                        */
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
                            tagNode.SetValue("$ = $$");
                        }
                        else
                        {
                            tagNode.SetValue("$ = $ + \"-\" + $$");
                        }
                        itemNode.Add(refNode);
                        itemNode.Add(tagNode);
                        //      oneofNode.Add(itemNode);
                        atomNode.Add(itemNode);
                    }
                    first = false;
                }

                string eleLast = arr[arr.Length - 1];
                if (!eleLast.Contains("."))
                {
                    XElement itemNode = new XElement("item");
                    itemNode.SetValue(eleLast);
                    atomNode.Add(itemNode);
                    //  oneofNode.Add(itemNode);              
                }
                else
                {
                    XElement itemNodeLast = new XElement("item");
                    XElement refNodeLast = new XElement("ruleref", new XAttribute("uri", "#" + eleLast));
                    XElement tagNodeLast = new XElement("tag");

                    string label = intent;
                    if (candIdx.ContainsKey(label))
                    {
                        label = candIdx[label];
                    }

                    if (!first)
                    {
                        tagNodeLast.SetValue(string.Format("$ = \"{0}\" + $ + $$", label));
                    }
                    else
                    {
                        tagNodeLast.SetValue(string.Format("$ = \"{0}\" + $$", label));
                    }

                    itemNodeLast.Add(refNodeLast);
                    itemNodeLast.Add(tagNodeLast);
                    atomNode.Add(itemNodeLast);
                }
                //atomNode.Add(oneofNode);
                Grammar.Add(atomNode);
            }

            /*
             *Add rule that Election grama that will call. 
             */
            XElement RAtomNode = new XElement("rule", new XAttribute("id", rootId));
            XElement ROneofNode = new XElement("one-of");
            foreach (string ruleGram in gramaRuleHs)
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

            Grammar.Save(grammaFile);

        }

        public static void loadTokens(string tokenfile, Dictionary<string, List<string>> tokenValues)
        {
            // load tokens and response values and store them into Dictionary<string, List<string>> tokenValues;
            using (StreamReader sr = new StreamReader(tokenfile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    if (arr.Length != 2)
                        continue;
                    string value = arr[0];
                    value = value.Substring("qpv2tkn-".Length);
                    // value = StopWordsDelete(value);
                    string key = arr[1].Split(';')[0].Trim(new char[] { '<', '>' });
                    if (!key.Contains('.'))
                    {
                        continue;
                    }
                    if (!tokenValues.ContainsKey(key))
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
                foreach (KeyValuePair<string, List<string>> pair in tokenValues)
                {
                    sw.WriteLine("{0}\t{1}", pair.Key, string.Join("\t", pair.Value));
                }
            }

        }
    }
}
