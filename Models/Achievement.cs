using System.ComponentModel.DataAnnotations.Schema;

namespace FastSteamCopy.Models
{
    public class Achievement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("Games")]
        public string GameId { get; set; }
        public virtual Game Games { get; set; }
    }
}
