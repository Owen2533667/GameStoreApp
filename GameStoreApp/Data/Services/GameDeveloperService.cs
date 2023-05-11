using GameStoreApp.Data.Base;
using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    /// <summary>
    /// Represents a service for managing game developers.
    /// </summary>
    public class GameDeveloperService : EntityBaseRepository<GameDeveloper>, IGameDeveloperService
    {
        private readonly GameStoreAppDbContext _context; // a private readonly attribute of GameStoreAppDbContext.


        /// <summary>
        /// Initializes a new instance of the <see cref="GameDeveloperService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GameDeveloperService(GameStoreAppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a game developer by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the game developer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the game developer with the specified ID.</returns>
        /// <remarks>This method uses Entity Framework Core to retrieve the game developer from the database.</remarks>
        public async Task<GameDeveloper> GetDeveloperByIdAsync(int id)
            {
                // Retrieve the game developer from the database using Entity Framework Core. 
                var data = await _context.GameDevelopers
                    .Include(g => g.Games)!.ThenInclude(r => r.GameRating) // include the list of games that has relationship with developer and the game rating with the result.
                    .FirstOrDefaultAsync(x => x.Id == id); // find the developer that has the matching id.

                return data!; // return data
        }
    }
}
