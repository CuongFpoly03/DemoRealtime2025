using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Application.Configurations.Mapper;

namespace Realtime.Application.extensions
{
    public static class ServiceCollectionExtensions
    {
          public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            // Register Automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
        
    }
}