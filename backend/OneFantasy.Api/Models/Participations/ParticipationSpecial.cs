using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using System;

namespace OneFantasy.Api.Models.Participations
{
    public class ParticipationSpecial : Participation
    {

        protected ParticipationSpecial() { }

        public ParticipationSpecial(
            DateTime date, Season season, MinigameGroupMatch2A minigameGroupMatch2A, MinigameGroupMatch2B minigameGroupMatch2B
        ) : base(date, season)
        {
            MinigameGroupMatch2A = minigameGroupMatch2A;
            MinigameGroupMatch2B = minigameGroupMatch2B;
        }

        public MinigameGroupMatch2A MinigameGroupMatch2A { get; set; }
        public MinigameGroupMatch2B MinigameGroupMatch2B { get; set; }

    }
}
