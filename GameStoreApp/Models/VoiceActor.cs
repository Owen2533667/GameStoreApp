using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class VoiceActor
    {
        [Key]
        public int Id { get; set; }
        public string PictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }

        //Relationships
        public List<VoiceActor_Game> VoiceActors_Games { get; set; }    
    }
}
