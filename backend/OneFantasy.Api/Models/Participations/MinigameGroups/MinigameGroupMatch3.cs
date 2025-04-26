using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameGroups
{
    public class MinigameGroupMatch3 : MinigameGroup
    {

        protected MinigameGroupMatch3() { }

        public MinigameGroupMatch3(MinigameScores minigameScores, MinigamePlayers minigamePlayers1, MinigamePlayers minigamePlayers2, 
            int homeTeamId, int visitingTeamId)
        {
            Minigames.Add(minigameScores);
            Minigames.Add(minigamePlayers1);
            Minigames.Add(minigamePlayers2);
            HomeTeamId = homeTeamId;
            VisitingTeamId = visitingTeamId;
        }

        //public int MinigameScoresId { get; set; }
        //public int MinigamePlayers1Id { get; set; }
        //public int MinigamePlayers2Id { get; set; }
        public int HomeTeamId { get; set; }
        public int VisitingTeamId { get; set; }

        [NotMapped]
        public MinigameScores MinigameScores => Minigames.OfType<MinigameScores>().Single();

        [NotMapped]
        public MinigamePlayers MinigamePlayers1 => Minigames.OfType<MinigamePlayers>().First();

        [NotMapped]
        public MinigamePlayers MinigamePlayers2 => Minigames.OfType<MinigamePlayers>().Last();

        public Team HomeTeam { get; set; }

        public Team VisitingTeam { get; set; }

    }
}
