using Microsoft.AspNetCore.SignalR;

namespace webapi.Hubs
{
    public class ChatHub : Hub
    {
        private static int OnlineAdminCount { get; set; } = 0;

        public async Task SendMessageToAdmin(string username, string message)
        {
            await Clients.Caller.SendAsync("messageReceived", username, message);
            await Clients.Group("Admin").SendAsync("messageReceivedFromUser", username, message, Context.ConnectionId);
        }

        public async Task SendMessageToUser(string connectionId, string message)
        {
            await Clients.Group("Admin").SendAsync("messageReceivedFromAdmin", connectionId, message);
            await Clients.Group(connectionId).SendAsync("messageReceived", Guid.NewGuid().ToString(), message);
        }

        public override async Task OnConnectedAsync()
        {
            if (Context?.User?.Identity?.IsAuthenticated == false)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
                OnlineAdminCount++;
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, Context.ConnectionId);
            }

            if (OnlineAdminCount == 0)
            {
                await Clients.Caller.SendAsync("NoAdminAlive");
            }
            else
            {
                await Clients.All.SendAsync("AdminAlive");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context?.User?.Identity?.IsAuthenticated == false)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admin");
                OnlineAdminCount--;
            }
            else
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.ConnectionId);
            }

            if (OnlineAdminCount == 0)
            {
                await Clients.All.SendAsync("NoAdminAlive");
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
