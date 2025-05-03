using System.Collections.Generic;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameGroups
{
    public abstract class MinigameGroup
    {

        protected MinigameGroup() { }

        public int Id { get; set; }
        public int ParticipationId { get; set; }
        public Participation Participation { get; set; }
        public virtual ICollection<Minigame> Minigames { get; set; } = [];

    }
}
