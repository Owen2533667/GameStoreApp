using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    /// <summary>
    /// Represents a service for managing orders.
    /// Implements the <see cref="IOrdersService"/> interface.
    /// </summary>
    public class OrdersService : IOrdersService
    {
        private readonly GameStoreAppDbContext _context;

        /// <summary>
        /// Initialises a new instance of the <see cref="OrdersService"/> class with the specified <see cref="GameStoreAppDbContext"/> instance.
        /// </summary>
        /// <param name="context">The <see cref="GameStoreAppDbContext"/> instance used for interacting with the data store.</param>
        public OrdersService(GameStoreAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves orders asynchronously for a specific user ID and user role.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve orders for.</param>
        /// <param name="userRole">The role of the user to retrieve orders for.</param>
        /// <returns>A task representing the asynchronous operation. The list of orders matching the user ID and user role.</returns>
        /// <inheritdoc cref="IOrdersService.GetOrdersByUserIdAndRoleAsync"/>
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            // Retrieve all orders from the data store, including associated order items and user information
            var orders = await _context.Orders.Include(x => x.OrderItems)!.ThenInclude(x => x.Game).Include(x => x.User).ToListAsync();

            // If user role is not admin
            if(userRole != "admin")
            {
                // Filter orders based on the specified user ID if the user is not an admin
                orders = orders.Where(x => x.UserId == userId).ToList();
            }

            return orders; // Return the orders
        }

        /// <summary>
        /// Stores an order asynchronously based on the provided shopping cart items, user ID, and user email address.
        /// </summary>
        /// <param name="items">The list of shopping cart items representing the order contents.</param>
        /// <param name="userId">The ID of the user placing the order.</param>
        /// <param name="userEmailAddress">The email address of the user placing the order.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <inheritdoc cref="IOrdersService.StoreOrderAsync"/>
        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            // Create a new order object
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };

            // Add the order to the context and save changes to generate an order ID
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                // Create a new order item for each shopping cart item
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    GameId = item.Game!.Id,
                    OrderId = order.Id,
                    Price = item.Game.Price
                };

                // Create a new order item for each shopping cart item
                await _context.OrderItems.AddAsync(orderItem);
            }

            // Save changes to persist the order items
            await _context.SaveChangesAsync();
        }
    }
}
