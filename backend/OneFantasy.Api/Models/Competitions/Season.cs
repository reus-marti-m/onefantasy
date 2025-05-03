using System.Collections.Generic;
using OneFantasy.Api.Models.Participations;

namespace OneFantasy.Api.Models.Competitions
{
    public class Season
    {

        protected Season() { }

        public Season(int year, Competition competition)
        {
            Year = year;
            Competition = competition;
            CompetitionId = competition.Id;
        }

        public int Id { get; set; }
        public int Year { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        public List<Team> Teams { get; set; } = [];
        public List<Participation> Participations { get; set; } = [];

    }
}
