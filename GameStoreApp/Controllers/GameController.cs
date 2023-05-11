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
        private readonly ILogger<GameController> _logger;

        //constructor:
        public GameController(IGameService service, ILogger<GameController> logger)
        {
            _service = service;
            _logger = logger;

        }

        /// <summary>
        /// Retrieves and displays a paginated list of games.
        /// </summary>
        /// <param name="pg">The page number.</param>
        /// <returns>An asynchronous task that represents the operation. Returns the index view with the paginated list of games.</returns>
        /// <remarks>
        /// This action is accessible to all users (anonymous and authenticated) due to the <seealso cref="AllowAnonymousAttribute"/> data annotation.
        /// It retrieves all games asynchronously using the <see cref="IGameService.GetAllAsync"/> method, including related entities like game publisher, game developer, and game rating. The list of games is paginated, with a default page size of 9. If the specified page number is less than 1, it is adjusted to 1. The total number of games is determined, and a pager object is created to handle pagination. The appropriate range of games is retrieved based on the current page number and page size. The pager object is assigned to the ViewBag to be accessed in the view. Finally, the view with the paginated list of games is returned.
        /// </remarks>
        [AllowAnonymous]
        public async Task<IActionResult> Index(int pg=1)
        {
            // Retrieve all games asynchronously with related entities using the GetAllAsync method
            var games = await _service.GetAllAsync(p => p.GamePublisher!, d => d.GameDeveloper!, r => r.GameRating!); 

            const int pageSize = 9; // Set page size to 9

            // Adjust the page number if it is less than 1
            if (pg < 1) 
                pg = 1;

            int totalItems = games.Count(); // Get the total amount of games

            // Create a pager object to handle pagination, passing the total items, page number and size of page.
            var pager = new Pager(totalItems, pg, pageSize);
            
            int recSkip = (pg - 1) * pageSize; // Calculates what index of the next 9 elements to get.

            // Retrieve the appropriate range of games based on the recSkip index calculated and take the next game elements using the page size value.
            var pagerData = games.Skip(recSkip).Take(pager.PageSize).ToList();

            // Assign the pager object to the ViewBag to be accessed in the view
            this.ViewBag.Pager = pager;

            // Return the view with the paginated list of games
            return View(pagerData);
        }

        /// <summary>
        /// Retrieves and displays the details of a game.
        /// </summary>
        /// <param name="id">The ID of the game.</param>
        /// <param name="returnController">The name of the return controller.</param>
        /// <param name="pg">The page number.</param>
        /// <returns>An asynchronous task that represents the operation. If the game is found, it returns the view with the game details; otherwise, returns the "NotFound" view.</returns>
        /// <remarks>
        /// This action is accessible to all users (anonymous and authenticated) due to the <seealso cref="AllowAnonymousAttribute"/> data annotation. It retrieves the game asynchronously using the <see cref="IGameService.GetGameByIdAsync"/> method. If the game is not found, it logs a warning with information about the user, game ID, and IP address. If the game is found, it sets the return controller and page number in the TempData dictionary. It then logs an information message with details about the user, game ID, and IP address. Finally, it returns the view with the game details.
        /// </remarks>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string returnController = "Game", int pg=1)
        {
            // Retrieve the game asynchronously using the GetGameByIdAsync method
            var data = await _service.GetGameByIdAsync(id); 

            if (data == null)
            {
                // Log a warning if the game is not found, including user, game ID, and IP address information
                _logger.LogWarning($"{User.Identity!.Name ?? "Guest"} tried to view details of Game with {id} at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"} but failed. [Reason: Game id not found]");

                // Return the "NotFound" view if the game is not found
                return View("NotFound"); 
            }

            // Set the return controller and page number in the TempData dictionary
            TempData["returnController"] = returnController; 
            TempData["pg"] = pg;

            // Log an information message with details about the user, game ID, and IP address
            _logger.LogInformation($"{User.Identity!.Name ?? "Guest"} viewed details of Game: [ID = {data.Id}, Name = {data.Name}, Price = {data.Price.ToString("c")}] at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"}");

            // Return the view with the game detail
            return View(data); 
        }


        //GET Game/Create
        /// <summary>
        /// Displays the create form for a new game.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation. Returns the create view for a new game.</returns>
        /// <remarks>
        /// <para>
        /// This action is asynchronous and retrieves the necessary dropdown data for select inputs using the <see cref="IGameService.GetNewGameDropDownValues"/> method.
        /// It sets up select lists for each input field by creating instances of <see cref="SelectList"/> with the retrieved data.
        /// The select lists are assigned to the <see cref="ViewBag"/> properties to make them available in the view.
        /// Finally, the action returns the create view for a new game to the user.
        /// </para>
        /// </remarks>
        public async Task<IActionResult> Create()
        {
            // Retrieve the dropdown data for the new game
            var gameDropdownData = await _service.GetNewGameDropDownValues();

            // Set up the select lists for the input fields using the retrieved dropdown data
            // Each select list is created with the appropriate data source and value/text field mappings
            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name"); 
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

            // Return the create view for a new game to the user
            return View(); 
        }

        /// <summary>
        /// Handles the HTTP POST request for creating a new game.
        /// </summary>
        /// <param name="game">The data for the new game.</param>
        /// <returns>An asynchronous task that represents the operation. Returns the index view if the model is valid; otherwise, returns the create view with validation errors.</returns>
        /// <remarks>
        /// <para>
        /// This action is triggered when a POST request is made to create a new game. It expects the data for the new game to be provided in the <paramref name="game"/> parameter.
        /// </para>
        /// <para>
        /// If the model state is not valid, indicating validation errors, the action retrieves the necessary dropdown data for select inputs using the <see cref="IGameService.GetNewGameDropDownValues"/> method.
        /// It sets up select lists for each input field by creating instances of <see cref="SelectList"/> with the retrieved data.
        /// The select lists are assigned to the <see cref="ViewBag"/> properties to make them available in the view.
        /// The action also logs a warning message and returns the create view with the provided <paramref name="game"/> and validation errors.
        /// </para>
        /// <para>
        /// If the model state is valid, the action adds the new game asynchronously using the <see cref="IGameService.AddNewGameAsync"/> method.
        /// It logs an information message indicating the user who added the game and redirects to the index view.
        /// </para>
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create(NewGameVM game)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid) 
            {
                // Retrieve the dropdown data for the new game
                var gameDropdownData = await _service.GetNewGameDropDownValues();

                // Set up the select lists for the input fields using the retrieved dropdown data
                // Each select list is created with the appropriate data source and value/text field mappings
                ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
                ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
                ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
                ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
                ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

                // Log a warning message indicating the failed game creation attempt
                _logger.LogWarning($"{User.Identity!.Name} tried to create Game at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"} but failed. [Reason: Game model was not valid]");

                // Return the create view with the provided game and validation errors
                return View(game); 
            }

            // Add the new game asynchronously using the provided game data
            await _service.AddNewGameAsync(game);

            // Log an information message indicating the successful game creation
            _logger.LogInformation($"{User.Identity!.Name} added a Game called {game.Name} at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"}.");

            // Redirect to the index action
            return RedirectToAction("Index"); 
        }

        //GET Game/Edit/{id}
        /// <summary>
        /// Displays the edit form for a game with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the game to edit.</param>
        /// <returns>An asynchronous task that represents the operation. Returns the edit view for the specified game if found; otherwise, returns the not found view.</returns>
        /// <remarks>
        /// <para>
        /// This action retrieves the game with the specified ID asynchronously using the <see cref="IGameService.GetGameByIdAsync"/> method. If the game is found, it creates a new instance of <see cref="NewGameVM"/> and populates it with the data from the retrieved game.
        /// </para>
        /// <para>
        /// The action also retrieves the necessary dropdown data for select inputs using the <see cref="IGameService.GetNewGameDropDownValues"/> method. It sets up select lists for each input field by creating instances of <see cref="SelectList"/> with the retrieved data. The select lists are assigned to the <see cref="ViewBag"/> properties to make them available in the view.
        /// </para>
        /// <para>
        /// Finally, the action returns the edit view for the specified game, passing the populated <see cref="NewGameVM"/> instance as the model.If the game is not found, it returns the not found view.
        /// </para>
        /// </remarks>
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the game with the specified ID
            var data = await _service.GetGameByIdAsync(id);

            // Check if the game exists, if not return the not found view.
            if (data == null)
            {
                _logger.LogWarning($"{User.Identity!.Name} tried to edit details of Game with {id} at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"} but failed. [Reason: Game id not found]");

                return View("NotFound");
            }

            // Create a new instance of NewGameVM and populate it with the data from the retrieved game
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
                VoiceActorIds = data.VoiceActors_Games!.Select(x => x.VoiceActorId).ToList(), 
                PlatformIds = data.Platforms_Games!.Select(x => x.PlatformId).ToList() 
            };

            // Retrieve the dropdown data for the edit form
            var gameDropdownData = await _service.GetNewGameDropDownValues();

            // Set up the select lists for the input fields using the retrieved dropdown data
            // Each select list is created with the appropriate data source and value/text field mappings
            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

            // Return the edit view for the specified game, passing the populated NewGameVM instance as the model
            return View(response); 
        }

        /// <summary>
        /// Handles the HTTP POST request for updating a game with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the game to update.</param>
        /// <param name="game">The updated data for the game.</param>
        /// <returns>An asynchronous task that represents the operation. Returns the index view if the model is valid and the ID matches; otherwise, returns the edit view with validation errors or the not found view.</returns>
        /// <remarks>
        /// <para>
        /// This action is triggered when a POST request is made to update a game with the specified ID submitted from the edit form, and the updated game data in the <paramref name="game"/> parameter.
        /// </para>
        /// <para>
        /// If the provided ID does not match the ID of the game in the <paramref name="game"/> parameter, the action returns the not found view.
        /// </para>
        /// <para>
        /// If the model state is not valid, indicating validation errors, the action retrieves the necessary dropdown data for select inputs using the <see cref="IGameService.GetNewGameDropDownValues"/> method. It sets up select lists for each input field by creating instances of <see cref="SelectList"/> with the retrieved data. The select lists are assigned to the <see cref="ViewBag"/> properties to make them available in the view. The action returns the edit view with the provided <paramref name="game"/> and validation errors.
        /// </para>
        /// <para>
        /// If the model state is valid and the provided ID matches the ID of the game, the action updates the game asynchronously using the <see cref="IGameService.UpdateGameAsync"/> method. It then redirects to the index view.
        /// </para>
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewGameVM game)
        {
            // Check if the provided ID matches the ID of the game, if not return not found view
            if (id != game.Id)
            {
                _logger.LogWarning($"{User.Identity!.Name} tried to edit details of Game with {id} at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"} but failed. [Reason: Game id not found]");

                return View("NotFound");
            }

            // Check if the model state is not valid
            if (!ModelState.IsValid) 
            {
                // Retrieve the dropdown data for the edit form
                var gameDropdownData = await _service.GetNewGameDropDownValues();

                // Set up the select lists for the input fields using the retrieved dropdown data
                // Each select list is created with the appropriate data source and value/text field mappings
                ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
                ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
                ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
                ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
                ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

                _logger.LogWarning($"{User.Identity!.Name} tried to edit details of Game with {id} at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"} but failed. [Reason: Invalid model state]");

                // Return the edit view with the provided game and validation errors
                return View(game); 
            }

            // Update the game asynchronously using the provided game data
            await _service.UpdateGameAsync(game);

            _logger.LogInformation($"{User.Identity!.Name} edited details of Game: [ID = {id}, Name = {game.Name}] at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"}.");

            // Redirect to the index view
            return RedirectToAction("Index"); 
        }


        /// <summary>
        /// Handles the filter functionality for games.
        /// </summary>
        /// <param name="searchString">The search string used for filtering games.</param>
        /// <returns>An asynchronous task that represents the operation. Returns the index view with filtered data if a search string is provided; otherwise, returns the index view with all games.</returns>
        /// <remarks>
        /// This action is decorated with the [AllowAnonymous] attribute, allowing unauthenticated access to the filter functionality. It handles the filter functionality for games based on the provided search string. The action retrieves all games asynchronously using the <see cref="IGameService.GetAllAsync"/> method, including the related entities for navigation properties (e.g., GamePublisher, GameDeveloper, GameRating).
        /// <para>
        /// If a non-empty search string is provided, the action filters the retrieved data by checking if the game's name or description contains the search string (case-insensitive comparison). The filtered data is then returned for the index view. If no search string is provided, the action returns all the retrieved game for the index view.
        /// </para>
        /// </remarks>
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            // Retrieve all games with related entities
            var data = await _service.GetAllAsync(p => p.GamePublisher!, d => d.GameDeveloper!, r => r.GameRating!);

            // Check if a search string is provided
            if (!string.IsNullOrEmpty(searchString))
            {
                // Filter the data based on the search string (case-insensitive comparison)
                var filteredData = data.Where(x => x.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Description!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

                _logger.LogInformation($"{User.Identity!.Name ?? "Guest"} filtered Games index using search term {searchString} at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"}.");

                // Return the index view with the filtered data
                return View("Index", filteredData);
            }

            // Return the index view with all games
            return View("Index", data);
        }

        //Get: Game/Delete/{id}
        /// <summary>
        /// Handles the HTTP GET request for deleting a game.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        /// <returns>An asynchronous task that represents the operation. Returns the delete view for the game if found; otherwise, returns the "NotFound" view.</returns>
        /// <remarks>
        /// <para>
        /// This action retrieves a specific game asynchronously using the <see cref="IGameService.GetGameByIdAsync"/> method based on the provided ID.
        /// If the game is found, it constructs a <see cref="NewGameVM"/> instance representing the game data.
        /// </para>
        /// <para>
        /// The action also retrieves additional data needed for populating dropdown lists in the view, such as publishers, developers, voice actors, ratings, and platforms.
        /// The retrieved data is assigned to the respective ViewBag properties with the help of the <see cref="SelectList"/> class.
        /// </para>
        /// <para>
        /// Finally, the action returns the delete view with the constructed <see cref="NewGameVM"/> instance as the model.
        /// If the game is not found, it returns the "NotFound" view instead.
        /// </para>
        /// </remarks>
        public async Task<IActionResult> Delete(int id)
        {
            // Retrieve the game by ID
            var data = await _service.GetGameByIdAsync(id);

            // Retrieve the game by ID
            if (data == null)
            {
                _logger.LogWarning($"{User.Identity!.Name} tried to delete Game with ID {id} at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"} but failed. [Reason: Game id not found]");

                return View("NotFound");
            }

            // Create a instance of NewGameVM representing the game data
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
                VoiceActorIds = data.VoiceActors_Games!.Select(x => x.VoiceActorId).ToList(), 
                PlatformIds = data.Platforms_Games!.Select(x => x.PlatformId).ToList(), 
            };

            // Retrieve additional data for dropdown lists
            var gameDropdownData = await _service.GetNewGameDropDownValues();

            // Assign the retrieved data to ViewBag properties using SelectList
            ViewBag.Publishers = new SelectList(gameDropdownData.Publisher, "Id", "Name");
            ViewBag.Developers = new SelectList(gameDropdownData.Developer, "Id", "Name");
            ViewBag.VoiceActors = new SelectList(gameDropdownData.VoiceActor, "Id", "FullName");
            ViewBag.Ratings = new SelectList(gameDropdownData.Rating, "Id", "Name");
            ViewBag.Platform = new SelectList(gameDropdownData.Platform, "Id", "Name");

            // Return the delete view with the constructed model
            return View(response); 
        }

        /// <summary>
        /// Handles the HTTP POST request to confirm the deletion of a game.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        /// <returns>An asynchronous task that represents the operation. Deletes the game if found and redirects to the index page; otherwise, returns the "NotFound" view.</returns>
        /// <remarks>
        /// <para>
        /// This action is triggered when the user confirms the deletion of a game by submitting the delete form.
        /// It retrieves the game details asynchronously using the <see cref="IGameService.GetGameByIdAsync"/> method based on the provided ID.
        /// If the game is found, it proceeds to delete the game by calling the <see cref="IGameService.DeleteAsync"/> method.
        /// </para>
        /// <para>
        /// If the game is not found, the action returns the "NotFound" view.
        /// After successful deletion, the action redirects to the index page using the <see cref="RedirectToAction"/> method.
        /// </para>
        /// </remarks>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Retrieve the game details by ID
            var dataDetails = await _service.GetGameByIdAsync(id);

            // Check if the game details are found
            if (dataDetails == null) return View("NotFound");

            _logger.LogInformation($"{User.Identity!.Name} deleted game: [ID = {id}, Name = {dataDetails.Name}] at {DateTime.Now} with IP {HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Not Detected"}.");

            // Delete the game
            await _service.DeleteAsync(id);

            // Redirect to the index page
            return RedirectToAction(nameof(Index)); 
        }

    }
}
