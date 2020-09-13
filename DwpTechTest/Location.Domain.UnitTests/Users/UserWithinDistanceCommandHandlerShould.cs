using AutoFixture.Xunit2;
using FluentAssertions;
using Location.Domain.Users;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Location.Domain.UnitTests.Users
{
    public class UserWithinDistanceCommandHandlerShould
    {
        [Theory]
        [AutoData]
        public async Task ReturnFailedResultIfCouldNotRetrieveUsersInCity(
            UserWithinDistanceCommandHandlerConfigurator configurator,
            UsersWithinDistanceCommand command,
            Exception exception)
        {
            var commandHandler = configurator
                .WithUsersInCityResultException(exception)
                .Create();

            var result = await commandHandler.Execute(command);

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
        public async Task ReturnSuccessResultIfNoDistanceSpecified(
            UserWithinDistanceCommandHandlerConfigurator configurator,
            User[] users,
            string city)
        {
            var commandHandler = configurator
                .WithUsersInCity(users)
                .Create();

            var result = await commandHandler.Execute(new UsersWithinDistanceCommand(city));

            result
                .Should()
                .BeEquivalentTo(
                    new
                    {
                        IsSuccess = true,
                        Users = users.ToList(),
                    });
        }

        [Theory]
        [AutoData]
        public async Task ReturnFailedResultIfCouldNotRetrieveUsers(
            UserWithinDistanceCommandHandlerConfigurator configurator,
            UsersWithinDistanceCommand command,
            Exception exception,
            User[] users)
        {
            var commandHandler = configurator
                .WithUsersInCity(users)
                .WithUsersResultException(exception)
                .Create();

            var result = await commandHandler.Execute(command);

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
        public async Task ReturnSuccessfulResultWithAdditionalUsersWithinDefinedDistance(
            UserWithinDistanceCommandHandlerConfigurator configurator,
            UsersWithinDistanceCommand command,
            User[] usersInCity,
            UserBuilder userBuilder)
        {
            var additionalUsers = userBuilder
                .WithCoordinate(command.Coordinate)
                .BuildMany(5);

            var commandHandler = configurator
                .WithUsersInCity(usersInCity)
                .WithUsers(additionalUsers)
                .Create();

            var result = await commandHandler.Execute(command);

            result
                .Should()
                .BeEquivalentTo(
                    new
                    {
                        IsSuccess = true,
                        Users = usersInCity.Union(additionalUsers).ToList(),
                    });
        }
    }
}
