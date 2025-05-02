using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.MinigameGroups;

namespace OneFantasy.Api.Models.Participations
{
    public class ParticipationExtra : Participation
    {

        protected ParticipationExtra() { }

        public ParticipationExtra(
            DateTime date, Season season, MinigameGroupMatch2A minigameGroupMatch2A, MinigameGroupMatch2B minigameGroupMatch2B
        ) : base(date, season, 200)
        {
            Groups.Add(minigameGroupMatch2A);
            Groups.Add(minigameGroupMatch2B);
        }

        [NotMapped]
        public MinigameGroupMatch2A MinigameGroupMatch2A => Groups.OfType<MinigameGroupMatch2A>().Single();

        [NotMapped]
        public MinigameGroupMatch2B MinigameGroupMatch2B => Groups.OfType<MinigameGroupMatch2B>().Single();

    }
}
