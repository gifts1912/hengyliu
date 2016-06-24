namespace MS.QU.QASMerger
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using QASConfig;

    public class QASConfigDecorator
    {
        private const string defaultClientName = "Default";

        private QASConfiguration _qasConfig;

        public QASConfiguration QasConfig { get { return _qasConfig; } }

        public QASConfigDecorator(string modelDirPath)
        {
            if(Directory.Exists(modelDirPath))
            {
                Console.Error.WriteLine("WARNING: Path for output model has already existed, removed.");
                Directory.Delete(modelDirPath, true);
            }

            Directory.CreateDirectory(modelDirPath);

            _qasConfig = new QASConfiguration();

            _qasConfig.ModelDirectory = modelDirPath;
            
            _qasConfig.ClientList.Add(defaultClientName, new Client(defaultClientName));
        }

        public QASConfigDecorator(QASConfiguration qasConfig)
        {
            _qasConfig = qasConfig;
            FlatClients();
        }

        public void ChangeDomainName(string srcDomain, string tagDomain)
        {
            QASConfig.Domain domain = _qasConfig.ClientList[defaultClientName].Domains[srcDomain];

            domain.Name = tagDomain;

            string tagDomainForm1 = ":" + tagDomain;
            string tagDomainForm2 = tagDomain + ":";

            string srcRegForm1 = string.Format(":{0}\\b", srcDomain);
            string srcRegForm2 = string.Format("\\b{0}:", srcDomain);

            foreach(QASConfig.Domain dom in _qasConfig.ClientList[defaultClientName].Domains.Values)
            {
                foreach(Component comp in dom.Components.Values)
                {
                    string defSection = Regex.Replace(comp.DefinitionSection, srcRegForm1, tagDomainForm1);
                    comp.DefinitionSection = Regex.Replace(defSection, srcRegForm2, tagDomainForm2);
                }
            }
        }

        public void ChangeDomainName(string tagDomain)
        {
            ChangeDomainName(_qasConfig.ClientList[defaultClientName].Domains.Values.First().Name, tagDomain);
        }

        public void Save(string qcsFile)
        {
            string[] files = Directory.GetFiles(this.QasConfig.ModelDirectory);

            foreach(string file in files)
            {
                if(file.EndsWith("queryprocessingconfiguration.ini", StringComparison.CurrentCultureIgnoreCase))
                {
                    File.Delete(file);
                    break;
                }
            }

            string fullQcsFilePath = Path.Combine(this.QasConfig.ModelDirectory, Path.GetFileName(qcsFile));

            using(StreamWriter writer = new StreamWriter(fullQcsFilePath))
            {
                writer.WriteLine("[global]\r\nTopoSort=true\r\n\r\n");

                writer.WriteLine("[query_domains]");

                QASConfig.Client defaultClient = GetDefaultClient();

                foreach(QASConfig.Domain domain in defaultClient.Domains.Values)
                {
                    writer.WriteLine("{0}={1}", domain.Name, domain.DefinitionName);
                }

                writer.WriteLine();

                foreach(QASConfig.Domain domain in defaultClient.Domains.Values)
                {
                    writer.WriteLine("[{0}_query_analysis]\r\n{1}\r\n\r\n", domain.DefinitionName, domain.DefinitionSection);

                    foreach(QASConfig.Component comp in domain.Components.Values)
                    {
                        writer.WriteLine("[{0}]\r\n{1}\r\n\r\n", comp.DefinitionName, comp.DefinitionSection);
                    }
                }
            }
        }

        public void Merge(QASConfigDecorator otherConfig)
        {
            foreach(string section in otherConfig.QasConfig.QASConfigSections.Keys)
            {
                if(!this.QasConfig.QASConfigSections.ContainsKey(section))
                {
                    this.QasConfig.QASConfigSections.Add(section, new Dictionary<string,string>());
                }

                foreach(var kvp in otherConfig.QasConfig.QASConfigSections[section])
                {
                    this.QasConfig.QASConfigSections[section].Add(kvp.Key, kvp.Value);
                }
            }

            QASConfig.Client defaultClient = GetDefaultClient();

            foreach(QASConfig.Domain domain in otherConfig.QasConfig.ClientList[defaultClientName].Domains.Values)
            {
                if(!defaultClient.Domains.ContainsKey(domain.Name))
                {
                    defaultClient.Domains.Add(domain.Name, domain);
                }
            }

            CopyFiles(otherConfig.QasConfig.ModelDirectory, this.QasConfig.ModelDirectory);
        }

        public void AddDomain(QASConfig.Domain domain)
        {
            GetDefaultClient().Domains.Add(domain.Name, domain);
        }

        public void CreateFile(string file, string content)
        {
            using(StreamWriter sw = new StreamWriter(Path.Combine(this.QasConfig.ModelDirectory, Path.GetFileName(file))))
            {
                sw.WriteLine(content);
            }
        }

        public void AddFile(string srcFile, string dstFile)
        {
            string dstFilePath = Path.Combine(this.QasConfig.ModelDirectory, Path.GetFileName(dstFile));
            File.Copy(srcFile, dstFilePath, true);
        }

        private QASConfig.Client GetDefaultClient()
        {
            if (!this.QasConfig.ClientList.ContainsKey(defaultClientName))
            {
                this.QasConfig.ClientList.Add(defaultClientName, new Client(defaultClientName));
            }

            QASConfig.Client defaultClient = this.QasConfig.ClientList[defaultClientName];

            return defaultClient;
        }

        private void FlatClients()
        {
            Client defaultClient = _qasConfig.ClientList.ContainsKey(defaultClientName)
                ? _qasConfig.ClientList[defaultClientName]
                : new Client(defaultClientName);

            foreach (string client in _qasConfig.ClientList.Keys)
            {
                if (client.Equals(defaultClientName))
                {
                    continue;
                }

                foreach (var kvp in _qasConfig.ClientList[client].Domains)
                {
                    defaultClient.Domains.Add(kvp.Key, kvp.Value);
                }

                string domainDefinitionSection = "query_domains:" + client;

                _qasConfig.QASConfigSections.Remove(domainDefinitionSection);
            }

            _qasConfig.ClientList.Clear();

            _qasConfig.ClientList.Add(defaultClientName, defaultClient);
        }

        private void CopyFiles(string fromDir, string toDir)
        {
            string[] files = Directory.GetFiles(fromDir);
            
            foreach(string file in files)
            {
                string srcFilePath = Path.Combine(fromDir, Path.GetFileName(file));
                string dstFilePath = Path.Combine(toDir, Path.GetFileName(file));

                File.Copy(srcFilePath, dstFilePath, true);
            }
        }
    }
}
