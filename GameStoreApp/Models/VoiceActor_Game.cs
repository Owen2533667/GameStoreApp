namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a relationship between a Voice actor and a game. 
    /// </summary>
    /// <remarks>This class is a joining class between the game and Voice actor classes. This is also a table in the db, that will model the relationship between the two tables. This class contains the game id, game object, Voice Actor id, and the VoiceActor object. </remarks>
    public class VoiceActor_Game
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
        /// Gets or sets the VoiceActor identifier.
        /// </summary>
        public int VoiceActorId { get; set; }

        /// <summary>
        /// Gets or sets the VoiceActor.
        /// </summary>
        public VoiceActor?  VoiceActor { get; set; } 
    }
}
