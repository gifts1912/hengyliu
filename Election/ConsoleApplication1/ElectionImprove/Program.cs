using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionImprove
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[1];
                args[0] = "tokenvaluegenerate";
                args[0] = "coveragecompute";
                args[0] = "AddIncreaseIdColumn";
                args[0] = "PatternEngineTemplateFormat";
                args[0] = "UrlAuthorityFeatureAdd";
                args[0] = "EntityLexicon";
                args[0] = "Sample10KQueryContainCandidat";
                args[0] = "OtherSlotLexicon";
            }
            string[] cmdArgs = args.Skip(1).ToArray();
            if(args[0].Equals("tokenvaluegenerate", StringComparison.OrdinalIgnoreCase))
            {
                QAS.tokenvalueformat.Run(cmdArgs);
            }
            else if(args[0].Equals("coveragecompute", StringComparison.OrdinalIgnoreCase))
            {
                QAS.CoverageCompute.Run(cmdArgs);
            }
            else if(args[0].Equals("AddIncreaseIdColumn", StringComparison.OrdinalIgnoreCase))
            {
                OfflineSBSPipeline.AddIncreaseIdColumn.Run(cmdArgs);
            }
            else if(args[0].Equals("UrlAuthorityFeatureAdd", StringComparison.OrdinalIgnoreCase))
            {
                OfflineSBSPipeline.UrlAuthorityFeatureAdd.Run(cmdArgs);
            }
            else if(args[0].Equals("PatternEngineTemplateFormat", StringComparison.OrdinalIgnoreCase))
            {
                OfflineSBSPipeline.PatternEngineTemplateFormat.Run(cmdArgs);
            }
            else if(args[0].Equals("EntityLexicon", StringComparison.OrdinalIgnoreCase))
            {
                NewIntent.EntityLexicon.Run(cmdArgs);
            }
            else if(args[0].Equals("Sample10KQueryContainCandidat", StringComparison.OrdinalIgnoreCase))
            {
                NewIntent.Sample10KQueryContainCandidate.Run(cmdArgs);
            }
            else if(args[0].Equals("OtherSlotLexicon", StringComparison.OrdinalIgnoreCase))
            {
                NewIntent.OtherSlotLexicon.Run(cmdArgs);
            }


        }
    }
}
