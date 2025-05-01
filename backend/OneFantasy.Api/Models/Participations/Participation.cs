using System;
using System.Collections.Generic;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.MinigameGroups;

namespace OneFantasy.Api.Models.Participations
{
    public abstract class Participation
    {

        protected Participation() { }

        protected Participation(DateTime date, Season season)
        {
            Date = date;
            Season = season;
            SeasonId = season.Id;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        public virtual ICollection<MinigameGroup> Groups { get; set; } = [];

    }
}
