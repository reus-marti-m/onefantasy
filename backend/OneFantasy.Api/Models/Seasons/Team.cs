using System.Collections.Generic;

namespace OneFantasy.Api.Models.Seasons
{
    public class Team
    {

        protected Team() { }

        public Team(string name, string abbreviation, List<Player> players)
        {
            Name = name;
            Abbreviation = abbreviation;
            Players = players;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<Player> Players { get; set; }

    }
}
