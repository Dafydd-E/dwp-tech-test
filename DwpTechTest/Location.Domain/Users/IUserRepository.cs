using System.Threading.Tasks;

namespace Location.Domain.Users
{
    public interface IUserRepository
    {
        public Task<GetUsersInCityResult> GetUsersInCityAsync(string city);

        public Task<GetUserResult> GetUsersAsync();
    }
}
