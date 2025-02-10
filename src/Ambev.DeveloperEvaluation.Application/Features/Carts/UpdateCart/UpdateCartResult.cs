namespace Ambev.DeveloperEvaluation.Application.Features.Carts.UpdateCart;

/// <summary>
/// Response model for UpdateCart operation
/// </summary>
public class UpdateCartResult
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
    /// Date when the cart was made.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// List of sold products.
    /// </summary>
    public List<UpdateCartItemResult> Products { get; set; } = [];
}

public class UpdateCartItemResult
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