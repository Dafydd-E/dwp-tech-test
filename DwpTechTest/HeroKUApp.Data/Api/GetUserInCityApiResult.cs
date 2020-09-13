using System;

namespace HeroKUApp.Data
{
    public class GetUserInCityApiResult
    {
        private GetUserInCityApiResult(Exception exception)
        {
            this.Exception = exception;
            this.IsSuccess = false;
        }

        private GetUserInCityApiResult(UserDto[] users)
        {
            this.IsSuccess = true;
            this.Users = users;
        }

        public bool IsSuccess { get; }

        public Exception Exception { get; }

        public UserDto[] Users { get; }

        public static GetUserInCityApiResult Success(UserDto[] users)
        {
            return new GetUserInCityApiResult(users);
        }

        public static GetUserInCityApiResult Failure(Exception exception)
        {
            return new GetUserInCityApiResult(exception);
        }
    }
}