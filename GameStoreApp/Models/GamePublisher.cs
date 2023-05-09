using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a game publisher.
    /// </summary>
    /// <remarks>This class implements the <seealso cref="IEntityBase"/> interface</remarks>
    public class GamePublisher : IEntityBase
    {
        /// <summary>
        /// Gets or sets the game publisher identifier.
        /// </summary>
        /// <remarks>
        /// The Id property is used as the primary key of the GamePublisher entity.
        /// </remarks>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the logo of the game publisher.
        /// </summary>
        /// <remarks>
        /// The logo of the game publisher is required.
        /// </remarks>
        [Display(Name = "Publisher Logo")]
        [Required(ErrorMessage = "The Logo is a required field")]
        public string? Logo { get; set; }

        /// <summary>
        /// Gets or sets the name of the game publisher.
        /// </summary>
        /// <remarks>
        /// The name of the game publisher is required
        /// </remarks>
        [Display(Name = "Publisher Name")]
        [Required(ErrorMessage = "The Name is a required field")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game publisher.
        /// </summary>
        /// <remarks>
        /// The description of the game publisher is required
        /// </remarks>
        [Display(Name = "Publisher Description")]
        [Required(ErrorMessage = "The Description is a required field")]
        public string? Description { get; set; }

        //Relationship
        /// <summary>
        /// Gets or sets the list of games that are published by the publisher.
        /// </summary>
        /// <remarks>This is a list of <seealso cref="Game"/>s and their to represent the relationship of game and the publisher. If a game is published by the publisher it will be included in this list.</remarks>
        public List<Game>? Games { get; set; }

    }
}
