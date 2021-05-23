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
        private readonly IDistanceService _distanceService;

        public DistanceController(IDistanceService distanceService)
        {
            _distanceService = distanceService ?? throw new ArgumentNullException(nameof(distanceService));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDistanceApiRequest r, CancellationToken ct)
        {
            var distance = await _distanceService.GetDistance(new AirportId(r.A), new AirportId(r.B), ct);

            return Ok(new ApiDistance
            {
                Miles = distance.Miles
            });
        }
    }
}