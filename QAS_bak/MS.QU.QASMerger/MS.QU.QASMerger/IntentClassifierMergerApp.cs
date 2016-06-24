namespace MS.QU.QASMerger
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using QASConfig;

    public class IntentClassifierMergerApp : ApplicationBase
    {
        private QASConfigDecorator _ruleQasConfig;

        private QASConfigDecorator _trainedQasConfig;

        private QASConfigDecorator _outputQasConfig;

        public string DomainName { get; private set; }

        public string SegmentName { get; private set; }

        public string RuleModelPath { get; private set; }

        public string TrainedModelPath { get; private set; }

        public string OutputModelPath { get; private set; }

        public string Intent2IdMappingFile { get; private set; }

        public string ResourceDir { get; private set; }

        public string QueryProcessConfigFile
        {
            get { return string.Format("{0}.{1}.Intents.queryprocessingconfiguration.ini", DomainName, SegmentName); }
        }

        public string RuleModelDomainName
        {
            get { return string.Format("{0}RuleBased", this.DomainName); }
        }

        public string TrainedModelDomainName
        {
            get { return string.Format("{0}Trained", this.DomainName); }
        }

        public IntentClassifierMergerApp(CommandLineArgument cmd)
        {
            this.DomainName = cmd.DomainName;
            this.SegmentName = cmd.SegmentName;
            this.RuleModelPath = cmd.RuleModelPath;
            this.TrainedModelPath = cmd.TrainedModelPath;
            this.OutputModelPath = cmd.OutputModelPath;
            this.Intent2IdMappingFile = cmd.Intent2IdMappingFile;
            this.ResourceDir = cmd.ResourceDir;
        }

        public override void Run()
        {
            LoadQASConfig();

            _ruleQasConfig.ChangeDomainName(this.RuleModelDomainName);
            _trainedQasConfig.ChangeDomainName(this.TrainedModelDomainName);

            _outputQasConfig = new QASConfigDecorator(this.OutputModelPath);

            _outputQasConfig.Merge(_trainedQasConfig);
            _outputQasConfig.Merge(_ruleQasConfig);

            Domain mergedDomain = CreateMergedIntentClassifier();

            _outputQasConfig.AddDomain(mergedDomain);

            PrepareFiles();

            _outputQasConfig.Save(QueryProcessConfigFile);
        }

        private void LoadQASConfig()
        {
            string ruleModelQcs = Directory.GetFiles(this.RuleModelPath)
                                           .Where(x => x.EndsWith("queryprocessingconfiguration.ini", StringComparison.CurrentCultureIgnoreCase))
                                           .First();

            string trainedModelQcs = Directory.GetFiles(this.TrainedModelPath)
                                           .Where(x => x.EndsWith("queryprocessingconfiguration.ini", StringComparison.CurrentCultureIgnoreCase))
                                           .First();

            QASConfig.QASConfiguration ruleQasConfig = new QASConfiguration(ruleModelQcs);
            QASConfig.QASConfiguration trainedQasConfig = new QASConfiguration(trainedModelQcs);
            ruleQasConfig.BuildDependency();
            trainedQasConfig.BuildDependency();

            _ruleQasConfig = new QASConfigDecorator(ruleQasConfig);
            _trainedQasConfig = new QASConfigDecorator(trainedQasConfig);
        }

        private QASConfig.Domain CreateMergedIntentClassifier()
        {
            string mergedDomainName = string.Format("{0}{1}Intents", this.DomainName, this.SegmentName);

            QASConfig.Domain domain = new Domain(mergedDomainName);
            
            domain.DefinitionName = "qd_" + mergedDomainName;

            string domainDetailDefinitionSection = domain.DefinitionName + "_query_analysis";

            //domainObject.DefinitionSection = qasConfigFullSections[domainDetailDefinitionSection];
            domain.DefinitionSection =
                string.Format("featurizer101={0}_featurizer\r\ndomainclassifier101={0}_domainclassifier\r\n\r\n", domain.DefinitionName);

            QASConfig.Component featComp = CreateFeaturizerComponent(mergedDomainName);
            QASConfig.Component domainComp = CreateDomainClassifierComponent(mergedDomainName);

            domain.Components.Add(featComp.Name, featComp);
            domain.Components.Add(domainComp.Name, domainComp);

            return domain;
        }

        private QASConfig.Component CreateFeaturizerComponent(string domainName)
        {
            QASConfig.Component featurizerComp = new Component("featurizer101");

            featurizerComp.DefinitionName = string.Format("qd_{0}_featurizer", domainName);

            StringBuilder compDefSec = new StringBuilder();
            compDefSec.AppendLine("implementationclassname=MLG34PipelineFeaturizer");
            compDefSec.AppendFormat(
                "input={0}:ipe_lu_{2}_{3}_enus_V1_intents_hotfix:ExternalInput1,{1}:MaxIntentScore:ExternalInput2",
                this.TrainedModelDomainName,
                this.RuleModelDomainName,
                this.DomainName,
                this.SegmentName);
            compDefSec.AppendLine();
            compDefSec.AppendLine("output=MaxIntentScore,MaxIntentId");
            compDefSec.AppendFormat("param:PipelineBaseFilename={0}.{1}.Intents.Hybrid", this.DomainName, this.SegmentName);
            compDefSec.AppendLine();
            compDefSec.AppendLine("param:OutputStringFeatures=true");
            compDefSec.AppendLine("param:ExpectNonEmptyFeatureSet=false");
            compDefSec.AppendFormat("param:FeatureNameDomainMapping=MaxIntentId:{0},MaxIntentScore:{0}", domainName);
            compDefSec.AppendLine();

            featurizerComp.DefinitionSection = compDefSec.ToString();

            return featurizerComp;
        }

        private QASConfig.Component CreateDomainClassifierComponent(string domainName)
        {
            QASConfig.Component domainClassComp = new Component("domainclassifier101");

            domainClassComp.DefinitionName = string.Format("qd_{0}_domainclassifier", domainName);

            StringBuilder compDefSec = new StringBuilder();

            compDefSec.AppendLine("implementationclassname=MLG34DomainClassifier");
            compDefSec.AppendLine("input=MaxIntentScore");
            compDefSec.AppendFormat("param:FeatureSetNameDomainMapping=MaxIntentScore:{0}", domainName);
            compDefSec.AppendLine();
            compDefSec.AppendLine("param:datastorelookup_implementationclassname=HashSetDataStoreLookupImpl");

            domainClassComp.DefinitionSection = compDefSec.ToString();

            return domainClassComp;
        }

        private void PrepareFiles()
        {
            string pipelineFileName = string.Format("{0}.{1}.Intents.Hybrid.pipeline.txt", this.DomainName, this.SegmentName);
            string tokenizerForIntentIdFileName = string.Format("{0}.{1}.Intents.tokenizerForIntentId.config.txt", this.DomainName, this.SegmentName);
            string intent2IdMapFileName = string.Format("{0}.{1}.Intents2IdMapping.config.txt", this.DomainName, this.SegmentName);

            using (StreamReader srPipe = new StreamReader(Path.Combine(this.ResourceDir, "MergedIntentClassifierPipelineTemplate.txt")))
            using (StreamReader srTokenizer = new StreamReader(Path.Combine(this.ResourceDir, "tokenizeforintentid.config.txt")))
            using (StreamReader srIntent2Id = new StreamReader(this.Intent2IdMappingFile))
            {
                string pipelineContent = srPipe.ReadToEnd();

                pipelineContent = pipelineContent.Replace("<<Intent2IdMapping>>", intent2IdMapFileName).Replace("<<TokenizeForIntentId>>", tokenizerForIntentIdFileName);

                _outputQasConfig.CreateFile(pipelineFileName, pipelineContent);

                string tokenizerContent = srTokenizer.ReadToEnd();

                _outputQasConfig.CreateFile(tokenizerForIntentIdFileName, tokenizerContent);

                string intentIdMapContent = srIntent2Id.ReadToEnd();

                _outputQasConfig.CreateFile(intent2IdMapFileName, intentIdMapContent);
            }
        }
    }
}
