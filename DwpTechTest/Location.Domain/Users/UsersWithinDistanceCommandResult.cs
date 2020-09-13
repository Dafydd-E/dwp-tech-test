using System;
using System.Collections.Generic;

namespace Location.Domain.Users
{
    public class UsersWithinDistanceCommandResult
    {
        public UsersWithinDistanceCommandResult(List<User> users)
        {
            this.Users = users;
            this.IsSuccess = true;
        }

        public UsersWithinDistanceCommandResult(Exception exception)
        {
            this.Exception = exception;
            this.IsSuccess = false;
        }

        public bool IsSuccess { get; }

        public Exception Exception { get; }

        public List<User> Users { get; }

        public static UsersWithinDistanceCommandResult Failure(Exception exception)
        {
            return new UsersWithinDistanceCommandResult(exception);
        }

        public static UsersWithinDistanceCommandResult Success(List<User> users)
        {
            return new UsersWithinDistanceCommandResult(users);
        }
    }
}