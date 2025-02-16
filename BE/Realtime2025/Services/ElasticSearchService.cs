using Microsoft.Extensions.Options;
using Nest;

namespace Realtime2025.Services
{
    public class ElasticSearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchService(IOptions<ElasticSearchConfig> config)
        {
            if (string.IsNullOrEmpty(config.Value.Url))
            {
                throw new ArgumentNullException(nameof(config.Value.Url), "Elasticsearch URL is required.");
            }

            var settings = new ConnectionSettings(new Uri(config.Value.Url))
                .DefaultIndex(config.Value.IndexName ?? "todos") // Nếu không có IndexName thì mặc định "todos"
                .DisableDirectStreaming(); // Giúp debug request/response

            _elasticClient = new ElasticClient(settings);
        }

        public IElasticClient GetClient() => _elasticClient;
    }

    public class ElasticSearchConfig
    {
        public string Url { get; set; } = string.Empty;
        public string IndexName { get; set; } = "todos";
    }
}
