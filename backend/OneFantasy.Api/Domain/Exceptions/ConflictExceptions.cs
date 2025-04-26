using Microsoft.AspNetCore.Http;

namespace OneFantasy.Api.Domain.Exceptions
{
    public abstract class ConflictExceptions : ApiException
    {
        public ConflictExceptions(string message) : 
            base("Conflict", message, StatusCodes.Status409Conflict) 
        { }
    }

    public class DuplicateException : ConflictExceptions
    {
        public DuplicateException(string entity, string name) : 
            base($"{entity} with name '{name}' already exists.") 
        { }
    }

    public class DuplicateSeasonException : ConflictExceptions
    {
        public DuplicateSeasonException(int competitionId, int year) : 
            base($"Season {year} already exists for competition {competitionId}.") 
        { }
    }

    public class DuplicateUserException : ConflictExceptions
    {
        public DuplicateUserException(string email) : 
            base($"The email '{email}' is already registered.") 
        { }
    }
}
