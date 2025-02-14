using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Infrastructure
{
    public class RealtimeDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IRealtimeDbContext
    {
        public RealtimeDbContext(DbContextOptions options) : base(options) { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        //session Realtime
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Todo> Todos { get; set; }
        //sesion Topicsql
        public DbSet<TopicSQL> TopicSQLs { get; set; }
        public DbSet<TopicSQL2> TopicSQL2s { get; set; }


        #region Seeding data
        public void Seed(CancellationToken cancellationToken = default)
        {
            // Add seeding data for each table here
            /*
            var exitsDemoData = this.Demo.Any();
            if (!exitsDemoData)
            {
                // DoSomething
            }
            */

            // Save
            base.SaveChangesAsync(cancellationToken);
        }

        #endregion Seeding data
    }
}