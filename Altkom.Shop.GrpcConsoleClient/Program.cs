using Altkom.Shop.GrpcServer;
using Bogus;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Shop.GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // dotnet add package Grpc.Net.Client
            Console.WriteLine("Hello gRPC Client!");

            // dotnet add package Bogus
            var locations = new Faker<AddLocationRequest>()
                .RuleFor(p => p.Name, f => f.Vehicle.Model())
                .RuleFor(p => p.Latitude, f => (float) f.Address.Latitude())
                .RuleFor(p => p.Longitude, f => (float) f.Address.Longitude())
                .RuleFor(p => p.Speed, f => f.Random.Int(0, 140))
                .RuleFor(p => p.Direction, f => f.Random.Float())
                .GenerateForever();


            const string url = "https://localhost:5001";

            GrpcChannel channel = GrpcChannel.ForAddress(url);

            var client = new TrackingService.TrackingServiceClient(channel);

            var request = new SubscibeRequest { SpeedLimit = 140, Model = "A6" };

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            var streamingLocations = client.Subscibe(request, cancellationToken: cts.Token);

            try
            {
                await foreach (var location in streamingLocations.ResponseStream.ReadAllAsync(cts.Token))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{location.Name} {location.Speed}");
                    Console.ResetColor();                   
                }
            }
            catch(RpcException e) when (e.StatusCode == StatusCode.Cancelled )
            {
                Console.WriteLine("Stream cancelled.");
            }

            
            // Send locations
            //foreach (var request in locations)
            //{
            //    Console.WriteLine($"{request.Name} lat={request.Latitude} lng={request.Longitude} dir={request.Direction} speed={request.Speed}");

            //    var response = await client.AddLocationAsync(request);

            //    Console.WriteLine($"Confirmed {response.IsConfirmed}");

            //    await Task.Delay(TimeSpan.FromMilliseconds(10));
            //}

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();


        }
    }
}
