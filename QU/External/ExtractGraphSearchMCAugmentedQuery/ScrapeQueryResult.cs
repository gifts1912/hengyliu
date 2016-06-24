namespace ExtractGraphSearchMCAugmentedQuery
{
    using System;
    using System.Runtime.CompilerServices;

    public class ScrapeQueryResult
    {
        public string EntityIndexAugmentedQuery { get; set; }

        public string ErrorMessage { get; set; }

        public bool HasEntityIndexAnswer { get; set; }

        public string MCResultEntityId { get; set; }

        public string PbxmlURI { get; set; }

        public string Query { get; set; }
    }
}

