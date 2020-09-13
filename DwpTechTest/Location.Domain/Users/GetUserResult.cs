using System;
using System.Collections.Generic;

namespace Location.Domain.Users
{
    public class GetUserResult
    {
        private GetUserResult(Exception exception)
        {
            this.Exception = exception;
            this.IsSuccess = false;
        }

        private GetUserResult(IEnumerable<User> users)
        {
            this.Users = users ?? throw new ArgumentNullException(nameof(users));
            this.IsSuccess = true;
        }

        public IEnumerable<User> Users { get; }

        public bool IsSuccess { get; }

        public Exception Exception { get; }

        public static GetUserResult Success(IEnumerable<User> users)
        {
            return new GetUserResult(users);
        }

        public static GetUserResult Failure(Exception exception)
        {
            return new GetUserResult(exception);
        }
    }
}