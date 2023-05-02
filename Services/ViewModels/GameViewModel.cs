using FastSteamCopy.Models;
using System.ComponentModel.DataAnnotations;

namespace FastSteamCopy.Services.ViewModels
{
    public class GameViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public List<Achievement> Achievements { get; set; }
    }
}
