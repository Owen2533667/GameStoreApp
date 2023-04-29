using GameStoreApp.Data.Base;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    public class PlatformService : EntityBaseRepository<Platform>, IPlatformService
    {
        public PlatformService(GameStoreAppDbContext context) : base(context)
        {
            
        }
    }
}
