using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QASConfigurationAPI
{
    /// <summary>
    /// Variant corresponds to file name for each environment.
    /// It consists of Environment-->Workflow-->VirtualService-->Dataset
    /// Every variant will correspond to one unique file in SearchGold.
    /// A Variant will then have list of models and each model will have flights, markets, inputs, outputs and scenarios under which they are placed.
    /// </summary>
    public class Variants
    {
        public List<Model> modelList { get; set; }
        public string Name { get; set; }
        public Variants(string name)
        {
            this.Name = name;
            this.modelList = new List<Model>();
        }
    }
    /// <summary>
    /// Model class represents a model in a given QueryProcessingConfiguration file.
    /// Class implements IEquatable interface since we want to ensure that two models are same if their name matches in the config files
    /// </summary>
    public class Model : IEquatable<Model>
    {
        public List<string> scenarioNames { get; set; }
        public List<string> flights { get; set; }
        public List<string> markets { get; set; }
        public List<string> inputs { get; set; }
        public List<string> outputs { get; set; }
        public string Name { get; set; }

        public Model(string name)
        {
            this.Name = name;
            this.markets = new List<string>();
            this.flights = new List<string>();
            this.scenarioNames = new List<string>();
            this.inputs = new List<string>();
            this.outputs = new List<string>();
        }

        public bool Equals(Model other)
        {
            if (this.Name.Equals(other.Name))
                return true;
            else
                return false;
        }

    }

    /// <summary>
    /// This is the main class where all the data population happens.
    /// </summary>
    public class QASConfiguration
    {
        /// <summary>
        /// populateScenarios function reads the configuration file, and populates model list with the scenarios and the clients that they fall under.
        /// </summary>
        /// <param name="modelList">List of models in a given variant to be populated</param>
        /// <param name="clientList">List of clients in a given configuration file</param>
        /// <param name="configFileName">Variant config file name</param>
        static void populateScenarios(ref List<Model> modelList, ref List<string> clientList, string configFileName)
        {
            StreamReader configFile = new StreamReader(configFileName);
            string line = String.Empty, scenarioName = String.Empty;
            Model existingModel = null;

            while ((line = configFile.ReadLine()) != null)
            {
                if (line.Contains("[clients]"))
                {
                    break;
                }

                if (line.StartsWith("KifSchema:"))
                {
                    scenarioName = line.Substring(line.IndexOf(":") + 1, line.IndexOf("=") - line.IndexOf(":") - 1);
                    string[] modelName = line.Split(',');

                    foreach (string model in modelName)
                    {
                        if (!model.Contains("KifSchema:"))
                        {
                            if (modelList.Contains(new Model(model)))
                            {
                                existingModel = modelList[modelList.IndexOf(new Model(model))];
                                existingModel.scenarioNames.Add(scenarioName);
                            }
                            else
                            {
                                existingModel = new Model(model);
                                existingModel.scenarioNames.Add(scenarioName);
                                modelList.Add(existingModel);
                            }
                        }
                        else
                        {

                            if (modelList.Contains(new Model(model.Substring(model.LastIndexOf(":") + 1))))
                            {
                                existingModel = modelList[modelList.IndexOf(new Model(model.Substring(model.LastIndexOf(":") + 1)))];
                                existingModel.scenarioNames.Add(scenarioName);
                            }
                            else
                            {
                                existingModel = new Model(model.Substring(model.LastIndexOf(":") + 1));
                                existingModel.scenarioNames.Add(scenarioName);
                                modelList.Add(existingModel);
                            }
                        }

                    }
                }
            }


            clientList.Add(String.Empty);
            while ((line = configFile.ReadLine()) != null)
            {
                if (line.Contains("[query_domains]"))
                {
                    break;
                }
                if (line.StartsWith(";"))
                {
                    continue;
                }

                try
                {
                    clientList.Add(line.Substring(0, line.IndexOf('=')));
                }
                catch (ArgumentOutOfRangeException)
                {
                    continue;
                }

            }

        }

        /// <summary>
        /// populateFlightMarkets function reads the configuration file, and populates model list with the flight and market information.
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="client"></param>
        /// <param name="configFileName"></param>
        static void populateFlightMarkets(ref List<Model> modelList, string client, string configFileName)
        {
            StreamReader configFile = new StreamReader(configFileName);
            string line = String.Empty, scenarioName = String.Empty;
            Model existingModel = null;

            while ((line = configFile.ReadLine()) != null)
            {
                if (line.Contains("[query_domains:" + client))
                    break;
            }


            while ((line != null) && ((line = configFile.ReadLine()) != null))
            {

                try
                {
                    string modelName = line.Substring(0, line.IndexOf('='));

                    if (modelName.StartsWith(";"))
                    {
                        continue;
                    }

                    if (modelName.Contains("deriveFromClient"))
                    {
                        foreach (Model model in modelList)
                        {
                            if (client.Contains("flt"))
                            {
                                model.flights.Add(client);
                            }
                            else
                            {
                                model.markets.Add(client);
                            }
                        }
                    }
                    else
                    {

                        if (modelList.Contains(new Model(modelName)))
                        {
                            existingModel = modelList[modelList.IndexOf(new Model(modelName))];
                            if (client.Contains("flt"))
                            {
                                existingModel.flights.Add(client);
                            }
                            else
                            {
                                existingModel.markets.Add(client);
                            }
                        }
                        else
                        {
                            existingModel = new Model(modelName);
                            modelList.Add(existingModel);

                            if (client.Contains("flt"))
                            {
                                existingModel.flights.Add(client);
                            }
                            else
                            {
                                existingModel.markets.Add(client);
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    if (line.Contains("[qd_") || line.Contains("[query_domains"))
                    {
                        break;
                    }

                    continue;
                }

                if (line.Contains("[qd_") || line.Contains("[query_domains"))
                {
                    break;
                }


            }

        }

        /// <summary>
        /// Function populatedefaultFlightMarkets populates the models list with default market enus. Not all models have to fall under this and hence the need to extract this very specific information.
        /// </summary>
        /// <param name="defaultmodelList">List of models to be populated</param>
        /// <param name="configFilename">Variant configuration file name</param>
        static void populatedefaultFlightMarkets(ref List<Model> defaultmodelList, string configFilename)
        {
            StreamReader configFile = new StreamReader(configFilename);
            string line = String.Empty, scenarioName = String.Empty;
            Model existingModel = null;

            while ((line = configFile.ReadLine()) != null)
            {
                if (line.Contains("[query_domains]"))
                    break;
            }


            while ((line != null) && ((line = configFile.ReadLine()) != null))
            {

                try
                {
                    string modelName = line.Substring(0, line.IndexOf('='));

                    if (modelName.StartsWith(";"))
                    {
                        continue;
                    }


                    if (defaultmodelList.Contains(new Model(modelName)))
                    {
                        existingModel = defaultmodelList[defaultmodelList.IndexOf(new Model(modelName))];
                    }
                    else
                    {
                        existingModel = new Model(modelName);
                        defaultmodelList.Add(existingModel);
                    }

                    existingModel.markets.Add("enus");

                }
                catch (ArgumentOutOfRangeException)
                {
                    if (line.Contains("[qd_") || line.Contains("[query_domains"))
                    {
                        break;
                    }

                    continue;
                }

                if (line.Contains("[qd_") || line.Contains("[query_domains"))
                {
                    break;
                }


            }

        }

        /// <summary>
        /// Function populateinputoutputdependencies identifies inputs and outputs of featurizers and domain classifiers updates the model list with this information.
        /// </summary>
        /// <param name="defaultmodelList"></param>
        /// <param name="configFilename"></param>
        static void populateinputoutputdependencies(ref List<Model> defaultmodelList, string configFilename)
        {
            StreamReader configFile = new StreamReader(configFilename);
            string line = String.Empty, scenarioName = String.Empty, modelName=String.Empty;
            Model existingModel = null;

            while ((line = configFile.ReadLine()) != null)
            {
                if (line.Contains("[qd_"))
                {
                    modelName = line.Substring(line.IndexOf("_") + 1, line.IndexOf("_", 4) - (line.IndexOf("_") + 1));
                    break;
                }
            }


            while ((line != null) && ((line = configFile.ReadLine()) != null))
            {

                try
                {
                    if (line.Contains("[qd_"))
                    {
                        modelName = line.Substring(line.IndexOf("_") + 1, line.IndexOf("_", 4) - (line.IndexOf("_") + 1));
                    }

                    if (line.StartsWith(";"))
                        continue;

                    if (line.StartsWith("input="))
                    {
                        if (defaultmodelList.Contains(new Model(modelName)))
                        {
                            existingModel = defaultmodelList[defaultmodelList.IndexOf(new Model(modelName))];
                            string[] inputList = line.Substring(line.IndexOf("=") + 1, line.Length - (line.IndexOf("=") + 1)).Split(',');
                            foreach (string s in inputList)
                            {
                                if (!existingModel.inputs.Contains(s))
                                {
                                    existingModel.inputs.Add(s);
                                }
                            }
                            
                        }
                        else
                        {
                            existingModel = new Model(modelName);
                            string[] inputList = line.Substring(line.IndexOf("=") + 1, line.Length - (line.IndexOf("=") + 1)).Split(',');
                            foreach (string s in inputList)
                            {
                                if (!existingModel.inputs.Contains(s))
                                {
                                    existingModel.inputs.Add(s);
                                }
                            }
                            defaultmodelList.Add(existingModel);

                        }
                    }

                    if (line.StartsWith("output="))
                    {
                        if (defaultmodelList.Contains(new Model(modelName)))
                        {
                            existingModel = defaultmodelList[defaultmodelList.IndexOf(new Model(modelName))];
                            string[] inputList = line.Substring(line.IndexOf("=") + 1, line.Length - (line.IndexOf("=") + 1)).Split(',');
                            foreach (string s in inputList)
                            {
                                if (!existingModel.outputs.Contains(s))
                                {
                                    existingModel.outputs.Add(s);
                                }
                            }

                        }
                        else
                        {
                            existingModel = new Model(modelName);
                            string[] inputList = line.Substring(line.IndexOf("=") + 1, line.Length - (line.IndexOf("=") + 1)).Split(',');
                            foreach (string s in inputList)
                            {
                                if (!existingModel.outputs.Contains(s))
                                {
                                    existingModel.outputs.Add(s);
                                }
                            }
                            defaultmodelList.Add(existingModel);

                        }
                    }

                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }

                
            }

        }
        /// <summary>
        /// Loads QAS data across all configuration files and populates model list with flights, markets, scenarios, inputs and outputs
        /// </summary>
        /// <returns></returns>
        public static List<Variants> LoadQASData()
        {
            List<Model> modelScenarioList = null, modelList = null;
            List<string> clientList;

            List<string> configurationFiles = Directory.GetFiles(@"\\lsdfs\\shares\\searchgold\\deploy\\builds\\data\\answersTest\\xapquserviceanswer", "Anspart*", SearchOption.TopDirectoryOnly).ToList();
            configurationFiles = configurationFiles.Concat(Directory.GetFiles(@"\\lsdfs\\shares\\searchgold\\deploy\\builds\\data\\answers\\xapquserviceanswer", "AnsInt*", SearchOption.TopDirectoryOnly)).ToList();
            configurationFiles = configurationFiles.Concat(Directory.GetFiles(@"\\lsdfs\\shares\\searchgold\\deploy\\builds\\data\\answers\\xapquserviceanswer", "Default20100527*", SearchOption.TopDirectoryOnly)).ToList();

            
            ArrayList variants = new ArrayList();
            List<Variants> uniquevariants = new List<Variants>();
            Variants myVariant = null;

            for (int i = 0; i < configurationFiles.Count; i++)
            {
                myVariant = new Variants(configurationFiles[i]);
                uniquevariants.Add(myVariant);
            }

            foreach (Variants s in uniquevariants)
            {
                if (File.Exists(s.Name))
                {
                    modelScenarioList = new List<Model>(100);
                    clientList = new List<string>(100);
                    modelList = new List<Model>(100);

                    populateScenarios(ref modelScenarioList, ref clientList, s.Name);
                    populatedefaultFlightMarkets(ref modelScenarioList, s.Name);
                    populateinputoutputdependencies(ref modelScenarioList, s.Name);
                            
                    foreach (string key in clientList)
                    {
                        string client = key;
                        if (client != string.Empty)
                        {
                            populateFlightMarkets(ref modelScenarioList, client, s.Name);
                        }
                    }
                    s.modelList = modelScenarioList;

                }
            }

            return uniquevariants;
        }
    }
}