using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class VoiceActor
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        public string PictureURL { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        //Relationships
        public List<VoiceActor_Game> VoiceActors_Games { get; set; }    
    }
}
