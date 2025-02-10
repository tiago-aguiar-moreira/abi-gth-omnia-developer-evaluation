namespace Ambev.DeveloperEvaluation.Application.Features.Carts.GetCart;

/// <summary>
/// Response model for GetCart operation
/// </summary>
public class GetCartResult
{
    /// <summary>
    /// Cart ID.
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
    public List<GetCartItemResult> Products { get; set; } = [];
}

public class GetCartItemResult
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