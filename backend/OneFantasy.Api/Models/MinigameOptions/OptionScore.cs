namespace OneFantasy.Api.Models.MinigameOptions
{
    public class OptionScore : Option
    {

        protected OptionScore() { }

        public OptionScore(int price, int homeGoals, int awayGoals) : base(price)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }

        public int HomeGoals { get; }
        public int AwayGoals { get; }

    }
}
