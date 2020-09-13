using System.Threading.Tasks;

namespace HeroKUApp.Data
{
    public interface IUserApi
    {
        public Task<GetUserApiResult> GetUsersAsync();

        public Task<GetUserInCityApiResult> GetUsersInCityAsync(string city);
    }
}