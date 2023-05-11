using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a shopping cart item in a shopping cart
    /// </summary>
    public class ShoppingCartItem
    {
        /// <summary>
        /// Gets or sets the identifier. This has the [Key] attribute to indicate that this id property is a primary key in the database for related table.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        public Game? Game { get; set; }

        /// <summary>
        /// Gets or sets the amount of this shopping cart item. If you want to add another game that is already an shopping cart item, this will be incremented. The oppersite occurs, if the item amount is decreased to zero then remove the item.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart identifier.
        /// </summary>
        public string? ShoppingCartId { get; set; }
    }
}
