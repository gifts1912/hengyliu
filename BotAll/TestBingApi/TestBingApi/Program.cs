using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Crawler;
using System.IO;

namespace TestBingApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string query2 = "movies by tom cruise";
            //string URL2 = "http://www.bing.com/search?q=" + string.Join("+", query.Split(' ')) + "&filter=dolphin%7Cfetch&format=pbxml&p1=%5bAnswerReducer%20Mode=%22Disabled%22%5d&addfeaturesnoexpansion=qpv3output,qpnocache,qpskipdomaincheck&setflight=magicmovie";
            string URL2 = "http://www.bing.com/search?q=" + string.Join("+", query2.Split(' ')) + "&filter=EntityWebAnswer%7CEntityWebPerson&format=pbxml&p1=%5bAnswerReducer%20Mode=%22Disabled%22%5d";
            PageContentCrawlerByWebrequest crawler2 = new PageContentCrawlerByWebrequest();
            string PbxmlString = crawler2.crawl(URL2);
            StreamWriter sw = new StreamWriter(@"D:\demo\entityWebAnswer.pbxml");
            sw.WriteLine(PbxmlString);
            sw.Close();
            Console.ReadLine();
        }
    }   
}
