using Altkom.Shop.Fakers;
using Altkom.Shop.Models;
using Bogus;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.Shop.SignalRSenderConsoleClient
{
    class Program
    {

        // =< C# 7.0
        //static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        //static async Task MainAsync(string[] args)
        //{

        //}

        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Sender!");

            const string url = "http://localhost:5000/signalr/customers";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");

            await connection.StartAsync();
            
            Console.WriteLine($"Connected {connection.ConnectionId}.");

            Faker<Customer> customerFaker = new CustomerFaker(new AddressFaker());

            Customer customer = customerFaker.Generate();

            Console.WriteLine($"Sending... {customer.FullName}");

            await connection.SendAsync("SendCustomer", customer);

            Console.WriteLine("Sent.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();

        }
    }
}
