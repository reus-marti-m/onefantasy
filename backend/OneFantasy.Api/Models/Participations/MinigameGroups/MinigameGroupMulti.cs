using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Models.Participations.MinigameGroups
{
    public class MinigameGroupMulti : MinigameGroup
    {

        protected MinigameGroupMulti() { }

        public MinigameGroupMulti(MinigameResult match1, MinigameResult match2, MinigameResult match3)
        {
            Minigames.Add(match1);
            Minigames.Add(match2);
            Minigames.Add(match3);
        }


        [NotMapped]
        public MinigameResult Match1 => Minigames.OfType<MinigameResult>().ElementAt(0);

        [NotMapped]
        public MinigameResult Match2 => Minigames.OfType<MinigameResult>().ElementAt(1);

        [NotMapped]
        public MinigameResult Match3 => Minigames.OfType<MinigameResult>().ElementAt(2);

    }
}
