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

        protected Participation(DateTime date, Season season, int budget, string round, string roundAbbreviation, int numberInRound)
        {
            Date = date;
            Season = season;
            SeasonId = season.Id;
            Budget = budget;
            Round = round;
            RoundAbbreviation = roundAbbreviation;
            NumberInRound = numberInRound;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Budget { get; set; }
        public int SeasonId { get; set; }
        public string Round { get; set; }
        public string RoundAbbreviation { get; set; }
        public int NumberInRound { get; set; }
        public Season Season { get; set; }
        public virtual ICollection<MinigameGroup> Groups { get; set; } = [];
        public ICollection<UserParticipation> UserParticipations { get; set; }

    }
}
