using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TopSiteMining
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                args = new string[1];
                args[0] = "mergegoogleandbingtopDaminurls";
               // args[0] = "intendpatternlayer";
              //  args[0] = "intentpatternslotlayer";
                
                args[0] = "intendpatternlayer";
                args[0] = "mergegoogleandbingtopDaminurls";
                args[0] = "TopUrlsFormat";
            }
            string[] cmdArgs = args.Skip(1).ToArray();
            if(args[0].Equals("intendlayer", StringComparison.OrdinalIgnoreCase))
            {
                TopSite.IntendLayer.Run(cmdArgs);
            }
            else if(args[0].Equals("intendpatternlayer", StringComparison.OrdinalIgnoreCase))
            {
     
                TopSite.IntendPatternLayer.Run(cmdArgs);
               
            }
            else if(args[0].Equals("mergegoogleandbingtopDaminurls", StringComparison.OrdinalIgnoreCase))
            {
                TopSite.MergeGoogAndBingTopDomainUrls.Run(cmdArgs);
            }
            else if(args[0].Equals("intentpatternslotlayer", StringComparison.OrdinalIgnoreCase))
            {
                TopSite.intentPatternSlotLayer.intentPatternSlotLayer.Run(cmdArgs);
            }
            else if(args[0].Equals("topurlsformat", StringComparison.OrdinalIgnoreCase))
            {
                TopSite.TopUrlsFormat.Run(cmdArgs);
            }
        }
    }
}