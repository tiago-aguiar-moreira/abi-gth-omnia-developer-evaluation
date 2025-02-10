using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartResponse
{
    /// <summary>
    /// Unique identifier for the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Date when the sale was made.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// List of sold products.
    /// </summary>
    public List<UpdateCartItemResponse> Products { get; set; } = [];
}

public class UpdateCartItemResponse
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }
}