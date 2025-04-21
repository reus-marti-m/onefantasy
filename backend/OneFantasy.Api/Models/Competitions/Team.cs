using System.Collections.Generic;

namespace OneFantasy.Api.Models.Competitions
{
    public class Team
    {

        protected Team() { }

        public Team(string name, string abbreviation, Season season)
        {
            Name = name;
            Abbreviation = abbreviation;
            Season = season;
            SeasonId = season.Id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<Player> Players { get; set; } = [];
        public int SeasonId { get; set; }
        public Season Season { get; set; }

    }
}
