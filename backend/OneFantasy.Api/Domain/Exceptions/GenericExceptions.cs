using System;

namespace OneFantasy.Api.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, int id) : base($"{entity} with id '{id}' was not found.") { }
    }
}
