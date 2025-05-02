using OneFantasy.Api.Models.Participations.MinigameOptions;

namespace OneFantasy.Api.Models.Participations.Users
{
    public class UserOption
    {

        protected UserOption() { }

        public UserOption(int optionId)
        {
            OptionId = optionId;
        }

        public int UserMinigameId { get; set; }
        public UserMinigame UserMinigame { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }

    }
}
