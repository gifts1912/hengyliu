using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QASConfig;

namespace MS.QU.QASMerger
{
    public static class Tests
    {
        public static void TestQASConfig()
        {
            string configFilePath = @"D:\src\relevanceprojects\scratch\yuxie\projects\MS.QU.QASMerger\MS.QU.QASMerger\TestData\RuleBasedIntentModel\MSFinance.Intents.QueryProcessingConfiguration.ini";

            QASConfig.QASConfiguration qasConfig = new QASConfiguration(configFilePath);
            qasConfig.BuildDependency();
        }
    }
}
