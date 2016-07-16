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
            if (args.Length == 0)
            {
                args = new string[1];
                args[0] = "tokenvaluegenerate";
                args[0] = "coveragecompute";
                args[0] = "AddIncreaseIdColumn";
                args[0] = "PatternEngineTemplateFormat";
                args[0] = "EntityLexicon";
                args[0] = "Sample10KQueryContainCandidat";
                args[0] = "OtherSlotLexicon";
                args[0] = "LossIntentAnalysis";
                args[0] = "SubStringTest";
                args[0] = "ElectionSBSCase";
                args[0] = "PETriggerNoQLF";
                args[0] = "PEReplacePatternWithPEId";
                args[0] = "PatternToPEId";
                args[0] = "TrimColumn";
                args[0] = "UrlAuthorityFeatureAdd";
                args[0] = "ReplaceUrlWithNormalizedUrl";
                args[0] = "Add_EntityDomainExactMatchFeature";
                args[0] = "AddContraintMatchFeature";
                args[0] = "AddRegexMatchFeature";
                args[0] = "L3RankerExecutor";
                args[0] = "DiscoverPRFConstraints";
                args[0] = "RepresentiveCaseSelect";
                args[0] = "trainDataSelect";
                args[0] = "AddEntityMatchFeature";
                args[0] = "AddEntityMatchFeatureV0_2";
                args[0] = "StructDataGenerate";
                args[0] = "PCFGGrammaFileGenerate";
            }
            string[] cmdArgs = args.Skip(1).ToArray();
            if (args[0].Equals("tokenvaluegenerate", StringComparison.OrdinalIgnoreCase))
            {
                QAS.tokenvalueformat.Run(cmdArgs);
            }
            else if (args[0].Equals("coveragecompute", StringComparison.OrdinalIgnoreCase))
            {
                QAS.CoverageCompute.Run(cmdArgs);
            }
            else if (args[0].Equals("AddIncreaseIdColumn", StringComparison.OrdinalIgnoreCase))
            {
                OfflineSBSPipeline.AddIncreaseIdColumn.Run(cmdArgs);
            }
            else if (args[0].Equals("UrlAuthorityFeatureAdd", StringComparison.OrdinalIgnoreCase))
            {
                OfflineSBSPipeline.UrlAuthorityFeatureAdd.Run(cmdArgs);
            }
            else if (args[0].Equals("PatternEngineTemplateFormat", StringComparison.OrdinalIgnoreCase))
            {
                OfflineSBSPipeline.PatternEngineTemplateFormat.Run(cmdArgs);
            }
            else if (args[0].Equals("EntityLexicon", StringComparison.OrdinalIgnoreCase))
            {
                NewIntent.EntityLexicon.Run(cmdArgs);
            }
            else if (args[0].Equals("Sample10KQueryContainCandidat", StringComparison.OrdinalIgnoreCase))
            {
                NewIntent.Sample10KQueryContainCandidate.Run(cmdArgs);
            }
            else if (args[0].Equals("OtherSlotLexicon", StringComparison.OrdinalIgnoreCase))
            {
                NewIntent.OtherSlotLexicon.Run(cmdArgs);
            }
            else if (args[0].Equals("LossIntentAnalysis", StringComparison.OrdinalIgnoreCase))
            {
                TriggerCoverageAnalysis.LossIntentAnalysis.Run(cmdArgs);
            }
            else if (args[0].Equals("SubStringTest", StringComparison.OrdinalIgnoreCase))
            {
                others.SubStringTest.Run(cmdArgs);
            }
            else if (args[0].Equals("ElectionSBSCase", StringComparison.OrdinalIgnoreCase))
            {
                others.ElectionSBSCase.Run(cmdArgs);
            }
            else if (args[0].Equals("PETriggerNoQLF", StringComparison.OrdinalIgnoreCase))
            {
                PE.PETriggerNoQLF.Run(cmdArgs);
            }
            else if (args[0].Equals("PatternToPEId", StringComparison.OrdinalIgnoreCase))
            {
                PE.PatternToPEId.Run(cmdArgs);
            }
            else if (args[0].Equals("PEReplacePatternWithPEId", StringComparison.OrdinalIgnoreCase))
            {
                PE.PEReplacePatternWithPEId.Run(cmdArgs);
            }
            else if (args[0].Equals("TrimColumn", StringComparison.OrdinalIgnoreCase))
            {
                PE.TrimColumn.Run(cmdArgs);
            }
            else if (args[0].Equals("ReplaceUrlWithNormalizedUrl", StringComparison.OrdinalIgnoreCase))
            {
                OfflineSBSPipeline.ReplaceUrlWithNormalizedUrl.Run(cmdArgs);
            }
            else if (args[0].Equals("RepresentiveCaseSelect", StringComparison.OrdinalIgnoreCase))
            {
                SBSAnalysis.RepresentiveCaseSelect.Run(cmdArgs);
            }
            else if (args[0].Equals("AddEntityMatchFeature", StringComparison.OrdinalIgnoreCase))
            {
                BoJiaPipeline.AddEntityMatchFeature.Run(cmdArgs);
            }
            else if (args[0].Equals("AddRegexMatchFeature", StringComparison.OrdinalIgnoreCase))
            {
                BoJiaPipeline.AddRegexMatchFeature.Run(cmdArgs);
            }
            else if (args[0].Equals("L3RankerExecutor", StringComparison.OrdinalIgnoreCase))
            {
                BoJiaPipeline.L3RankerExecutor.Run(cmdArgs);
            }
            else if (args[0].Equals("Add_EntityDomainExactMatchFeature", StringComparison.OrdinalIgnoreCase))
            {
                BoJiaPipeline.Add_EntityDomainExactMatchFeature.Run(cmdArgs);
            }
            else if (args[0].Equals("DiscoverPRFConstraints", StringComparison.OrdinalIgnoreCase))
            {
                BoJiaPipeline.DiscoverPRFConstraints.Run(cmdArgs);
            }
            else if (args[0].Equals("AddContraintMatchFeature", StringComparison.OrdinalIgnoreCase))
            {
                BoJiaPipeline.AddContraintMatchFeature.Run(cmdArgs);
            }
            else if (args[0].Equals("trainDataSelect", StringComparison.OrdinalIgnoreCase))
            {
                mlRankerTrain.trainDataSelect.Run(cmdArgs);
            }
            else if (args[0].Equals("AddEntityMatchFeatureV0_2", StringComparison.OrdinalIgnoreCase))
            {
                BoJiaPipeline.AddEntityMatchFeatureV0_2.Run(cmdArgs);
            }
            else if (args[0].Equals("StructDataGenerate", StringComparison.OrdinalIgnoreCase))
            {
                mlRankerTrain.StructDataGenerate.Run(cmdArgs);
            }
            else if(args[0].Equals("PCFGGrammaFileGenerate", StringComparison.OrdinalIgnoreCase))
            {
                QAS.PCFGGrammaFileGenerate.Run(cmdArgs);
            }
        }
    }
}
