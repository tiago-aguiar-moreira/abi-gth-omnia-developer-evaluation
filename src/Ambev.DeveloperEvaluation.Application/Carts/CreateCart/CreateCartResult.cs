namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
public class CreateCartResult
{
    /// <summary>
    /// Unique identifier for the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier for the sale.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// User who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Total value of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// List of sold products.
    /// </summary>
    public List<CreateCartItemResult> Products { get; set; } = [];

    /// <summary>
    /// Indicates whether the sale is canceled.
    /// </summary>
    public bool IsCanceled { get; set; }
}

public class CreateCartItemResult
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

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
}