namespace Location.Domain.Users
{
    public class Coordinate
    {
        public Coordinate(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude { get; }

        public double Longitude { get; }
    }
}
