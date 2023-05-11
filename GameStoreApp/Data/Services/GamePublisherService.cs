using GameStoreApp.Data.Base;
using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    /// <summary>
    /// Represents a service for managing game publishers.
    /// </summary>
    public class GamePublisherService : EntityBaseRepository<GamePublisher>, IGamePublisherService
    {
        private readonly GameStoreAppDbContext _context;

        /// <summary>
        /// Initialises a new instance of the <see cref="GamePublisherService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GamePublisherService(GameStoreAppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a game publisher by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the game publisher to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the game publisher with the specified ID, or null if not found.
        /// </returns>
        public async Task<GamePublisher> GetPublisherByIdAsync(int id)
        {
            var data = await _context.GamePublishers
                .Include(g => g.Games)!.ThenInclude(r => r.GameRating) // Include related Games and their GameRating
                .FirstOrDefaultAsync(x => x.Id == id); // Find the first game publisher with the specified ID

            return data!;
        }
    }
}
