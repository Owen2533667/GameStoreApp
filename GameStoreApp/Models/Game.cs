using GameStoreApp.Data.Base;
using GameStoreApp.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreApp.Models
{
    public class Game : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }
        public string? ImageURL { get; set; }
        public GameGenre GameGenre { get; set; }

        //Relationships
        public List<VoiceActor_Game> VoiceActors_Games { get; set; }

        public List<Platform_Game> Platforms_Games { get; set; }

        //Publisher
        public int GameRatingId { get; set; }
        [ForeignKey("GameRatingId")]
        public GameRating GameRating { get; set; }

        //Developer
        public int GameDeveloperId { get; set; }
        [ForeignKey("GameDeveloperId")]
        public GameDeveloper GameDeveloper { get; set; }

        //Publisher
        public int GamePublisherId { get; set; }
        [ForeignKey("GamePublisherId")]
        public GamePublisher GamePublisher { get; set; }


    }
}
