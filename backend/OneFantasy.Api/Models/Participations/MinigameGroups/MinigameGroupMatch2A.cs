using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
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
            Minigames.Add(minigameScores);
            Minigames.Add(minigamePlayers);
            HomeTeamId = homeTeamId;
            VisitingTeamId = visitingTeamId;
        }

        public int HomeTeamId { get; set; }

        public int VisitingTeamId { get; set; }

        [NotMapped]
        public MinigameScores MinigameScores => Minigames.OfType<MinigameScores>().Single();

        [NotMapped]
        public MinigamePlayers MinigamePlayers => Minigames.OfType<MinigamePlayers>().Single();

        public Team HomeTeam { get; set; }

        public Team VisitingTeam { get; set; }
    }
}
