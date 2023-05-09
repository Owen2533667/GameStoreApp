using GameStoreApp.Data.Base;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    /// <summary>
    /// Represents a service for managing game developers.
    /// </summary>
    public interface IGameDeveloperService : IEntityBaseRepository<GameDeveloper>
    {
        /// <summary>
        /// Gets a game developer by ID.
        /// </summary>
        /// <param name="id">The ID of the game developer.</param>
        /// <returns>The game developer with the specified ID.</returns>
        Task<GameDeveloper> GetDeveloperByIdAsync(int id);
    }
}
