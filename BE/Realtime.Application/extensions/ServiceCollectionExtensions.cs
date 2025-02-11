using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Application.Commands.TodoCommand;
using Realtime.Application.Configurations.Mapper;

namespace Realtime.Application.extensions
{
    public static class ServiceCollectionExtensions
    {
          public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            // Register Automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
             // Register MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(CreateTodoCommand).Assembly);
            });
            return services;
        }
        
    }
}