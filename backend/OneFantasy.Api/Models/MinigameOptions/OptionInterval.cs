using OneFantasy.Api.Models.Minigames;

namespace OneFantasy.Api.Models.MinigameOptions
{
    public class OptionInterval : Option
    {

        protected OptionInterval() { }

        private OptionInterval(int price, int? min, int? max) : base(price)
        {
            Min = min;
            Max = max;
        }

        public int? Min { get; set; }
        public int? Max { get; set; }

        public static OptionInterval FromMin(MinigameMatch minigameMatch, int price, int min) => new(price, min, null);

        public static OptionInterval FromMax(MinigameMatch minigameMatch, int price, int max) => new(price, null, max);

        public static OptionInterval FromRange(MinigameMatch minigameMatch, int price, int min, int max) => new(price, min, max);

    }
}
