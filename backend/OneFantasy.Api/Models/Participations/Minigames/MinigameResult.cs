using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using OneFantasy.Api.Models.Participations.MinigameGroups;
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

        //public int DrawId { get; set; }
        [NotMapped]
        public Option Draw => Options.OfType<Option>().Single();

        [NotMapped]
        public OptionTeam HomeVictory => Options.OfType<OptionTeam>().First();

        [NotMapped]
        public OptionTeam VisitingVictory => Options.OfType<OptionTeam>().Last();

    }
}
