using System;

namespace OneFantasy.Api.Domain.Exceptions
{
    public class DuplicateCompetitionException : Exception
    {
        public DuplicateCompetitionException(string name) : base($"A competition with name '{name}' already exists.") { }
    }
}
