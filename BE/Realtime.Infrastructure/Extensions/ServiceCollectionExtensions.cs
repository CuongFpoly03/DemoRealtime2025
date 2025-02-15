using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Infrastructure.Interfaces;
using Realtime.Infrastructure.Persistence.Repositories;
using Realtime.Infrastructure.services;

namespace Realtime.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
             
            // Register Repository
            services.AddScoped(typeof(IRealtimeDbContext), typeof(RealtimeDbContext));
            services.AddScoped(typeof(IJwtService), typeof(JwtService));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(ITodoRepository), typeof(TodoRepository));
            services.AddScoped(typeof(IWebhookRepository), typeof(WebHookRepository));
            return services;
        }
    }
}