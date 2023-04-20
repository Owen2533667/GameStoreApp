using GameStoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Controllers
{
    public class GameController : Controller
    {
        //private readonly attribute to get db context
        private readonly GameStoreAppDbContext _context;

        //constructor
        public GameController(GameStoreAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Games.Include(p => p.GamePublisher).Include(d => d.GameDeveloper).OrderBy(g => g.Name).ToListAsync();
            return View(data);
        }
    }
}
