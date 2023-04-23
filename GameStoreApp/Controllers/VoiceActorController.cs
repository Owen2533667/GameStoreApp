using GameStoreApp.Data;
using GameStoreApp.Data.Services;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreApp.Controllers
{
    public class VoiceActorController : Controller
    {

        private readonly IVoiceActorService _service;

        //constructor
        public VoiceActorController(IVoiceActorService service)
        {
            _service = service;
        }

        //Index: An async IActionResult for Index that will get all voice actor data from the database and pass that data to the index view.
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
        public async Task<IActionResult> Create([Bind("FullName,PictureURL,Bio")] VoiceActor va)
        {
            //Checks to see if model from submission is not valid. If the model IsValid = false then return to create view with VoiceActor model.
            if(!ModelState.IsValid)
            {
                return View(va);
            }

            await _service.AddAsync(va);

            return RedirectToAction(nameof(Index));
        }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName,PictureURL,Bio")] VoiceActor va)
        {

            if (!ModelState.IsValid)
            {
                return View(va);
            }

            await _service.UpdateAsync(id, va);

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
