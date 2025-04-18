using System.Collections.Generic;
using OneFantasy.Api.Models.MinigameGroups;
using OneFantasy.Api.Models.MinigameOptions;

namespace OneFantasy.Api.Models.Minigames
{
    public class MinigamePlayers : Minigame
    {

        protected MinigamePlayers() { }

        public MinigamePlayers(List<OptionPlayer> options, MinigamePlayersType type) : base()
        {
            Options = options;
            Type = type;
        }

        public List<OptionPlayer> Options { get; }
        public MinigamePlayersType Type { get;  }

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
