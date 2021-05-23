using System;
using System.Threading;
using System.Threading.Tasks;
using Distances.Models;
using Distances.Services;
using Microsoft.AspNetCore.Mvc;

namespace Distances.Controllers
{
    [ApiController]
    [Route("api/v1/distance")]
    public class DistanceController : ControllerBase
    {
        private readonly IAirportDistanceService _distanceService;

        public DistanceController(IAirportDistanceService distanceService)
        {
            _distanceService = distanceService ?? throw new ArgumentNullException(nameof(distanceService));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDistanceApiRequest r, CancellationToken ct = default)
        {
            var distance = await _distanceService.GetDistance(new AirportId(r.A), new AirportId(r.B), ct);

            return Ok(new ApiDistance
            {
                Miles = Math.Round(distance.Miles, 1, MidpointRounding.AwayFromZero),
            });
        }
    }
}