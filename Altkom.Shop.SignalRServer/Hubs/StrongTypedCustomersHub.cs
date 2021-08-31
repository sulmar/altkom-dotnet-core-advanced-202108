using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.SignalRServer.Hubs
{
    public class StrongTypedCustomersHub : Hub<ICustomerClient>, ICustomerServer
    {
        public override Task OnConnectedAsync()
        {
            Trace.WriteLine($"Connected {this.Context.ConnectionId}");

            this.Groups.AddToGroupAsync(Context.ConnectionId, "GrupaA");

            return base.OnConnectedAsync();
        }

        public async Task SendCustomer(Customer customer)
        {
            await Clients.Others.YouHaveGotNewCustomer(customer);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.Pong(message);
        }


    }
}
