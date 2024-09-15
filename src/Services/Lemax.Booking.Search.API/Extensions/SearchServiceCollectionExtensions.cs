using Lemax.Booking.Search.API.Infrastructure.Configurations;
using Nest;

namespace Lemax.Booking.Search.API.Extensions
{
    public static class SearchServiceCollectionExtensions
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ElasticSearchOptions>(configuration.GetSection("ElasticSearch"));

            var elasticsearchOptions = new ElasticSearchOptions();
            configuration.GetSection("ElasticSearch").Bind(elasticsearchOptions);

            if (string.IsNullOrWhiteSpace(elasticsearchOptions.Url))
                throw new ArgumentException("Elasticsearch URL cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(elasticsearchOptions.DefaultIndex))
                throw new ArgumentException("Elasticsearch default index cannot be null or empty.");

            var settings = new ConnectionSettings(new Uri(elasticsearchOptions.Url))
                .DefaultIndex(elasticsearchOptions.DefaultIndex)
                .EnableDebugMode()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            services.AddSingleton<IElasticSearchService, ElasticSearchService>();

            return services;
        }
    }
}
