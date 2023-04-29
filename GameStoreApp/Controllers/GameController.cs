using GameStoreApp.Data;
using GameStoreApp.Data.Services;
using GameStoreApp.Data.Static;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class GameController : Controller
    {
        
        private readonly IGameService _service;

        //constructor
        public GameController(IGameService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int pg=1)
        {
            var games = await _service.GetAllAsync(p => p.GamePublisher, d => d.GameDeveloper, r => r.GameRating); //uses the getAllAsync method from the IGameService and passes two parameters that will be included in the 

            const int pageSize = 10; //set the page size to 10

            //Checks to see if the parameter given to index is less than one.
            if (pg < 1) // if pg is less then 1, set pg back to one. 
                pg = 1;

            //gets the totalItems for the pager count by getting the count of all elements in games.
            int totalItems = games.Count();

            //creates a new Pager object with the parameters totalItems, page, and pageSize (this will be 10)
            var pager = new Pager(totalItems, pg, pageSize);

            //the current page number - 1 times pagesize
            int recSkip = (pg - 1) * pageSize;

            //pagerData will be the next element that will be displayed. 
            var pagerData = games.Skip(recSkip).Take(pager.PageSize).ToList();//game.skip will return the elements that remain after bypassing elements that was specified. After that, The take will take the next specified elements (10) and then return this a list.

            //send the pager information to the view using the view bag.
            this.ViewBag.Pager = pager;

            //return View(Games);
            //return to the view of index with the pagerData.
            return View(pagerData);
        }

        [AllowAnonymous]
        //GET: Game/Detail/{id}
        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetGameByIdAsync(id);

            if (data == null) return View("NotFound");

            return View(data);
        }


        //GET Game/Create
        public async Task<IActionResult> Create()
        {
            var gameDropdownData = await _service.GetNewGameDropDownValues();

            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

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
                ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
                ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

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
                GameRatingId = data.GameRatingId,
                GamePublisherId = data.GamePublisherId,
                GameDeveloperId = data.GameDeveloperId,
                VoiceActorIds = data.VoiceActors_Games.Select(x => x.VoiceActorId).ToList(),
                PlatformIds = data.Platforms_Games.Select(x => x.PlatformId).ToList(),
            };


            var gameDropdownData = await _service.GetNewGameDropDownValues();

            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

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
                ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
                ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

                return View(game);
            }

            await _service.UpdateGameAsync(game);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
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

        //Get: 
        public async Task<IActionResult> Delete(int id)
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
                GameRatingId = data.GameRatingId,
                GamePublisherId = data.GamePublisherId,
                GameDeveloperId = data.GameDeveloperId,
                VoiceActorIds = data.VoiceActors_Games.Select(x => x.VoiceActorId).ToList(),
                PlatformIds = data.Platforms_Games.Select(x => x.PlatformId).ToList(),
            };


            var gameDropdownData = await _service.GetNewGameDropDownValues();

            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataDetails = await _service.GetGameByIdAsync(id);
            if (dataDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
