using QU.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs
{
    public class CRFModel
    {
        private string modelName;
        public string ModelName
        {
            get { return this.modelName; }
        }

        private string domainName;
        public string DomainName
        {
            get { return this.domainName; }
        }

        private string modelDirectory;
        /// <summary>
        /// ModelDirectory is App_Data\ModelName.
        /// Inside the directory, these files should exist:
        /// schema.xml, lexicon.bin, grammar.xml, model
        /// </summary>
        public string ModelDirectory
        {
            get { return this.modelDirectory; }
        }

        SMCRFParser parser;
        public SMCRFParser Parser
        {
            get { return this.parser; }
        }

        static readonly string schemaFileName = "schema.xml";
        static readonly string lexiconFileName = "lexicon.bin";
        static readonly string grammarFileName = "grammar.xml";
        static readonly string modelFileName = "model";

        public CRFModel(string modelDirectory, string modelName, string domainName)
        {
            this.modelName = modelName;
            this.modelDirectory = modelDirectory;
            if (!LoadModel(domainName))
            {
                this.parser = null;
            }
        }

        public bool LoadModel(string domainName)
        {
            this.domainName = domainName;
            parser = new SMCRFParser();
            string binDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string schemaFile = Path.Combine(this.modelDirectory, schemaFileName);
            string lexiconFile = Path.Combine(this.modelDirectory, lexiconFileName);
            string grammarFile = Path.Combine(this.modelDirectory, grammarFileName);
            string modelFile = Path.Combine(this.modelDirectory, modelFileName);
            return parser.LoadModel(binDir, schemaFile, lexiconFile, grammarFile, modelFile, domainName, 1);
        }
    }
}
