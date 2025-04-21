using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameGroups
{
    public class MinigameGroupMatch2A : MinigameGroup
    {

        protected MinigameGroupMatch2A() { }

        public MinigameGroupMatch2A(MinigameScores minigameScores, MinigamePlayers minigamePlayers, int homeTeamId, int visitingTeamId)
        {
            MinigameScores = minigameScores;
            MinigamePlayers = minigamePlayers;
            HomeTeamId = homeTeamId;
            VisitingTeamId = visitingTeamId;
        }

        public int MinigameScoresId { get; set; }
        public int MinigamePlayersId { get; set; }
        public int HomeTeamId { get; set; }
        public int VisitingTeamId { get; set; }
        public MinigameScores MinigameScores { get; set; }
        public MinigamePlayers MinigamePlayers { get; set; }
        public Team HomeTeam { get; set; }
        public Team VisitingTeam { get; set; }
    }
}
