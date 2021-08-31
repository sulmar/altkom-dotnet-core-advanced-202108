using Altkom.Shop.Fakers;
using Altkom.Shop.IServices;
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

            const string url = "https://localhost:5001/signalr/customers";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            Console.WriteLine("Connecting...");

            connection.Reconnecting += Connection_Reconnecting;
            connection.Reconnected += Connection_Reconnected;

            await connection.StartAsync();

            Console.WriteLine($"Connected {connection.ConnectionId}.");

            Faker<Customer> customerFaker = new CustomerFaker(new AddressFaker());

            var customers = customerFaker.GenerateForever();

            foreach (var customer in customers)
            {
                Console.WriteLine($"Sending... {customer.FullName}");

                await connection.SendAsync(nameof(ICustomerServer.SendCustomer), customer);

                Console.WriteLine("Sent.");

                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }

        private static Task Connection_Reconnected(string arg)
        {
            Console.WriteLine("Reconnected.");

            // TODO: send cached data

            return Task.CompletedTask;
        }

        private static Task Connection_Reconnecting(Exception arg)
        {
            Console.WriteLine("Reconnecting...");

            return Task.CompletedTask;

        }
    }
}
