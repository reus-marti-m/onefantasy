using OneFantasy.Api.Models.Minigames;
using OneFantasy.Api.Models.Seasons;

namespace OneFantasy.Api.Models.MinigameGroups
{
    public class MinigameGroupMatch2A : MinigameGroup
    {
        protected MinigameGroupMatch2A() { }

        public MinigameGroupMatch2A(MinigameScores minigameScores, MinigamePlayers minigamePlayers, Team homeTeam, Team visitingTeam) : base()
        {
            MinigameScores = minigameScores;
            MinigamePlayers = minigamePlayers;
            HomeTeam = homeTeam;
            VisitingTeam = visitingTeam;
        }

        public MinigameScores MinigameScores { get; set; }
        public MinigamePlayers MinigamePlayers { get; set; }
        public Team HomeTeam { get; set; }
        public Team VisitingTeam { get; set; }
    }
}
