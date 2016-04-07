using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace GramaTest
{
    class Program
    {
        public static void AssignTest()
        {
            Dictionary<string, string> nameAlias = new Dictionary<string, string>();
            nameAlias.Add("A", "a");

            string alias = nameAlias["A"];
            alias = "b";
            Console.WriteLine(nameAlias["A"]);
            Console.WriteLine("Changed list is :");
            Console.WriteLine(alias);
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            AssignTest();       
        }
    }
}
