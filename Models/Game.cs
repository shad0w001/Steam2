﻿using MessagePack;

namespace FastSteamCopy.Models
{
    public class Game
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public List<Achievement> Achievements { get; set;}
    }
}
