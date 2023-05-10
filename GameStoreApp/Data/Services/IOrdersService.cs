using GameStoreApp.Data.Cart;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    /// <summary>
    /// Represents an interface for managing orders.
    /// </summary>
    public interface IOrdersService
    {
        /// <summary>
        /// Stores an order asynchronously based on the provided shopping cart items, user ID, and user email address.
        /// </summary>
        /// <param name="items">The list of shopping cart items representing the order contents.</param>
        /// <param name="userId">The ID of the user placing the order.</param>
        /// <param name="userEmailAddress">The email address of the user placing the order.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method stores an order in the db based on the provided shopping cart items.
        /// It associates the order with the specified user by using the user ID and records the user's email address for future reference.
        /// The order is processed and saved in the db.
        /// </remarks>
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        /// <summary>
        /// Retrieves orders asynchronously for a specific user ID and user role.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve orders for.</param>
        /// <param name="userRole">The role of the user to retrieve orders for.</param>
        /// <returns>A task representing the asynchronous operation. The list of orders matching the user ID and user role.</returns>
        /// <remarks>
        /// This method retrieves orders from the db based on the specified user ID and user role.
        /// It returns a list of orders that match the provided criteria.
        /// The user ID is used to filter orders for a specific user, while the user role can further refine the search based on the user's role within the system.
        /// </remarks>
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
