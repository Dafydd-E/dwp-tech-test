using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace NearLondonLocationAPI.IntegrationTests
{
    public class LocationControllerShould : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> applicationFactory;

        public LocationControllerShould(WebApplicationFactory<Startup> applicationFactory)
        {
            this.applicationFactory = applicationFactory;
        }

        [Fact]
        public async Task ReturnSuccessfulResponseWithCorrectParameters()
        {
            using(var client = this.applicationFactory.CreateClient())
            {
                var result = await client.GetAsync("/Location?city=London&withinDistance=50");

                result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task ReturnUnprocessableEntityIfLocationNotSpecified()
        {
            using (var client = this.applicationFactory.CreateClient())
            {
                var result = await client.GetAsync("/Location");

                result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.UnprocessableEntity);
            }
        }
    }
}
