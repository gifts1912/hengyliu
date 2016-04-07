using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                args = new string[1];
               // args[0] = "querySlotFormat";
                // args[0] = "intendpatternlayer";
                 
              //  args[0] = "AppendTopSiteLabelFeature_ByQueryPatternV2";
                args[0] = "simslotmining";                  
                args[0] = "querySlotFormat";                        

                args[0] = "QueryPatternChangeModule";
                args[0] = "AppendTopSiteLabelFeature_ByQueryPatternV2";
                args[0] = "LargerScoreProcess";
                
                
                args[0] = "IntentSlotAnalysis";
                args[0] = "querySlotFormat";
                
                
                
                
                args[0] = "Utility";                                   
               
                          
               
                      
                args[0] = "SlotTriggerPbmxlFile";
                args[0] = "IntentSlotQueryFormat";
                args[0] = "intentpatternslotlayer";
                args[0] = "MergeGoogAndBingTopDomainUrls";
                args[0] = "TopoLogisticSort";
                args[0] = "AdjustBaseTopoLogisticSort";
                args[0] = "AdjustBaseTopSiteTunning";
                
            }
            string[] cmdArgs = args.Skip(1).ToArray();
            if (args[0].Equals("queryslotformat", StringComparison.OrdinalIgnoreCase))
            {
                QU.QuerySlotFormat.Run(cmdArgs);
            }
            else if (args[0].Equals("appendtopsitelabelfeature_byquerypatternv2", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.AppendTopSiteLabelFeature_ByQueryPatternV2.Program.Run(cmdArgs);
            }
            else if(args[0].Equals("simslotmining", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.TopSite.SimSlotMining.Run(cmdArgs);
            }
            else if(args[0].Equals("intentpatternslotlayer", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.TopSite.intentPatternSlotLayer.intentPatternSlotLayer.Run(cmdArgs);
            }
            else if(args[0].Equals("mergegoogandbingtopdomainurls", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.TopSite.MergeGoogAndBingTopDomainUrls.Run(cmdArgs);
            }
            else if (args[0].Equals("largerscoreprocess", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.TopSite.LargerScoreProcess.Run(cmdArgs);
            }
            else if(args[0].Equals("querypatternchangemodule", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.QueryPatternChangeModule.QueryPatternChangeModule.Run(cmdArgs);
            }
            else if(args[0].Equals("intentslotanalysis", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.QU.IntentSlotAnalysis.IntentSlotAnalysis.Run(cmdArgs);
            }
            else if(args[0].Equals("intentslotqueryformat", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.QU.IntentSlotQueryFormat.Run(cmdArgs);
            }
            else if(args[0].Equals("utility", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.Utility.Utility.Run(cmdArgs);
            }
            else if(args[0].Equals("slottriggerpbmxlfile", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.QU.SlotTriggerPbmxlFile.SlotTriggerPbmxlFile.Run(cmdArgs);
            }
            else if(args[0].Equals("TopoLogisticSort", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.TopSite.TopoLogisticSort.TopoLogisticSort.Run(cmdArgs);
            }
            else if(args[0].Equals("AdjustBaseTopoLogisticSort", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.TopSite.AdjustTopSiteListBasedOnPartialSort.TopoLogisticSort.TopoLogisticSort.Run(cmdArgs);
            }
            else if(args[0].Equals("AdjustBaseTopSiteTunning", StringComparison.OrdinalIgnoreCase))
            {
                Ranking.TopSite.AdjustTopSiteListBasedOnPartialSort.TopSiteTunning.TopSiteTunning.Run(cmdArgs);
            }
        }
    }
}
