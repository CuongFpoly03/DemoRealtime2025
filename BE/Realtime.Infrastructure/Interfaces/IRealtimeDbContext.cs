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
        DbSet<Payment> Payments { get; set; }
        DbSet<PaymentWeekHook> PaymentWeekHooks { get; set; }
        DbSet<WebHookData> WebHookDatas { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}