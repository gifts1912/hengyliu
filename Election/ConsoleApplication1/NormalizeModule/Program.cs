using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NormalizeModule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[1];
                args[0] = "NormalizedUrl";
            }
            string[] cmdArgs = args.Skip(1).ToArray();
            
            if(args[0].Equals("NormalizedUrl", StringComparison.OrdinalIgnoreCase))
            {
                NormalizeUrlAndQuery.NormalizedUrl.Run(cmdArgs);
            }
        }
              NormalizeUrlAndQuery.NormalizedUrl.Run(cmdArgs);
    }
}
