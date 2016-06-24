using Microsoft.DataIntegration.Common.Data;
using Microsoft.DataIntegration.FuzzyMatching;
using Microsoft.DataIntegration.FuzzyMatching.FuzzyLookupBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility
{
    public class EntityRewriter
    {

    }

    public class EntityFuzzyMatcher
    {
        DomainManager domainManager;
        DataTable referenceTable;
        FuzzyQuery fuzzyQuery;
        DataTable outputTable;
        public const string DomainName = "FuzzyEntity";
        static string EntityColumnName = "Entity";

        public EntityFuzzyMatcher(string entityFile)
        {
            domainManager = new DomainManager();
            domainManager.CreateDomain(DomainName);

            referenceTable = LoadEntitiesFromFile(entityFile);

            // Create a new Fuzzy Lookup builder
            // Set up a few parameters (see the code for FuzzyLookupBuilder for more options)
            FuzzyLookupBuilder builder = new FuzzyLookupBuilder();

            builder.EnableEditTransformations = true;
            builder.EnablePrefixTransformations = false;
            builder.EnableTokenMergeTransformations = false;
            builder.EnableTokenSplitTransformations = false;
            
            builder.MatchAcrossColumns = false;
            builder.EditThreshold = 0.75;
            builder.MaxEditRulesPerToken = 10;
            builder.IgnoreCase = true;
            builder.IgnoreNonSpacing = true;
            builder.MinQueryThreshold = 0.8;

            // Create a lookup object over the reference table
            FuzzyLookup fuzzyLookup = builder.CreateFuzzyLookup(referenceTable, EntityColumnName);

            // Create a query over the lookup 
            fuzzyQuery = builder.CreateFuzzyQuery(fuzzyLookup);

            // Create the output table
            outputTable = CreateOutputTable(fuzzyQuery.MatchResultSchema);
        }

        public bool Match(string query, out DataTable output)
        {
            outputTable.Rows.Clear();

            DataTable dt = new DataTable();
            dt.Columns.Add(EntityColumnName, typeof(string));
            SimpleDataRecord dr = new SimpleDataRecord(dt, new object[] { query.Replace(' ', '.') });

            bool matchesFound = false;

            using (MatchResultsReader resultsReader = fuzzyQuery.Match(dr))
            {
                while (resultsReader.Read())
                {
                    object[] values = new object[outputTable.Columns.Count];
                    resultsReader.GetValues(values);
                    values[values.Length - 1] = resultsReader.ComparisonResult.Similarity;
                    outputTable.Rows.Add(values);
                    matchesFound = true;
                }

                if (!matchesFound)
                {
                    object[] values = new object[outputTable.Columns.Count];
                    resultsReader.InputRecord.GetValues(values);
                    outputTable.Rows.Add(values);
                }
            }

            output = outputTable;

            return matchesFound;
        }

        static DataTable LoadEntitiesFromFile(string file, int col = 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(EntityColumnName, typeof(string));

            using (StreamReader sr = new StreamReader(file))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split('\t');
                    if (items.Length < col + 1)
                        continue;
                    dt.Rows.Add(items[col].Replace(' ', '.'));
                }
            }

            return dt;
        }

        static RecordBinding CreateDomainBinding(DataTable schema, bool matchAcrossColumns, params string[] columnsToMatchOn)
        {
            RecordBinding recordBinding = new RecordBinding(schema);

            if (matchAcrossColumns)
            {
                recordBinding.Bind(schema.TableName, columnsToMatchOn);
            }
            else
            {
                foreach (string columnName in columnsToMatchOn)
                {
                    recordBinding.Bind(columnName, columnName);
                }
            }

            return recordBinding;
        }

        // The output table consists of the reference columns followed by RawScore and FinalScore columns.
        static DataTable CreateOutputTable(DataTable schemaTable)
        {
            DataTable outputTable = new DataTable();

            // Add a column for each reference column
            foreach (DataRow r in schemaTable.Rows)
            {
                outputTable.Columns.Add(r[SchemaTableColumn.ColumnName] as string, (Type)r[SchemaTableColumn.DataType]);
            }

            return outputTable;
        }
    }
}
