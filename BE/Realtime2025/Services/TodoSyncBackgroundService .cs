using Realtime.Infrastructure.services;

namespace Realtime.Infrastructure.Services
{
    public class TodoSyncBackgroundService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TodoSyncBackgroundService> _logger;

        public TodoSyncBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<TodoSyncBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("🔄 Bắt đầu đồng bộ dữ liệu Todo vào Elasticsearch...");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var syncService = scope.ServiceProvider.GetRequiredService<TodoSyncService>();
                await syncService.SyncTodosToElasticsearch();
            }

            _logger.LogInformation("✅ Đồng bộ dữ liệu Todo hoàn tất!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("🛑 TodoSyncBackgroundService đang dừng...");
            return Task.CompletedTask;
        }
    }
}
