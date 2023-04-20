using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class GameDeveloper
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Developer Logo")]
        public string Logo { get; set; }
        [Display(Name = "Developer Name")]
        public string Name { get; set; }
        [Display(Name = "Developer Description")]
        public string Description { get; set; }

        //Relationships
        public List<Game> Games { get; set; }
    }
}
