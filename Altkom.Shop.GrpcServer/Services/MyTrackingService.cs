using Bogus;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.GrpcServer.Services
{
    public class MyTrackingService : Altkom.Shop.GrpcServer.TrackingService.TrackingServiceBase
    {
        private readonly ILogger<MyTrackingService> logger;

        public MyTrackingService(ILogger<MyTrackingService> logger)
        {
            this.logger = logger;
        }

        public override Task<AddLocationResponse> AddLocation(AddLocationRequest request, ServerCallContext context)
        {
            logger.LogInformation($"{request.Name} lat={request.Latitude} lng={request.Longitude} dir={request.Direction} speed={request.Speed}");

            var response = new AddLocationResponse { IsConfirmed = true };

            return Task.FromResult(response);
        }

        public override async Task Subscibe(SubscibeRequest request, IServerStreamWriter<SubscibeResponse> responseStream, ServerCallContext context)
        {
            var locations = new Faker<SubscibeResponse>()
               .RuleFor(p => p.Name, f => f.Vehicle.Model())
               .RuleFor(p => p.Latitude, f => (float)f.Address.Latitude())
               .RuleFor(p => p.Longitude, f => (float)f.Address.Longitude())
               .RuleFor(p => p.Speed, f => f.Random.Int(0, 180))
               .RuleFor(p => p.Direction, f => f.Random.Float())
               .GenerateForever();

            locations = locations                
                .Where(location => location.Speed > request.SpeedLimit);

            foreach (var location in locations)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await responseStream.WriteAsync(location);

                logger.LogInformation($"{location.Name} lat={location.Latitude} lng={location.Longitude} dir={location.Direction} speed={location.Speed}");


                await Task.Delay(TimeSpan.FromSeconds(1));

            }            
        }
    }
}
