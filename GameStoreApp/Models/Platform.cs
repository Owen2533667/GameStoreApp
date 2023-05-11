using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a platform that a game can be played on.
    /// </summary>
    /// <remarks>This class implements the <seealso cref="IEntityBase"/> interface</remarks>
    public class Platform : IEntityBase
    {
        /// <summary>
        /// Gets or sets the platform identifier.
        /// </summary>
        /// <remarks>
        /// The Id property is used as the primary key of the Platform entity.
        /// </remarks>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the platform.
        /// </summary>
        /// <remarks>
        /// The name of the platform is required field
        /// </remarks>
        [Display(Name = "Platform Name")]
        [Required(ErrorMessage = "The Platform Name is rquired")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the platform.
        /// </summary>
        /// <remarks>
        /// The description of the platform is required and must be between 1 and 500 characters long.
        /// </remarks>
        [Display(Name = "Platform Description")]
        [Required(ErrorMessage = "The Platform Description is rquired")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the release date of the platform.
        /// </summary>
        /// <remarks>
        /// The release date of the platform is required
        /// </remarks>
        [Display(Name = "Platform Release Date")]
        [Required(ErrorMessage = "The Platform Release Date is rquired")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the price of the platform.
        /// </summary>
        /// <remarks>
        /// The price of the platform is required and must be greater than 0.
        /// </remarks>
        [Display(Name = "Platform Price")]
        [Required(ErrorMessage = "The Platform Price is rquired")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Platform Price must be greater than 0.")]
        public double Price { get; set; }

        ///<summary>Gets or sets the developer of the platform.</summary> 
        ///<remarks>The developer of the platform is required.</remarks> 
        [Display(Name = "Platform Developer")]
        [Required(ErrorMessage = "The Platform Developer is rquired")]
        public string? PlatformDeveloper { get; set; }

        ///<summary>Gets or sets the image URL of the platform.</summary> 
        ///<remarks>The image path of the platform is required.</remarks> 
        [Display(Name = "Platform Image")]
        [Required(ErrorMessage = "The Platform Image is rquired")]
        public string? ImageURL { get; set; }

        //Relationships
        /// <summary>
        /// Gets or sets the Platforms_Games for the platform property.
        /// </summary>
        /// <remarks>This property is a List of <seealso cref="Platform_Game"/>, that will represent the replationship between games and platforms./></remarks>
        public List<Platform_Game>? Platforms_Games { get; set; }
    }
}
