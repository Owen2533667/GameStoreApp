using GameStoreApp.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreApp.Data.ViewComponets
{
    /// <summary>
    /// This ViewComponent displays a summary of the items in the shopping cart.
    /// </summary>
    /// <remarks>
    /// This ViewComponent is used to display a summary of the items in the shopping cart. It uses the
    /// <see cref="IViewComponentResult"/> interface to render the summary in a view. It is typically used in Razor views
    /// and can be invoked using the <see cref="Invoke"/> method.
    /// </remarks>
    public class ShoppingCartSummary : ViewComponent
    {
        // Private field to hold the ShoppingCart instance
        private readonly ShoppingCart _shoppingCart;

        /// <summary>
        /// Constructor that initialises the ShoppingCart instance.
        /// </summary>
        /// <param name="shoppingCart">The <see cref="ShoppingCart"/> instance to use.</param>
        /// <remarks>
        /// This constructor is used to inject the <see cref="ShoppingCart"/> instance into the ShoppingCartSummary ViewComponent.
        /// </remarks>
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        /// <summary>
        /// Invokes the ViewComponent to display the shopping cart summary.
        /// </summary>
        /// <returns>The <see cref="IViewComponentResult"/> containing the number of items in the shopping cart.</returns>
        /// <remarks>
        /// This method retrieves the cart items from the <see cref="_shoppingCart"/> instance and passes the count to the view.
        /// </remarks>
        public IViewComponentResult Invoke() 
        {
            // Get the cart items
            var items = _shoppingCart.GetCartItems();

            // Pass the count of cart items to the vie
            return View(items.Count);
        }
    }
}
