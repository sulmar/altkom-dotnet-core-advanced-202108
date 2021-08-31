using Altkom.Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Altkom.Shop.SignalRServer.Hubs
{

   // [Authorize]
    public class CustomersHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Trace.WriteLine($"Connected {this.Context.ConnectionId}");

            //string country = this.Context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Country).Value;
            //this.Groups.AddToGroupAsync(Context.ConnectionId, country);

            return base.OnConnectedAsync();
        }
        
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendCustomer(Customer customer)
        {
            // await Clients.Others.SendAsync("YouHaveGotNewCustomer", customer);

            await Clients.Group("GrupaA").SendAsync("YouHaveGotNewCustomer", customer);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.SendAsync("pong", message);
        }
    }
}
