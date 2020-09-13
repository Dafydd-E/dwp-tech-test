using System;
using System.Collections.Generic;

namespace Location.Domain.Users
{
    public class GetUsersInCityResult
    {
        private GetUsersInCityResult(IEnumerable<User> users)
        {
            this.Users = users ?? throw new ArgumentNullException(nameof(users));
            this.IsSuccess = true;
        }

        private GetUsersInCityResult(Exception exception)
        {
            this.Exception = exception;
            this.IsSuccess = false;
        }

        public IEnumerable<User> Users { get; }

        public bool IsSuccess { get; }

        public Exception Exception { get; }

        public static GetUsersInCityResult Success(IEnumerable<User> users)
        {
            return new GetUsersInCityResult(users);
        }

        public static GetUsersInCityResult Failure(Exception exception)
        {
            return new GetUsersInCityResult(exception);
        }
    }
}