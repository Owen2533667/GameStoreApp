using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents an order.
    /// </summary>
    /// <remarks>
    /// The class Order will have an id that represents the id of the order, a string that will be the email of the user making the order, the user id of the user making the order, a gamestoreuser object that will be the user making the order, and a List of order items being ordered by the user. The list will contain instances of order items, which will have information about the game.
    /// </remarks>
    public class Order
    {
        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email associated with the order.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user associated with the order.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the order.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public GameStoreUser? User { get; set; }

        /// <summary>
        /// Gets or sets the list of order items associated with the order.
        /// </summary>
        public List<OrderItem>? OrderItems { get; set; }
    }
}
