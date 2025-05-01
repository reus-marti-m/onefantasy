namespace OneFantasy.Api.Models.Participations.MinigameOptions
{
    public class OptionScore : Option
    {

        protected OptionScore() { }

        public OptionScore(int price, int homeGoals, int awayGoals) : base(price)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

    }
}
