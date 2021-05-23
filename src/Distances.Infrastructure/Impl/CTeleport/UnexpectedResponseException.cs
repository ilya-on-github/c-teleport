using System;
using Distances.Infrastructure.Impl.CTeleport.Models;
using RestSharp;

namespace Distances.Infrastructure.Impl.CTeleport
{
    public class UnexpectedResponseException : Exception
    {
        public IRestResponse Response { get; }

        public UnexpectedResponseException(IRestResponse<AirportInfoDto> response)
        {
            Response = response;
        }
    }
}