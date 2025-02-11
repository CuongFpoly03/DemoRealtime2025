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
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
#region Declare info to this Processor
// Register DB, You can register multiple databases here
builder.Services.AddDbContext<RealtimeDbContext>(op => op.UseNpgsql(config.GetConnectionString("DatabaseConnectionString")));
builder.Services.AddInfrastructure(config);
builder.Services.AddApplication(config);

// Add cors
builder.Services.AddHttpContextAccessor();
// addcontroller
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ProducesAttribute("application/json"));
    opt.Filters.Add(new ConsumesAttribute("application/json"));

    // Authorization policy
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
    );

// add serilog 
AddSerilog(config, builder.Services);

// add identity
builder.Services.AddIdentityModule(config);

// add awagger
builder.Services.AddSwaggerModule();
#endregion

#region Init & start this Processor. End of it, ready to handle work
var appConfiguration = GetAppConfiguration();
// Đăng ký MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();
// Configure the HTTP request pipeline.
IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the Database:
// - migrate db: (every time) dev env or (production / stagging) with special parameter: /mig, Notice replicas = 1
// - seeding data for first time run /seed and db tables have no row.
var isMig = (args.Any(x => x == "mig") || app.Environment.IsDevelopment());
var isSeed = args.Any(x => x == "seed");
app.UseApplicationDatabase<RealtimeDbContext>(serviceProvider, isMig);
app.SeedData(serviceProvider, appConfiguration, isSeed);

// Configure CORS
app.UseApplicationCors(builder.Services, builder.Configuration);

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

#region startup configuration
static void AddSerilog(IConfiguration configuration, IServiceCollection services)
{
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(configuration)
    .CreateLogger();

    services.AddLogging(loggingBuilder =>
        loggingBuilder.AddSerilog(dispose: true)); // Dispose of logger on shutdown
}

static IConfiguration GetAppConfiguration()
{
    // Actually, before ASP.NET bootstrap, we must rely on environment variable to get environment name
    // https://docs.microsoft.com/fr-fr/aspnet/core/fundamentals/environments?view=aspnetcore-2.2
    // Pay attention to casing for Linux environment. By default it's pascal case.
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{environment}.json", true)
        .AddEnvironmentVariables()      // Get from OS, Docker Orchestrator
                                        // .AddCommandLine(args) for any specify action
        .Build();
}
#endregion

