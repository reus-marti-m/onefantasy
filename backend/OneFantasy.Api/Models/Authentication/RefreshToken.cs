using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace OneFantasy.Api.Models.Authentication
{
    public class RefreshToken
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime Expires { get; set; }

        [NotMapped]
        public bool IsExpired => DateTime.UtcNow >= Expires;

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
