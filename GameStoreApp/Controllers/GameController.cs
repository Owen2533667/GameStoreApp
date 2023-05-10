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
    //Authrized users with role admin can access all of the controller actions.
    [Authorize(Roles = UserRoles.Admin)]
    public class GameController : Controller
    {
        
        private readonly IGameService _service; //The service interface that will be used to interact with the database as a datalayer.

        //constructor:
        public GameController(IGameService service)
        {
            _service = service;
        }

        /// <summary>
        /// The index action method will return the user to the index view with the collection of games from the database. Moreover, this view will act as an home page for the games e-commerece platform. This is an async method, that returns a task of IActionResult which will be the view Index for the game controller. It will send the game collection to the view as well. This method will allow anonymous users, meaning the user does not have to be authorised or logged in.
        /// </summary>
        /// <param name="pg">The page number of the current page. The default, if not provided, is one.</param>
        /// <returns>Index view for the game controller, with the pagered collection of games. This should be nine games.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index(int pg=1)
        {
            var games = await _service.GetAllAsync(p => p.GamePublisher!, d => d.GameDeveloper!, r => r.GameRating!); //uses the getAllAsync method from the IGameService and passes three parameters that will be included in the result

            const int pageSize = 9; //set the page size to 9. There will now only be 9 games displayed per page. This can be changed by updating this vaule.

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
            var pagerData = games.Skip(recSkip).Take(pager.PageSize).ToList();//game.skip will return the elements that remain after bypassing elements that was specified. After that, The take will take the next specified elements (9) and then return this a list.

            //send the pager information to the view using the view bag.
            this.ViewBag.Pager = pager;

            //return View(Games);
            //return to the view of index with the pagerData.
            return View(pagerData);
        }

        //Allows unautherized user to access this, even if the user is not logged in.
        [AllowAnonymous]
        //GET: Game/Detail/{id}?{returnController}?{returnId}
        //This async action method called details accepts three parameters. The first being the int called id, which will be the id number of the game you are trying to view details for.
        //the second parameter is optional string called returnController that has defualt of "Game", This will be used when clicking on a game from the developer/publisher and using the back button to return to that detial view of publisher or developer of game.
        //the last parameter is an int called pg, this will be used when the user clicks view details of a game using the index view, It sends the current page number so when the user clicks back it will send them to index at that page number.
        public async Task<IActionResult> Details(int id, string returnController = "Game", int pg=1)
        {
            var data = await _service.GetGameByIdAsync(id); //Gets the game that has the id passed to action.

            if (data == null) return View("NotFound"); //If the id passed returns null, send the user to "NotFound" View.

            TempData["returnController"] = returnController; //Send TempData of the controller.
            TempData["pg"] = pg; //Send the TempData of the pg number

            return View(data); //Send the user to details view with data.
        }


        //GET Game/Create
        /// <summary>
        /// An async action method called Create, that will be used to return the create view form for admin user, that will provide the select input data for the form.
        /// </summary>
        /// <returns>The create view</returns>
        public async Task<IActionResult> Create()
        {
            var gameDropdownData = await _service.GetNewGameDropDownValues(); //Gets the values that will be displayed in select inputs.

            //Uses ViewBag to send the new instances of SelectList, which will contain the revelent data for input field. Example: publisher will have an id and name sent to select input on create form.
            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name"); 
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

            return View(); //Return the create view for game to user.
        }

        /// <summary>
        /// This Create action method, accepts one parameter of NewGameVM, will take the Create Form submit request and update the database with the form data. This will allow the creation of games by an admin user.
        /// </summary>
        /// <param name="game">A NewGameVM, This will be the game that will be created and added to the database.</param>
        /// <returns>If successful, redirects user to action method called index. If the model passed is not valid, return the create form and send back the game model data.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(NewGameVM game)
        {
            //Checks to see if the game being created is valid. If not, send the user back to the create view with the select input data again, and the NewGameVM passed as parameter back.
            if (!ModelState.IsValid) 
            {
                var gameDropdownData = await _service.GetNewGameDropDownValues(); //Get the dropdown data for select inputs on the create form.

                //Send this data using the viewbag.
                ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
                ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
                ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
                ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
                ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

                return View(game); //Send user to create form with the game model passed.
            }

            await _service.AddNewGameAsync(game); //If the model is valid, then add the new game to the database. 

            return RedirectToAction("Index"); //Redirect the user to the index action.
        }

        //GET Game/Edit/{id}
        /// <summary>
        /// An async action method called edit that will allow for a game to be updated using the id passed as a parameter. This action method will only send the admin user to the edit view, with
        /// the game with the matching id passed as a parameter. It will send the game with the id to the edit form, so the user can see and update. Moreover, it will send the select input data to the edit form, so
        /// that the user can select data for the select inputs.
        /// </summary>
        /// <param name="id">The id of the game that will be updated to the database</param>
        /// <returns>task of an action result. In this case, returns the view edit sending the game details.</returns>
        public async Task<IActionResult> Edit(int id)
        {

            var data = await _service.GetGameByIdAsync(id); //Get the game from the database with the given id.

            if (data == null) return View("NotFound"); //if data is null, meaning no game with id found, return the "NotFound" view.

            var response = new NewGameVM() //Create an instance of NewGameVM and set the values as the same the game retreived from the database.
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
                VoiceActorIds = data.VoiceActors_Games.Select(x => x.VoiceActorId).ToList(), // returns all voice actors ids to a list of int.
                PlatformIds = data.Platforms_Games.Select(x => x.PlatformId).ToList(), // returns all platforms ids to a list of int.
            };


            var gameDropdownData = await _service.GetNewGameDropDownValues(); //get the dropdown data form the select inputs.

            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

            return View(response); //return the edit view and send response.
        }

        [HttpPost]
        /// <summary>
        ///  This async action method will receive data from an HttpPost request submitted from the edit form. The action method accepts two parameters, a int called id and A NewGameVM that will be the updated game details.
        ///  This action method will update the game, in the database, with matching id to the updated game values passed as second param.
        /// </summary>
        /// <param name="id">The id of the game being edited</param>
        /// <param name="game">A NewGameVM which will be the new game details that will be updated to the game with the id passed.</param>
        /// <returns>Task<IActionResult>, which is a redirect to action of index.</returns>
        public async Task<IActionResult> Edit(int id, NewGameVM game)
        {
            if (id != game.Id) return View("NotFound"); //If the id passed does not match the id of NewGameVM passed, then return the "NotFound" view.

            if (!ModelState.IsValid) //Checks to see if model is valid. If not then Send the admin user back to edit view with game passed and select input data.
            {
                var gameDropdownData = await _service.GetNewGameDropDownValues();

                ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
                ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
                ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
                ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
                ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

                return View(game); 
            }

            await _service.UpdateGameAsync(game); //Update game with the game passed.

            return RedirectToAction("Index"); //Redirect to action "Index"
        }


        /// <summary>
        /// AllowsAnonymous meaning that unautherised users can use this action
        /// </summary>
        /// <param name="searchString"> A string that will be used to filter the list of games by.</param>
        /// <returns>An Task<IActionResult>, this will return the view "Index" with the filtered list of games.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            //Gets all games, and include properties game publisher, game developer and game rating
            var data = await _service.GetAllAsync(p => p.GamePublisher, d => d.GameDeveloper, r => r.GameRating);

            //if search string is not null or empty
            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredData = data.Where(x => x.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Description!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    
                return View("Index", filteredData);
            }

            return View("Index", data);
        }

        //Get:
        /// <summary>
        /// A delete action method that will take the authorised admin user to an delete conformation screen of a game with the id provided. This is an async method that will return a task of iactionresult which is the return of the delete view for games controller. 
        /// </summary>
        /// <param name="id">The id of the game that the admin wants to delete.</param>
        /// <returns>The delete view for the games controller with the game data.</returns>
        public async Task<IActionResult> Delete(int id)
        {

            var data = await _service.GetGameByIdAsync(id); //Get the game where the id passed to action matches game in the database id.

            if (data == null) return View("NotFound"); //if no game with id found, meaning the data is null, then return the view "NotFound".

            var response = new NewGameVM() //creates an new instance of NewGameVM
            {
                //set the response (NewGameVM) instance properties to the game instance called data, this was retreived from the database using the id provided. 
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
                VoiceActorIds = data.VoiceActors_Games.Select(x => x.VoiceActorId).ToList(), //Select all voice actor ids to a list.
                PlatformIds = data.Platforms_Games.Select(x => x.PlatformId).ToList(), //Select all platform ids to a list.
            };


            var gameDropdownData = await _service.GetNewGameDropDownValues(); //get the dropdown data that will be displayed in the select inputs on the delete view. 

            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

            return View(response); //return the delete view and send the response data.
        }

        /// <summary>
        /// This action method will be called when the admin user clicks submit on the delete form in the delete view, conforming that they want to delte the game. This is an async method that returns an task of IActionResult and redirects the user to the action index.
        /// </summary>
        /// <param name="id">The id of the game being deleted.</param>
        /// <returns>If the game was deleted, then redirect to the action index. If the game id was not found, then returns "NotFound" view.</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataDetails = await _service.GetGameByIdAsync(id); // get the game from the db which the id that matches the id passed.
            if (dataDetails == null) return View("NotFound"); //if the data is null then return the "NotFound" View.

            await _service.DeleteAsync(id); //Delete the game from the db, with the id passed.

            return RedirectToAction(nameof(Index)); //Redirect the user to the index action method.
        }

    }
}
