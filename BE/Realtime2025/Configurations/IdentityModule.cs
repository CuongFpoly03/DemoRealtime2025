using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Realtime.Domain.Entity;
using Realtime.Infrastructure;

namespace Realtime2025.Configurations
{
    public static class IdentityModule
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 6;
                opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                opt.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<RealtimeDbContext>()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, RealtimeDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, RealtimeDbContext, Guid>>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"]))
                };
                opt.Events = new JwtBearerEvents
                {
                    OnChallenge = ctx =>
                    {
                        ctx.HandleResponse();
                        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        ctx.Response.ContentType = "application/json";
                        var res = JsonSerializer.Serialize(new { message = "Unauthorized" });
                        return ctx.Response.WriteAsync(res);
                    }
                };
            });
            IdentityModelEventSource.ShowPII = true;

            return services;
        }
    }
}