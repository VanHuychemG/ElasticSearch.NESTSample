namespace ElasticSearch.NESTSample.Infrastructure.Configuration
{
    public class IndexConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string LiveIndexAlias { get; set; }

        public int BackOffRetries { get; set; }
        public int BackOffTimeInSeconds { get; set; }
        public int MaxDegreeOfParallelism { get; set; }
        public int BatchSize { get; set; }
    }
}
