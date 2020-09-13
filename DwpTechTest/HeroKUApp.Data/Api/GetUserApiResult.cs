using System;
using System.Net.Http.Headers;

namespace HeroKUApp.Data
{
    public class GetUserApiResult
    {
        private GetUserApiResult(Exception exception)
        {
            this.IsSuccess = false;
            this.Exception = exception;
        }

        private GetUserApiResult(UserDto[] users)
        {
            this.Users = users ?? throw new ArgumentNullException(nameof(users));
            this.IsSuccess = true;
        }

        public bool IsSuccess { get; }

        public UserDto[] Users { get; }

        public Exception Exception { get; }

        public static GetUserApiResult Failure(Exception exception)
        {
            return new GetUserApiResult(exception);
        }

        public static GetUserApiResult Success(UserDto[] users)
        {
            return new GetUserApiResult(users);
        }
    }
}