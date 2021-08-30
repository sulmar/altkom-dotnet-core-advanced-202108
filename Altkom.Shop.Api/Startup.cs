using Altkom.Shop.Api.Middlewares;
using Altkom.Shop.Fakers;
using Altkom.Shop.FakeServices;
using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AuthorizationMiddleware>();

            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<Faker<Address>, AddressFaker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Logger Middleware
            //app.Use(async (context, next) =>
            //{
            //    Trace.WriteLine($"{context.Request.Method} {context.Request.Path}");

            //    await next();

            //    Trace.WriteLine($"{context.Response.StatusCode}");

            //});



            // Authorization Middleware
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.ContainsKey("Authorization"))
            //    {
            //        await next();
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    }

            //});

            // app.UseMiddleware<LoggerMiddleware>();
            // app.UseMiddleware<AuthorizationMiddleware>();

            app.UseLogger();
            app.UseMyAuthorization();

            // app.Map("api/customers", )

            // Login Middleware
            // app.Run(context => context.Response.WriteAsync("Hello World!" ));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async context => await context.Response.WriteAsync("Hello World!"));

                // api/customers
                endpoints.MapGet("/api/customers", async context =>
                {
                    ICustomerService customerService = context.RequestServices.GetRequiredService<ICustomerService>();

                    var customers = customerService.Get();

                    await context.Response.WriteAsJsonAsync(customers);
                });

               

               // api/customers/{id}
               endpoints.MapGet("/api/customers/{id:int}", async context =>
                {
                    int id = Convert.ToInt32( context.Request.RouteValues["id"]);

                    ICustomerService customerService = context.RequestServices.GetRequiredService<ICustomerService>();

                    var customer = customerService.Get(id);

                    await context.Response.WriteAsJsonAsync(customer);

                    // await context.Response.WriteAsync($"Witaj customer {id}");
                });

                // NET 6.0
                // endpoints.MapGet("/api/customers/{id:int}", (id,  ICustomerService customerService) => customerService.Get(id));
            });

        }
    }
}
