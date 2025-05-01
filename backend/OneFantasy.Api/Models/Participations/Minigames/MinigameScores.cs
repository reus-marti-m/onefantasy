using OneFantasy.Api.Models.Participations.MinigameOptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OneFantasy.Api.Models.Participations.Minigames
{
    public class MinigameScores : Minigame
    {

        protected MinigameScores() { }

        public MinigameScores(List<OptionScore> options)
        {
            foreach (var o in options)
                Options.Add(o);
        }

        [NotMapped]
        public List<OptionScore> ScoreOptions => [.. Options.OfType<OptionScore>()];

    }
}
