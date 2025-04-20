using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Minigames;

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
