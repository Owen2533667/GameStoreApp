using GameStoreApp.Data.Base;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    public class GameRatingService : EntityBaseRepository<GameRating>, IGameRatingService
    {
        public GameRatingService(GameStoreAppDbContext context) : base(context)
        {
            
        }
    }
}
