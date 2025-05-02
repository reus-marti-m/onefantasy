using System.Collections.Generic;
using OneFantasy.Api.Models.Participations.MinigameGroups;

namespace OneFantasy.Api.Models.Participations.Users
{
    public class UserMinigameGroup
    {

        protected UserMinigameGroup() { }

        public UserMinigameGroup(MinigameGroup minigameGroup, ICollection<UserMinigame> userMinigames)
        {
            MinigameGroup = minigameGroup;
            UserMinigames = userMinigames;
        }

        public int Id { get; set; }
        public int UserParticipationId { get; set; }
        public UserParticipation UserParticipation { get; set; }
        public int MinigameGroupId { get; set; }
        public MinigameGroup MinigameGroup { get; set; }
        public int? Points { get; set; }
        public ICollection<UserMinigame> UserMinigames { get; set; }
    }
}
