﻿using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Models.Participations.MinigameOptions
{
    public class OptionPlayer : Option
    {

        protected OptionPlayer() { }

        public OptionPlayer(int price, int playerId) : base(price)
        {
            PlayerId = playerId;
        }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
