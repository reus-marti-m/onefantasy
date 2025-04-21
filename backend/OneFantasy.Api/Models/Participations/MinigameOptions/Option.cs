using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameOptions
{
    public class Option
    {

        protected Option() { }

        public Option(int price)
        {
            Price = price;
        }

        public int MinigameId { get; set; }
        public Minigame Minigame { get; set; }
        public int Id { get; set; }
        public int Price { get; set; }

    }
}
