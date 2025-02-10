namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    /// <summary>
    /// Unique identifier for the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Sale number.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// Date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// User who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Branch where the sale took place.
    /// </summary>
    public string? Branch { get; set; }

    /// <summary>
    /// Indicates whether the sale is active/canceled.
    /// </summary>
    public short Status { get; set; }

    /// <summary>
    /// List of sold products.
    /// </summary>
    public List<UpdateSaleItemRequest> Products { get; set; } = [];
}

public class UpdateSaleItemRequest
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
