using Newtonsoft.Json;

namespace HeroKUApp.Data
{
    public class UserDto
    {
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
