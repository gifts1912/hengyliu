using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace ElectionImprove.QAS
{
    class PCFGGrammaFileGenerate
    {
        private static string DomainName = "MSElection";
        private static HashSet<string> needSlotsHs = new HashSet<string>();
        public static void Run(string [] args)
        {
            if(args.Length == 0)
            {
                args = new string[4];
                args[0] = @"D:\demo\ElectionRules.tsv";
                args[1] = @"D:\demo\ElectionTokens.tsv";
                args[2] = @"D:\sumStoneTemplate\pcfg\MSElectionFinallyv3.0\MSElection.grammar_v3.0.xml";
                //args[3] = "election.debate.plural election.debate.plural election.debate.plural election.debate.plural election.debate.plural election.debate.plural election.debate.plural election.debate.single election.debate.single election.debate.single election.debate.single election.debate.single election.debate.single location.location election.usa election.usa election.state election.usa election.usa election.usa election.usa onelection.bpiissue";
                args[3] = "election.debate.plural;election.debate.plural;election.debate.plural;election.debate.plural;election.debate.plural;election.debate.plural;election.debate.plural;election.debate.single;election.debate.single;election.debate.single;election.debate.single;election.debate.single;election.debate.single;location.location;election.usa;election.usa;election.state;election.usa;election.usa;election.usa;election.usa;onelection.bpiissue";
            }

            string rulesFile = args[0];
            string tokensFile = args[1];
            string outGrammaFile = args[2];
            string filterRuleContainSlot = args[3];

            GenerateGrammaFile(rulesFile, tokensFile, outGrammaFile, filterRuleContainSlot);
        }
        public static void GenerateGrammaFile(string rulesFile, string tokensFile, string outGrammaFile, string filterRuleContainSlot)
        {
            HashSet<string> filterRuleContainSlotHs = new HashSet<string>(filterRuleContainSlot.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
            Dictionary<string, string> rulesIntentDic = new Dictionary<string, string>();
            LoadRules(rulesFile, rulesIntentDic, filterRuleContainSlotHs);

            Dictionary<string, List<string>> tokenValues = new Dictionary<string, List<string>>();
            LoadTokens(tokensFile, tokenValues);

            GenerateGramma(rulesIntentDic, tokenValues, outGrammaFile);
        }
        public static void LoadRules(string rulesFile, Dictionary<string, string> rulesIntentDic, HashSet<string> filterRuleContainSlot)
        {
            StreamReader sr = new StreamReader(rulesFile);
            string line;
            int startIdx = "qpv2rule-".Length;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string rule = arr[0], intent = arr[1];
                rule = rule.Substring(startIdx);
                rule = rule.Replace("<", "").Replace(">", "");
                if (FilterRule(rule, filterRuleContainSlot))
                    continue;
                string[] valueArr = intent.Split(';');
                intent = valueArr[valueArr.Length - 4];
                rulesIntentDic[rule] = intent;
            }
            sr.Close();
        }

        public static bool FilterRule(string rule, HashSet<string> filterHs)
        {
            string [] ruleArr = rule.Split(' ');
            foreach(string ele in ruleArr)
            {
                if (filterHs.Contains(ele))
                    return true;
            }
            return false;
        }
        public static void LoadTokens(string tokensFile, Dictionary<string, List<string>> tokenValues)
        {
            StreamReader sr = new StreamReader(tokensFile);
            string line;
            int startIdx = "qpv2tkn-".Length;
            while ((line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split('\t');
                string tokenValue = arr[0], tokenKey = arr[1];
                tokenValue = tokenValue.Substring(startIdx);
                tokenKey = tokenKey.Split(';')[0];
                tokenKey = tokenKey.Replace("<", "").Replace(">", "");
                if(!tokenValues.ContainsKey(tokenKey))
                {
                    tokenValues[tokenKey] = new List<string>();
                }
                tokenValues[tokenKey].Add(tokenValue);
            }
            sr.Close();
        }
        public static void GenerateGramma(Dictionary<string, string> rulesIntentDic, Dictionary<string, List<string>> tokenValues, string outGrammaFile)
        {
            XElement grammarNode = new XElement("grammar", new XAttribute("root", DomainName));

            GenerateTokensGrammar(tokenValues, grammarNode);

            HashSet<string> rulesAdded = new HashSet<string>();
            GenerateRulesGrammar(rulesIntentDic, ref rulesAdded, grammarNode);
            GenerateMainGrammar(rulesAdded, grammarNode);
            grammarNode.Save(outGrammaFile);
        }

        public static void GenerateMainGrammar(HashSet<string> rulesAdded, XElement grammarNode)
        {
            XElement ruleNode = new XElement("rule", new XAttribute("id", DomainName));
            XElement oneOfNode = new XElement("one-of");
            foreach(string rule in rulesAdded)
            {
                XElement itemNode = new XElement("item");
                XElement ruleRefNode = new XElement("ruleref", new XAttribute("uri", "#" + rule));
                XElement tagNode = new XElement("tag");
                tagNode.SetValue("$=$$");
                itemNode.Add(ruleRefNode);
                itemNode.Add(tagNode);
                oneOfNode.Add(itemNode);
            }
            ruleNode.Add(oneOfNode);
            grammarNode.Add(ruleNode);
        }
        public static void GenerateRulesGrammar(Dictionary<string, string> rulesIntentDic, ref HashSet<string> rulesAdded, XElement grammarNode)
        {
            /*
             * Generate rule name pre and current idex
             */
            HashSet<string> RuleNamePre = new HashSet<string>(rulesIntentDic.Values);
            Dictionary<string, int> RuleNameCurIdx = new Dictionary<string, int>();
            foreach(string rule in RuleNamePre)
            {
                RuleNameCurIdx[rule] = 0;
            }

            string intent;
            foreach (KeyValuePair<string, string> ruleIntentEle in rulesIntentDic)
            {
                intent = ruleIntentEle.Value;
                int idx = RuleNameCurIdx[intent];
                RuleNameCurIdx[intent]++;
                intent = intent.Replace(".", "").Replace(" ", "").ToLower();
                intent = string.Format("{0}_{1}", intent, idx);

                XElement ruleNode = new XElement("rule", new XAttribute("id", intent));
                string rule = ruleIntentEle.Key;
                string[] ruleArr = rule.Split(' ');
                XElement itemNode = null;
                for (int i = 0; i < ruleArr.Length; i++)
                {
                    string ele = ruleArr[i];
                    itemNode = new XElement("item");
                    if(ele.IndexOf('.') == -1)
                    {
                        itemNode.SetValue(ele);
                    }
                    else
                    {
                        XElement refNode = new XElement("ruleref", new XAttribute("uri", "#" + ele));
                        itemNode.Add(refNode);
                    }
                    if(i != ruleArr.Length - 1)
                    {
                        ruleNode.Add(itemNode);
                    }
                }
                XElement tagNode = new XElement("tag");
                string tagValue = GenerateTagValue(rule, intent);
                tagNode.SetValue(string.Format("$=\"{0}\"", tagValue));
                itemNode.Add(tagNode);
                ruleNode.Add(itemNode);
                grammarNode.Add(ruleNode);
                rulesAdded.Add(intent);
            }
        }
        public static void GenerateTokensGrammar(Dictionary<string, List<string>> tokenValues, XElement grammarNode)
        {
            foreach(KeyValuePair<string, List<string>> pair in tokenValues)
            {
                string tokenName = pair.Key;
                XElement ruleNode = new XElement("rule", new XAttribute("id", tokenName));
                XElement oneOfNode = new XElement("one-of");
                foreach(string ele in pair.Value)
                {
                    XElement itemNode = new XElement("item");
                    itemNode.SetValue(ele);
                    oneOfNode.Add(itemNode);
                }
                ruleNode.Add(oneOfNode);
                grammarNode.Add(ruleNode);
            }
        }

        public static string GenerateTagValue(string rule, string intent)
        {
            rule = rule.Replace(" ", "").Replace(".", "").ToLower();
            return string.Format("{0}_{1}", intent, rule);
        }
    }
}
