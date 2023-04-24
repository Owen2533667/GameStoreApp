using GameStoreApp.Data;
using GameStoreApp.Data.Services;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Controllers
{
    public class GameController : Controller
    {
        
        private readonly IGameService _service;

        //constructor
        public GameController(IGameService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(p => p.GamePublisher, d => d.GameDeveloper);
            return View(data);
        }

        //GET: Game/Detail/{id}
        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetGameByIdAsync(id);

            return View(data);
        }

        //GET Game/Create
        public async Task<IActionResult> Create()
        {
            var gameDropdownData = await _service.GetNewGameDropDownValues();

            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewGameVM game)
        {
            if (!ModelState.IsValid) 
            {
                var gameDropdownData = await _service.GetNewGameDropDownValues();

                ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
                ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
                ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");

                return View(game);
            }

            await _service.AddNewGameAsync(game);

            return RedirectToAction("Index");
        }

        //GET Game/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {

            var data = await _service.GetGameByIdAsync(id);

            if (data == null) return View("NotFound");

            var response = new NewGameVM() 
            { 
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                ReleaseDate = data.ReleaseDate,
                GameGenre = data.GameGenre,
                GamePublisherId = data.GamePublisherId,
                GameDeveloperId = data.GameDeveloperId,
                VoiceActorIds = data.VoiceActors_Games.Select(x => x.VoiceActorId).ToList(),
            };


            var gameDropdownData = await _service.GetNewGameDropDownValues();

            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewGameVM game)
        {
            if (id != game.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var gameDropdownData = await _service.GetNewGameDropDownValues();

                ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
                ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
                ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");

                return View(game);
            }

            await _service.UpdateGameAsync(game);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _service.GetAllAsync(p => p.GamePublisher, d => d.GameDeveloper);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredData = data.Where(x => x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    
                return View("Index", filteredData);
            }

            return View("Index", data);
        }

    }
}
