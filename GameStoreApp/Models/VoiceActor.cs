using GameStoreApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a Voice Actor that can act in a game.
    /// </summary>
    /// <remarks>This class implements the <seealso cref="IEntityBase"/> interface</remarks>
    public class VoiceActor : IEntityBase
    {
        /// <summary>
        /// Gets or sets the voice actor identifier.
        /// </summary>
        /// <remarks>
        /// The Id property is used as the primary key of the VoiceActor entity.
        /// </remarks>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the URL of the profile picture for the voice actor.
        /// </summary>
        /// <remarks>
        /// The PictureURL property is used to store the URL of the profile picture for the voice actor. The voice actors picture is required.
        /// </remarks>
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "A picture is required")]
        public string? PictureURL { get; set; }

        /// <summary>
        /// Gets or sets the full name of the voice actor.
        /// </summary>
        /// <remarks>
        /// The FullName property is used to store the full name of the voice actor. The voice actors full name is required, and has the constraint of 3 to 50 characters long.
        /// </remarks>
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "A name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the biography of the voice actor.
        /// </summary>
        /// <remarks>
        /// The Bio property is used to store the biography of the voice actor. The biography is required.
        /// </remarks>
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "A biography is required")]
        public string? Bio { get; set; }


        //Relationships
        ///<summary> 
        ///Gets or sets the list of games that this voice actor has worked on.
        ///</summary>
        ///<remarks>
        ///
        /// </remarks>
        public List<VoiceActor_Game>? VoiceActors_Games { get; set; }    
    }
}
