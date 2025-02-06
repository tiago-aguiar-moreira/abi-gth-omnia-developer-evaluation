namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Response model for GetCart operation
/// </summary>
public class GetCartResult
{
    /// <summary>
    /// Unique identifier for the sale.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// Date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// List of sold products.
    /// </summary>
    public List<GetCartItemResult> Products { get; set; } = [];

    /// <summary>
    /// Indicates whether the sale is canceled.
    /// </summary>
    public bool IsCanceled { get; set; }

    /// <summary>
    /// User who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }
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