using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HeroKUApp.Data.IntegrationTests
{
    public class UserApiShould
    {
        private readonly IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string>()
                {
                    {
                        FlurlClientConfiguration.ConfigurationSettingName,
                        "https://bpdts-test-app.herokuapp.com"
                    }
                })
            .Build();

        [Fact]
        public async Task SucceedWhenCallingUsersEndpoint()
        {
            var userApi = new UserApi(this.configuration);

            var result = await userApi.GetUsersAsync();

            result.Should().BeEquivalentTo(
                new
                {
                    IsSuccess = true,
                });
        }

        [Theory]
        [InlineData("London")]
        public async Task SucceedWhenCallingUsersInCityEndpoint(string city)
        {
            var userApi = new UserApi(this.configuration);

            var result = await userApi.GetUsersInCityAsync(city);

            result.Should().BeEquivalentTo(
                new
                {
                    IsSuccess = true,
                });
        }

        [Theory]
        [AutoData]
        public async Task FailWhenCallingUsersInCityEndpointWithIncorrectConfiguration(string city)
        {
            var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection()
            .Build();

            var userApi = new UserApi(configuration);

            var result = await userApi.GetUsersInCityAsync(city);

            result.Should().BeEquivalentTo(
                new
                {
                    IsSuccess = false,
                });
        }

        [Fact]
        public async Task FailWhenCallingUsersEndpointWithIncorrectConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            var userApi = new UserApi(configuration);

            var result = await userApi.GetUsersAsync();

            result.Should().BeEquivalentTo(
                new
                {
                    IsSuccess = false,
                });
        }
    }
}
