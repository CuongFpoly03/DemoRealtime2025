
using Realtime.Domain.Entity;

namespace Realtime.Infrastructure.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo> AddTodoAsync(Todo todo, CancellationToken cancellationToken = default);        
    }
}