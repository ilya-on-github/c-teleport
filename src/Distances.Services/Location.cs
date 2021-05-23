using System;

namespace Distances.Services
{
    public class Location
    {
        public double LatDeg { get; }
        public double LonDeg { get; }

        public Location(double latDeg, double lonDeg)
        {
            if (latDeg < -90 || latDeg > 90)
                throw new ArgumentOutOfRangeException(nameof(lonDeg), "Latitude must be in range [-90, 90].");

            if (lonDeg < -180 || lonDeg > 180)
                throw new ArgumentOutOfRangeException(nameof(lonDeg), "Longitude must be in range [-180, 180].");

            LonDeg = lonDeg;
            LatDeg = latDeg;
        }

        protected bool Equals(Location other)
        {
            return LonDeg.Equals(other.LonDeg) && LatDeg.Equals(other.LatDeg);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Location) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LonDeg, LatDeg);
        }
    }
}