using System;
using System.Threading;
using System.Threading.Tasks;
using Distances.Services;

namespace Distances.Infrastructure.CTeleport
{
    public class DistanceService : IDistanceService
    {
        public Task<Distance> GetDistance(AirportId one, AirportId two, CancellationToken ct = default)
        {
            // TODO: implement
            return Task.FromResult(new Distance(new Random().NextDouble()));
        }
    }
}