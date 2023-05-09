using GameStoreApp.Models;

namespace GameStoreApp.Data.ViewModel
{
    /// <summary>
    /// View model for select input dropdown menus data used in creating a new game.
    /// </summary>
    /// <remarks>
    /// This view model is used to store dropdown menu options for various fields used in creating a new game, including
    /// the game's publisher, developer, voice actors, platform, and rating. These select input dropdown menus options will be used in
    /// conjunction with Razor view for creating, editing and deleting a game by displaying select input form elements to the user for selecting these options.
    /// </remarks>
    public class NewGameDropdownVM
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="NewGameDropdownVM"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initialises the lists for the <see cref="Publisher"/>, <see cref="Developer"/>,
        /// <see cref="VoiceActor"/>, <see cref="Platform"/>, and <see cref="Rating"/> properties to empty lists.
        /// </remarks>
        public NewGameDropdownVM()
        {
            Publisher = new List<GamePublisher>();
            Developer = new List<GameDeveloper>();
            VoiceActor = new List<VoiceActor>();
            Platform = new List<Platform>();
            Rating = new List<GameRating>();
        }

        /// <summary>
        /// Gets or sets the list of available game publishers.
        /// </summary>
        /// <remarks>
        /// This property represents a list of available game publishers. It will be populated from a database, and is used to populate the select input for selecting a game publisher when creating a new game.
        /// </remarks>
        public List<GamePublisher> Publisher { get; set; }

        /// <summary>
        /// Gets or sets the list of available game developers.
        /// </summary>
        /// <remarks>
        /// This property represents a list of available game developers. It will be populated from the database, and is used to populate the select input for selecting a game developer when creating a new game.
        /// </remarks>
        public List<GameDeveloper> Developer { get; set; }

        /// <summary>
        /// Gets or sets the list of available voice actors.
        /// </summary>
        /// <remarks>
        /// This property represents a list of available voice actors. It will be populated from the database, and is used to populate the select input for selecting a voice actor when creating a new game.
        /// </remarks>
        public List<VoiceActor> VoiceActor { get; set; }

        /// <summary>
        /// Gets or sets the list of available game platforms.
        /// </summary>
        /// <remarks>
        /// This property represents a list of available game platforms. It will be populated from the database, and is used to populate the select input data for selecting a game platform when creating a new game.
        /// </remarks>
        public List<Platform> Platform { get; set; }

        /// <summary>
        /// Gets or sets the list of available game ratings.
        /// </summary>
        /// <remarks>
        /// This property represents a list of available game ratings. It will be populated from a database, and is then used to populate a the select input for selecting a game rating when creating a new game.
        /// </remarks>
        public List<GameRating> Rating { get; set; }
    }
}
