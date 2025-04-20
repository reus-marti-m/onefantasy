using OneFantasy.Api.Models.Minigames;
using OneFantasy.Api.Models.Seasons;

namespace OneFantasy.Api.Models.MinigameGroups
{
    public class MinigameGroupMatch3 : MinigameGroup
    {

        protected MinigameGroupMatch3() { }

        public MinigameGroupMatch3(MinigameScores minigameScores, MinigamePlayers mp1, MinigamePlayers mp2, Team homeTeam, Team visitingTeam) : base()
        {
            MinigameScores = minigameScores;
            MinigamePlayers1 = mp1;
            MinigamePlayers2 = mp2;
            HomeTeam = homeTeam;
            VisitingTeam = visitingTeam;
        }

        public MinigameScores MinigameScores { get; set; }
        public MinigamePlayers MinigamePlayers1 { get; set; }
        public MinigamePlayers MinigamePlayers2 { get; set; }
        public Team HomeTeam { get; set; }
        public Team VisitingTeam { get; set; }

    }
}
