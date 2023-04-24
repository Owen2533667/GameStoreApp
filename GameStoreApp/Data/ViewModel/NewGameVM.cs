using GameStoreApp.Data.Base;
using GameStoreApp.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreApp.Models
{
    public class NewGameVM
    {
        public int Id { get; set; }

        [Display(Name = "Game Name")]
        [Required(ErrorMessage ="The Name is rquired")]
        public string? Name { get; set; }

        [Display(Name = "Game Description")]
        [Required(ErrorMessage = "The Description is rquired")]
        public string? Description { get; set; }

        [Display(Name = "Game Release Date")]
        [Required(ErrorMessage = "The Release Date is rquired")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Price in £")]
        [Required(ErrorMessage = "The Price is rquired")]
        public double Price { get; set; }

        [Display(Name = "Game Image")]
        [Required(ErrorMessage = "The Image is rquired")]
        public string? ImageURL { get; set; }

        [Display(Name = "Select a Game Genre")]
        [Required(ErrorMessage = "The Game Genre is rquired")]
        public GameGenre GameGenre { get; set; }

        //Relationships
        [Display(Name = "Select Voice Actor(s)")]
        //[Required(ErrorMessage = "Voice Actor(s) is rquired")]
        public List<int> VoiceActorIds { get; set; }

        [Display(Name = "Select Developer")]
        [Required(ErrorMessage = "The Developer is rquired")]
        public int GameDeveloperId { get; set; }

        [Display(Name = "Select Publisher")]
        [Required(ErrorMessage = "The Publisher is rquired")]
        public int GamePublisherId { get; set; }

    }
}
