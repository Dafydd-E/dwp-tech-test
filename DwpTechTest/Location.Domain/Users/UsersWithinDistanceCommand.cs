using System;

namespace Location.Domain.Users
{
    public class UsersWithinDistanceCommand
    {
        public UsersWithinDistanceCommand(
            string city,
            double distance = 0,
            Coordinate coordinate = null)
        {
            this.City = !string.IsNullOrWhiteSpace(city) ? city : throw new ArgumentNullException(nameof(city));

            if (distance > 0 && coordinate == null)
            {
                throw new ArgumentNullException(nameof(coordinate), "Coordinate must be specified if distance from city is not zero");
            }

            this.Distance = distance >= 0 ? distance : throw new ArgumentOutOfRangeException(nameof(distance));
            this.Coordinate = coordinate;
        }

        public string City { get; }

        public double Distance { get; }

        public Coordinate Coordinate { get; }
    }
}