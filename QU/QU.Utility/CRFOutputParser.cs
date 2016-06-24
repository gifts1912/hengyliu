using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QU.Utility
{
    public class CRFOutputParser
    {
        /// <summary>
        /// Parse result like {"Span":"kidney","Begin":3,"End":9,"Score":0.979564,"Type":"Ent_bodystructure4"}
        /// </summary>
        /// <param name="result">e.g. {"Span":"kidney","Begin":3,"End":9,"Score":0.979564,"Type":"Ent_bodystructure4"}</param>
        /// <returns></returns>
        public static QueryParseResult ParseResult(string result)
        {
            return JsonConvert.DeserializeObject<QueryParseResult>(result);
        }

        /// <summary>
        /// Parse result like {"Span":"kidney","Begin":3,"End":9,"Score":0.979564,"Type":"Ent_bodystructure4"}|{...}
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static List<QueryParseResult> ParseResults(string results)
        {
            List<QueryParseResult> qpResults = new List<QueryParseResult>();

            string[] items = results.Split('|');
            foreach (string item in items)
            {
                if (string.IsNullOrEmpty(item))
                    continue;

                var qpResult = ParseResult(item);
                if (null != qpResult)
                    qpResults.Add(qpResult);
            }

            return qpResults;
        }
    }

    public class QueryParseResult
    {
        [JsonProperty(PropertyName = "Span", Required = Required.Always)]
        public string Span;

        [JsonProperty(PropertyName = "Begin", Required = Required.Always)]
        public int Begin;

        [JsonProperty(PropertyName = "End", Required = Required.Always)]
        public int End;

        [JsonProperty(PropertyName = "Score", Required = Required.Always)]
        public double Score;

        [JsonProperty(PropertyName = "Type", Required = Required.Always)]
        public string Type;
    }
}
