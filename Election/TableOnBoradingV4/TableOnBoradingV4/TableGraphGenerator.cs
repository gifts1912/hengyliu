using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using TSVUtility;
using Utility;
using Utility.Entity;
using Utility.NLP;

namespace TableOnBoardingV4
{
    public interface INormalizer<T>
    {
        T Normalize(T source);
    }

    public class StringCommonNormalizer : INormalizer<string>
    {
        public string[] Separators { get; set; }

        public virtual string Normalize(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            return string.Join(" ", source.Split(Separators, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower()));
        }

        public StringCommonNormalizer() { }
    }

    public static class NgramHelper
    {
        public static IEnumerable<string[]> GetNgramArray(string[] terms, int n, int range, bool keepOrder)
        {
            if (n <= 0 || n > range)
            {
                throw new ArgumentException("invalid parameter value");
            }
            if (terms.Length < n)
            {
                yield break;
            }

            if (n == 1)
            {
                foreach (string ngram in terms)
                {
                    yield return new string[1] { ngram };
                }
                yield break;
            }

            if (n == range)
            {
                for (int i = 0; i <= terms.Length - n; ++i)
                {
                    string[] ngram = new string[n];
                    for (int j = 0; j < n; ++j)
                    {
                        ngram[j] = terms[i + j];
                    }
                    if (keepOrder)
                    {
                        yield return ngram;
                    }
                    else
                    {
                        yield return ngram.OrderBy(x => x).ToArray();
                    }
                }
                yield break;
            }

            int[] index = new int[n];
            for (int i = 0; i < n; ++i)
            {
                index[i] = i;
            }
            while (true)
            {
                string[] ngram = index.Select(x => terms[x]).ToArray();
                if (keepOrder)
                {
                    yield return ngram;
                }
                else
                {
                    yield return ngram.OrderBy(x => x).ToArray();
                }
                int pos = n - 1;
                while (pos >= 0)
                {
                    bool canMove;
                    if (pos == n - 1)
                    {
                        canMove = (index[pos] < terms.Length - 1) && (index[pos] < index[0] + range - 1);
                    }
                    else
                    {
                        canMove = (index[pos] < index[pos + 1] - 1);
                    }
                    if (canMove)
                    {
                        ++index[pos];
                        for (int i = pos + 1; i < n; ++i)
                        {
                            index[i] = index[pos] + i - pos;
                        }
                        break;
                    }
                    else
                    {
                        --pos;
                    }
                }
                if (pos < 0)
                {
                    yield break;
                }
            }
        }

        public static IEnumerable<string> GetNgramString(string[] terms, int n, int range, bool keepOrder, string delim = " ")
        {
            return GetNgramArray(terms, n, range, keepOrder).Select(x => string.Join(delim, x));
        }

        public static IEnumerable<string> GetRealNgramString(string[] terms, int n, string delim = " ")
        {
            for (int i = 0; i <= terms.Length - n; i++)
            {
                yield return string.Join(delim, terms.Skip(i).Take(n));
            }
        }
    }
    public class ConversationalQueryNormalizer : StringCommonNormalizer
    {
        // also provide function call
        static ConversationalQueryNormalizer instance = null;

        public static string NormalizeString(string source)
        {
            if (instance == null)
            {
                instance = new ConversationalQueryNormalizer();
            }

            return instance.Normalize(source);
        }

        public ConversationalQueryNormalizer()
        {
            Separators = new string[] { " ", "\r", "\n", "\t" };
            Separators = Separators.Concat(@"`~!@#$%^&*()-_+[]{}\|;:'"",/?".ToCharArray().Select(x => x.ToString())).ToArray();
        }

        public override string Normalize(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            string[] items = source.Split(Separators, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower()).ToArray();

            for (int i = 0; i < items.Length; i++)
            {
                string temp = items[i];
                if (temp.Contains(">="))
                {
                    temp = temp.Replace(">=", " >= ");
                }
                else if (temp.Contains("<="))
                {
                    temp = temp.Replace("<=", " <= ");
                }
                else if (temp.Contains(">"))
                {
                    temp = temp.Replace(">", " > ");
                }
                else if (temp.Contains("<"))
                {
                    temp = temp.Replace("<", " < ");
                }
                else if (temp.Contains("=="))
                {
                    temp = temp.Replace("==", " == ");
                }
                else if (temp.Contains("="))
                {
                    temp = temp.Replace("=", " = ");
                }

                items[i] = temp;
            }

            return Regex.Replace(string.Join(" ", items), "[ ]+", " ");
        }
    }

    public class HtmlRawTable
    {
        private string url;

        private string[] header;

        private List<string[]> rows;

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public string[] Header
        {
            get
            {
                return header;
            }

            set
            {
                header = value;
            }
        }

        public List<string[]> Rows
        {
            get
            {
                return rows;
            }

            set
            {
                rows = value;
            }
        }

        private JsonSerializerSettings settings;
        public HtmlRawTable()
        {
            rows = new List<string[]>();
            settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, this.settings);
        }
    }


    public class TableHeaderDefinition
    {
        public string TableName;
        public string TableHeader;
        public List<string> Types;
        public PredicateValueType ValueType = PredicateValueType.String;
        public bool NeedIndex = false;
        public Regex ValueExtractor = null;
    }

    public class GenTableGraph
    {
        public static Dictionary<string, TableHeaderDefinition> ReadHeaderDef(string file)
        {
            var ths = new Dictionary<string, TableHeaderDefinition>();
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    string line;
                    string[] items = sr.ReadSplitLine(out line, 6);
                    if (items == null)
                        continue;

                    string tableName = items[0];
                    string tableHeader = items[1];
                    string[] types = items[2].Split(',', ';');
                    PredicateValueType valueType;
                    if (!Enum.TryParse<PredicateValueType>(items[3], out valueType))
                    {
                        valueType = PredicateValueType.String;
                    }

                    int needIndex = 0;
                    if(items[4].ToLower() == "true")
                    {
                        needIndex = 1;
                    }
                    else if (items[4].ToLower() == "false" || !int.TryParse(items[4], out needIndex))
                    {
                        needIndex = 0;
                    }

                    Regex r = null;
                    if (!string.IsNullOrEmpty(items[5]))
                    {
                        try
                        {
                            r = new Regex(items[5]);
                        }
                        catch
                        { r = null; }
                    }

                    string key = GenKey(tableName, tableHeader);
                    ths[key] = new TableHeaderDefinition
                    {
                        TableName = tableName,
                        TableHeader = tableHeader,
                        Types = types.ToList(),
                        ValueType = valueType,
                        NeedIndex = needIndex > 0,
                        ValueExtractor = r
                    };
                }
            }

            return ths;
        }

        public static bool IsEntityType(string type)
        {
            return type.Split('.').Length == 2;
        }

        public static string GenEntityID(string entity, string type, MD5 md)
        {
            return CommonUtils.GetMd5Hash(md, entity + type);
        }

        public static string GenKey(string tableName, string tableHeader)
        {
            return (tableName + tableHeader).ToLower();
        }

        public static string ExtractValue(Regex r, string tgt)
        {
            if (r == null)
                return tgt;

            string val = tgt.ToLower();
            var m = r.Match(val);
            if (m == null || !m.Success)
                return tgt;

            return m.Groups["val"].Value;
        }

        public static void Index(string val, string predicate, ref Dictionary<string, Dictionary<string, double>> ngram2predicates,
                                    StopWordDict stopwords, int ngram = 3)
        {
            val = ConversationalQueryNormalizer.NormalizeString(val);
            HashSet<string> processed = new HashSet<string>();
            for (int i = 1; i <= ngram; i++)
            {
                var ngrams = NgramHelper.GetRealNgramString(val.Split(' '), i);
                foreach (var t in ngrams)
                {
                    int temp;
                    if (string.IsNullOrWhiteSpace(t) || stopwords.Contains(t) || int.TryParse(t, out temp))
                        continue;

                    if (AllStopwords(t, stopwords))
                        continue;

                    if (processed.Contains(RemoveStopwords(t, stopwords)))
                    {
                        continue;
                    }

                    processed.Add(t);

                    if (!ngram2predicates.ContainsKey(t))
                    {
                        ngram2predicates.Add(t, new Dictionary<string, double>());
                    }

                    if (ngram2predicates[t].ContainsKey(predicate))
                    {
                        ++ngram2predicates[t][predicate];
                    }
                    else
                    {
                        ngram2predicates[t][predicate] = 1;
                    }
                }
            }
        }

        static string RemoveStopwords(string ngram, StopWordDict stopwords)
        {
            return string.Join(" ", ngram.Split(' ').Where(t => !stopwords.Contains(t)));
        }

        static bool AllStopwords(string ngram, StopWordDict stopwords)
        {
            string[] items = ngram.Split(' ');
            foreach (var t in items)
            {
                int temp;
                if (string.IsNullOrWhiteSpace(t) || stopwords.Contains(t) || int.TryParse(t, out temp))
                    continue;

                return false;
            }

            return true;
        }
    }

    public class GenTableGraphFromTSV
    {

        public static void Run(string tsvFileInput, string HeaderDefinitionFile, string dirPath)
        {

            string[] tsvFileInfos = tsvFileInput.Split(',');

            var dictEntity2Types = new Dictionary<string, HashSet<string>>();
            var dictEntity2PredicateAnswer = new Dictionary<string, Dictionary<string, string>>();
            string headDemoFile = string.Format("{0}{1}", dirPath, HeaderDefinitionFile);
            var dictHeader2Type = GenTableGraph.ReadHeaderDef(headDemoFile);

            var dictNGram2Predicates = new Dictionary<string, Dictionary<string, double>>();

            Environment.CurrentDirectory = dirPath;
            NLPConstants.NLPModelDir = Environment.CurrentDirectory;
            StopWordDict stopwords = new StopWordDict();

            foreach (var tsv in tsvFileInfos)
            {
                string[] info2file = tsv.Split(':');
                string tableName = info2file[0];
                string file = info2file[1];
                tableName = file.Split('.')[0];
                file = dirPath + file;
                using (StreamReader sr = new StreamReader(file))
                {
                    TSVReader tsvReader = new TSVReader(sr, true);
                    while (!tsvReader.EndOfTSV)
                    {
                        TSVLine tsvLine = tsvReader.ReadLine();

                        foreach (var hi in tsvReader.HeaderTSVLine)
                        {
                            try
                            {
                                string ci = tsvLine[hi];
                                string keyi = GenTableGraph.GenKey(tableName, hi);

                                TableHeaderDefinition defi;
                                if (!dictHeader2Type.TryGetValue(keyi, out defi))
                                    continue;
                                List<string> ciTypes = defi.Types;

                                foreach (var cti in ciTypes)
                                {
                                    if (!GenTableGraph.IsEntityType(cti))
                                        continue;

                                    ci = Normalizer.SimpleNormalizeQuery(ci);

                                    if (!dictEntity2Types.ContainsKey(ci))
                                        dictEntity2Types.Add(ci, new HashSet<string>());
                                    dictEntity2Types[ci].Add(cti);

                                    string et = ci + "_" + cti;
                                    if (!dictEntity2PredicateAnswer.ContainsKey(et))
                                        dictEntity2PredicateAnswer.Add(et, new Dictionary<string, string>());

                                    foreach (var hj in tsvReader.HeaderTSVLine)
                                    {
                                        //if (hj == hi)
                                        //    continue;

                                        try
                                        {
                                            string keyj = GenTableGraph.GenKey(tableName, hj);

                                            TableHeaderDefinition defj;
                                            if (!dictHeader2Type.TryGetValue(keyj, out defj))
                                                continue;
                                            List<string> cjTypes = defj.Types;

                                            string cj = tsvLine[hj];
                                            foreach (var ctj in cjTypes)
                                            {
                                                if (GenTableGraph.IsEntityType(ctj))
                                                    continue;

                                                string valOfInterest = GenTableGraph.ExtractValue(defj.ValueExtractor, cj);
                                                //if (ctj.Equals(NLPConstants.EntityNamePredicate))
                                                //{
                                                //    valOfInterest = Normalizer.SimpleNormalizeQuery(valOfInterest);
                                                //}

                                                dictEntity2PredicateAnswer[et][ctj] = valOfInterest;

                                                if (defj.NeedIndex)
                                                {
                                                    GenTableGraph.Index(valOfInterest, ctj, ref dictNGram2Predicates, stopwords, 3);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                } // StreamReader

                #region output

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                using (MD5 md5 = MD5.Create())
                {
                    string Output = string.Format("{0}.{1}.entityids.txt",_Default.BotName, _Default.FileName );
                    Output = dirPath + Output;
                    using (StreamWriter sw = new StreamWriter(Output))
                    {
                        foreach (var p in dictEntity2Types)
                        {
                            foreach (var v in p.Value)
                            {
                                sw.WriteLine("{0}\t{1}", p.Key, GenTableGraph.GenEntityID(p.Key, v, md5));
                            }
                        }
                    }

                    string OutputInfo = string.Format("{0}.{1}.entityinfos.txt", _Default.BotName, _Default.FileName);
                    OutputInfo = dirPath + OutputInfo;
                    using (StreamWriter swInfo = new StreamWriter(OutputInfo))
                    {
                        foreach (var p in dictEntity2PredicateAnswer)
                        {
                            string[] temp = p.Key.Split('_');
                            string entity = temp[0];
                            string type = temp[1];
                            string id = GenTableGraph.GenEntityID(entity, type, md5);
                            var properties = new List<EntityApiHandler.Property>();
                            foreach (var vp in p.Value)
                            {
                                EntityApiHandler.Property property = new EntityApiHandler.Property()
                                {
                                    name = vp.Key,
                                    values = new EntityApiHandler.Value[]
                                                                            { new EntityApiHandler.Value { value = vp.Value } }
                                };
                                properties.Add(property);
                            }

                            //properties.Add(new EntityApiHandler.Property()
                            //{
                            //    name = "mso:type.object.name",
                            //    values = new EntityApiHandler.Value[] { new EntityApiHandler.Value { value = entity } }
                            //});

                            swInfo.WriteLine("{0}\t{1}\t{2}",
                                                id,
                                                type,
                                                JsonConvert.SerializeObject(properties.ToArray(), Formatting.None, settings)
                                            );
                        }
                    }
                }

                string PredicateValueIndex = string.Format("{0}.{1}.pred.index", _Default.BotName, _Default.FileName);
                PredicateValueIndex = dirPath + PredicateValueIndex;
                using (StreamWriter swIndex = new StreamWriter(PredicateValueIndex))
                {
                    foreach (var p in dictNGram2Predicates)
                    {
                        var items = (from v in p.Value where v.Value > 1 select (v.Key + "|||" + v.Value));
                        if (items.Count() > 0)
                            swIndex.WriteLine("{0}\t{1}", p.Key, string.Join(";", items));
                    }
                }

                #endregion
            }
        }
    }
}