using System;

namespace Distances.Infrastructure.Impl.Caching
{
    public class CachingAirportDistanceServiceOptions
    {
        public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(1);
    }
}