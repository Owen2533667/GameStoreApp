using GameStoreApp.Data.Base;
using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    public class VoiceActorService : EntityBaseRepository<VoiceActor>, IVoiceActorService
    {


        public VoiceActorService(GameStoreAppDbContext context) : base(context) { }
    }
}
