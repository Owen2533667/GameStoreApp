using GameStoreApp.Data.Base;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    public class GameDeveloperService : EntityBaseRepository<GameDeveloper>, IGameDeveloperService
    {
        public GameDeveloperService(GameStoreAppDbContext context) : base(context)
        {
                
        }
    }
}
