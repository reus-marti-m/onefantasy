using System.Collections.Generic;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.Users
{
    public class UserMinigame
    {

        protected UserMinigame() { }

        public UserMinigame(Minigame minigame, ICollection<UserOption> userOptions)
        {
            Minigame = minigame;
            UserOptions = userOptions;
        }

        public int Id { get; set; }
        public int UserMinigameGroupId { get; set; }
        public UserMinigameGroup UserMinigameGroup { get; set; }
        public int MinigameId { get; set; }
        public Minigame Minigame { get; set; }
        public int? Points { get; set; }
        public ICollection<UserOption> UserOptions { get; set; }

    }
}
