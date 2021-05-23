namespace Distances.Services
{
    public interface ILocationDistanceService
    {
        Distance GetDistance(Location one, Location two);
    }
}