using GameStoreApp.Data.Services;
using GameStoreApp.Data.Static;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GameStoreApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class PlatformController : Controller
    {
        //Private readonly attribute IPlatformService, an interface that will interact to allow the retreival of data from the database.
        private readonly IPlatformService _service;

        //constructor: A constructor that will accept a IPlatformService and set _service to the one passed as parameter.
        public PlatformController(IPlatformService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        //Index: An async IActionResult for Index that will get all platform data from the database and pass that data to the index view.
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();

            return View(data);
        }


        //Get: VoiceActors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Description, ReleaseDate, Price, PlatformDeveloper, ImageURL")] Platform platform)
        {
            //Checks to see if model from submission is not valid. If the model IsValid = false then return to create view with VoiceActor model.
            if (!ModelState.IsValid)
            {
                return View(platform);
            }

            await _service.AddAsync(platform);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        //Get:
        public async Task<IActionResult> Details(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id);

            if (dataDetails == null) return View("NotFound");
            return View(dataDetails);
        }

        //Get: 
        public async Task<IActionResult> Edit(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id);
            if (dataDetails == null) return View("NotFound");
            return View(dataDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description, ReleaseDate, Price, PlatformDeveloper, ImageURL")] Platform platform)
        {

            if (!ModelState.IsValid)
            {
                return View(platform);
            }

            await _service.UpdateAsync(id, platform);

            return RedirectToAction(nameof(Index));
        }

        //Get: 
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
