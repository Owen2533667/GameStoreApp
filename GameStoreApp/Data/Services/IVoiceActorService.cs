using GameStoreApp.Data.Base;
using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    public interface IVoiceActorService:IEntityBaseRepository<VoiceActor>
    {
    }
}
