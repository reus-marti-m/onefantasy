using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameOptions
{
    public class OptionTeam : Option
    {

        protected OptionTeam() { }

        public OptionTeam(int price, int teamId) : base(price)
        {
            TeamId = teamId;
        }

        public int TeamId { get; set; }
        public Team Team { get; set; }

    }
}
