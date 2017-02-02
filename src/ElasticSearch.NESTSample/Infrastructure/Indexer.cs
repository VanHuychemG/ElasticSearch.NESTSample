using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using ElasticSearch.NESTSample.Domain;
using ElasticSearch.NESTSample.Infrastructure.Configuration;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Nest;

namespace ElasticSearch.NESTSample.Infrastructure
{
    public interface IIndexer
    {
        void Index(IList<NewsItem> newsItems);
    }

    public class Indexer: IIndexer
    {
        private readonly ILogger<Indexer> _logger;
        private readonly IndexConfiguration _configuration;

        private static string _host;
        private static int _port;

        public Indexer(
            ILogger<Indexer> logger,
            IOptions<IndexConfiguration> configuration)
        {
            _logger = logger;
            _configuration = configuration.Value;

            _host = _configuration.Host;
            _port = _configuration.Port;

            CreateTemplates();
        }

        private static ElasticClient Client
        {
            get
            {
                return new ElasticClient(new ConnectionSettings(CreateUri())
                    .DefaultIndex("belga")
                    .InferMappingFor<NewsItem>(i => i
                            .TypeName(typeof(NewsItem).Name)
                            .IndexName("belga")
                    ));
            }
        }

        private static Uri CreateUri()
        {
            if (Process.GetProcessesByName("fiddler").Any())
                _host = "ipv4.fiddler";

            return new Uri("http://" + _host + ":" + _port);
        }

        private void CreateTemplates()
        {
            //  dutch template
            const string dutchTemplateName = "news-nl-template";
            var dutchTemplate = Client.GetIndexTemplate(t => t.Name(dutchTemplateName));
            if (dutchTemplate.IsValid)
                Client.DeleteIndexTemplate(dutchTemplateName);

            Client.PutIndexTemplate(dutchTemplateName, t => t
                .Template("news-nl-*")
                .Settings(s => s
                    .NumberOfShards(3)
                    .NumberOfReplicas(0)
                    .Analysis(Analysis.Descriptors.DutchAnalysis))
                .Mappings(m => m
                    .Map<NewsItem>(Mapping.Descriptors.DutchMappings)));

            _logger.LogInformation($"Created template {dutchTemplateName}");

            //  french template
            const string frenchTemplateName = "news-fr-template";
            var frenchTemplate = Client.GetIndexTemplate(t => t.Name(frenchTemplateName));
            if (frenchTemplate.IsValid)
                Client.DeleteIndexTemplate(frenchTemplateName);

            Client.PutIndexTemplate(frenchTemplateName, t => t
                .Template("news-fr-*")
                .Settings(s => s
                    .NumberOfShards(3)
                    .NumberOfReplicas(0)
                    .Analysis(Analysis.Descriptors.FrenchAnalysis))
                .Mappings(m => m
                    .Map<NewsItem>(Mapping.Descriptors.FrenchMappings)));

            _logger.LogInformation($"Created template {frenchTemplateName}");
        }

        public void Index(IList<NewsItem> newsItems)
        {
            _logger.LogInformation("Started Indexing");

            _logger.LogInformation("Grouping news items per target index");

            var groupedNewsItems = newsItems
                .GroupBy(u => u.TargetIndex)
                .Select(grp => grp.ToList())
                .ToList();

            _logger.LogInformation($"{groupedNewsItems.Count} groups of news items created");

            _logger.LogInformation("Iterating target indices");

            foreach (var groupedNewsItem in groupedNewsItems)
            {
                var targetIndex = groupedNewsItem.First().TargetIndex;

                _logger.LogInformation($"Indexing {groupedNewsItem.Count} documents into elasticsearch.{targetIndex}");

                var waitHandle = new CountdownEvent(1);

                var bulkAll = Client.BulkAll(groupedNewsItem, b => b
                    .Index(targetIndex)
                    .BackOffRetries(_configuration.BackOffRetries)
                    .BackOffTime(TimeSpan.FromSeconds(_configuration.BackOffTimeInSeconds))
                    .RefreshOnCompleted()
                    .MaxDegreeOfParallelism(_configuration.MaxDegreeOfParallelism)
                    .Size(_configuration.BatchSize)
                );

                bulkAll.Subscribe(new BulkAllObserver(
                    b => //onNext
                    {
                        _logger.LogInformation($"Page {b.Page} of batch sent.");
                    },
                    e => //onError
                    {
                        _logger.LogError($"An error occurred ({e.Message}).");
                    },
                    () => waitHandle.Signal() //onCompleted
                ));

                waitHandle.Wait();

                _logger.LogInformation($"Indexed {groupedNewsItem.Count} documents into elasticsearch.{targetIndex}");
            }

            _logger.LogInformation("Finished Indexing");
        }
    }
}
