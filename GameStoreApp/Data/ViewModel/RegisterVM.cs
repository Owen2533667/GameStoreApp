using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GameStoreApp.Data.ViewModel
{
    /// <summary>
    /// Represents the view model for user registration.
    /// </summary>
    /// <remarks>
    /// The <c>RegisterVM</c> class serves as a view model for user registration. It contains properties that capture the user's registration information, such as their first name, family name, email address, password, and confirmation password.
    /// </remarks>
    public class RegisterVM
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        /// <value>The first name of the user.</value>
        /// <remarks>
        /// This property represents the first name of the user. It is a required field for registration and must be provided by the user.
        /// </remarks>
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the family name of the user.
        /// </summary>
        /// <remarks>
        /// This field represents the family name of the user. It is a required field for registration.
        /// </remarks>
        [Required(ErrorMessage = "Family Name is required")]
        [Display(Name = "Family Name")]
        [DataType(DataType.Text)]
        public string? FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <remarks>
        /// This field represents the email address of the user. It is a required field for registration and should be in a valid email format.
        /// </remarks>
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <remarks>
        /// This field represents the password of the user. It is a required field for registration and should be kept secure.
        /// </remarks>
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password of the user.
        /// </summary>
        /// <remarks>
        /// This field represents the confirmation password of the user. It is a required field for registration and should match the password entered.
        /// </remarks>
        [Required(ErrorMessage = "Comfirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Comfirm Password")]
        [Compare("Password", ErrorMessage = "Password do not match!")]
        public string? ComfirmPassword { get; set; }
    }
}
