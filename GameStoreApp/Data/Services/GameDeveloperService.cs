using GameStoreApp.Data.Base;
using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    public class GameDeveloperService : EntityBaseRepository<GameDeveloper>, IGameDeveloperService
    {
        private readonly GameStoreAppDbContext _context;

        public GameDeveloperService(GameStoreAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GameDeveloper> GetDeveloperByIdAsync(int id)
            {
                var data = await _context.GameDevelopers
                    .Include(g => g.Games)!.ThenInclude(r => r.GameRating)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return data!;
        }
    }
}
