using GameStoreApp.Data;
using GameStoreApp.Data.Services;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Controllers
{
    public class GameDeveloperController : Controller
    {

        private readonly IGameDeveloperService _service;

        //constructor
        public GameDeveloperController(IGameDeveloperService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get:
        public async Task<IActionResult> Details(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id);

            if (dataDetails == null) return View("NotFound");
            return View(dataDetails);
        }

        //Get: GameDeveloper/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name, Description")] GameDeveloper developer)
        {
            //
            if (!ModelState.IsValid)
            {
                return View(developer);
            }

            await _service.AddAsync(developer);

            return RedirectToAction(nameof(Index));
        }

        //Get: GameDeveloper/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id);
            if (dataDetails == null) return View("NotFound");
            return View(dataDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Logo, Name, Description")] GameDeveloper developer)
        {
            //Checks to see if model state is valid, if not, then return edit form with developer details
            if (!ModelState.IsValid) return View(developer);

            if (id == developer.Id)
            {
                // async update of model with id provided with the new details provided.
                await _service.UpdateAsync(id, developer);

                //redirect to action index
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //Get: GameDeveloper/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id);
            if (dataDetails == null) return View("NotFound");
            return View(dataDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id);
            if (dataDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
