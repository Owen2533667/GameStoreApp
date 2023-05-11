using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a user in the game store.
    /// </summary>
    /// <remarks>
    /// Inherits from IdentityUser.
    /// </remarks>
    public class GameStoreUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [Display(Name = "First Name")]
        public string? firstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        [Display(Name = "Family Name")]
        public string? lastName { get; set; }
    }
}
