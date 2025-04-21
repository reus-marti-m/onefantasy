using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameGroups
{
    public class MinigameGroupMatch3 : MinigameGroup
    {

        protected MinigameGroupMatch3() { }

        public MinigameGroupMatch3(MinigameScores minigameScores, MinigamePlayers minigamePlayers1, MinigamePlayers minigamePlayers2, 
            Team homeTeam, Team visitingTeam)
        {
            MinigameScores = minigameScores;
            MinigamePlayers1 = minigamePlayers1;
            MinigamePlayers2 = minigamePlayers2;
            HomeTeam = homeTeam;
            VisitingTeam = visitingTeam;
        }

        public int MinigameScoresId { get; set; }
        public int MinigamePlayers1Id { get; set; }
        public int MinigamePlayers2Id { get; set; }
        public int HomeTeamId { get; set; }
        public int VisitingTeamId { get; set; }
        public MinigameScores MinigameScores { get; set; }
        public MinigamePlayers MinigamePlayers1 { get; set; }
        public MinigamePlayers MinigamePlayers2 { get; set; }
        public Team HomeTeam { get; set; }
        public Team VisitingTeam { get; set; }

    }
}
