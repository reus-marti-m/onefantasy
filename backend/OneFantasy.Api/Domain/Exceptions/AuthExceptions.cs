using System;

namespace OneFantasy.Api.Domain.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(string email) : base($"The email '{email}' is already registered.") { }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid credentials.") { }
    }

    public class UnauthorizedAdminException : Exception
    {
        public UnauthorizedAdminException() : base("Only an administrator can create another administrator.") { }
    }
}
