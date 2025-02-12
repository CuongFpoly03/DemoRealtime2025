using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace Realtime2025.Hubs
{
    public class TodoHub : Hub
    {
        private readonly IDatabase _redisDb;
        private readonly ILogger<TodoHub> _logger;

        public TodoHub(IDatabase redisDb, ILogger<TodoHub> logger)
        {
            _redisDb = redisDb;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string? userId = await _redisDb.StringGetAsync($"Connect:{Context.ConnectionId}");
            if (!string.IsNullOrEmpty(userId))
            {
                await _redisDb.KeyDeleteAsync($"User:{userId}");
            }

            _logger.LogInformation($"Client disconnected: {Context.ConnectionId}, UserId: {userId}");

            if (exception != null)
            {
                _logger.LogError(exception, "Error on disconnect.");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogWarning("JoinGroup failed: userId is null or empty.");
                return;
            }

            string connectionId = Context.ConnectionId;
            await _redisDb.StringSetAsync($"User:{userId}", connectionId);
            await Groups.AddToGroupAsync(connectionId, userId);

            _logger.LogInformation($"User {userId} joined group {userId} with connection {connectionId}.");
        }

        public async Task LeaveGroup(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogWarning("LeaveGroup failed: userId is null or empty.");
                return;
            }

            string connectionId = Context.ConnectionId;
            await _redisDb.KeyDeleteAsync($"User:{userId}");
            await Groups.RemoveFromGroupAsync(connectionId, userId);

            _logger.LogInformation($"User {userId} left group {userId}.");
        }

        public async Task SendNotification(string type, string targetId, string message)
        {
            if (string.IsNullOrWhiteSpace(targetId) || string.IsNullOrWhiteSpace(message))
            {
                _logger.LogWarning("SendNotification failed: targetId or message is null or empty.");
                return;
            }

            switch (type.ToLower())
            {
                case "user":
                    string? connectionId = await _redisDb.StringGetAsync($"User:{targetId}");
                    if (!string.IsNullOrEmpty(connectionId))
                    {
                        await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
                        _logger.LogInformation($"Sent notification to user {targetId}.");
                    }
                    else
                    {
                        _logger.LogWarning($"User {targetId} not connected.");
                    }
                    break;

                case "group":
                    await Clients.Group(targetId).SendAsync("ReceiveNotification", message);
                    _logger.LogInformation($"Sent notification to group {targetId}.");
                    break;

                case "all":
                    await Clients.All.SendAsync("ReceiveNotification", message);
                    _logger.LogInformation("Sent notification to all users.");
                    break;

                default:
                    _logger.LogError($"Invalid notification type: {type}");
                    throw new ArgumentException("Invalid notification type.");
            }
        }
    }
}
