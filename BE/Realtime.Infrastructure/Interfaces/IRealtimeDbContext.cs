using Microsoft.EntityFrameworkCore;
using Realtime.Domain.Entity;

namespace Realtime.Infrastructure.Interfaces
{
    public interface IRealtimeDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }
        
    }
}