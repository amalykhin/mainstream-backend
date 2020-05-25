using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamingService.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var chatEntry = new {
                sender = user,
                message = message
            };
            await Clients.All.SendAsync("ReceiveMessage", chatEntry);
        }
    }
}
