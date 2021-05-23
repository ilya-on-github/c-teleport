using System;
using System.Threading;
using System.Threading.Tasks;
using Distances.Infrastructure.Impl.CTeleport;
using Distances.Services;

namespace Distances.Infrastructure.Impl
{
    public class AirportDistanceService : IAirportDistanceService
    {
        private readonly ICTeleportClient _cTeleport;
        private readonly ILocationDistanceService _locationDistance;

        public AirportDistanceService(ICTeleportClient cTeleport, ILocationDistanceService locationDistance)
        {
            _cTeleport = cTeleport ?? throw new ArgumentNullException(nameof(cTeleport));
            _locationDistance = locationDistance ?? throw new ArgumentNullException(nameof(locationDistance));
        }

        public async Task<Distance> GetDistance(AirportId one, AirportId two, CancellationToken ct = default)
        {
            if (one == null)
                throw new ArgumentNullException(nameof(one));
            if (two == null)
                throw new ArgumentNullException(nameof(two));

            var getOne = _cTeleport.GetAirportInfo(one.IataCode, ct);
            var getTwo = _cTeleport.GetAirportInfo(two.IataCode, ct);

            await Task.WhenAll(getOne, getTwo);

            var lOne = new Location(getOne.Result.Location.Lat, getOne.Result.Location.Lon);
            var lTwo = new Location(getTwo.Result.Location.Lat, getTwo.Result.Location.Lon);

            return _locationDistance.GetDistance(lOne, lTwo);
        }
    }
}