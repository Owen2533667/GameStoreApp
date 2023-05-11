using GameStoreApp.Data;
using GameStoreApp.Data.Services;
using GameStoreApp.Data.Static;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreApp.Controllers
{
    /// <summary>
    /// Controller responsible for managing voice actors.
    /// Requires authorisation with the role "Admin" for accessing its actions unless the action is specifies that it allows anonymous access
    /// </summary>
    [Authorize(Roles = UserRoles.Admin)]
    public class VoiceActorController : Controller
    {

        private readonly IVoiceActorService _service;

        /// <summary>
        /// Initialises a new instance of the <see cref="VoiceActorController"/> class.
        /// </summary>
        /// <param name="service">The voice actor service.</param>
        public VoiceActorController(IVoiceActorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Displays the index view with all voice actors.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The index view with voice actor data.</returns>
        /// <remarks>
        /// This action allows anonymous access and retrieves all voice actors asynchronously from the voice actor service using the <see cref="IVoiceActorService.GetAllAsync"/> method.
        /// </remarks>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Retrieve all voice actors asynchronously from the voice actor service
            var data = await _service.GetAllAsync();

            // Return the index view with voice actor data
            return View(data);
        }

        /// <summary>
        /// Displays the create view for adding a new voice actor.
        /// </summary>
        /// <returns>The create view.</returns>
        /// <remarks>
        /// This action is used to render the create view, where users can enter details for a new voice actor.
        /// </remarks>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request for creating a new voice actor.
        /// </summary>
        /// <param name="va">The voice actor data.</param>
        /// <returns>A task representing the asynchronous operation. A redirect to the index view if the model is valid; otherwise, returns the create view with validation errors.</returns>
        /// <remarks>
        /// This action is responsible for processing the form submission from the create view asynchronously.
        /// It first checks if the model is valid, and if not, it returns the create view with validation errors.
        /// If the model is valid, it adds the voice actor asynchronously using the <see cref="IVoiceActorService.AddAsync"/> method.
        /// After successful addition, it redirects to the index view.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,PictureURL,Bio")] VoiceActor va)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                // If not, return the create view with validation errors
                return View(va);
            }

            // Add the voice actor asynchronously using the IVoiceActorService.AddAsync method
            await _service.AddAsync(va);

            // Redirect to the index view after successful addition
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the details view for a specific voice actor.
        /// </summary>
        /// <param name="id">The ID of the voice actor.</param>
        /// <param name="returnId">The Game ID of the previous view.</param>
        /// <returns>A task representing the asynchronous operation. The details view for the specified voice actor.</returns>
        /// <remarks>
        /// This action allows anonymous access and retrieves a specific voice actor asynchronously from the voice actor service using the <see cref="IVoiceActorService.GetByIdAsync"/> method.
        /// If the voice actor is not found, it returns the "NotFound" view.
        /// It sets the return ID in TempData and returns the details view for the specified voice actor.
        /// </remarks>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id , int returnId = 0)
        {
            // Retrieve a specific voice actor asynchronously from the voice actor service using the GetByIdAsync method
            var dataDetails = await _service.GetByIdAsync(id);

            // If the voice actor is not found, return the "NotFound" view
            if (dataDetails == null) return View("NotFound");

            // Set the return ID in TempData
            TempData["ReturnId"] = returnId;

            // Return the details view for the specified voice actor
            return View(dataDetails);
        }

        /// <summary>
        /// Displays the edit view for a specific voice actor.
        /// </summary>
        /// <param name="id">The ID of the voice actor.</param>
        /// <returns>A task representing the asynchronous operation. The edit view for the specified voice actor.</returns>
        /// <remarks>
        /// This action retrieves a specific voice actor asynchronously from the voice actor service using the <see cref="IVoiceActorService.GetByIdAsync"/> method.
        /// If the voice actor is not found, it returns the "NotFound" view.
        /// It returns the edit view for the specified voice actor.
        /// </remarks>
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve a specific voice actor asynchronously from the voice actor service using the GetByIdAsync method
            var dataDetails = await _service.GetByIdAsync(id);

            // If the voice actor is not found, return the "NotFound" view
            if (dataDetails == null) return View("NotFound");

            // Return the edit view for the specified voice actor
            return View(dataDetails);
        }

        /// <summary>
        /// Handles the HTTP POST request for editing a voice actor.
        /// </summary>
        /// <param name="id">The ID of the voice actor.</param>
        /// <param name="va">The voice actor data.</param>
        /// <returns>A task representing the asynchronous operation. A redirect to the index view if the model is valid; otherwise, returns the edit view with validation errors.</returns>
        /// <remarks>
        /// This action retrieves a specific voice actor asynchronously from the voice actor service using the <see cref="IVoiceActorService.UpdateAsync"/> method.
        /// If the model is not valid, it returns the edit view with validation errors.
        /// If the model is valid, it updates the voice actor asynchronously using the UpdateAsync method.
        /// After successful update, it redirects to the index view.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName,PictureURL,Bio")] VoiceActor va)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                // If not, return the edit view with validation errors
                return View(va);
            }

            // Update the voice actor asynchronously using the UpdateAsync method
            await _service.UpdateAsync(id, va);

            // Redirect to the index view after successful update
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the delete view for a specific voice actor.
        /// </summary>
        /// <param name="id">The ID of the voice actor.</param>
        /// <returns>A task representing the asynchronous operation. The delete view for the specified voice actor.</returns>
        /// <remarks>
        /// This action retrieves a specific voice actor asynchronously from the voice actor service using the <see cref="IVoiceActorService.GetByIdAsync"/> method.
        /// If the voice actor is not found, it returns the "NotFound" view.
        /// It returns the delete view for the specified voice actor.
        /// </remarks>
        public async Task<IActionResult> Delete(int id)
        {
            // Retrieve a specific voice actor asynchronously from the voice actor service using the GetByIdAsync method
            var dataDetails = await _service.GetByIdAsync(id);

            // If the voice actor is not found, return the "NotFound" view
            if (dataDetails == null) return View("NotFound");

            // Return the delete view for the specified voice actor
            return View(dataDetails);
        }

        /// <summary>
        /// Handles the HTTP POST request for deleting a voice actor.
        /// </summary>
        /// <param name="id">The ID of the voice actor.</param>
        /// <returns>A task representing the asynchronous operation. A redirect to the index view after successful deletion.</returns>
        /// <remarks>
        /// This action retrieves a specific voice actor asynchronously from the voice actor service using the <see cref="IVoiceActorService.GetByIdAsync"/> method.
        /// If the voice actor is not found, it returns the "NotFound" view.
        /// It deletes the voice actor asynchronously using the DeleteAsync method.
        /// After successful deletion, it redirects to the index view.
        /// </remarks>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Retrieve a specific voice actor asynchronously from the voice actor service using the GetByIdAsync method
            var dataDetails = await _service.GetByIdAsync(id);

            // If the voice actor is not found, return the "NotFound" view
            if (dataDetails == null) return View("NotFound");

            // Delete the voice actor asynchronously using the DeleteAsync method
            await _service.DeleteAsync(id);

            // Redirect to the index view after successful deletion
            return RedirectToAction(nameof(Index));
        }
    }
}
