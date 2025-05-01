using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OneFantasy.Api.Models.Participations.MinigameOptions;

namespace OneFantasy.Api.Models.Participations.Minigames
{
    public class MinigameResult : Minigame
    {

        protected MinigameResult() { }

        public MinigameResult(OptionTeam homeVictory, int drawPrice, OptionTeam visitingVictory)
        {
            Options.Add(homeVictory);
            Options.Add(new Option(drawPrice));
            Options.Add(visitingVictory);
        }

        [NotMapped]
        public Option Draw => Options.Single(o => o.GetType() == typeof(Option));

        [NotMapped]
        public OptionTeam HomeVictory => Options.OfType<OptionTeam>().First();

        [NotMapped]
        public OptionTeam VisitingVictory => Options.OfType<OptionTeam>().Last();

    }
}
