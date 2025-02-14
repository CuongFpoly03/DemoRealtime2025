using Microsoft.EntityFrameworkCore;
using Realtime.Domain.Entity;

namespace Realtime.Infrastructure.Interfaces
{
    public interface IRealtimeDbContext
    {
        // sesion Realtime
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<Todo> Todos { get; set; }
        // session TOPIC SQL
        DbSet<TopicSQL> TopicSQLs { get; set; }
        DbSet<TopicSQL2> TopicSQL2s { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}