using System.Collections.Generic;
using System.Linq;
using OneFantasy.Api.Models.Participations.MinigameOptions;

namespace OneFantasy.Api.Models.Participations.Minigames
{
    public class MinigamePlayers : Minigame
    {

        protected MinigamePlayers() { }

        public MinigamePlayers(List<OptionPlayer> options, MinigamePlayersType type)
        {
            foreach (var o in options)
                Options.Add(o);
            Type = type;
        }

        public List<OptionPlayer> PlayerOptions => [.. Options.OfType<OptionPlayer>()];
        public MinigamePlayersType Type { get; set; }

        public enum MinigamePlayersType
        {
            Scorer,
            Assister,
            ScorerOrAssister,
            YellowCard,
            RedCard
        }

    }
}
