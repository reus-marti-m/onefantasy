using System;
using System.Collections.Generic;
using OneFantasy.Api.Models.Authentication;

namespace OneFantasy.Api.Models.Participations.Users
{
    public class UserParticipation
    {

        protected UserParticipation() { }

        public UserParticipation(ApplicationUser user, Participation participation, ICollection<UserMinigameGroup> groups)
        {
            User = user;
            Participation = participation;
            Groups = groups;
            LastUpdate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ParticipationId { get; set; }
        public Participation Participation { get; set; }
        public DateTime LastUpdate { get; set; }
        public int UsedBudget { get; set; }
        public int? Points { get; set; }
        public ICollection<UserMinigameGroup> Groups { get; set; }
    }
}
