namespace OneFantasy.Api.Models.Competitions
{
    public class Player
    {

        protected Player() { }

        public Player(string name, Team team)
        {
            Name = name;
            Team = team;
            TeamId = team.Id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; } 

    }
}
