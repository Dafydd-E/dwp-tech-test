using Location.Domain;
using Moq;
using System;

namespace HeroKUApp.Data.UnitTests
{
    public class UserRepositoryConfigurator
    {
        private readonly Mock<IUserApi> userApi;

        public UserRepositoryConfigurator(Mock<IUserApi> userApi)
        {
            this.userApi = userApi ?? throw new ArgumentNullException(nameof(userApi));
        }

        public UserRepositoryConfigurator WithUsers(UserDto[] users)
        {
            this.userApi
                .Setup(x => x.GetUsersAsync())
                .ReturnsAsync(GetUserApiResult.Success(users));

            this.userApi
                .Setup(x => x.GetUsersInCityAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserInCityApiResult.Success(users));

            return this;
        }

        public UserRepositoryConfigurator WithException(Exception exception)
        {
            this.userApi
                .Setup(x => x.GetUsersAsync())
                .ReturnsAsync(GetUserApiResult.Failure(exception));

            this.userApi
                .Setup(x => x.GetUsersInCityAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserInCityApiResult.Failure(exception));

            return this;
        }

        public UserRepository Create()
        {
            return new UserRepository(this.userApi.Object);
        }
    }
}
