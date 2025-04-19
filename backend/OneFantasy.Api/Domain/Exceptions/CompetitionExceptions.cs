using System;

namespace OneFantasy.Api.Domain.Exceptions
{
    public class CompetitionExceptions
    {

        public class DuplicateCompetitionException : Exception
        {
            public DuplicateCompetitionException(string name) : base($"A competition with name '{name}' already exists.") { }
        }

    }
}
