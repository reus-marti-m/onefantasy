using Microsoft.AspNetCore.Http;

namespace OneFantasy.Api.Domain.Exceptions
{
    public abstract class ForbiddenExceptions : OneFantasyException
    {
        public ForbiddenExceptions(string message) : 
            base("Forbidden", message, StatusCodes.Status403Forbidden) 
        { }
    }

    public class UnauthorizedAdminException : ForbiddenExceptions
    {
        public UnauthorizedAdminException() : 
            base("Only an administrator can create another administrator.") 
        { }
    }
}
