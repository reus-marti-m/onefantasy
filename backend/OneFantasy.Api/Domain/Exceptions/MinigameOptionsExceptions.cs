using System;
using OneFantasy.Api.Models.Participations.MinigameOptions;
using System.Collections.Generic;

namespace OneFantasy.Api.Domain.Exceptions
{
    public class PlayerNotInMatchTeamsException : Exception
    {
        public PlayerNotInMatchTeamsException(int playerId, int homeTeamId, int visitingTeamId) : 
            base($"Player {playerId} does not belong to HomeTeam {homeTeamId} or VisitingTeam {visitingTeamId}.") { }
    }

    public class DuplicateScoreCombinationsException : Exception
    {
        public DuplicateScoreCombinationsException() : base($"Duplicate score combinations are not allowed.") { }
    }

    public class IntervalWithoutMinAndMaxException : Exception
    {
        public IntervalWithoutMinAndMaxException() : base($"Each OptionInterval must specify at least Min or Max.") { }
    }

    public class InvalidInterval : Exception
    {
        public InvalidInterval(int min, int max) : base($"OptionInterval has Min ({min}) greater than Max ({max}).") { }
    }

}
