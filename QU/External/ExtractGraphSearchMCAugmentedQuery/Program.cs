namespace ExtractGraphSearchMCAugmentedQuery
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string azureFolder = File.ReadAllText(args[0]).Trim();
            int totalSegments = int.Parse(args[1]);
            int currentSegment = int.Parse(args[2]);
            string naturallyOccuringQEs = args[3];
            string logFile = args[4];
            List<ScrapeQueryResult> scrapResults = new PbxmlScraperResultsProcessor(azureFolder, totalSegments, currentSegment).ExtractAugmentedEntityIndexQuery();
            IEnumerable<string> augmentedQueries = from curscrapequeryresult in scrapResults
                where curscrapequeryresult.Query != string.Empty
                select curscrapequeryresult.Query + "\t" + curscrapequeryresult.EntityIndexAugmentedQuery + "\t" + curscrapequeryresult.MCResultEntityId + "\t" + curscrapequeryresult.HasEntityIndexAnswer.ToString();
            File.WriteAllLines(naturallyOccuringQEs, augmentedQueries);
            IEnumerable<string> logelements = from curscrapequeryresult in scrapResults select curscrapequeryresult.Query + "\t" + curscrapequeryresult.PbxmlURI + "\t" + curscrapequeryresult.ErrorMessage;
            File.WriteAllLines(logFile, logelements);
        }
    }
}

