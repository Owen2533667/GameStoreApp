using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a game developer.
    /// </summary>
    /// <remarks>This class implements the <seealso cref="IEntityBase"/> interface</remarks>
    public class GameDeveloper : IEntityBase
    {
        /// <summary>
        /// Gets or sets the game developer identifier.
        /// </summary>
        /// <remarks>
        /// The Id property is used as the primary key of the GameDeveloper entity.
        /// </remarks>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the logo of the game developer.
        /// </summary>
        /// <remarks>
        /// The logo of the game developer is required.
        /// </remarks>
        [Display(Name = "Developer Logo")]
        [Required(ErrorMessage = "The Logo is a required field")]
        public string? Logo { get; set; }

        /// <summary>
        /// Gets or sets the name of the game developer.
        /// </summary>
        /// <remarks>
        /// The name of the game developer is required
        /// </remarks>
        [Display(Name = "Developer Name")]
        [Required(ErrorMessage = "The Name is a required field")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game developer.
        /// </summary>
        /// <remarks>
        /// The description of the game developer is required
        /// </remarks>
        [Display(Name = "Developer Description")]
        [Required(ErrorMessage = "The Description is a required field")]
        public string? Description { get; set; }


        //Relationships
        /// <summary>
        /// Gets or sets the list of games that are developed by the developer.
        /// </summary>
        /// <remarks>This is a list of <seealso cref="Game"/>s and their to represent the relationship of game and the developer. If a game is developed by the developer it will be included in this list.</remarks>
        public List<Game>? Games { get; set; }
    }
}
