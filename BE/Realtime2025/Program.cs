using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Realtime.Application.extensions;
using Realtime.Infrastructure;
using Realtime.Infrastructure.Extensions;
using Realtime2025.Configurations;
using Realtime2025.Hubs;
using Realtime2025.Services;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

#region Dependency Injection Setup

// Register DB Context
builder.Services.AddDbContext<RealtimeDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("DatabaseConnectionString")));

builder.Services.AddInfrastructure(config);
builder.Services.AddApplication(config);

// Add HttpContext Accessor
builder.Services.AddHttpContextAccessor();

// Configure Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));

    // Require authentication for all controllers
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Configure Redis Connection
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(config.GetConnectionString("Redis") ?? "localhost:6379"));
builder.Services.AddScoped<IDatabase>(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

// Add Serilog Logging
ConfigureSerilog(config, builder.Services);

// Add Identity Module
builder.Services.AddIdentityModule(config);

// Add Swagger
builder.Services.AddSwaggerModule();

// Add SignalR
builder.Services.AddSignalR();
builder.Services.AddHostedService<TodoReminderService>();

// Register SignalR Hub
builder.Services.AddTransient<TodoHub>();

#endregion

#region Application Startup

var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure Database Migration & Seeding
IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
bool isMigration = args.Contains("mig") || app.Environment.IsDevelopment();
bool isSeeding = args.Contains("seed");

app.UseApplicationDatabase<RealtimeDbContext>(serviceProvider, isMigration);
app.SeedData(serviceProvider, config, isSeeding);

// Configure CORS
app.UseApplicationCors(builder.Services, builder.Configuration);

// Configure HTTP Pipeline
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Configure SignalR
app.MapHub<TodoHub>("/todoHub");

app.Run();

#endregion

#region Helper Methods

static void ConfigureSerilog(IConfiguration configuration, IServiceCollection services)
{
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

    services.AddLogging(loggingBuilder =>
        loggingBuilder.AddSerilog(dispose: true)); // Dispose of logger on shutdown
}

#endregion
