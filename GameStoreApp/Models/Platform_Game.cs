namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a relationship between a game and a platform. 
    /// </summary>
    /// <remarks>This class is a joining class between the game and platform classes. This is also a table in the db, that will model the relationship between the two tables. This class contains the game id, game object, platform id, and the platform object. </remarks>
    public class Platform_Game
    {
        /// <summary>
        /// Gets or sets the game identifier.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        public Game? Game { get; set; }

        /// <summary>
        /// Gets or sets the platform identifier.
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        public Platform? Platform { get; set; }
    }
}
