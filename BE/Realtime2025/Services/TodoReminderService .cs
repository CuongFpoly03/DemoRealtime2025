using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Realtime.Infrastructure;
using Realtime2025.Hubs;

namespace Realtime2025.Services
{
    public class TodoReminderService : BackgroundService
    {
        private readonly IHubContext<TodoHub> _HubContext;
        private readonly IServiceScopeFactory _ServiceScopeFactory;

        public TodoReminderService(IHubContext<TodoHub> hubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _HubContext = hubContext;
            _ServiceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken StoppingToken)
        {
            while (!StoppingToken.IsCancellationRequested)
            {
                using (var scope = _ServiceScopeFactory.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetRequiredService<RealtimeDbContext>();
                    var now = DateTime.UtcNow;
                    var todos = await dbcontext.Todos
                    .Where(t => !t.IsCompleted && !t.IsExpired && (t.DueDate.Value - now).TotalMinutes <= 3)
                    .ToListAsync();

                    foreach (var todo in todos)
                    {
                        await _HubContext.Clients.Group(todo.UserId.ToString()).SendAsync("ReceiveNotification",
                        $"Công việc '{todo.Title}' sẽ hết hạn sau {(todo.DueDate.Value - now).TotalMinutes} phút!");
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(3), StoppingToken);
            }
        }

    }
}