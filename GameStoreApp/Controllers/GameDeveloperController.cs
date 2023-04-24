using GameStoreApp.Data;
using GameStoreApp.Data.Services;
using GameStoreApp.Data.Static;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class GameDeveloperController : Controller
    {
        //Private readonly attribute IGameDeveloperService, an interface that will interact to allow the retreival of data from the database.
        private readonly IGameDeveloperService _service;

        //constructor: A constructor that will accept a IGameDeveloperService and set _service to the one passed as parameter.
        public GameDeveloperController(IGameDeveloperService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        // Index: A asynchronous operation that will return result of an action method called Index.
        // ueses _service.GetAllAsync() to return all data from the related table. It then passes this to the view called Index.
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        [AllowAnonymous]
        //Get: /Details/{Id}
        //Details: A asynchronous operation that will return result of an action method called Details.
        //This action method accepts a id which is an int.
        public async Task<IActionResult> Details(int id)
        {
            //Gets a model object from the table GameDeveloper using an id passed as parameter to the action result method.
            var dataDetails = await _service.GetByIdAsync(id);

            //if the object retrieved is null then return the view "NotFound"
            if (dataDetails == null) return View("NotFound");

            //return the view Details with model.
            return View(dataDetails);
        }

        //Get: /Create
        // An IActionResult method called Create that returns the view Create.cshtml
        public IActionResult Create()
        {
            return View();
        }

        //HttpPost request that was sent from the Create.cshtml form.
        // A asynchronous method that accepts a GameDeveloper model. Also, bind the data from the form to model.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name, Description")] GameDeveloper developer)
        {
            //Checks to see if the model state passed is valid. If not then returns the create.cshtml view with the model passed as parameter.
            if (!ModelState.IsValid)
            {
                return View(developer);
            }

            //if the model state is valid then add model to the database related table with the _service method AddAsync() passing the model as a parameter.
            await _service.AddAsync(developer);

            //Return an redirectToAction that will redirect to the controller action method called Index.
            return RedirectToAction(nameof(Index));
        }

        //Get: /Edit/{id}
        // A asynchronous IActionResult method called Edit that accepts int called id.
        public async Task<IActionResult> Edit(int id)
        {
            //Uses _service.GetByIdAsync(id) to retreive the model data from the related table.
            var dataDetails = await _service.GetByIdAsync(id);
            //If that model is equal to null then return the notfound view.
            if (dataDetails == null) return View("NotFound");
            //if the model is ok, then return the Edit view with the model detials.
            return View(dataDetails);
        }

        //HttpPost: request that was sent from the Edit.cshtml form.
        // A asynchronous IActionResult method that accepts an id and a GameDeveloper object. Also, bind the data from the form to model.
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

        //Get: /delete/{id}
        // A asynchronous IActionResult method called Delete that accepts an id.
        public async Task<IActionResult> Delete(int id)
        {
            //Uses _service.GetByIdAsync(id) to retreive the model data from the related table.
            var dataDetails = await _service.GetByIdAsync(id);
            //If that model is equal to null then return the notfound view.
            if (dataDetails == null) return View("NotFound");
            //Returns the data to the Delete View
            return View(dataDetails);
        }

        //HttpPost: when the use clicks confirm on the delete view the request performs this action.
        // A asynchronous IActionResult method called DeleteConfirmed that accepts an id.
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //gets the data using the id.
            var dataDetails = await _service.GetByIdAsync(id);
            //checks to see if the data is not null, if so return the notfound view.
            if (dataDetails == null) return View("NotFound");

            //delete the data from the related table using the id provided.
            await _service.DeleteAsync(id);

            //redirect to the index action method.
            return RedirectToAction(nameof(Index));
        }
    }
}
