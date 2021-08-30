using Altkom.Shop.Fakers;
using Altkom.Shop.FakeServices;
using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Bogus;
using Microsoft.Extensions.DependencyInjection;

namespace Altkom.Shop.WebApi
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFakeServices(this IServiceCollection services)
        {
            services.AddFakeCustomerServices();
            services.AddFakeOrderServices();

            return services;
        }

        public static IServiceCollection AddFakeCustomerServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();

            return services;
        }

        public static IServiceCollection AddFakeOrderServices(this IServiceCollection services)
        {
            return services;
        }


    }
}
