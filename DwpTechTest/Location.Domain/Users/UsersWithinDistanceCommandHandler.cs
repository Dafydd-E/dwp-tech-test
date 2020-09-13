using System.Linq;
using System.Threading.Tasks;

namespace Location.Domain.Users
{
    public class UsersWithinDistanceCommandHandler : ICommandService<UsersWithinDistanceCommand, UsersWithinDistanceCommandResult>
    {
        private const double MetreInMile = 1609;

        private readonly IUserRepository userRepository;
        private readonly IDistanceAlgorithm distanceAlgorithm;

        public UsersWithinDistanceCommandHandler(
            IUserRepository userRepository,
            IDistanceAlgorithm distanceAlgorithm)
        {
            this.userRepository = userRepository;
            this.distanceAlgorithm = distanceAlgorithm;
        }

        public async Task<UsersWithinDistanceCommandResult> Execute(UsersWithinDistanceCommand command)
        {
            var usersInCityResult = await this.userRepository.GetUsersInCityAsync(command.City);
            if (!usersInCityResult.IsSuccess)
            {
                return UsersWithinDistanceCommandResult.Failure(usersInCityResult.Exception);
            }

            // convert to dictionary to prevent duplication of users
            var users = usersInCityResult.Users.ToDictionary(x => x.Id);
            // if no distance is specified, short circuit and return users marked in city
            if (command.Distance == 0)
            {
                return UsersWithinDistanceCommandResult.Success(usersInCityResult.Users.ToList());
            }

            var usersResult = await this.userRepository.GetUsersAsync();
            if (!usersResult.IsSuccess)
            {
                return UsersWithinDistanceCommandResult.Failure(usersResult.Exception);
            }

            foreach (var user in usersResult.Users)
            {
                var distanceInMetres = this.distanceAlgorithm.CalculateDistance(command.Coordinate, user.Coordinate);
                var distanceInMiles = distanceInMetres / MetreInMile; // approximation
                if (distanceInMiles <= command.Distance && !users.ContainsKey(user.Id))
                {
                    users.Add(user.Id, user);
                }
            }

            return UsersWithinDistanceCommandResult.Success(users.Values.ToList());
        }
    }
}
