using System.Net;
using AutoFixture;
using Distances.Infrastructure.Impl.CTeleport.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Distances.Tests
{
    [TestFixture]
    public class TestRestResponseBuilderTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Data_ReturnsCorrectValue()
        {
            // arrange
            var value = _fixture.Create<AirportInfoDto>();

            // act
            var mock = new TestRestResponseBuilder<AirportInfoDto>()
                .WithData(value)
                .Build();

            // assert
            mock.Object.Data.Should().Be(value);
        }

        [Test]
        public void StatusCode_ReturnsCorrectValue()
        {
            // arrange
            var value = _fixture.Create<HttpStatusCode>();

            // act
            var mock = new TestRestResponseBuilder<AirportInfoDto>()
                .WithStatusCode(value)
                .Build();

            // assert
            mock.Object.StatusCode.Should().Be(value);
        }
    }
}