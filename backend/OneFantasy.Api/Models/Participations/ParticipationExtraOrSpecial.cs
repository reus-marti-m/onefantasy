using System;
using OneFantasy.Api.Models.MinigameGroups;
using OneFantasy.Api.Models.Seasons;

namespace OneFantasy.Api.Models.Participations
{
    public class ParticipationExtraOrSpecial : Participation
    {

        protected ParticipationExtraOrSpecial() { }

        public ParticipationExtraOrSpecial(
            DateTime date, CompetitionSeason competitionSeason, MinigameGroupMatch2A minigameGroupMatch2A, MinigameGroupMatch2B minigameGroupMatch2B
        ) : base(date, competitionSeason)
        {
            MinigameGroupMatch2A = minigameGroupMatch2A;
            MinigameGroupMatch2B = minigameGroupMatch2B;
        }

        public MinigameGroupMatch2A MinigameGroupMatch2A { get; set; }
        public MinigameGroupMatch2B MinigameGroupMatch2B { get; set; }

    }
}
