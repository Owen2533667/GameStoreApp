using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Data.ViewModel
{
    /// <summary>
    /// View model for login information.
    /// </summary>
    /// <remarks>
    /// This view model is used to store login information, including the user's email address and password. It is typically
    /// used in conjunction with a Razor view to display a login form to the user. The <see cref="EmailAddress"/> property
    /// is required and must be a valid email address. The <see cref="Password"/> property is also required and is typically
    /// validated against a password policy before being accepted.
    /// </remarks>
    public class LoginVM
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <remarks>
        /// This property represents the email address of the user. It is marked as required using the
        /// <see cref="RequiredAttribute"/>, and its format is validated using the <see cref="DataTypeAttribute"/> with a
        /// value of <see cref="DataType.EmailAddress"/>. If the email address is not provided or is not in a valid format,
        /// an error message will be displayed to the user.
        /// </remarks>
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <remarks>
        /// This property represents the password of the user. It is marked as required using the
        /// <see cref="RequiredAttribute"/>, and its value is typically validated against a password policy before being
        /// accepted. The password is stored as plain text in this view model, but it should be hashed or otherwise
        /// encrypted before being stored in a database.
        /// </remarks>
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
