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
            HomeVictoryId = homeVictory.Id;
            Draw = new Option(drawPrice);
            DrawId = Draw.Id;
            VisitingVictory = visitingVictory;
            VisitingVictoryId = visitingVictory.Id;
        }

        public int DrawId { get; set; }
        public Option Draw { get; set; } = null!;

        public int HomeVictoryId { get; set; }
        public OptionTeam HomeVictory { get; set; } = null!;

        public int VisitingVictoryId { get; set; }
        public OptionTeam VisitingVictory { get; set; } = null!;


    }
}
