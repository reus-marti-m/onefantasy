using Microsoft.AspNetCore.Http;

namespace OneFantasy.Api.Domain.Exceptions
{
    public abstract class UnauthorizedExceptions : OneFantasyException
    {
        public UnauthorizedExceptions(string message) : 
            base("Unauthorized", message, StatusCodes.Status401Unauthorized) 
        { }
    }

    public class InvalidCredentialsException : UnauthorizedExceptions
    {
        public InvalidCredentialsException() : 
            base("Invalid credentials.") 
        { }
    }
}
