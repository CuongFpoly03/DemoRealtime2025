using Microsoft.EntityFrameworkCore;
using Realtime.Domain.Entity;

namespace Realtime.Infrastructure.Interfaces
{
    public interface IRealtimeDbContext
    {
        // sesion Realtime
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<Todo> Todos { get; set; }
        // session Weekhook
        DbSet<WebhookEvent> WebhookEvents { get; set; }
        DbSet<WebhookLog> WebhookLogs { get; set; }
        DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}