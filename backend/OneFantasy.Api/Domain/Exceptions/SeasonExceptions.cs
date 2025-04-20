using System;

namespace OneFantasy.Api.Domain.Exceptions
{
    public class DuplicateSeasonException : Exception
    {
        public DuplicateSeasonException(int competitionId, int year) : base($"Season {year} already exists for competition {competitionId}.") { }
    }
}
