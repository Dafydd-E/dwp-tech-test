using Location.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeroKUApp.Data
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterHokuApi(this IServiceCollection service, IConfiguration configuration)
        {
            FlurlClientConfiguration.ConfigureClient(configuration);

            return service
                .AddTransient<IUserApi, UserApi>()
                .AddTransient<IUserRepository, UserRepository>();
        }
    }
}
