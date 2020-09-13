using Location.Domain.Users;
using Moq;
using System;
using System.Collections.Generic;

namespace Location.Domain.UnitTests.Users
{
    public class UserWithinDistanceCommandHandlerConfigurator
    {
        private readonly Mock<IUserRepository> userRepository;

        private HarversineDistanceAlgorithm distanceAlgorithm;

        public UserWithinDistanceCommandHandlerConfigurator(
            Mock<IUserRepository> userRepository,
            HarversineDistanceAlgorithm distanceAlgorithm)
        {
            this.userRepository = userRepository;
            this.distanceAlgorithm = distanceAlgorithm;
        }

        public UserWithinDistanceCommandHandlerConfigurator WithUsers(IEnumerable<User> users)
        {
            this.userRepository
                .Setup(x => x.GetUsersAsync())
                .ReturnsAsync(GetUserResult.Success(users));

            return this;
        }

        public UserWithinDistanceCommandHandlerConfigurator WithUsersResultException(Exception exception)
        {
            this.userRepository
                .Setup(x => x.GetUsersAsync())
                .ReturnsAsync(GetUserResult.Failure(exception));

            return this;
        }

        public UserWithinDistanceCommandHandlerConfigurator WithUsersInCity(User[] users)
        {
            this.userRepository
                .Setup(x => x.GetUsersInCityAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUsersInCityResult.Success(users));

            return this;
        }

        public UserWithinDistanceCommandHandlerConfigurator WithUsersInCityResultException(Exception exception)
        {
            this.userRepository
                .Setup(x => x.GetUsersInCityAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUsersInCityResult.Failure(exception));

            return this;
        }

        public UsersWithinDistanceCommandHandler Create()
        {
            return new UsersWithinDistanceCommandHandler(
                this.userRepository.Object,
                this.distanceAlgorithm);
        }
    }
}
