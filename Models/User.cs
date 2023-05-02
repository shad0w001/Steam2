namespace FastSteamCopy.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Game>? OwnedGames { get; set; }
        public List<User>? Friends { get; set; }
        public List<Achievement>? EarnedAchievements { get; set;}
    }
}
