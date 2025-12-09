using Microsoft.AspNetCore.SignalR;
using TaskyRevamp.Dtos.Discussion;

namespace TaskyRevamp.Infrastructure.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(Guid receiverId, DiscussionMessageDto message)
    {
        await Clients.User(receiverId.ToString().ToLower()).SendAsync("ReceiveMessage", receiverId, message);
    }
}
