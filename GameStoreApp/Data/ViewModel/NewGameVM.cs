using GameStoreApp.Data.Base;
using GameStoreApp.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents the view model for creating a new game.
    /// </summary>
    public class NewGameVM
    {
        /// <summary>
        /// Gets or sets the ID of the game.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <remarks>
        /// This field represents the name of the game. It is a required field for creating a new game.
        /// </remarks>
        [Display(Name = "Game Name")]
        [Required(ErrorMessage ="The Name is rquired")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game.
        /// </summary>
        /// <remarks>
        /// This field represents the description of the game. It is a required field for creating a new game.
        /// </remarks>
        [Display(Name = "Game Description")]
        [Required(ErrorMessage = "The Description is rquired")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the release date of the game.
        /// </summary>
        /// <remarks>
        /// This field represents the release date of the game. It is a required field for creating a new game.
        /// </remarks>
        [Display(Name = "Game Release Date")]
        [Required(ErrorMessage = "The Release Date is rquired")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the price of the game in £.
        /// </summary>
        /// <remarks>
        /// This field represents the price of the game in pounds (£). It is a required field for creating a new game.
        /// </remarks>
        [Display(Name = "Price in £")]
        [Required(ErrorMessage = "The Price is rquired")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the game's image.
        /// </summary>
        /// <remarks>
        /// This field represents the URL of the game's image. It is a required field for creating a new game.
        /// </remarks>
        [Display(Name = "Game Image")]
        [Required(ErrorMessage = "The Image is rquired")]
        public string? ImageURL { get; set; }

        /// <summary>
        /// Gets or sets the selected game genre.
        /// </summary>
        /// <remarks>
        /// This field represents the selected game genre. It is a required field for creating a new game. The data type in a enum value.
        /// </remarks>
        [Display(Name = "Select a Game Genre")]
        [Required(ErrorMessage = "The Game Genre is rquired")]
        public GameGenre GameGenre { get; set; }

        //Relationships

        /// <summary>
        /// Gets or sets the list of selected voice actor IDs for the game.
        /// </summary>
        /// <remarks>
        /// This field represents the list of selected voice actor IDs for the game. It is an optional field for creating a new game.
        /// </remarks>
        [Display(Name = "Select Voice Actor(s)")]
        //[Required(ErrorMessage = "Voice Actor(s) is rquired")]
        public List<int>? VoiceActorIds { get; set; }

        /// <summary>
        /// Gets or sets the list of selected platform IDs for the game.
        /// </summary>
        /// <remarks>
        /// This field represents the list of selected platform IDs for the game. It is an optional field for creating a new game.
        /// </remarks>
        [Display(Name = "Select Platform(s)")]
        //[Required(ErrorMessage = "Platform(s) is rquired")]
        public List<int>? PlatformIds { get; set; }

        /// <summary>
        /// Gets or sets the ID of the game developer.
        /// </summary>
        /// <remarks>
        /// This field represents the ID of the selected game developer. It is a required field for creating a new game.
        /// </remarks>
        [Display(Name = "Select Developer")]
        [Required(ErrorMessage = "The Developer is rquired")]
        public int GameDeveloperId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the game publisher.
        /// </summary>
        /// <remarks>
        /// This field represents the ID of the selected game publisher. It is a required field for creating a new game.
        /// </remarks>
        [Display(Name = "Select Publisher")]
        [Required(ErrorMessage = "The Publisher is rquired")]
        public int GamePublisherId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the game rating.
        /// </summary>
        /// <remarks>
        /// This field represents the ID of the selected game rating. It is a required field for creating a new game.
        /// </remarks>
        [Display(Name = "Select Rating")]
        [Required(ErrorMessage = "The Rating is rquired")]
        public int GameRatingId { get; set; }

    }
}
