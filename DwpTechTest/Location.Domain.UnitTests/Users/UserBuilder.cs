using Location.Domain.Users;
using System.Linq;

namespace Location.Domain.UnitTests.Users
{
    public class UserBuilder
    {
        private readonly int id;
        private readonly string firstName;
        private readonly string lastName;
        private readonly string email;
        private readonly string ipAddress;

        private Coordinate coordinate;

        public UserBuilder(
            int id,
            string firstName,
            string lastName,
            string email,
            string ipAddress,
            Coordinate coordinate)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.ipAddress = ipAddress;
            this.coordinate = coordinate;
        }

        public UserBuilder WithCoordinate(Coordinate coordinate)
        {
            this.coordinate = coordinate;
            return this;
        }

        public User Create()
        {
            return new User(
                this.id,
                this.firstName,
                this.lastName,
                this.email,
                this.ipAddress,
                this.coordinate);
        }

        public User[] BuildMany(int count)
        {
            return Enumerable.Repeat(this.Create(), count).ToArray();
        }
    }
}
