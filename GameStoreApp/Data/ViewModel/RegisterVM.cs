using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GameStoreApp.Data.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Family Name is required")]
        [Display(Name = "Family Name")]
        [DataType(DataType.Text)]
        public string? FamilyName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Comfirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Comfirm Password")]
        [Compare("Password", ErrorMessage = "Password do not match!")]
        public string? ComfirmPassword { get; set; }
    }
}
