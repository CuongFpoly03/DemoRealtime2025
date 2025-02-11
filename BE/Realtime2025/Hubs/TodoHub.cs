using Microsoft.AspNetCore.SignalR;

namespace Realtime2025.Hubs
{
    public class TodoHub : Hub
    {
        public async Task JoinGroup(Guid UserId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserId.ToString());
        }

        public async Task LeaveGroup(Guid UserId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, UserId.ToString());
        }
    }
}