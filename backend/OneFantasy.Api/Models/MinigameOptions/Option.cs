using OneFantasy.Api.Models.Minigames;

namespace OneFantasy.Api.Models.MinigameOptions
{
    public class Option
    {

        protected Option() { }

        public Option(int price)
        {
            Price = price;
        }

        public int MinigameId { get; set; }
        public Minigame Minigame { get; set; } = null!;
        public int Id { get; set; }
        public int Price { get; set; }

    }
}
