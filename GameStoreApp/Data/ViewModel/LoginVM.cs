using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Data.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
