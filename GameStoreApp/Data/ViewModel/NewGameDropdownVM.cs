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
        }
        public List<GamePublisher> Publisher { get; set; }
        public List<GameDeveloper> Developer { get; set; }
        public List<VoiceActor> VoiceActor { get; set; }
    }
}
