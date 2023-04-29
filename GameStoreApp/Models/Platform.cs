using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class Platform : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Platform Name")]
        [Required(ErrorMessage = "The Platform Name is rquired")]
        public string? Name { get; set; }

        [Display(Name = "Platform Description")]
        [Required(ErrorMessage = "The Platform Description is rquired")]
        public string? Description { get; set; }

        [Display(Name = "Platform Release Date")]
        [Required(ErrorMessage = "The Platform Release Date is rquired")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Platform Price")]
        [Required(ErrorMessage = "The Platform Price is rquired")]
        public double Price { get; set; }

        [Display(Name = "Platform Developer")]
        [Required(ErrorMessage = "The Platform Developer is rquired")]
        public string? PlatformDeveloper { get; set; }

        [Display(Name = "Platform Image")]
        [Required(ErrorMessage = "The Platform Image is rquired")]
        public string? ImageURL { get; set; }

        //Relationships
        public List<Platform_Game>? Platforms_Games { get; set; }
    }
}
