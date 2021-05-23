using System.Threading;
using System.Threading.Tasks;

namespace Distances.Services
{
    public interface IDistanceService
    {
        Task<Distance> GetDistance(AirportId one, AirportId two, CancellationToken ct = default);
    }
}