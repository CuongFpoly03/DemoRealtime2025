
using Nest;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Infrastructure.Persistence.Repositories
{
    public class TodoSearchRepository : IElasticsearchRepository
    {
        private readonly ElasticClient _elasticClient;
        public TodoSearchRepository(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        // them todo vao elasticsearch
        public async Task IndexTodoAsync(Todo todo, CancellationToken cancellationToken = default)
        {
            var indexResponse = await _elasticClient.IndexDocumentAsync(todo);
            if (!indexResponse.IsValid)
            {
                throw new Exception($"lỗi khi index document: {indexResponse.DebugInformation}");
            }
        }

        // search todo theo keyword
        public async Task<ISearchResponse<Todo>> SearchTodoAsync(string keyword, CancellationToken cancellationToken = default)
        {
            var searchResponse = await _elasticClient.SearchAsync<Todo>(s => s.Query(q => q.Match(m => m.Field(f => f.Title).Query(keyword))));
            return searchResponse;
        }

    }
}