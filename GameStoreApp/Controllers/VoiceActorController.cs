using GameStoreApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreApp.Controllers
{
    public class VoiceActorController : Controller
    {
        //private readonly attribute to get db context
        private readonly GameStoreAppDbContext _context;

        //constructor
        public VoiceActorController(GameStoreAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.VoiceActors.ToList();
            return View(data);
        }
    }
}
