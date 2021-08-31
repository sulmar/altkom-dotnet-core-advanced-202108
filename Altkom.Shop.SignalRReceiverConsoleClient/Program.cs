using Altkom.Shop.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.Shop.SignalRReceiverConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Receiver!");

            const string url = "http://localhost:5000/signalr/customers";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            connection.On<Customer>("YouHaveGotNewCustomer",
              customer => Console.WriteLine($"Received {customer.FullName}"));

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine($"Connected {connection.ConnectionId}.");


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();


        }
    }
}
