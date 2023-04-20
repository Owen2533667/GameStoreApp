using GameStoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Controllers
{
    public class GamePublisherController : Controller
    {
        //private readonly attribute to get db context
        private readonly GameStoreAppDbContext _context;

        //constructor
        public GamePublisherController(GameStoreAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.GamePublishers.ToListAsync();
            return View(data);
        }
    }
}
