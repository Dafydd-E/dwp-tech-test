using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace HeroKUApp.Data
{
    public class UserApi : IUserApi
    {
        private readonly string baseUri;

        public UserApi(IConfiguration configuration)
        {
            this.baseUri = configuration[FlurlClientConfiguration.ConfigurationSettingName];
        }

        public async Task<GetUserApiResult> GetUsersAsync()
        {
            try
            {
                var result = await $"{this.baseUri}/users".GetJsonAsync<UserDto[]>();

                return GetUserApiResult.Success(result);
            }
            catch (Exception e)
            {
                return GetUserApiResult.Failure(e);
            }
        }

        public async Task<GetUserInCityApiResult> GetUsersInCityAsync(string city)
        {
            try
            {
                var result = await $"{this.baseUri}/city/{city}/users".GetJsonAsync<UserDto[]>();

                return GetUserInCityApiResult.Success(result);
            }
            catch (Exception e)
            {
                return GetUserInCityApiResult.Failure(e);
            }
        }
    }
}
