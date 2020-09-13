using AutoFixture.Xunit2;
using FluentAssertions;
using Location.Domain;
using Location.Domain.Users;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HeroKUApp.Data.UnitTests
{
    public class UserRepositoryShould
    {
        [Theory]
        [AutoData]
        public async Task ReturnUsersMappedToDomainModel(
            UserRepositoryConfigurator configurator,
            UserDto[] users)
        {
            var repository = configurator
                .WithUsers(users)
                .Create();

            var result = await repository.GetUsersAsync();

            result
                .Should()
                .BeEquivalentTo(
                    new
                    {
                        IsSuccess = true,
                        Users = users.Select(x => new User(x.Id, x.FirstName, x.LastName, x.Email, x.IpAddress, new Coordinate(x.Latitude, x.Longitude))),
                    });
        }

        [Theory]
        [AutoData]
        public async Task ReturnUsersInCityMappedToDomainModel(
            UserRepositoryConfigurator configurator,
            UserDto[] users,
            string city)
        {
            var repository = configurator
                .WithUsers(users)
                .Create();

            var result = await repository.GetUsersInCityAsync(city);

            result
                .Should()
                .BeEquivalentTo(
                    new
                    {
                        IsSuccess = true,
                        Users = users.Select(x => new User(x.Id, x.FirstName, x.LastName, x.Email, x.IpAddress, new Coordinate(x.Latitude, x.Longitude))),
                    });
        }

        [Theory]
        [AutoData]
        public async Task ReturnFailedUserResultIfApiCallFails(
            UserRepositoryConfigurator configurator,
            Exception exception)
        {
            var repository = configurator
                .WithException(exception)
                .Create();

            var result = await repository.GetUsersAsync();

            result
                .Should()
                .BeEquivalentTo(
                    new
                    {
                        IsSuccess = false,
                        Exception = exception,
                    });
        }

        [Theory]
        [AutoData]
        public async Task ReturnFailedUserInCityResultIfApiCallFails(
            UserRepositoryConfigurator configurator,
            Exception exception,
            string city)
        {
            var repository = configurator
                .WithException(exception)
                .Create();

            var result = await repository.GetUsersInCityAsync(city);

            result
                .Should()
                .BeEquivalentTo(
                    new
                    {
                        IsSuccess = false,
                        Exception = exception,
                    });
        }
    }
}
