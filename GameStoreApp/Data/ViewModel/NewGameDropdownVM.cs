using GameStoreApp.Models;

namespace GameStoreApp.Data.ViewModel
{
    public class NewGameDropdownVM
    {
        public NewGameDropdownVM()
        {
            Publisher = new List<GamePublisher>();
            Developer = new List<GameDeveloper>();
            VoiceActor = new List<VoiceActor>();
            Platform = new List<Platform>();
            Rating = new List<GameRating>();
        }
        public List<GamePublisher> Publisher { get; set; }
        public List<GameDeveloper> Developer { get; set; }
        public List<VoiceActor> VoiceActor { get; set; }
        public List<Platform> Platform { get; set; }
        public List<GameRating> Rating { get; set; }
    }
}
