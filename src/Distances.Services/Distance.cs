using System;

namespace Distances.Services
{
    public class Distance
    {
        private const double MileToMeterRatio = 1609.34;

        public static Distance FromMeters(double meters)
        {
            var miles = meters / MileToMeterRatio;
            return new Distance(miles);
        }

        public double Miles { get; }

        public Distance(double miles)
        {
            if (miles < 0)
                throw new ArgumentOutOfRangeException(nameof(miles), "Distance can't be negative.");

            Miles = miles;
        }

        protected bool Equals(Distance other)
        {
            return Miles.Equals(other.Miles);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Distance) obj);
        }

        public override int GetHashCode()
        {
            return Miles.GetHashCode();
        }
    }
}