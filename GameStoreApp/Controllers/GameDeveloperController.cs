using GameStoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Controllers
{
    public class GameDeveloperController : Controller
    {
        //private readonly attribute to get db context
        private readonly GameStoreAppDbContext _context;

        //constructor
        public GameDeveloperController(GameStoreAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.GameDevelopers.ToListAsync();
            return View(data);
        }
    }
}
