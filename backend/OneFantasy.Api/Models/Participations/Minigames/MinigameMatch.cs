using System.Collections.Generic;
using System.Linq;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using OneFantasy.Api.Models.Participations.MinigameOptions;

namespace OneFantasy.Api.Models.Participations.Minigames
{
    public class MinigameMatch : Minigame
    {

        protected MinigameMatch() { }

        public MinigameMatch(List<OptionInterval> options, MinigameMatchType type)
        {
            foreach (var o in options)
                Options.Add(o);
            Type = type;
        }

        public MinigameMatchType Type { get; set; }
        public List<OptionInterval> IntervalOptions => [.. Options.OfType<OptionInterval>()];

        public enum MinigameMatchType
        {
            Corner,
            YellowCards,
            Goals
        }

    }
}
