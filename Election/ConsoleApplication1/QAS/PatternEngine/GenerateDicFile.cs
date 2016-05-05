using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace QAS.PatternEngine
{
    public class GenerateDicFile
    {
        private static StreamWriter logWriter;
        private static string metaStreamSuffix = "Dp14";
        private static Dictionary<string, string> UrlToMetaStream = new Dictionary<string, string>();
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[5];
                args[0] = @"D:\demo\PdiDumpingFile.tsv";
                args[1] = @"D:\demo\UrlToPdiDumpFile.tsv";
                args[2] = @"D:\demo\ProLog.tsv";
            }
            string pdiDumpFile = args[0];
            string UrlToPdiDumpFile = args[1];
            string logFile = args[2];

            logWriter = new StreamWriter(logFile);
            Parse(pdiDumpFile);
            StoreUrlToPdi(UrlToPdiDumpFile);

            logWriter.Close();
        }
        public static void Parse(string pdiDumpFile)
        {
            using (var sr = new StreamReader(pdiDumpFile))
            {
                string line = null;
                string url = null;
                while (null != (line = sr.ReadLine()))
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    if (line.StartsWith("URL: "))
                    {
                        if (null != url)
                        {
                            LogProc(string.Format("Missing MetaStream Item: {0}", url));
                        }

                        url = line.Substring(5).Trim();
                    }

                    if (null != url && line.Contains(metaStreamSuffix))
                    {
                        string metaStream = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(metaStream))
                        {
                            if (!UrlToMetaStream.ContainsKey(url))
                            {
                                UrlToMetaStream.Add(url, metaStream.Trim());
                            }
                        }
                        else
                        {
                            LogProc(string.Format("Missing MetaStream Content: {0}", url));
                        }

                        url = null;
                        metaStream = null;
                    }
                }
            }
        }
        
        public static void StoreUrlToPdi(string UrlToPdiDumpFile)
        {
            using(StreamWriter sw = new StreamWriter(UrlToPdiDumpFile))
            {
                foreach(KeyValuePair<string, string> urlMetaPair in UrlToMetaStream)
                {
                    sw.WriteLine("{0}\t{1}", urlMetaPair.Key, urlMetaPair.Value);
                }
            }
        }
        public static void LogProc(string logInfo)
        {
            logWriter.WriteLine(logInfo);
        }
    }
}