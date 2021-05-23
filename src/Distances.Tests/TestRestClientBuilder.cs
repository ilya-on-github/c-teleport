using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Distances.Infrastructure.Impl.CTeleport.Models;
using Moq;
using RestSharp;

namespace Distances.Tests
{
    public class TestCTeleportRestClientBuilder
    {
        private readonly Mock<IRestClient> _mock;

        private readonly Dictionary<string, TestRestResponseBuilder<AirportInfoDto>> _buildersByIataCode =
            new Dictionary<string, TestRestResponseBuilder<AirportInfoDto>>();

        public TestCTeleportRestClientBuilder(Mock<IRestClient> mock = null)
        {
            _mock = mock ?? new Mock<IRestClient>();
        }

        public TestCTeleportRestClientBuilder With(string iataCode,
            TestRestResponseBuilder<AirportInfoDto> responseBuilder)
        {
            _buildersByIataCode.Add(iataCode, responseBuilder);
            return this;
        }

        public Mock<IRestClient> Build()
        {
            foreach (var (iataCode, builder) in _buildersByIataCode)
            {
                var responseMock = builder.Build();

                _mock.Setup(x =>
                        x.ExecuteAsync<AirportInfoDto>(
                            It.Is<IRestRequest>(r => r.Resource.EndsWith($"/{iataCode}")),
                            It.IsAny<CancellationToken>()))
                    .ReturnsAsync(responseMock.Object);
            }

            return _mock;
        }
    }
}