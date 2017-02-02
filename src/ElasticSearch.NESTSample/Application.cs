using ElasticSearch.NESTSample.Infrastructure;

using Microsoft.Extensions.Logging;

namespace ElasticSearch.NESTSample
{
    public class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly IReader _reader;
        private readonly IIndexer _indexer;

        public Application(
            ILogger<Application> logger,
            IReader reader,
            IIndexer indexer)
        {
            _logger = logger;
            _reader = reader;
            _indexer = indexer;
        }

        public void Run()
        {
            _logger.LogInformation("Application started.");

            var newsItems = _reader.Read();

            _indexer.Index(newsItems);

            _logger.LogInformation("Application ended.");
        }
    }
}
