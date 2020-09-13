using Location.Domain.Users;
using System.Linq;
using System.Threading.Tasks;

namespace HeroKUApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserApi userApi;

        public UserRepository(IUserApi userApi)
        {
            this.userApi = userApi;
        }

        public async Task<GetUsersInCityResult> GetUsersInCityAsync(string city)
        {
            var result = await this.userApi.GetUsersInCityAsync(city);

            if (result.IsSuccess)
            {
                var mappedUsers = result
                    .Users
                    .Select(
                        user => new User(
                            user.Id,
                            user.FirstName,
                            user.LastName,
                            user.Email,
                            user.IpAddress,
                            new Coordinate(user.Latitude, user.Longitude)));

                return GetUsersInCityResult.Success(mappedUsers);
            }

            return GetUsersInCityResult.Failure(result.Exception);
        }

        public async Task<GetUserResult> GetUsersAsync()
        {
            var result = await this.userApi.GetUsersAsync();

            if (result.IsSuccess)
            {
                var mappedUsers = result
                    .Users
                    .Select(
                        user => new User(
                            user.Id,
                            user.FirstName,
                            user.LastName,
                            user.Email,
                            user.IpAddress,
                            new Coordinate(user.Latitude, user.Longitude)));

                return GetUserResult.Success(mappedUsers);
            }

            return GetUserResult.Failure(result.Exception);
        }
    }
}
