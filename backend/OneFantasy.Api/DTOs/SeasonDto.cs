﻿using System.ComponentModel.DataAnnotations;

namespace OneFantasy.Api.DTOs
{
    public class SeasonDto
    {
        [Required]
        public int Year { get; set; }
    }

    public class SeasonDtoResponse : SeasonDto
    {
        public int Id { get; set; }
    }
}
