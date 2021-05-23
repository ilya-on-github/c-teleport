namespace Distances.Services
{
    public class AirportId
    {
        public string IataCode { get; }

        public AirportId(string iataCode)
        {
            IataCode = iataCode;
        }

        protected bool Equals(AirportId other)
        {
            return IataCode == other.IataCode;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AirportId) obj);
        }

        public override int GetHashCode()
        {
            return (IataCode != null ? IataCode.GetHashCode() : 0);
        }
    }
}