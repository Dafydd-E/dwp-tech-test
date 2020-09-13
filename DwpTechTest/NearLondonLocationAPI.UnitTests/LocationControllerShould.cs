using AutoFixture.Xunit2;
using FluentAssertions;
using Location.Domain;
using Location.Domain.Users;
using Moq;
using NearLondonLocationAPI.Location;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace NearLondonLocationAPI.UnitTests
{
    public class LocationControllerShould
    {
        [Theory]
        [AutoData]
        public async Task ReturnInternalServerErrorIfCommandHandlerFails(
            Mock<ICommandService<UsersWithinDistanceCommand, UsersWithinDistanceCommandResult>> commandHandler,
            Exception exception,
            double distance)
        {
            commandHandler
                .Setup(x => x.Execute(It.IsAny<UsersWithinDistanceCommand>()))
                .ReturnsAsync(UsersWithinDistanceCommandResult.Failure(exception));

            var controller = new LocationController(commandHandler.Object);

            var result = await controller.GetUsersWithinDistanceOfCity("London", distance);

            result
                .Should()
                .BeEquivalentTo(
                    new
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                    });
        }
    }
}
