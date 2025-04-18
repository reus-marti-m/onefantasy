using System;
using OneFantasy.Api.Models.Seasons;

namespace OneFantasy.Api.Models.Participations
{
    public abstract class Participation
    {

        protected Participation() { }

        public Participation(DateTime date, CompetitionSeason competitionSeason)
        {
            Date = date;
            CompetitionSeason = competitionSeason;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CompetitionSeason CompetitionSeason { get; set; }

    }
}
