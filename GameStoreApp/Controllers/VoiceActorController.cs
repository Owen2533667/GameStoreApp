using GameStoreApp.Data;
using GameStoreApp.Data.Services;
using GameStoreApp.Data.Static;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class VoiceActorController : Controller
    {

        private readonly IVoiceActorService _service;

        //constructor
        public VoiceActorController(IVoiceActorService service)
        {
            _service = service;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        //Get: VoiceActor/Details/{Id}
        //Details: A asynchronous operation that returns a Task<IActionResult> called details. The method accepts two int values, the id of the voice actor to get and pass to the view. The other int is an optional with a defualt of 0. It will get this vaule from the game details, when the user clicks on the <a> for game voice actor. The second is used to store the return id, so when the user clcisk the back button it will return to the game detials with that id.
        public async Task<IActionResult> Details(int id , int returnId = 0)
        {
            var dataDetails = await _service.GetByIdAsync(id);

            if (dataDetails == null) return View("NotFound");

            //pass temp data of the return id to the view.
            TempData["ReturnId"] = returnId;

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
