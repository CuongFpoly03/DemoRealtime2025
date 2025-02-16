using MediatR;
using Nest;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

// Định nghĩa alias cho IRequest của MediatR
using MediatRRequest = MediatR.IRequest<Nest.ISearchResponse<Realtime.Domain.Entity.Todo>>;

namespace Realtime.Application.Queries.TodoElasticsearch
{
    // Định nghĩa Query request
    public class SearchTodoWithElasticsearchQuery : MediatRRequest
    {
        public string Keyword { get; }

        public SearchTodoWithElasticsearchQuery(string keyword)
        {
            Keyword = keyword;
        }
    }

    // Handler xử lý truy vấn Elasticsearch
    public class SearchTodoWithElasticsearchQueryHandler : IRequestHandler<SearchTodoWithElasticsearchQuery, ISearchResponse<Todo>>
    {
        private readonly IElasticsearchRepository _elasticsearchRepository;

        public SearchTodoWithElasticsearchQueryHandler(IElasticsearchRepository elasticsearchRepository)
        {
            _elasticsearchRepository = elasticsearchRepository;
        }

        public async Task<ISearchResponse<Todo>> Handle(SearchTodoWithElasticsearchQuery request, CancellationToken cancellationToken)
        {
            return await _elasticsearchRepository.SearchTodoAsync(request.Keyword, cancellationToken);
        }
    }
}
