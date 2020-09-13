using Location.Domain.Users;

namespace Location.Domain
{
    public interface IDistanceAlgorithm
    {
        public double CalculateDistance(Coordinate firstCoordinate, Coordinate secondCoordinate);
    }
}
