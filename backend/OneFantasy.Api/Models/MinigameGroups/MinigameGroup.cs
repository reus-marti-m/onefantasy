using OneFantasy.Api.Models.Participations;

namespace OneFantasy.Api.Models.MinigameGroups
{
    public abstract class MinigameGroup
    {

        protected MinigameGroup() { }

        public int Id { get; set; }
        public Participation Participation { get; set; } = null!;

    }
}
