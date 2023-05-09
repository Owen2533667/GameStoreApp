using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a game rating.
    /// </summary>
    /// <remarks>
    /// Implements <seealso cref="IEntityBase"/>.
    /// </remarks>
    public class GameRating : IEntityBase
    {
        /// <summary>
        /// Gets or sets the ID of the game rating.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the logo of the game rating.
        /// </summary>
        [Display(Name = "Rating Logo")]
        [Required(ErrorMessage = "The Logo is a required field")]
        public string? Logo { get; set; }

        /// <summary>
        /// Gets or sets the name of the game rating.
        /// </summary>
        [Display(Name = "Rating Name")]
        [Required(ErrorMessage = "The Name is a required field")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game rating.
        /// </summary>
        [Display(Name = "Rating Description")]
        [Required(ErrorMessage = "The Description is a required field")]
        public string? Description { get; set; }


        //Relationships

        /// <summary>
        /// Gets or sets the list of games associated with the game rating.
        /// </summary>
        public List<Game>? Games { get; set; }
    }
}
