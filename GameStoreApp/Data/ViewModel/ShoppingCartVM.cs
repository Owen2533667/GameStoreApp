using GameStoreApp.Data.Cart;

namespace GameStoreApp.Data.ViewModel
{
    /// <summary>
    /// Represents the view model for the shopping cart.
    /// </summary>
    public class ShoppingCartVM
    {
        /// <summary>
        /// Gets or sets the shopping cart object.
        /// </summary>
        /// <remarks>
        /// This property represents the shopping cart object containing the cart items. It can be null if the shopping cart is empty.
        /// </remarks>
        public ShoppingCart? ShoppingCart { get; set; }

        /// <summary>
        /// Gets or sets the total value of the shopping cart.
        /// </summary>
        /// <remarks>
        /// This property represents the total value of the shopping cart. It is the sum of the prices of all items in the cart.
        /// </remarks>
        public double ShoppingCartTotal { get; set; }
    }
}
