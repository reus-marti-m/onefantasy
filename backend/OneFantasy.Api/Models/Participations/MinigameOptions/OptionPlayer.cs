using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameOptions
{
    public class OptionPlayer : Option
    {

        protected OptionPlayer() { }

        public OptionPlayer(int price, Player player) : base(price)
        {
            Player = player;
            PlayerId = player.Id;
        }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
