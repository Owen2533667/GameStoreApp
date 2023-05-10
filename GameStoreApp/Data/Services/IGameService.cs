using GameStoreApp.Data.Base;
using GameStoreApp.Data.ViewModel;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    /// <summary>
    /// Represents an interface for managing games. This interface extends the functionality provided by the <see cref="IEntityBaseRepository{T}"/> interface, specialised for the <see cref="Game"/> entity type.
    /// </summary>
    /// <remarks>
    /// The <c>IGameService</c> interface defines methods for managing games, such as retrieving games by ID, retrieving dropdown values for creating new games, adding new games, and updating existing games.
    /// It inherits the basic repository operations from the <see cref="IEntityBaseRepository{T}"/> interface, providing additional game-specific functionality.
    /// Implementations of this interface are responsible for interacting with the underlying data store to perform the required operations.
    /// </remarks>
    public interface IGameService : IEntityBaseRepository<Game>
    {
        /// <summary>
        /// Retrieves a game by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the game to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The retrieved game.</returns>
        /// <remarks>
        /// This method retrieves a game from the db using the provided ID.
        /// If a game with the specified ID exists, it is returned; otherwise, the returned value is null.
        /// </remarks>
        Task<Game> GetGameByIdAsync(int id);

        /// <summary>
        /// Retrieves dropdown values for creating a new game asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The dropdown values for a new game.</returns>
        /// <remarks>
        /// This method retrieves the dropdown values required for creating a new game.
        /// It returns a <see cref="NewGameDropdownVM"/> object containing the available options for different properties.
        /// </remarks>
        Task<NewGameDropdownVM> GetNewGameDropDownValues();

        /// <summary>
        /// Adds a new game asynchronously with the provided data.
        /// </summary>
        /// <param name="data">The data to create the new game.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method creates a new game in the db with the provided data.
        /// The properties of the new game are populated with the corresponding values from the provided <see cref="NewGameVM"/> object.
        /// </remarks>
        Task AddNewGameAsync(NewGameVM data);

        /// <summary>
        /// Updates a game asynchronously with the provided data.
        /// </summary>
        /// <param name="data">The data to update the game.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method updates an existing game in db with the provided data.
        /// It retrieves the game based on the ID specified in the provided <see cref="NewGameVM"/> object.
        /// If a game with the specified ID exists, its properties are updated with the corresponding values from the provided object.
        /// Additionally, the associated voice actors and platforms are updated according to the provided data.
        /// </remarks>
        Task UpdateGameAsync(NewGameVM data);
    }
}
