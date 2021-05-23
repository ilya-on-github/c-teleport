using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Distances.Infrastructure.Impl.CTeleport.Models;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace Distances.Tests
{
    [TestFixture]
    public class TestRestClientBuilderTests
    {
        [Test]
        public async Task ExecuteAsync_ReturnsCorrectValue()
        {
            // arrange
            var iataCode = "AMS";
            var statusCode = HttpStatusCode.OK;
            var data = new AirportInfoDto {Location = new LocationDto {Lat = 52.309069, Lon = 4.763385}};
            var request = new RestRequest($"airports/{iataCode}");
            var mock = new TestCTeleportRestClientBuilder()
                .With(iataCode, new TestAirportInfoRestResponseBuilder()
                    .WithStatusCode(statusCode)
                    .WithData(data))
                .Build();

            // act
            var response = await mock.Object.ExecuteAsync<AirportInfoDto>(request, CancellationToken.None);

            // assert
            response.Data.Should().Be(data);
            response.StatusCode.Should().Be(statusCode);
        }
    }
}