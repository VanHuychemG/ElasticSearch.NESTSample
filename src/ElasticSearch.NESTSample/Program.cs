using System.IO;

using ElasticSearch.NESTSample.Infrastructure;
using ElasticSearch.NESTSample.Infrastructure.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ElasticSearch.NESTSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var app = serviceProvider.GetService<Application>();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();

            services.AddSingleton(loggerFactory);
            services.AddLogging();

            var configuration = GetConfiguration();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddOptions();
            services.Configure<IndexConfiguration>(configuration.GetSection("Elastic:Index"));

            services.AddSingleton<IIndexer, Indexer>();
            services.AddSingleton<IReader, Reader>();
            services.AddTransient<Application>();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            return configuration.Build();
        }
    }
}
