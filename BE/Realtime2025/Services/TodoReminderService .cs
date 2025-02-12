using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Realtime.Infrastructure;
using Realtime2025.Hubs;

namespace Realtime2025.Services
{
    public class TodoReminderService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<TodoHub> _hubContext;
        private readonly IDatabase _redisDb;

        public TodoReminderService(IServiceScopeFactory serviceScopeFactory, IHubContext<TodoHub> hubContext, IConnectionMultiplexer redis)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
            _redisDb = redis.GetDatabase();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<RealtimeDbContext>();
                    var now = DateTime.UtcNow;
                    Console.WriteLine($"🕒 Checking todos at: {now}");

                    var todos = await dbContext.Todos
                        .Where(t => !t.IsCompleted && !t.IsExpired && t.DueDate.HasValue && (t.DueDate.Value - now).TotalMinutes <= 180)
                        .ToListAsync();

                    foreach (var todo in todos)
                    {
                        string userId = todo.UserId.ToString();
                        string message = $"Công việc '{todo.Title}' sắp đến hạn sau {(todo.DueDate.Value - now).TotalMinutes} phút!";
                        
                        // Lấy connectionId từ Redis
                        string? connectionId = await _redisDb.StringGetAsync($"User:{userId}");
                        
                        if (!string.IsNullOrEmpty(connectionId))
                        {
                            // Gửi thông báo trực tiếp đến connectionId của user
                            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
                            Console.WriteLine($"Sent notification to user {userId} (connectionId: {connectionId}).");
                        }
                        else
                        {
                            Console.WriteLine($"User {userId} không online, không thể gửi thông báo.");
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
