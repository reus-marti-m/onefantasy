using System;
using System.Collections.Generic;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using OneFantasy.Api.Models.Participations.Users;

namespace OneFantasy.Api.Models.Participations
{
    public abstract class Participation
    {

        protected Participation() { }

        protected Participation(DateTime date, Season season, int budget)
        {
            Date = date;
            Season = season;
            SeasonId = season.Id;
            Budget = budget;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Budget { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        public virtual ICollection<MinigameGroup> Groups { get; set; } = [];
        public ICollection<UserParticipation> UserParticipations { get; set; }

    }
}
