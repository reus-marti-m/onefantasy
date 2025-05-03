using System.Collections.Generic;
using System;

namespace OneFantasy.Api.DTOs
{
    public class CreateUserParticipationDto
    {
        public List<UserPlayGroupDto> Groups { get; set; }
    }

    public class UserPlayGroupDto
    {
        public int GroupId { get; set; }
        public List<UserPlayMinigameDto> Minigames { get; set; }
    }

    public class UserPlayMinigameDto
    {
        public int MinigameId { get; set; }
        public List<int> SelectedOptionIds { get; set; }
    }

    public class UserParticipationResponseDto : CreateUserParticipationDto
    {
        public int Id { get; set; }
        public DateTime LastUpdate { get; set; }
        public int UsedBudget { get; set; }
        public new List<UserParticipationGroupResponseDto> Groups { get; set; }
    }

    public class UserParticipationGroupResponseDto : UserPlayGroupDto
    {
        public int Id { get; set; }
        public new List<UserMinigameResponseDto> Minigames { get; set; }
    }

    public class UserMinigameResponseDto : UserPlayMinigameDto
    {
        public int Id { get; set; }
    }
}
