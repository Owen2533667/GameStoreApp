using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class GameRating : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Rating Logo")]
        [Required(ErrorMessage = "The Logo is a required field")]
        public string? Logo { get; set; }

        [Display(Name = "Rating Name")]
        [Required(ErrorMessage = "The Name is a required field")]
        public string? Name { get; set; }

        [Display(Name = "Rating Description")]
        [Required(ErrorMessage = "The Description is a required field")]
        public string? Description { get; set; }


        //Relationships
        public List<Game>? Games { get; set; }
    }
}
