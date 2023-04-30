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
            var data = await _service.GetAllAsync(); //Gets all platforms from the database which is returned to the var data. Data will be a IEnumerable<Platform>

            return View(data); // pass to the index view the data.
        }


        //Get: Platform/Create/
        public IActionResult Create()
        {
            return View(); //returns the create view.
        }

        //Post:
        // An asynchronous method that returns Task<IActionResult> called create that accepts a parameter of type Platform. This is the platform that was submitted from the create form and will be added to the database if it is valid.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Description, ReleaseDate, Price, PlatformDeveloper, ImageURL")] Platform platform)
        {
            //Checks to see if model from submission is not valid. If the model IsValid = false then return to create view with platform model.
            if (!ModelState.IsValid)
            {
                return View(platform);
            }

            await _service.AddAsync(platform); // add the platform to the databse 

            return RedirectToAction(nameof(Index)); //redirect the user to the index action.
        }

        [AllowAnonymous]
        //Get: Platform/Details/{id}?{rturnId}
        //This is a asynchronous of Task<IActionResult> method called details that accepts two parameters. The first being the id of the platform you want details for and the second, optional, parameter int called return id that is defualt to 0 if not given. Returns the view called Details and passes the data to the view. 
        public async Task<IActionResult> Details(int id, int returnId = 0)
        {
            var dataDetails = await _service.GetByIdAsync(id); //returns the data belonging to the id given.

            if (dataDetails == null) return View("NotFound"); //If data is null, then return not found view.

            TempData["ReturnId"] = returnId; // passes the returnId to view. This will be used to redirect the user back to game detials if they come from it.

            return View(dataDetails); //returns the view with the data.
        }

        //Get: Platform/Edit/{Id}
        //This is aynchronous of Task<IActionResult called Edit that will return the view for Edit and pass the current platform in the database with the id that matches. The method accepts one parameter of type int called id. This will be used to get the platform from the database with that id.
        public async Task<IActionResult> Edit(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id); //Returns the platform with the provided Id from the related database table.
            if (dataDetails == null) return View("NotFound"); //if the data returned is null, then return view not found to user.
            return View(dataDetails); //return the view Edit to the user with the model Platform
        }

        //Post: 
        //This asynchronous that returns a Task<IActionResult> called Edit, accepts two parameters with the first being an int called id. The id will be used to update the information in the database where the id matches. The final parameter is th model submitted from the edit form. 
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description, ReleaseDate, Price, PlatformDeveloper, ImageURL")] Platform platform)
        {
            //if the model state is not valid, then return the edit form and pass back the model information.
            if (!ModelState.IsValid)
            {
                return View(platform);
            }

            await _service.UpdateAsync(id, platform); // Update the data with the id with the new model informaiton.

            return RedirectToAction(nameof(Index)); // redirect the user to the index action.
        }

        //Get: Platform/Delete/{Id}
        //This async method that returns a Task<IActionResult> called Delete will accept a parameter of int called id. This is the id of the platform that will be deleted from the database.
        public async Task<IActionResult> Delete(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id);//gets the platform that has the id provided.
            if (dataDetails == null) return View("NotFound"); //if this returns a null, then return the not found view.
            return View(dataDetails); // return the view delete with the platform with the provided id. This will be used to ask for conformation before deleting it.
        }

        //Post:
        //After the user clicks submit in the delete conformation form, the platform will be deleted form the database.
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataDetails = await _service.GetByIdAsync(id); //get the platform with the given id.
            if (dataDetails == null) return View("NotFound"); // if null then return the not found view.

            await _service.DeleteAsync(id); //delete the platform with the id that matches.

            return RedirectToAction(nameof(Index)); //redirect the user to the index action.
        }
    }
}
