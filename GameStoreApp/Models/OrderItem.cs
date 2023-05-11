using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents an item in an order.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the ID of the order item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the amount of the order item.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the price of the order item.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the ID of the game associated with the order item.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the game associated with the order item.
        /// </summary>
        [ForeignKey("GameId")]
        [Required]
        public Game? Game { get; set; }

        /// <summary>
        /// Gets or sets the ID of the order associated with the order item.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order associated with the order item.
        /// </summary>
        [ForeignKey("OrderId")]
        [Required]
        public Order? Order { get; set; }
    }
}
