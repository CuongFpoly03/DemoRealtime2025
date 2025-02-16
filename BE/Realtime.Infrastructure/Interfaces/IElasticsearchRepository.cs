using Nest;
using Realtime.Domain.Entity;

namespace Realtime.Infrastructure.Interfaces
{
    public interface IElasticsearchRepository
    {
        Task IndexTodoAsync(Todo todo, CancellationToken cancellationToken = default);
        Task<ISearchResponse<Todo>>  SearchTodoAsync(string keyword, CancellationToken cancellationToken = default);
    }
}