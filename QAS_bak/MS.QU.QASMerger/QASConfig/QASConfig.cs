using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Web.Configuration;
using System.Runtime.Serialization;
using System.Diagnostics;


namespace QASConfig
{
    [Serializable]
    [DataContract(Name = "Client", Namespace = "QASConfig")]
    public class Client
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Dictionary<string, Domain> Domains { get; set; }

        public Client(string name)
        {
            this.Name = name;
            Domains = new Dictionary<string, Domain>();
        }
    }

    [Serializable]
    [DataContract(Name = "Domain", Namespace = "QASConfig")]
    public class Domain
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DefinitionName { get; set; }
        [DataMember]
        public Dictionary<string, Component> Components { get; set; }
        [DataMember]
        public List<string> Dependents { get; set; }
        [DataMember]
        public string EntityExtractSchemaFile { get; set; }
        [DataMember]
        public string DefinitionSection { get; set; }

        [DataMember]
        public List<string> FileList { get; set; }

        public Domain(string name)
        {
            this.Name = name;
            Components = new Dictionary<string, Component>();
            Dependents = new List<string>();
            this.EntityExtractSchemaFile = String.Empty;
            this.DefinitionSection = String.Empty;
            FileList = new List<string>();
        }
    }

    [Serializable]
    [DataContract(Name = "Component", Namespace = "QASConfig")]
    public class Component
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DefinitionName { get; set; }
        [DataMember]
        public List<string> Dependents { get; set; }
        [DataMember]
        public string EntityExtractSchemaFile { get; set; }
        [DataMember]
        public string DefinitionSection { get; set; }

        public Dictionary<string, string> Parameters
        {
            get
            {
                if (string.IsNullOrEmpty(DefinitionSection))
                {
                    return new Dictionary<string, string>();
                }
                return DefinitionSection
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(o => o.Trim())
                    .Select(o => o.StartsWith(";") ? "" : o)
                    .Where(o => !string.IsNullOrEmpty(o))
                    .Select(o =>
                    {
                        var parts = o.Split(new[] { '=' });
                        var key = "";
                        var value = "";
                        if (parts.Length >= 2)
                        {
                            key = parts[0];
                            value = parts[1];
                        }
                        return new { Key = key, Value = value };
                    })
                    .Where(o => !string.IsNullOrEmpty(o.Key))
                    .ToDictionary(o => o.Key, o => o.Value);
            }
        }
        public Component(string name)
        {
            this.Name = name;
            Dependents = new List<string>();
            this.EntityExtractSchemaFile = String.Empty;
            this.DefinitionSection = String.Empty;
        }
    }

    [Serializable]
    [DataContract(Name = "QASConfig", Namespace = "QASConfig")]
    public class QASConfiguration
    {
        [DataMember]
        public string file;
        [DataMember]
        public string configVar;
        //section -> key -> value
        [DataMember]
        Dictionary<string, Dictionary<string, string>> qasConfigSections;
        [DataMember]
        Dictionary<string, string> qasConfigFullSections;
        [DataMember]
        Dictionary<string, Component> componentList;

        private readonly Dictionary<string, string> allFileList;        
        private readonly Regex fileRefRegex = new Regex(@"=\s*(?<fileRef>[^\s]+\.\w{3,})(\s|$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex xmlFileRef = new Regex(@"""(?<fileRef>[^""]+\.\w{3})""", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string ModelDirectory { get; set; }

        [DataMember]
        public string Column { get; set; }
        [DataMember]
        public Dictionary<string, Client> ClientList { get; set; }

        [DataMember]
        public Dictionary<string, Dictionary<string, string>> QASConfigSections
        {
            get
            {
                return qasConfigSections;
            }
            set
            {
                qasConfigSections = value;
            }
        }

        public Dictionary<string, Component> ComponentList
        {
            get
            {
                return componentList;
            }
            set
            {
                componentList = value;
            }
        }

        public QASConfiguration(string file)
        {
            this.file = file;
            string fileName = Path.GetFileName(file);
            allFileList = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            ModelDirectory = Path.GetDirectoryName(this.file);

            configVar = fileName.Substring(0, fileName.Length - 33);
            qasConfigSections = new Dictionary<string, Dictionary<string, string>>();
            qasConfigFullSections = new Dictionary<string, string>();
            componentList = new Dictionary<string, Component>();
            ClientList = new Dictionary<string, Client>();

            LoadConfiguration();
            if (QASConfigSections.Count > 0)
            {
                BuildMappings();
            }
        }

        public QASConfiguration()
        {
            allFileList = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);           
                       
            qasConfigSections = new Dictionary<string, Dictionary<string, string>>();
            qasConfigFullSections = new Dictionary<string, string>();
            componentList = new Dictionary<string, Component>();
            ClientList = new Dictionary<string, Client>();
        }

        public void LoadConfiguration()
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string section = String.Empty;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    line = line.Trim();
                    if (line.Length == 0 || line.StartsWith(";"))
                    {
                        continue;
                    }
                    if (line.StartsWith("["))
                    {
                        section = line.Substring(1, line.Length - 2);
                        continue;
                    }
                    if (section.Length == 0)
                    {
                        continue;
                    }
                    string[] terms = line.Split('=');
                    if (terms.Length < 2)
                    {
                        continue;
                    }
                    string key = terms[0].Trim();
                    string value = String.Empty;
                    for (int i = 1; i < terms.Length; i++)
                    {
                        //Should not exist cases such as length > 2, maybe need to double check later
                        value += terms[i].Trim();
                    }
                    if (qasConfigSections.ContainsKey(section))
                    {
                        if (!qasConfigSections[section].ContainsKey(key))
                        {
                            qasConfigSections[section].Add(key, value);
                        }
                        qasConfigFullSections[section] += "\r\n" + line;
                    }
                    else
                    {
                        Dictionary<string, string> tmp = new Dictionary<string, string>();
                        tmp.Add(key, value);
                        qasConfigSections.Add(section, tmp);
                        qasConfigFullSections.Add(section, line);
                    }
                }
            }
        }

        public bool BuildMappings()
        {
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                CreateAllFileList(allFileList);

                ClientList.Add("Default", new Client("Default"));
                if (QASConfigSections.ContainsKey("clients"))
                {
                    foreach (string client in QASConfigSections["clients"].Keys)
                    {
                        ClientList.Add(client, new Client(client));
                    }
                }
                foreach (string client in ClientList.Keys)
                {
                    ProcessClient(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while build mapping: " + e.Message);
                throw;
            }
            sw.Stop();
            Console.WriteLine("BuildMappings took {0}.", sw.Elapsed);
            return true;
        }

        private void CreateAllFileList(Dictionary<string, string> allFileList)
        {
            allFileList.Clear();

            var targetFileList = new List<string>();

            string configVarLower = this.configVar.ToLower();
            if (configVarLower.Equals("ranker"))
            {
                targetFileList.Add("XAPQCS-en-us.txt");
                targetFileList.Add("XAPQCSA.txt");
                targetFileList.Add("QasDsIV.txt");
                targetFileList.Add("QASBGS.txt");
                targetFileList.Add("QASPrototypes.txt");
                targetFileList.Add("QASSangam.txt");
            }

            /*
            string[] terms = this.configVar.Split('.');
            if (terms.Length > 1)
            {
                targetFileList.Add(String.Format("{0}.txt", terms[1]));
            }
             * */

            if (targetFileList.Count > 0)
            {
                foreach (string tfile in targetFileList)
                {
                    string targetFile = Path.Combine(ModelDirectory, tfile);
                    if (File.Exists(targetFile))
                    {
                        using (StreamReader sr = new StreamReader(targetFile))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine().ToLower();
                                if (!allFileList.ContainsKey(line))
                                {
                                    allFileList.Add(line, line);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                string[] allFiles = Directory.GetFiles(ModelDirectory);
                foreach (string f in allFiles)
                {
                    string ff = Path.GetFileName(f).ToLower();
                    if (!f.ToLower().Contains(".queryprocessingconfiguration.ini"))
                    {
                        allFileList.Add(ff, ff);
                    }
                }
            }
        }

        private void ProcessClient(string client)
        {
            string domainDefinitionSection = "query_domains";
            if (!client.Equals("Default"))
            {
                domainDefinitionSection = "query_domains:" + client;
            }
            if (!QASConfigSections.ContainsKey(domainDefinitionSection))
            {
                return;
            }
            Dictionary<string, string> domains = QASConfigSections[domainDefinitionSection];
            foreach (string domain in domains.Keys)
            {
                string domainInfo = domains[domain];
                string domainInfoTrimmed = domainInfo.Trim();

                Dictionary<string, Domain> domainsInfo = ClientList[client].Domains;
                if (domain.Equals("deriveFromClient"))
                {
                    string derivedClient = "Default";
                    if (domainInfoTrimmed.Length > 0)
                    {
                        derivedClient = domainInfoTrimmed;
                    }
                    if (ClientList.ContainsKey(derivedClient))
                    {
                        Dictionary<string, Domain> derivedClientDomains = ClientList[derivedClient].Domains;
                        foreach (string d in derivedClientDomains.Keys)
                        {
                            if (!domainsInfo.ContainsKey(d))
                            {
                                Domain dom = Helper.CloneObject<Domain>(derivedClientDomains[d]);
                                domainsInfo.Add(d, dom);
                            }
                        }
                    }
                    continue;
                }
                if (domainInfoTrimmed.Length == 0)
                {
                    //Domain disabled
                    continue;
                }
                if (domainsInfo.ContainsKey(domain))
                {
                    //Domain gets redefined
                    domainsInfo.Remove(domain);
                    domainsInfo = domainsInfo.ToDictionary(o => o.Key, o => o.Value);
                    ClientList[client].Domains = domainsInfo;
                }
                domainsInfo.Add(domain, new Domain(domain));
                Domain domainObject = domainsInfo[domain];
                domainObject.DefinitionName = domainInfoTrimmed;
                string domainDetailDefinitionSection = domainInfoTrimmed + "_query_analysis";

                domainObject.DefinitionSection = qasConfigFullSections[domainDetailDefinitionSection];
                foreach (string componentName in QASConfigSections[domainDetailDefinitionSection].Keys)
                {
                    Dictionary<string, Component> components = domainObject.Components;
                    if (!components.ContainsKey(componentName))
                    {
                        Component c = new Component(componentName);
                        components.Add(componentName, c);
                    }

                    string defName = QASConfigSections[domainDetailDefinitionSection][componentName];
                    if (!qasConfigSections.ContainsKey(defName))
                    {
                        foreach (string sName in qasConfigSections.Keys)
                        {
                            if (defName.ToLower().Equals(sName.ToLower()))
                            {
                                defName = sName;
                                break;
                            }
                        }
                    }
                    Component component = components[componentName];
                    component.DefinitionName = defName;
                    component.DefinitionSection = qasConfigFullSections[defName];
                    //Load file list for the domain
                    foreach (string parameter in QASConfigSections[defName].Keys)
                    {
                        var fileList = domainObject.FileList;
                        string value = QASConfigSections[defName][parameter];
                        string parameterLower = parameter.ToLower();
                        string valueLower = value.ToLower();

                        switch (parameterLower)
                        {
                            case "param:pipelinebasefilename":
                                ProcessPipeline(value, fileList);
                                break;
                            case "param:entities":
                            case "param:entitiesvnext":
                                ProcessEntities(value, fileList);
                                break;
                            case "param:intentenginenormalizerconfigfile":
                                ProcessMLG(value, fileList);
                                break;
                            case "param:datastorelookup_datastores":
                                ProcessCuckoo(value, fileList);
                                break;
                            case "param:lesconfig":
                            case "param:lesconfigvnext":
                            case "param:lesconfigontologyfeature":
                                ProcessLESConfig(value, fileList);
                                break;
                            case "filename":
                                ProcessFilename(valueLower, fileList);
                                break;
                            case "param:intentenginenormalizerconfigfile10":
                                ProcessXml(valueLower, fileList);
                                break;
                            default:
                                if (parameterLower.Contains("param:intentenginenormalizerconfigfile"))
                                {
                                    ProcessMLG(value, fileList);
                                }
                                else
                                {
                                    ProcessOther(valueLower, fileList, parameterLower.Contains("basefilename"));
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void ProcessOther(string file, List<string> fileList, bool partialFileNameMatch)
        {
            #region Parameter Process For other File

            var fileRefs = GetFileRefs("=" + file, null).ToList();

            if (!string.IsNullOrEmpty(fileRefs.FirstOrDefault()) || partialFileNameMatch)
            {
                if ((fileRefs.Count > 0 && !allFileList.ContainsKey(fileRefs[0])) || partialFileNameMatch)
                {
                    fileRefs = allFileList.Keys.Where(o => o.StartsWith(file)).ToList();
                }
                foreach (var fileRef in fileRefs)
                {
                    if (!string.IsNullOrEmpty(fileRef) && !fileList.Contains(fileRef))
                    {
                        Add(fileList, fileRef);
                    }
                }
            }
            #endregion
        }

        private void ProcessFilename(string file, List<string> fileList)
        {
            #region Parameter Process For "filename"

            foreach (string key in allFileList.Keys)
            {
                if (file.Contains(key) || key.Contains(file))
                {
                    if (!fileList.Contains(key))
                    {
                        Add(fileList, key);
                    }
                }
            }
            #endregion
        }

        private IEnumerable<string> GetFileRefs(string line, Regex r)
        {
            if (r == null)
            {
                r = fileRefRegex;
            }
            var matches = r.Matches(line);
            return from Match m in matches select m.Groups["fileRef"].Value.Trim();
        }

        private void ProcessLESConfig(string file, List<string> fileList)
        {
            #region Parameter Process For Loading LES config

            string lesConfigFile = file;
            string lesConfigFileFull = Path.Combine(ModelDirectory, lesConfigFile);
            if (File.Exists(lesConfigFileFull))
            {
                if (!fileList.Contains(lesConfigFile))
                {
                    Add(fileList, lesConfigFile);
                }

                using (StreamReader sr = new StreamReader(lesConfigFileFull))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine().ToLower();
                        var key = GetFileRefs(line, null).FirstOrDefault();

                        if (!string.IsNullOrEmpty(key) && allFileList.ContainsKey(key))
                        {
                            {
                                if (!fileList.Contains(key))
                                {
                                    Add(fileList, key);
                                }

                                if (key.Contains("pipeline"))
                                {
                                    ProcessPipeline(key, fileList);
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void ProcessCuckoo(string file, List<string> fileList)
        {
            #region Parameter Process For cuckoo File

            string[] filePrefixes = file.Split(',').Select(o => o.ToLower()).ToArray();
            string[] fileSufixes = { "", ".data.bin", ".index.bin", ".offsets.bin" };
            foreach (string filePrefix in filePrefixes)
            {
                foreach (string fileSufix in fileSufixes)
                {
                    {
                        string fileName = filePrefix + fileSufix;
                        if (allFileList.ContainsKey(fileName))
                        {
                            string fullPath = Path.Combine(ModelDirectory, fileName);
                            if (!fileList.Contains(fileName))
                            {
                                Add(fileList, fileName);
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void ProcessMLG(string file, List<string> fileList)
        {
            #region Parameter Process For MLG File

            string configFile = file + ".Config.xml";
            if (File.Exists(Path.Combine(ModelDirectory, configFile)))
            {
                if (!fileList.Contains(configFile))
                {
                    Add(fileList, configFile);
                }
                using (StreamReader sr = new StreamReader(Path.Combine(ModelDirectory, configFile)))
                {
                    Regex regex = new Regex("<(.+) name=\"(.+)\"");
                    Match match;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        match = regex.Match(line);
                        if (match.Success)
                        {
                            string keyStr = match.Groups[1].ToString();
                            string aFile = match.Groups[2].ToString();
                            if (File.Exists(Path.Combine(ModelDirectory, aFile)))
                            {
                                if (!fileList.Contains(aFile))
                                {
                                    Add(fileList, aFile);
                                }
                            }
                            if (keyStr.Equals("interpretationprinciples"))
                            {
                                using (StreamReader sri = new StreamReader(Path.Combine(ModelDirectory, aFile)))
                                {
                                    Regex regexI = new Regex("name=\"(.+)\"");
                                    Match matchI;
                                    while (!sri.EndOfStream)
                                    {
                                        string lineI = sri.ReadLine();
                                        matchI = regexI.Match(lineI);
                                        if (matchI.Success)
                                        {
                                            foreach (string iFile in allFileList.Keys)
                                            {
                                                if (lineI.ToLower().Contains(iFile.ToLower()))
                                                {
                                                    if (!fileList.Contains(iFile))
                                                    {
                                                        Add(fileList, iFile);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void ProcessEntities(string file, List<string> fileList)
        {
            #region Parameter Process For Entity File

            if (!file.ToLower().EndsWith(".xml"))
            {
                file += ".Config.xml";
            }
            if (File.Exists(Path.Combine(ModelDirectory, file)))
            {
                if (!fileList.Contains(file))
                {
                    Add(fileList, file);
                }
            }
            #endregion
        }


        private void ProcessPipeline(string file, List<string> fileList)
        {
            #region Parameter Process For Pipeline File
            string pipeLineFile = file;
            if (!pipeLineFile.Contains("pipeline"))
            {
                pipeLineFile = String.Format("{0}.pipeline.txt", file);
            };
            string pipeLineFileFull = pipeLineFile;
            if (!pipeLineFile.Contains("\\"))
            {
                pipeLineFileFull = Path.Combine(ModelDirectory, pipeLineFile);
            }

            if (File.Exists(pipeLineFileFull))
            {
                if (!fileList.Contains(pipeLineFile))
                {
                    Add(fileList, pipeLineFile);
                }
                using (StreamReader sr = new StreamReader(pipeLineFileFull))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine().ToLower();
                        var fileRefs = GetFileRefs(line, null);
                        foreach (var fileRef in fileRefs)
                        {
                            ProcessOther(fileRef, fileList, false);
                        }
                    }
                }
            }
            #endregion
        }

        private void ProcessXml(string file, List<string> fileList)
        {
            var configFile = file;
            if (!configFile.Contains("config.xml"))
            {
                configFile = file + ".config.xml";
            }
            string configFileFull = configFile;
            if (!configFile.Contains("\\"))
            {
                configFileFull = Path.Combine(ModelDirectory, configFile);
            }
            if (File.Exists(configFileFull))
            {
                if (!fileList.Contains(configFile))
                {
                    ProcessOther(file, fileList, true);
                }
                using (StreamReader sr = new StreamReader(configFileFull))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine().ToLower();
                        var fileRefs = GetFileRefs(line, xmlFileRef);
                        foreach (var fileRef in fileRefs)
                        {
                            AddFileRef(fileRef, fileList);
                        }
                    }
                }
            }
        }

        private void AddFileRef(string fileRef, List<string> fileList)
        {
            if (allFileList.ContainsKey(fileRef))
            {
                if (!fileList.Contains(fileRef))
                {
                    Add(fileList, fileRef);
                }
                if (fileRef.Contains(".config.ini"))
                {
                    ProcessPipeline(fileRef, fileList);
                }
            }
        }

        private void Add(List<string> fileList, string value)
        {
            value = value.Trim();
            fileList.Add(value);
        }

        public void BuildDependency()
        {
            if (ClientList.Count == 0)
            {
                BuildMappings();
            }

            //output -> component
            Dictionary<string, string> outputList;

            foreach (string client in ClientList.Keys)
            {
                outputList = new Dictionary<string, string>();

                foreach (string domain in ClientList[client].Domains.Keys)
                {
                    foreach (string componentName in ClientList[client].Domains[domain].Components.Keys)
                    {
                        string componentDefinitionName = ClientList[client].Domains[domain].Components[componentName].DefinitionName;
                        if (!QASConfigSections.ContainsKey(componentDefinitionName))
                        {
                            continue;
                        }
                        if (componentDefinitionName.Contains("domainclassifier"))
                        {
                            string insertKey = String.Format("{0}:{1}", domain, "DomainConfidenceLevel");
                            if (outputList.ContainsKey(insertKey))
                            {
                                if (outputList[insertKey].Equals(componentName))
                                {
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("Warning: Same DomainConfidenceLevel map to different component: " + insertKey + "@@@@@" + outputList[insertKey] + "#####" + componentDefinitionName);
                                    continue;
                                }
                            }
                            outputList.Add(String.Format("{0}:{1}", domain, "DomainConfidenceLevel"), componentName);
                        }
                        foreach (string key in QASConfigSections[componentDefinitionName].Keys)
                        {
                            if (key.Equals("output"))
                            {
                                string[] terms = QASConfigSections[componentDefinitionName][key].Split(',');
                                foreach (string term in terms)
                                {
                                    string outputName = String.Format("{0}:{1}", domain, term.Replace('.', ':'));
                                    if (!outputList.ContainsKey(outputName))
                                    {
                                        outputList.Add(outputName, componentName);
                                    }
                                    else
                                    {
                                        if (!componentName.Equals(outputList[outputName]))
                                        {
                                            if (componentDefinitionName.Contains("featurizer"))
                                            {
                                                outputList[outputName] = componentName;
                                            }
                                            Console.WriteLine("Warning: Same output detected: " + outputName + "==>" + componentName + " & " + outputList[outputName] + " in " + Path.GetFileName(file));
                                        }
                                    }
                                }
                            }
                            if (key.Equals("param:Labels", StringComparison.OrdinalIgnoreCase))
                            {
                                ClientList[client].Domains[domain].Components[componentName].EntityExtractSchemaFile = QASConfigSections[componentDefinitionName][key];
                            }
                            if (key.Equals("param:Entities", StringComparison.OrdinalIgnoreCase))
                            {
                                ClientList[client].Domains[domain].Components[componentName].EntityExtractSchemaFile = QASConfigSections[componentDefinitionName][key];
                            }
                        }

                    }

                }

                foreach (string domain in ClientList[client].Domains.Keys)
                {
                    foreach (string componentName in ClientList[client].Domains[domain].Components.Keys)
                    {
                        string componentDefinitionName = ClientList[client].Domains[domain].Components[componentName].DefinitionName;
                        if (!QASConfigSections.ContainsKey(componentDefinitionName))
                        {
                            continue;
                        }
                        if (QASConfigSections[componentDefinitionName].ContainsKey("input"))
                        {
                            string[] terms = QASConfigSections[componentDefinitionName]["input"].Split(',');
                            foreach (string term in terms)
                            {
                                string input = term.Replace('.', ':');
                                string[] nameTerms = input.Split(':');
                                string dependentDomain = String.Empty;
                                if (nameTerms.Length == 1)
                                {
                                    input = String.Format("{0}:{1}", domain, nameTerms[0]);
                                    dependentDomain = domain;
                                }
                                if (nameTerms.Length > 1)
                                {
                                    input = String.Format("{0}:{1}", nameTerms[0], nameTerms[1]);
                                    dependentDomain = nameTerms[0];
                                }
                                if (nameTerms.Length > 3)
                                {
                                    input = String.Format("{0}:{1}", input, nameTerms[2]);
                                }
                                if (outputList.ContainsKey(input))
                                {
                                    if (!ClientList[client].Domains[domain].Components[componentName].Dependents.Contains(outputList[input]))
                                    {
                                        ClientList[client].Domains[domain].Components[componentName].Dependents.Add(outputList[input]);
                                    }
                                    if (!ClientList[client].Domains[domain].Dependents.Contains(dependentDomain))
                                    {
                                        ClientList[client].Domains[domain].Dependents.Add(dependentDomain);
                                    }
                                }
                            }
                        }
                        //special case for MLG34Featurier
                        if (QASConfigSections[componentDefinitionName].ContainsKey("param:FeatureSetNameDomainMapping"))
                        {
                            string[] terms = QASConfigSections[componentDefinitionName]["param:FeatureSetNameDomainMapping"].Split(',');
                            foreach (string term in terms)
                            {
                                string input = term.Replace('.', ':');
                                string[] nameTerms = input.Split(':');
                                if (nameTerms.Length < 2)
                                {
                                    //Incorrect format?
                                    continue;
                                }
                                if (ClientList[client].Domains.ContainsKey(nameTerms[1]))
                                {
                                    if (!ClientList[client].Domains[domain].Dependents.Contains(nameTerms[1]))
                                    {
                                        ClientList[client].Domains[domain].Dependents.Add(nameTerms[1]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Serialize(string outputFile)
        {
            Helper.DataContractSerialize<QASConfiguration>(this, outputFile);
        }
    }

    [Serializable]
    [DataContract(Name = "QASSetting", Namespace = "QASConfig")]
    public class QASSetting
    {
        [DataMember]
        public List<QASConfiguration> QASConfigs { get; set; }
        string searchGoldRoot;
        Dictionary<string, string> datasetConfig;

        public void LoadQASConfigs(string SGRoot)
        {
            QASConfigs = new List<QASConfiguration>();
            searchGoldRoot = SGRoot;
            List<string> configurationFiles = Directory.GetFiles(searchGoldRoot + @"\\deploy\\builds\\data\\answers\\xapquserviceanswer", "Default20100527*", SearchOption.TopDirectoryOnly).ToList();
            configurationFiles = configurationFiles.Concat(Directory.GetFiles(searchGoldRoot + @"\\deploy\\builds\\data\\answers\\xapquserviceanswer", "AnsInt*", SearchOption.TopDirectoryOnly)).ToList();
            configurationFiles = configurationFiles.Concat(Directory.GetFiles(searchGoldRoot + @"\\deploy\\builds\\data\\answersTest\\xapquserviceanswer", "Anspart*", SearchOption.TopDirectoryOnly)).ToList();
            configurationFiles = configurationFiles.Concat(Directory.GetFiles(searchGoldRoot + @"\\deploy\\builds\\data\\answersTest\\qasecosystem", "Anspart*", SearchOption.TopDirectoryOnly)).ToList();

            foreach (string file in configurationFiles)
            {
                QASConfiguration qasConfig = new QASConfiguration(file);
                foreach (string dataset in datasetConfig.Keys)
                {
                    if (file.ToLower().Contains(dataset.ToLower()))
                    {
                        qasConfig.Column = datasetConfig[dataset];
                        break;
                    }
                }
                qasConfig.BuildDependency();
                QASConfigs.Add(qasConfig);
            }
        }

        public void LoadDataSetSettings(string DSConfig)
        {
            string file = DSConfig;
            datasetConfig = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] terms = line.Split('\t');
                    datasetConfig.Add(terms[0], terms[1]);
                }
            }
        }

        public void Serialize(string outputFile)
        {
            Helper.DataContractSerialize<QASSetting>(this, outputFile);
        }
    }
}