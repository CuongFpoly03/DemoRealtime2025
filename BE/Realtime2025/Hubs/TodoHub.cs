using Microsoft.AspNetCore.SignalR;

namespace Realtime2025.Hubs
{
    public class TodoHub : Hub
    {
        public async Task JoinGroup(string UserId) {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserId);
        }

        public async Task LeaveGroup(string UserId) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, UserId);
        }
    }
}