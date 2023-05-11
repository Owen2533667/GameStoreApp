using GameStoreApp.Data.Base;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    /// <summary>
    /// Represents a service for managing game publishers.
    /// Inherits the <see cref="IEntityBaseRepository{T}"/> interface with the type parameter <see cref="GamePublisher"/>.
    /// </summary>
    public interface IGamePublisherService : IEntityBaseRepository<GamePublisher>
    {
        /// <summary>
        /// Retrieves a game publisher by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the game publisher to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The retrieved game publisher.</returns>
        Task<GamePublisher> GetPublisherByIdAsync(int id);
    }
}
