using System;
using System.Threading;
using System.Threading.Tasks;
using Distances.Infrastructure.Impl.CTeleport.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Distances.Infrastructure.Impl.CTeleport.Caching
{
    public class CachingCTeleportClient : ICTeleportClient
    {
        private readonly ICTeleportClient _inner;
        private readonly IMemoryCache _cache;
        private readonly IOptions<CachingCTeleportClientOption> _options;

        public CachingCTeleportClient(ICTeleportClient inner, IMemoryCache cache,
            IOptions<CachingCTeleportClientOption> options)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<AirportInfoDto> GetAirportInfo(string iataCode, CancellationToken ct = default)
        {
            var cacheKey = GetCacheKey(iataCode);
            if (_cache.TryGetValue<AirportInfoDto>(cacheKey, out var airportInfoCached))
                return airportInfoCached;

            var airportInfo = await _inner.GetAirportInfo(cacheKey, ct);
            _cache.Set(cacheKey, airportInfo, new MemoryCacheEntryOptions
            {
                SlidingExpiration = _options.Value.SlidingExpiration
            });

            return airportInfo;
        }

        private static string GetCacheKey(string iataCode) => iataCode;
    }
}