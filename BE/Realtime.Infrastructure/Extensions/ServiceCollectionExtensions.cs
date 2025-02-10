using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Register Repository
            services.AddScoped(typeof(IRealtimeDbContext), typeof(RealtimeDbContext));
            return services;
        }
    }
}