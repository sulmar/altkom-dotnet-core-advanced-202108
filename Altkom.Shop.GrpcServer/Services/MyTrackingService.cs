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
    }
}
