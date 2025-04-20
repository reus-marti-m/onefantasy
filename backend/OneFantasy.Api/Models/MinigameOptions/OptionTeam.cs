using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Models.MinigameOptions
{
    public class OptionTeam : Option
    {

        protected OptionTeam() { }

        public OptionTeam(int price, Team team) : base(price)
        {
            Team = team;
        }

        public Team Team { get; set; }

    }
}
