using System;

namespace Distances.Infrastructure.Impl.CTeleport.Caching
{
    public class CachingCTeleportClientOption
    {
        public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(1);
    }
}