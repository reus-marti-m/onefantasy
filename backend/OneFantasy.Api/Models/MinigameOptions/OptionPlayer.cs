using OneFantasy.Api.Models.Minigames;
using OneFantasy.Api.Models.Seasons;

namespace OneFantasy.Api.Models.MinigameOptions
{
    public class OptionPlayer : Option
    {

        protected OptionPlayer() { }

        public OptionPlayer(int price, Player player) : base(price)
        {
            Player = player;
        }

        public Player Player { get; set; }
    }
}
