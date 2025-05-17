using Microsoft.AspNetCore.Http;

namespace OneFantasy.Api.Domain.Exceptions
{
    public abstract class NotFoundExceptions : OneFantasyException
    {
        public NotFoundExceptions(string message) : 
            base("Not Found", message, StatusCodes.Status404NotFound) 
        { }
    }

    public class NotFoundException : NotFoundExceptions
    {
        public NotFoundException(string entity, int id) : 
            base($"{entity} with id '{id}' was not found.") 
        { }
    }
}
