using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameGroups
{
    public class MinigameGroupMatch2B : MinigameGroup
    {

        protected MinigameGroupMatch2B() { }

        public MinigameGroupMatch2B(MinigameMatch minigameMatch, MinigamePlayers minigamePlayers, Team homeTeam, Team visitingTeam)
        {
            MinigameMatch = minigameMatch;
            MinigamePlayers = minigamePlayers;
            HomeTeam = homeTeam;
            VisitingTeam = visitingTeam;
        }

        public int MinigameMatchId { get; set; }
        public int MinigamePlayersId { get; set; }
        public int HomeTeamId { get; set; }
        public int VisitingTeamId { get; set; }
        public MinigameMatch MinigameMatch { get; set; }
        public MinigamePlayers MinigamePlayers { get; set; }
        public Team HomeTeam { get; set; }
        public Team VisitingTeam { get; set; }

    }
}
