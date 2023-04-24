using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class GameStoreUser:IdentityUser
    {
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Family Name")]
        public string lastName { get; set; }
    }
}
