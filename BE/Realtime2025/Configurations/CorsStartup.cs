using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Realtime2025.Configurations
{
    public static class CorsStartup
    {
        public static IApplicationBuilder UseApplicationCors(this IApplicationBuilder app, IServiceCollection services, IConfiguration configuration)
        {
            string cors = configuration.GetSection("Cors").Value ?? "*";
            if (cors.Equals("*"))
            {
                app.UseCors(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            }
            else
            {
                app.UseCors(builder =>
                {
                    var listCors = cors.Split(',');
                    foreach (var c in listCors)
                    {
                        builder
                        .WithOrigins(c)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    }
                });
            }

            return app;
        }
    }
}