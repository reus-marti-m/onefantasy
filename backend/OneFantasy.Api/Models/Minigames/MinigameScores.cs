using OneFantasy.Api.Models.MinigameGroups;
using OneFantasy.Api.Models.MinigameOptions;
using System.Collections.Generic;

namespace OneFantasy.Api.Models.Minigames
{
    public class MinigameScores : Minigame
    {

        protected MinigameScores() { }

        public MinigameScores(List<OptionScore> options) : base()
        {
            Options = options;
        }

        public List<OptionScore> Options { get; set; }

    }
}
