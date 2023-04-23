using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class VoiceActor : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "A picture is required")]

        public string? PictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "A name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]

        public string? FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "A biography is required")]
        public string? Bio { get; set; }


        //Relationships
        public List<VoiceActor_Game>? VoiceActors_Games { get; set; }    
    }
}
