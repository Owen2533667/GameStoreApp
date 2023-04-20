using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    public class GamePublisher
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Publisher Logo")]
        public string Logo { get; set; }
        [Display(Name = "Publisher Name")]
        public string Name { get; set; }
        [Display(Name = "Publisher Description")]
        public string Description { get; set; }

        //Relationship
        public List<Game> Games { get; set; }

    }
}
