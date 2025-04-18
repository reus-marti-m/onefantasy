using OneFantasy.Api.Models.Minigames;

namespace OneFantasy.Api.Models.MinigameGroups
{
    public class MinigameGroupMulti : MinigameGroup
    {

        protected MinigameGroupMulti() { }

        public MinigameGroupMulti(MinigameResult match1, MinigameResult match2, MinigameResult match3) : base()
        {
            Match1 = match1;
            Match2 = match2;
            Match3 = match3;
        }

        public MinigameResult Match1 { get; set; }
        public MinigameResult Match2 { get; set; }
        public MinigameResult Match3 { get; set; }

    }
}
