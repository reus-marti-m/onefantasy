using OneFantasy.Api.Models.MinigameGroups;
using OneFantasy.Api.Models.MinigameOptions;

namespace OneFantasy.Api.Models.Minigames
{
    public class MinigameResult : Minigame
    {

        protected MinigameResult() { }

        public MinigameResult(OptionTeam homeVictory, int drawPrice, OptionTeam visitingVictory) : base()
        {
            HomeVictory = homeVictory;
            Draw = new Option(drawPrice);
            VisitingVictory = visitingVictory;
        }

        public OptionTeam HomeVictory { get; }
        public Option Draw { get; }
        public OptionTeam VisitingVictory { get; }


    }
}
