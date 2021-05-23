using System.Net;
using System.Threading.Tasks;
using Distances.Controllers;
using Distances.Infrastructure.Impl.CTeleport;
using Distances.Infrastructure.Impl.CTeleport.Models;
using Distances.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace Distances.Tests
{
    [TestFixture]
    public class DistanceControllerScenarioTests
    {
        private Mock<IRestClient> _mockRestClient;

        [SetUp]
        public void SetUp()
        {
            _mockRestClient = new Mock<IRestClient>();
        }

        [Test]
        public async Task Get_ReturnsCorrectResult()
        {
            new TestCTeleportRestClientBuilder(_mockRestClient)
                .With("AMS", new TestAirportInfoRestResponseBuilder()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithData(new AirportInfoDto {Location = new LocationDto {Lat = 52.309069, Lon = 4.763385}}))
                .With("SVO", new TestAirportInfoRestResponseBuilder()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithData(new AirportInfoDto {Location = new LocationDto {Lat = 55.966324, Lon = 37.416574}}))
                .Build();

            var controller = GetController();

            var result = await controller.Get(
                new GetDistanceApiRequest
                {
                    A = "AMS",
                    B = "SVO"
                });

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should()
                .BeEquivalentTo(new ApiDistance
                {
                    Miles = 1332.5442441166228,
                    Meters = 2144516.7538266457
                });
        }

        private DistanceController GetController()
        {
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
            hostBuilder.ConfigureServices(PatchServices);
            var host = hostBuilder.Build();

            var serviceProvider = host.Services;
            var scope = serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<DistanceController>();
        }

        private void PatchServices(IServiceCollection services)
        {
            services.AddSingleton(
                new CTeleportClient(_mockRestClient.Object));
        }
    }
}