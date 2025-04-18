using OneFantasy.Api.Models.MinigameGroups;

namespace OneFantasy.Api.Models.Minigames
{
    public abstract class Minigame
    {
        protected Minigame() { }

        public int Id { get; set; }
        public MinigameGroup Group { get; set; }

    }
}
