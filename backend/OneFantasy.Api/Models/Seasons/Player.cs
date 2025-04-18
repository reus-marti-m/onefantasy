namespace OneFantasy.Api.Models.Seasons
{
    public class Player
    {

        protected Player() { }

        public Player(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Team Team { get; set; } = null!;

    }
}
