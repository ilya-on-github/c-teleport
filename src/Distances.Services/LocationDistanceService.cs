using System;

namespace Distances.Services
{
    public class LocationDistanceService : ILocationDistanceService
    {
        public Distance GetDistance(Location one, Location two)
        {
            // didn't check if it's a right algorithm
            // found it on the internet

            var lat1 = one.LatDeg;
            var lat2 = two.LatDeg;
            var lon1 = one.LonDeg;
            var lon2 = two.LonDeg;

            const double r = 6371e3; // metres
            var φ1 = lat1 * Math.PI / 180; // φ, λ in radians
            var φ2 = lat2 * Math.PI / 180;
            var δφ = (lat2 - lat1) * Math.PI / 180;
            var δλ = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(δφ / 2) * Math.Sin(δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) * Math.Sin(δλ / 2) * Math.Sin(δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = r * c; // in metres

            return Distance.FromMeters(d);
        }
    }
}