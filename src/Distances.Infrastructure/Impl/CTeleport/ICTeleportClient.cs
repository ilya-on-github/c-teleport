using System.Threading;
using System.Threading.Tasks;
using Distances.Infrastructure.Impl.CTeleport.Models;

namespace Distances.Infrastructure.Impl.CTeleport
{
    public interface ICTeleportClient
    {
        Task<AirportInfoDto> GetAirportInfo(string iataCode, CancellationToken ct = default);
    }
}