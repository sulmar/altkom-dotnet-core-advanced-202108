using Altkom.Shop.Fakers;
using Altkom.Shop.FakeServices;
using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Altkom.Shop.WebApi.HealtChecks;
using Bogus;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.UI.Core;

namespace Altkom.Shop.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFakeServices();

            // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Altkom.Shop.WebApi", Version = "v1" });
            });

            services.AddHealthChecks()
                .AddCheck<RandomHealtCheck>("Random");

            // dotnet add package AspNetCore.HealthChecks.UI
            // dotnet add package AspNetCore.HealthChecks.UI.InMemory.Storage
            services
                .AddHealthChecksUI()
                .AddInMemoryStorage();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Altkom.Shop.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // dotnet add package AspNetCore.HealthChecks.UI.Client
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });


                // healthchecks-ui
                endpoints.MapHealthChecksUI();

                //endpoints.MapHealthChecksUI(options => {
                //    options.UIPath = "/health-ui";
                //    options.ApiPath = "/health";

                //}
                //);

            });

        }
    }
}
