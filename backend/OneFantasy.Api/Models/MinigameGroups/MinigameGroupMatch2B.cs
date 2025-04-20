using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Minigames;
using OneFantasy.Api.Models.Participations;

namespace OneFantasy.Api.Models.MinigameGroups
{
    public class MinigameGroupMatch2B : MinigameGroup
    {

        protected MinigameGroupMatch2B() { }

        public MinigameGroupMatch2B(MinigameMatch minigameMatch, MinigamePlayers minigamePlayers, Team homeTeam, Team visitingTeam) : base()
        {
            MinigameMatch = minigameMatch;
            MinigamePlayers = minigamePlayers;
            HomeTeam = homeTeam;
            VisitingTeam = visitingTeam;
        }

        public MinigameMatch MinigameMatch { get; set; }
        public MinigamePlayers MinigamePlayers { get; set; }
        public Team HomeTeam { get; set; }
        public Team VisitingTeam { get; set; }

    }
}
