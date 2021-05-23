using Distances.Filters;
using Distances.Infrastructure.Impl;
using Distances.Infrastructure.Impl.Caching;
using Distances.Infrastructure.Impl.CTeleport;
using Distances.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Distances
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore(options => { options.Filters.Add(new HandleExceptionFilter()); })
                .AddNewtonsoftJson()
                .AddControllersAsServices();

            services.AddSingleton<ILocationDistanceService, LocationDistanceService>();

            services.AddSingleton<AirportDistanceService>();
            services.AddSingleton<ICTeleportClient>(
                new CTeleportClient(
                    new RestClient("https://places-dev.cteleport.com")));

            services.AddMemoryCache();
            services.AddSingleton<IAirportDistanceService>(provider => new CachingAirportDistanceService(
                provider.GetRequiredService<AirportDistanceService>(), provider.GetRequiredService<IMemoryCache>(),
                provider.GetRequiredService<IOptions<CachingAirportDistanceServiceOptions>>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}