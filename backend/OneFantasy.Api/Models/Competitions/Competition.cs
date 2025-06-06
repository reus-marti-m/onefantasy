﻿using System.Collections.Generic;

namespace OneFantasy.Api.Models.Competitions
{
    public class Competition
    {
        protected Competition() { }

        public Competition(string name, CompetitionType type, CompetitionFormat format)
        {
            Name = name;
            Type = type;
            Format = format;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public CompetitionType Type { get; set; }
        public CompetitionFormat Format { get; set; }
        public List<Season> Seasons { get; set; } = [];

        public enum CompetitionType
        {
            Regular,
            Tournament
        }

        public enum CompetitionFormat
        {
            League,
            Knockout,
            Tournament
        }
    }
}
