using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class CartItem : BaseEntity
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Discount applied to the product.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Total value for this product after applying quantity and discount.
    /// </summary>
    public decimal TotalPrice => (UnitPrice * Quantity) - Discount;

    /// <summary>
    /// Gets the date and time when the cart item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the cart item's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public CartItem()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
