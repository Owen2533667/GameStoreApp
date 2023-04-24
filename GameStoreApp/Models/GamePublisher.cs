using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class GamePublisher : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Publisher Logo")]
        [Required(ErrorMessage = "The Logo is a required field")]
        public string? Logo { get; set; }
        [Display(Name = "Publisher Name")]
        [Required(ErrorMessage = "The Name is a required field")]
        public string? Name { get; set; }
        [Display(Name = "Publisher Description")]
        [Required(ErrorMessage = "The Description is a required field")]
        public string? Description { get; set; }

        //Relationship
        public List<Game>? Games { get; set; }

    }
}
