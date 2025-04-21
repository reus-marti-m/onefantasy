using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using System;

namespace OneFantasy.Api.Models.Participations
{
    public class ParticipationStandard : Participation
    {

        protected ParticipationStandard() { }

        public ParticipationStandard(
            DateTime date, Season season, MinigameGroupMulti minigameGroupMulti, MinigameGroupMatch3 minigameGroupMatch3
        ) : base(date, season)
        {
            MinigameGroupMulti = minigameGroupMulti;
            MinigameGroupMatch3 = minigameGroupMatch3;
        }

        public MinigameGroupMulti MinigameGroupMulti { get; set; }
        public MinigameGroupMatch3 MinigameGroupMatch3 { get; set; }

    }
}
