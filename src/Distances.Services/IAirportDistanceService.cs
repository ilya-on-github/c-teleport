using System.Threading;
using System.Threading.Tasks;

namespace Distances.Services
{
    public interface IAirportDistanceService
    {
        Task<Distance> GetDistance(AirportId one, AirportId two, CancellationToken ct = default);
    }
}