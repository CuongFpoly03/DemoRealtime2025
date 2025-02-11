using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Infrastructure.Persistence.Repositories
{
    public class TodoRepository : ITodoRepository
    {

        private readonly IRealtimeDbContext _context;


        public TodoRepository(IRealtimeDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> AddTodoAsync(Todo todo, CancellationToken cancellationToken = default)
        {
            // defined
            todo.CreatedAt = DateTime.UtcNow;
            todo.UpdatedAt = DateTime.UtcNow;
            await _context.Todos.AddAsync(todo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return todo;
        }
    }
}