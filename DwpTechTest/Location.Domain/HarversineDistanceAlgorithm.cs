using Location.Domain.Users;
using System;

namespace Location.Domain
{
    public class HarversineDistanceAlgorithm : IDistanceAlgorithm
    {
        private const double Radius = 6371e3;

        public double CalculateDistance(Coordinate firstCoordinate, Coordinate secondCoordinate)
        {
            var firstLatitude = this.ConvertToRadians(firstCoordinate.Latitude);
            var secongLatitude = this.ConvertToRadians(secondCoordinate.Latitude);

            var latitudeDelta = this.ConvertToRadians(secondCoordinate.Latitude - firstCoordinate.Latitude);
            var longitudeDelta = this.ConvertToRadians(secondCoordinate.Longitude - firstCoordinate.Longitude);

            var a = Math.Pow(Math.Sin(latitudeDelta / 2), 2) +
                Math.Cos(firstLatitude) * Math.Cos(secongLatitude) * Math.Pow(Math.Sin(longitudeDelta / 2), 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // distance is returned in metres
            return Radius * c;
        }

        private double ConvertToRadians(double angle) => angle * (Math.PI / 180);
    }
}
