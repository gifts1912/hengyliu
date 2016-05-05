using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace QAS.PCFG.grammarToLabelId
{
    class grammarToLabelId
    {
        public static void Run(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[3];
                args[0] = @"D:\sumStoneTemplate\pcfg\MSElection\MSElection.grammar.xml";
                args[1] = @"D:\sumStoneTemplate\pcfg\MSElection\MSElectionIdx.grammar.xml";
                args[2] = @"D:\sumStoneTemplate\pcfg\MSElection\SlotToIdx.tsv";
            }
            string grammarFile = args[0];
            string grammarTurnFile = args[1];
            string slotIdxFile = args[2];

            GrammarTokenLabel(grammarFile, grammarTurnFile, slotIdxFile);
            //GrammarTokenLabel(grammarFile, grammarTurnFile);
        }

        public static void GrammarTokenLabel(string rawXmlFile, string newXmlFile, string slotIdxFile)
        {
            List<int> layerCurIdx = new List<int>(new int[3]);
            layerCurIdx[0] = 1; // the index that can use in intent layer
            layerCurIdx[1] = 1; // the index that can use in slot layer
            layerCurIdx[2] = 1; // the index that can use in slot value layer
            int layerIdex = -1, curIdx = -1;
            Dictionary<string, int> slotIdxDic = new Dictionary<string, int>();
            XDocument xdc = XDocument.Load(rawXmlFile);
            XElement rootNode = xdc.Element("grammar");
            foreach(XElement node in rootNode.Elements("rule"))
            {
                string ruleId = node.Attribute("id").Value;
                if(!slotIdxDic.ContainsKey(ruleId))
                {
                    if(ruleId.IndexOf("_") != -1)
                    {
                        layerIdex = 0;
                    }
                    else if(ruleId.IndexOf(".") != -1)
                    {
                        layerIdex = 1;
                    }
                    else
                    {
                        layerIdex = 2;
                    }
                    slotIdxDic[ruleId] = layerCurIdx[layerIdex];
                    layerCurIdx[layerIdex]++;
                }
            }

            StreamWriter sw = new StreamWriter(slotIdxFile);
            foreach(KeyValuePair<string, int> pair in slotIdxDic)
            {
                if (pair.Key.Equals("MSElection", StringComparison.OrdinalIgnoreCase))
                    continue;
                string value = string.Format("{0:D4}", pair.Value);
                sw.WriteLine("{0}\t{1}", pair.Key, value);
            }
            sw.Close();
        }
        public static void GrammarTokenLabel(string grammarFile, string grammarTurnFile)
        {
            try
            {
                XDocument xdoc = new XDocument(
                    new XElement("Users", new XElement("User", 
                        new XAttribute("ID", "11111"),
                        new XElement("name", "EricSun"),
                        new XElement("password", "123456"),
                        new XElement("description", "Hello I'm from Dalinan")
                        ),
                        new XElement("User", 
                        new XAttribute("ID", "22222"),
                        new XElement("name", "Ray"),
                        new XElement("password", "654321"),
                        new XElement("description", "Hello I'm from JiLin")                     
                    )   )    );
              xdoc.Save(grammarTurnFile);
            }
            catch(Exception ec)
            {
                Console.WriteLine(ec.ToString());
            }


            try
            {
                XDocument myXDoc = XDocument.Load(grammarFile);
                XElement rootNode = myXDoc.Element("grammar");
                foreach(XElement xel in rootNode.Elements("rule"))
                {
                    Console.WriteLine(xel.Attribute("id").Value);
                }
                Console.ReadKey();
            }
            catch(Exception ec)
            {
                Console.WriteLine(ec.ToString());
            }
        }
    }
}
