using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OneFantasy.Api.Models.Participations
{
    public class ParticipationStandard : Participation
    {

        protected ParticipationStandard() { }

        public ParticipationStandard(
            DateTime date, Season season, MinigameGroupMulti minigameGroupMulti, MinigameGroupMatch3 minigameGroupMatch3
        ) : base(date, season, 300)
        {
            Groups.Add(minigameGroupMulti);
            Groups.Add(minigameGroupMatch3);
        }

        [NotMapped]
        public MinigameGroupMulti MinigameGroupMulti => Groups.OfType<MinigameGroupMulti>().Single();

        [NotMapped]
        public MinigameGroupMatch3 MinigameGroupMatch3 => Groups.OfType<MinigameGroupMatch3>().Single();

    }
}
