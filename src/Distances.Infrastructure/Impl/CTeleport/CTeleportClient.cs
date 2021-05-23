using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Distances.Infrastructure.Impl.CTeleport.Models;
using RestSharp;

namespace Distances.Infrastructure.Impl.CTeleport
{
    public class CTeleportClient : ICTeleportClient
    {
        private readonly IRestClient _client;

        public CTeleportClient(IRestClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <remarks>
        /// Example: https://places-dev.cteleport.com/airports/AMS
        /// Response:
        /// {
        ///  "country":"Netherlands",
        ///  "city_iata":"AMS",
        ///  "iata":"AMS",
        ///  "city":"Amsterdam",
        ///  "timezone_region_name":"Europe/Amsterdam",
        ///  "country_iata":"NL",
        ///  "rating":3,
        ///  "name":"Amsterdam",
        ///  "location":{
        ///      "lon":4.763385,
        ///      "lat":52.309069
        ///  },
        ///  "type":"airport",
        ///  "hubs":7
        /// }
        /// </remarks>
        /// <param name="iataCode">Airport IATA code.</param>
        /// <param name="ct">You know.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnexpectedResponseException"></exception>
        public async Task<AirportInfoDto> GetAirportInfo(string iataCode, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(iataCode))
                throw new ArgumentException("IATA code can't be empty.");

            var request = new RestRequest($"airports/{iataCode}", Method.GET);
            var response = await _client.ExecuteAsync<AirportInfoDto>(request, ct);

            // TODO: make more complex check based on Data values
            if (response.StatusCode != HttpStatusCode.OK || response.Data == null)
                throw new UnexpectedResponseException(response);

            return response.Data;
        }
    }
}