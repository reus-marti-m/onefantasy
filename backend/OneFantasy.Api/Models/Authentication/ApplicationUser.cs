using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using OneFantasy.Api.Models.Participations.Users;

namespace OneFantasy.Api.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {

        protected ApplicationUser() { }

        public ApplicationUser(string email) : base(email)
        {
            Email = email;
            UserName = email;
        }

        public ICollection<UserParticipation> UserParticipations { get; set; }

    }
}
