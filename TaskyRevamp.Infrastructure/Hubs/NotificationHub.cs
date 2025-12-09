using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TaskyRevamp.Dtos.NotificationDtos;

namespace TaskyRevamp.Infrastructure.Hubs
{
    // [Authorize]
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
