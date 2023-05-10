using GameStoreApp.Data.Cart;
using GameStoreApp.Data.Services;
using GameStoreApp.Data.Static;
using GameStoreApp.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace GameStoreApp.Controllers
{
    /// <summary>
    /// Controller responsible for managing orders.
    /// Requires authorization for accessing its actions.
    /// </summary>
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        /// <summary>
        /// Initialises a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="gameService">The game service.</param>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="ordersService">The orders service.</param>
        public OrdersController(IGameService gameService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _gameService = gameService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        /// <summary>
        /// Displays the index page asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. Returns the view containing the user's orders.</returns>
        /// <remarks>
        /// Retrieves the user's ID and role from claims.
        /// Uses the <see cref="IOrdersService.GetOrdersByUserIdAndRoleAsync(string, string)"/> method to retrieve the user's orders asynchronously.
        /// Passes the retrieved orders to the view for display.
        /// </remarks>
        public async Task<IActionResult> Index()
        {
            // Retrieve the user's ID and role from claims
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            string userRole = User.FindFirstValue(ClaimTypes.Role)!;

            // Retrieve the user's orders asynchronously
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);

            // Return the view containing the user's orders
            return View(orders);
        }

        /// <summary>
        /// Displays the shopping cart page.
        /// </summary>
        /// <returns>The shopping cart view.</returns>
        /// <remarks>
        /// Retrieves the shopping cart items.
        /// Initializes a new instance of <see cref="ShoppingCartVM"/> to hold the shopping cart information.
        /// Passes the shopping cart and the total cart value to the view for display.
        /// </remarks>
        public IActionResult ShoppingCart()
        {
            // Retrieve the shopping cart items
            var items = _shoppingCart.GetCartItems();
            _shoppingCart.ShoppingCartItems = items;

            // Create a new ShoppingCartVM instance to hold the shopping cart information
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetCartTotal()
            };

            // Return the shopping cart view with response
            return View(response);
        }

        /// <summary>
        /// Adds a game item to the shopping cart asynchronously.
        /// </summary>
        /// <param name="id">The ID of the game to add.</param>
        /// <returns>A task that represents the asynchronous operation. Redirects to the shopping cart view.</returns>
        /// <remarks>
        /// If the game item exists, it is added to the shopping cart.
        /// Uses the <see cref="IGameService.GetGameByIdAsync(int)"/> method to retrieve the game item asynchronously,
        /// and then uses the <see cref="ShoppingCart.AddItemToCart(Models.Game)"/> method to add the game item to the shopping cart.
        /// </remarks>
        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            // Retrieve the game item by ID using the game service
            var item = await _gameService.GetGameByIdAsync(id);

            // If item is not null, meaning a game was retreived
            if (item != null)
            {
                // Add the game item to the shopping cart
                _shoppingCart.AddItemToCart(item);
            }

            // Redirect to the shopping cart view
            return RedirectToAction(nameof(ShoppingCart));
        }

        /// <summary>
        /// Removes a game item from the shopping cart asynchronously.
        /// </summary>
        /// <param name="id">The ID of the game to remove.</param>
        /// <returns>A task that represents the asynchronous operation. Redirects to the shopping cart view.</returns>
        /// <remarks>
        /// If the game item exists, it is removed from the shopping cart.
        /// Uses the <see cref="IGameService.GetGameByIdAsync(int)"/> method to retrieve the game item asynchronously,
        /// and then uses the <see cref="ShoppingCart.RemoveItemFromCart(Models.Game)"/> method to remove the game item from the shopping cart.
        /// </remarks>
        public async Task<IActionResult> RemoveFromShoppingCart(int id)
        {
            // Retrieve the game item by ID using the game service
            var item = await _gameService.GetGameByIdAsync(id);

            if (item != null)
            {
                // Remove the game item from the shopping cart
                _shoppingCart.RemoveItemFromCart(item);
            }

            // Redirect to the shopping cart view
            return RedirectToAction(nameof(ShoppingCart));
        }

        /// <summary>
        /// Completes the order asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. Returns the "OrderCompleted" view.</returns>
        /// <remarks>
        /// Retrieves the shopping cart items.
        /// Retrieves the user's ID and email address from claims.
        /// Stores the order asynchronously using the <see cref="IOrdersService.StoreOrderAsync(List{ShoppingCartItem}, string, string)"/> method.
        /// Clears the shopping cart asynchronously using the <see cref="ShoppingCart.ClearShoppingCartAsync"/> method.
        /// Returns the "OrderCompleted" view to indicate that the order has been completed successfully.
        /// </remarks>
        public async Task<IActionResult> CompleteOrder()
        {
            // Retrieve the shopping cart items
            var items = _shoppingCart.GetCartItems();

            // Retrieve the user's ID and email address from claims
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email)!;

            // Store the order asynchronously
            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);

            // Clear the shopping cart asynchronously
            await _shoppingCart.ClearShoppingCartAsync();

            // Return the "OrderCompleted" view
            return View("OrderCompleted");
        }
    }
}
