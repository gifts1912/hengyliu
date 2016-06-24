namespace MS.QU.QASMerger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.TMSN.CommandLine;

    public class CommandLineArgument
    {
        [Argument(ArgumentType.Required, HelpText = "The application type to run", ShortName = "app")]
        public ApplicationType ApplicationType = ApplicationType.IntentClassifierMerger;

        [Argument(ArgumentType.Required, HelpText = "The domain name of the QAS model.", ShortName = "d")]
        public string DomainName = string.Empty;

        [Argument(ArgumentType.Required, HelpText = "The segment name of the QAS model.", ShortName = "s")]
        public string SegmentName = string.Empty;

        [Argument(ArgumentType.Required, HelpText = "The path to the rule model.", ShortName = "rm")]
        public string RuleModelPath = string.Empty;

        [Argument(ArgumentType.Required, HelpText = "The path to the trained model.", ShortName = "tm")]
        public string TrainedModelPath = string.Empty;

        [Argument(ArgumentType.Required, HelpText = "The path to the output model.", ShortName = "om")]
        public string OutputModelPath = string.Empty;

        [Argument(ArgumentType.Required, HelpText = "The path to file of mapping intent to intent id.", ShortName = "i2d")]
        public string Intent2IdMappingFile = string.Empty;

        [Argument(ArgumentType.Required, HelpText = "The path to dir of resource files.", ShortName = "rs")]
        public string ResourceDir = string.Empty;

        private CommandLineArgument()
        {            
        }

        public static CommandLineArgument CreateCommandLineArgs(string[] args)
        {
            CommandLineArgument cmd = new CommandLineArgument();

            if (!Parser.ParseArguments(args, cmd, Console.Error.WriteLine))
            {
                Console.WriteLine("Unable to parse command line arguments.");
                Environment.Exit(-1);
            }

            return cmd;
        }
    }
}
