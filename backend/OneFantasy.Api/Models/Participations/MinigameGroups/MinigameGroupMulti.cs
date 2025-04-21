using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameGroups
{
    public class MinigameGroupMulti : MinigameGroup
    {

        protected MinigameGroupMulti() { }

        public MinigameGroupMulti(MinigameResult match1, MinigameResult match2, MinigameResult match3)
        {
            Match1 = match1;
            Match2 = match2;
            Match3 = match3;
        }

        public int Match1Id { get; set; }
        public int Match2Id { get; set; }
        public int Match3Id { get; set; }
        public MinigameResult Match1 { get; set; }
        public MinigameResult Match2 { get; set; }
        public MinigameResult Match3 { get; set; }

    }
}
