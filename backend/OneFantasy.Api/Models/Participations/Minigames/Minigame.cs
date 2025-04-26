using System.Collections.Generic;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using OneFantasy.Api.Models.Participations.MinigameOptions;

namespace OneFantasy.Api.Models.Participations.Minigames
{
    public abstract class Minigame
    {
        protected Minigame() { }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public bool IsResolved { get; set; }
        public MinigameGroup Group { get; set; }

        public virtual ICollection<Option> Options { get; set; } = [];

    }
}
