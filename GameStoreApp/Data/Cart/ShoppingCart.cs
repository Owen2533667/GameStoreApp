using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Cart
{
    public class ShoppingCart
    {
        /// <summary>
        /// The database context for the GameStoreApp.
        /// </summary>
        public GameStoreAppDbContext _context { get; set; } 

        public String ShoppingCartId { get; set; } //A string that will represent the id of the shopping cart.
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } //A list of shopping cart items that the user wants to buy.

        /// <summary>
        /// Initialises a new instance of the ShoppingCart class.
        /// </summary>
        /// <param name="context">The database context for the GameStoreApp.</param>
        public ShoppingCart(GameStoreAppDbContext context)
        {
            // Set the database context for the shopping cart.
            _context = context;
        }

        /// <summary>
        /// Gets the shopping cart for the current user.
        /// </summary>
        /// <param name="services">The service provider to reteive the required service objects.</param>
        /// <returns>A ShoppingCart object.</returns>
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            // Get the current user session object from the HttpContextAccessor.
            ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext!.Session;

            // Get the GameStoreAppDbContext object from the service provider.
            var context = services.GetService<GameStoreAppDbContext>();

            // Get the cart ID from the current session. If it doesn't exist, create a new one.
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            // Return a new ShoppingCart object with the GameStoreAppDbContext and setting the ShoppingCartId to cartId.
            return new ShoppingCart(context!) { ShoppingCartId = cartId };
        }

        /// <summary>
        /// Adds a game item to the shopping cart.
        /// </summary>
        /// <param name="game">The game to add to the shopping cart.</param>
        public void AddItemToCart(Game game)
        {
            // Get the shopping cart item, that also has the shopping cart id, for the specified game passed to method.
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(x => x.Game.Id == game.Id && x.ShoppingCartId == ShoppingCartId);

            // If the shopping cart item doesn't exist, create a new one.
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem() //Create a new instance of ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId, //Assign the shopping cart id to the current shopping cart id.
                    Game = game, //Assign the game to the game passed
                    Amount = 1, //Assign the amount to one.
                };

                _context.ShoppingCartItems.Add(shoppingCartItem); // Add the new shopping cart item to the database.

            }
            else // Otherwise, increment the amount of the existing shopping cart item.
            {
                // Increment the amount of the existing shopping cart item.
                shoppingCartItem.Amount++;
            }

            // Save changes to the database.
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes a game from the shopping cart.
        /// </summary>
        /// <param name="game">The game to remove from the shopping cart</param>
        public void RemoveItemFromCart(Game game)
        {
            // Find the shopping cart item that corresponds to the specified game.
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(x => x.Game.Id == game.Id && x.ShoppingCartId == ShoppingCartId);

            // If the shopping cart item exists, and if the amount is greate than one, decrement its amount by 1. If the amount is 0, remove the item from the shopping cart.
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                } else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            // Save changes to the database.
            _context.SaveChanges();
        }
        
        /// <summary>
        /// Returns the list of shopping cart items where it has the shopping cart id.
        /// </summary>
        /// <returns>A list of ShoppingCartItem objects.</returns>
        public List<ShoppingCartItem> GetCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Include(x => x.Game).ToList()); //Returns List<ShoppingCartItems>. If this is null ?? then assign values by getting the shopping cart items from the db where the shopping cart items, in the ShoppingCartItems table, have the ShoppingCartId, and then include the related game entity in the results. Then call ToList, to return a of List<ShoppingCartItems>.
        }

        /// <summary>
        /// Calculates the total cost of all items in the shopping cart.
        /// </summary>
        /// <returns> The total cost of all items in the shopping cart as a double</returns>
        public double GetCartTotal() => _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Game.Price * x.Amount).Sum();

        /// <summary>
        /// An async method that clears the shopping cart items where they have the shopping cart id. This retreives the shopping cart id that has the matching shopping cart id and saves that List called items, and then removes a range, using the list of items,  of data from the db table ShoppingCartItems. This means that all shopping cart items with the shopping cart id will be removed from the db. It then set the ShoppingCartItems property to a new List of ShoppingCartItems
        /// </summary>
        /// <returns> A task that represents the asynchronous operation.</returns>
        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).ToListAsync(); //Retreives the ShoppingCartItems from the db table ShoppingCartItems, where the ShoppingCartId equals the ShoppingCartId of the current cart and get these element to a List<ShoppingCartItems> and assign this to items.

            _context.ShoppingCartItems.RemoveRange(items); //Removes range of elements, using the items List<ShoppingCartItems> elements, from the table ShoppingCartItems.

            await _context.SaveChangesAsync(); //Save the changes to the db.

            ShoppingCartItems = new List<ShoppingCartItem>(); //Assing propety ShoppingCartItems to a new instance of List<ShoppingCartItem>
        }
    }
}
