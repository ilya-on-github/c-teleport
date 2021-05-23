using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Distances.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Distances.Infrastructure.Impl.Caching
{
    public class CachingAirportDistanceService : IAirportDistanceService
    {
        private readonly IAirportDistanceService _inner;
        private readonly IMemoryCache _cache;
        private readonly IOptions<CachingAirportDistanceServiceOptions> _options;

        public CachingAirportDistanceService(IAirportDistanceService inner, IMemoryCache cache,
            IOptions<CachingAirportDistanceServiceOptions> options)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<Distance> GetDistance(AirportId one, AirportId two, CancellationToken ct = default)
        {
            var cacheKey = GetCacheKey(one, two);
            if (_cache.TryGetValue<Distance>(cacheKey, out var distanceCached))
                return distanceCached;

            var distance = await _inner.GetDistance(one, two, ct);
            _cache.Set(cacheKey, distance, new MemoryCacheEntryOptions
            {
                SlidingExpiration = _options.Value.SlidingExpiration
            });

            return distance;
        }

        private string GetCacheKey(AirportId one, AirportId two)
        {
            // AB distance = BA distance, so
            var ordered = new[] {one.IataCode, two.IataCode}
                .OrderBy(x => x);

            return string.Join("-", ordered);
        }
    }
}