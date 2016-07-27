using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescalValue
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                args = new string[1];
                args[0] = "DecimalValueRescal";
            }
            string[] cmdArgs = args.Skip(1).ToArray();
            

            if(args[0].Equals("DecimalValueRescal", StringComparison.OrdinalIgnoreCase))
            {
                ValueRescal.DecimalValueRescal.Run(cmdArgs);
            }
        }
    }
}
