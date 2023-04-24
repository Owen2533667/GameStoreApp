using GameStoreApp.Data.Base;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    public class GamePublisherService : EntityBaseRepository<GamePublisher>, IGamePublisherService
    {
        public GamePublisherService(GameStoreAppDbContext context) : base(context)
        {

        }
    }
}
