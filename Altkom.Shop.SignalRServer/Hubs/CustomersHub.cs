using Altkom.Shop.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.SignalRServer.Hubs
{
    public class CustomersHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Trace.WriteLine($"Connected {this.Context.ConnectionId}");

            return base.OnConnectedAsync();
        }

        public async Task SendCustomer(Customer customer)
        {
            await Clients.Others.SendAsync("YouHaveGotNewCustomer", customer);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.SendAsync("pong", message);
        }
    }
}
