using GameStoreApp.Data.Base;
using GameStoreApp.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Repsents a game that will be displayed and sold on the IGame e-commerce application.
    /// </summary>
    /// <remarks>This class implements <seealso cref="IEntityBase"/> interface</remarks>
    public class Game : IEntityBase
    {
        /// <summary>
        ///  Gets or sets the Game identifier.
        /// </summary>
        /// <remarks>The Id property is used as the primary key of the Game entity.</remarks>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Price { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string? ImageURL { get; set; }

        /// <summary>
        /// Gets or sets the genre of the game.
        /// </summary>
        /// <remarks>
        /// The game genre is an GameGenre enum value. If you want to see what genres there are go to the GameGenre enum at <seealso cref="GameGenre"/> 
        /// </remarks>
        public GameGenre GameGenre { get; set; }

        //Relationships

        ///<summary> 
        ///Gets or sets the list of voice actors that worked on this game.
        ///</summary> 
        /// <remarks>
        /// This will be provided from the entity joining table between game and voice actor where the game id matches the game id of the current object instance.
        /// </remarks>
        public List<VoiceActor_Game>? VoiceActors_Games { get; set; }

        ///<summary> 
        ///Gets or sets the list of platforms that this game is available on.
        ///</summary> 
        /// <remarks>
        /// This will be provided from the entity joining table between game and platform where the game id matches the game id of the current object instance.
        /// </remarks>
        public List<Platform_Game>? Platforms_Games { get; set; }

        //Publisher
        /// <summary>
        /// Gets or sets the ID of the game rating.
        /// </summary>
        /// <remarks>
        /// The GameRatingId property is used as a foreign key to the GameRating entity.
        /// </remarks>
        public int GameRatingId { get; set; }

        /// <summary>
        /// Gets or sets the game rating.
        /// </summary>
        [ForeignKey("GameRatingId")]
        public GameRating? GameRating { get; set; }

        //Developer
        /// <summary>
        /// Gets or sets the ID of the game developer.
        /// </summary>
        /// <remarks>
        /// The GameDeveloperId property is used as a foreign key to the GameDeveloper entity.
        /// </remarks>
        public int GameDeveloperId { get; set; }

        /// <summary>
        /// Gets or sets the game developer.
        /// </summary>
        [ForeignKey("GameDeveloperId")]
        public GameDeveloper? GameDeveloper { get; set; }

        //Publisher
        /// <summary>
        /// Gets or sets the ID of the game publisher.
        /// </summary>
        /// <remarks>
        /// The GamePublisherId property is used as a foreign key to the GamePublisher entity.
        /// </remarks>
        public int GamePublisherId { get; set; }

        /// <summary>
        /// Gets or sets the game publisher.
        /// </summary>
        [ForeignKey("GamePublisherId")]
        public GamePublisher? GamePublisher { get; set; }
    }
}
