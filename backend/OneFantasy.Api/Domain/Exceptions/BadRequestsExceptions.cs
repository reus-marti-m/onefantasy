using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace OneFantasy.Api.Domain.Exceptions
{
    public abstract class BadRequestsExceptions : ApiException
    {
        public BadRequestsExceptions(string message) : 
            base("Bad Request", message, StatusCodes.Status400BadRequest) 
        { }
    }

    public class DuplicateScoreCombinationsException : BadRequestsExceptions
    {
        public DuplicateScoreCombinationsException() :
            base($"Duplicate score combinations are not allowed.")
        { }
    }

    public class IntervalWithoutMinAndMaxException : BadRequestsExceptions
    {
        public IntervalWithoutMinAndMaxException() :
            base($"Each OptionInterval must specify at least Min or Max.")
        { }
    }

    public class InvalidInterval : BadRequestsExceptions
    {
        public InvalidInterval(int min, int max) :
            base($"OptionInterval has Min ({min}) greater than Max ({max}).")
        { }
    }

    public class OptionNotInMinigameException : BadRequestsExceptions
    {
        public OptionNotInMinigameException(List<int> optionIds, int minigameId) :
            base($"Option/s {string.Join(", ", optionIds)} do not belong to Minigame {minigameId}.")
        { }
    }

    public class InvalidResultOfMinigame : BadRequestsExceptions
    {
        public InvalidResultOfMinigame(string type, int minigameId) :
            base($"Minigame {minigameId} is a {type} type and does not have exactly one option that ocurred.")
        { }
    }

    public class ParticipationNotInSeasonException : BadRequestsExceptions
    {
        public ParticipationNotInSeasonException(int participation, int season) :
            base($"Participation {participation} does not belong to season {season}.")
        { }
    }

    public class PlayerNotInMatchTeamsException : BadRequestsExceptions
    {
        public PlayerNotInMatchTeamsException(int playerId, int homeTeamId, int visitingTeamId) :
            base($"Player {playerId} does not belong to HomeTeam {homeTeamId} or VisitingTeam {visitingTeamId}.")
        { }
    }

    public class AlreadyPlayedException : BadRequestsExceptions
    {
        public AlreadyPlayedException(int participationId, string userId) :
            base($"Participation {participationId} has already been played by user {userId}.")
        { }
    }

    public class ParticipationGroupsCountException : BadRequestsExceptions
    {
        public ParticipationGroupsCountException(int count) :
            base($"Participation must have {count} groups.")
        { }
    }

    public class ParticipationMinigamesCountException : BadRequestsExceptions
    {
        public ParticipationMinigamesCountException(int id, int count) :
            base($"Group {id} must have {count} minigames.")
        { }
    }

    public class DuplicatedSelectedOptions : BadRequestsExceptions
    {
        public DuplicatedSelectedOptions(int id) :
            base($"Minigame {id} has duplicated options.")
        { }
    }

    public class InvalidSelectedOptionNum : BadRequestsExceptions
    {
        public InvalidSelectedOptionNum(int id, int num) :
            base($"Minigame {id} has an invalid number of selected options ({num}).")
        { }
    }

    public class ParticipationBudgetExceededException : BadRequestsExceptions
    {
        public ParticipationBudgetExceededException(int budget, int total) :
            base($"Participation budget of {budget} was exceeded by {total}.")
        { }
    }
}
