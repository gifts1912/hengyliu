using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.QU.QASMerger
{
    public static class ApplicationFactory
    {
        public static ApplicationBase CreateApplication(string[] args)
        {
            CommandLineArgument cmd = CommandLineArgument.CreateCommandLineArgs(args);

            switch (cmd.ApplicationType)
            {
                case ApplicationType.IntentClassifierMerger:
                    return new IntentClassifierMergerApp(cmd);

                default:
                    throw new InvalidDataException("Unsupported application specified.");
            }
        }
    }
}
