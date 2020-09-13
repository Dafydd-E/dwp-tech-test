using System;

namespace Location.Domain.Users
{
    public class User
    {
        public User(
            int id,
            string firstName,
            string lastName,
            string email,
            string ipAddress,
            Coordinate coordinate)
        {
            this.Id = id;
            this.FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
            this.LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
            this.Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
            this.IpAddress = !string.IsNullOrWhiteSpace(ipAddress) ? ipAddress : throw new ArgumentNullException(ipAddress);
            this.Coordinate = coordinate ?? throw new ArgumentNullException(nameof(coordinate));
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string IpAddress { get; }

        public Coordinate Coordinate { get; }
    }
}
