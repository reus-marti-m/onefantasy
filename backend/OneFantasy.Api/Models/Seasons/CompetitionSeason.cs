using System.Collections.Generic;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations;

namespace OneFantasy.Api.Models.Seasons
{
    public class CompetitionSeason
    {

        protected CompetitionSeason() { }

        public CompetitionSeason(int year, Competition competition, List<Team> teams)
        {
            Year = year;
            Competition = competition;
            Teams = teams;
        }

        public int Id { get; set; }
        public int Year { get; set; }
        public Competition Competition { get; set; }
        public List<Team> Teams { get; set; }
        public List<Participation> Participations { get; set; } = [];

    }
}
