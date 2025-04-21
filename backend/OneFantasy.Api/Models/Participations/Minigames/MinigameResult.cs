using OneFantasy.Api.Models.Participations.MinigameGroups;
using OneFantasy.Api.Models.Participations.MinigameOptions;

namespace OneFantasy.Api.Models.Participations.Minigames
{
    public class MinigameResult : Minigame
    {

        protected MinigameResult() { }

        public MinigameResult(OptionTeam homeVictory, int drawPrice, OptionTeam visitingVictory)
        {
            HomeVictory = homeVictory;
            Draw = new Option(drawPrice);
            VisitingVictory = visitingVictory;
        }

        public int DrawId { get; set; }
        public Option Draw { get; set; }

        public int HomeVictoryId { get; set; }
        public OptionTeam HomeVictory { get; set; }

        public int VisitingVictoryId { get; set; }
        public OptionTeam VisitingVictory { get; set; }

    }
}
