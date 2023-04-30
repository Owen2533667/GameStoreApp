using GameStoreApp.Data.Base;
using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    public class GamePublisherService : EntityBaseRepository<GamePublisher>, IGamePublisherService
    {
        private readonly GameStoreAppDbContext _context;

        public GamePublisherService(GameStoreAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GamePublisher> GetPublisherByIdAsync(int id)
        {
            var data = await _context.GamePublishers
                .Include(g => g.Games)!.ThenInclude(r => r.GameRating)
                .FirstOrDefaultAsync(x => x.Id == id);

            return data;
        }
    }
}
