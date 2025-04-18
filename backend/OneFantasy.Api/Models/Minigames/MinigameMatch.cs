using System.Collections.Generic;
using OneFantasy.Api.Models.MinigameGroups;
using OneFantasy.Api.Models.MinigameOptions;

namespace OneFantasy.Api.Models.Minigames
{
    public class MinigameMatch : Minigame
    {

        protected MinigameMatch() { }

        public MinigameMatch(List<OptionInterval> options, MinigameMatchType type) : base()
        {
            Options = options;
            Type = type;
        }

        public MinigameMatchType Type { get; set; }
        public List<OptionInterval> Options { get; set; }

        public enum MinigameMatchType
        {
            Corner,
            YellowCards,
            Goals
        }

    }
}
