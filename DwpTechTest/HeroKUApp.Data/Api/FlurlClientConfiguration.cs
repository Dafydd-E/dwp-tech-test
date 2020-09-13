using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace HeroKUApp.Data
{
    public class FlurlClientConfiguration
    {
        public const string ConfigurationSettingName = "HeroKUAppBaseUri";

        public static void ConfigureClient(IConfiguration configuration)
        {
            FlurlHttp.ConfigureClient(configuration[ConfigurationSettingName], (client) => 
            {
                client.WithHeaders(new 
                {
                    Accept = "application/json",
                });
            });
        }
    }
}
