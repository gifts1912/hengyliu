using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSVUtility;
using System.IO;
using MyMisc = Utility;

namespace QU.Miscs.PostwebQU
{
    public class AnalyzePotential
    {
        class Args : CmdOptions
        {
            [Argument(ArgumentType.Required, ShortName = "extraction")]
            public string Extraction = "";

            [Argument(ArgumentType.Required, ShortName = "authority")]
            public string AuthorityFile = "";

            [Argument(ArgumentType.Required, ShortName = "out")]
            public string Output = "";

            [Argument(ArgumentType.AtMostOnce, ShortName = "streams")]
            public string ContentStreams = "MultiInstanceTitle;Body;DUMultiInstanceUrlV2";

            [Argument(ArgumentType.AtMostOnce, ShortName = "topn")]
            public int TopN = 20;

            [Argument(ArgumentType.AtMostOnce, ShortName = "neg")]
            public double NegAuthority = 0;

            [Argument(ArgumentType.AtMostOnce, ShortName = "pos")]
            public double PosAuthority = 2;

            [Argument(ArgumentType.AtMostOnce, ShortName = "minpos")]
            public int MinPosUrl = 5;
        }

        public static void Run(string[] args)
        {
            Args arguments = new Args();
            if (!Parser.ParseArgumentsWithUsage(args, arguments))
            {
                Console.WriteLine("Invalid args!");
                return;
            }

            if (!File.Exists(arguments.Extraction) || !File.Exists(arguments.AuthorityFile))
            {
                Console.WriteLine("No Alteration File!");
                return;
            }

            string[] contentStreamNames = arguments.ContentStreams.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Read authorities.
            Dictionary<string, double> domainAuthorities = ReadDomainAuthorities(arguments.AuthorityFile);

            StreamWriter sw = new StreamWriter(arguments.Output);

            // Get potential queries.
            using (StreamReader sr = new StreamReader(TSVFile.OpenInputTSVStream(arguments.Extraction)))
            {
                TSVReader reader = new TSVReader(sr, true);
                string currQueryId = null;
                List<DocumentInfo> docs = new List<DocumentInfo>();
                uint numberOfWords = 0;

                while (!reader.EndOfTSV)
                {
                    try
                    {
                        TSVLine rawLine = reader.ReadLine();
                        string qid = rawLine["m:QueryId"];
                        uint pos = rawLine.GetFeatureValue("SortedPosition");
                        numberOfWords = Math.Min(rawLine.GetFeatureValue("NumberOfWordsInQuery"), 10);

                        if (numberOfWords <= 3)
                            continue;

                        if ((!string.IsNullOrEmpty(currQueryId) && !string.Equals(qid, currQueryId))
                            || pos == arguments.TopN)
                        {
                            Process(numberOfWords, docs.OrderBy(d => d.pos),
                    arguments.NegAuthority, arguments.PosAuthority, arguments.MinPosUrl, sw);
                            docs.Clear();
                        }

                        if (pos > arguments.TopN)
                        {
                            continue;
                        }

                        DocumentInfo doc = new DocumentInfo();
                        doc.line = rawLine;
                        doc.pos = pos;

                        //doc.docId = rawLine["m:DocId"];
                        //string rating = rawLine["m:Rating"];
                        //string canonicalQuery = rawLine["m:CanonicalQuery"];

                        doc.url = rawLine["m:Url"];
                        //doc.domain = Misc.Normalizer.GetUrlDomain(doc.url).ToLower();
                        doc.domain = MyMisc.Normalizer.GetUrlHost(MyMisc.Normalizer.NormalizeUrl(doc.url)).ToLower();
                        if (!domainAuthorities.TryGetValue(doc.domain, out doc.authority))
                        {
                            doc.authority = 0;
                        }

                        // Fetch WordFound_Url_x, WordFound_Title_x and WordFound_Body_x
                        uint[] wfUTB = new uint[10];
                        for (int i = 0; i < contentStreamNames.Length; i++)
                        {
                            uint[] wfContentStream;
                            GetStreamFeatures(rawLine, out wfContentStream, contentStreamNames[i], "WordFound");
                            for (int j = 0; j < numberOfWords; j++)
                            {
                                wfUTB[j] += wfContentStream[j];
                            }
                        }

                        int numWF = 0;
                        for (int i = 0; i < numberOfWords; i++)
                        {
                            if (wfUTB[i] > 0)
                            {
                                numWF++;
                            }
                            else
                            {
                                doc.missedTermIndexes.Add(i);
                            }
                        }

                        doc.allMatched = numWF == numberOfWords;

                        docs.Add(doc);
                        currQueryId = qid;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Process(numberOfWords, docs.OrderBy(d => d.pos), 
                    arguments.NegAuthority, arguments.PosAuthority, arguments.MinPosUrl, sw);
            }

            sw.Flush();
            sw.Close();
        }

        class DocumentInfo
        {
            public TSVLine line;
            public string url;
            public uint pos;
            public string domain;
            public double authority;
            public List<int> missedTermIndexes = new List<int>();
            public bool allMatched;
        }

        static void Process(uint terms, IEnumerable<DocumentInfo> docs, 
            double negAuthority, double posAuthority, 
            int minPosUrls, StreamWriter sw)
        {
            // trigger condition: top 1 or top 2 (non-authoritive urls) all term matched
            //  however, majority of authoritive urls not all matched below top 2.
            bool top1NegUrlMatched = false, top2NegUrlMatched = false;
            int numPosUrlNonMatched = 0;

            foreach (var doc in docs)
            {
                if (doc.pos == 0 && doc.allMatched && doc.authority <= negAuthority)
                {
                    top1NegUrlMatched = true;
                }
                else if (doc.pos == 1 && doc.allMatched && doc.authority <= negAuthority)
                {
                    top2NegUrlMatched = true;
                }
                else if (doc.pos > 1 && doc.authority >= posAuthority)
                {
                    numPosUrlNonMatched += (doc.allMatched ? 0 : 1);
                }
            }

            if (!(top1NegUrlMatched || top2NegUrlMatched)
                || numPosUrlNonMatched < minPosUrls)
            {
                return;
            }

            foreach (var doc in docs)
            {
                sw.WriteLine(doc.line["m:RawQuery"]
                    + "\t" + doc.line["m:QueryId"]
                    + "\t" + doc.url
                    + "\t" + doc.pos
                    + "\t" + doc.authority
                    + "\t" + string.Join(";", doc.missedTermIndexes)
                    );
            }
        }

        static void GetStreamFeatures(TSVLine line, out uint[] wf, string streamName, string featureCategory)
        {
            wf = new uint[10];
            for (int i = 0; i < 10; i++)
            {
                wf[i] = 0;
            }

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    wf[i] = line.GetFeatureValue(string.Format("{0}_{1}_{2}", featureCategory, streamName, i));
                }
            }
            catch { }
        }

        /// <summary>
        /// Read authorities from file.
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>Mapping from domain to authority</returns>
        private static Dictionary<string, double> ReadDomainAuthorities(string file)
        {
            Dictionary<string, double> authorities = new Dictionary<string, double>();
            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split('\t');
                    if (items.Length < 2)
                        continue;

                    try
                    {
                        string domain = items[0].ToLower();
                        double authority = double.Parse(items[1]);
                        authorities[domain] = authority;
                    }
                    catch { continue; }
                }
            }

            return authorities;
        }
    }
}
