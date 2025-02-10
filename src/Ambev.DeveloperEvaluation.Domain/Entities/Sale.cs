using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class Sale : BaseEntity
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
    /// User who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

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
    public List<SaleItem> Products { get; set; } = [];

    /// <summary>
    /// Indicates whether the sale is active/canceled.
    /// </summary>
    public short Status { get; set; }

    /// <summary>
    /// Gets the date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the user's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public Sale() => CreatedAt = DateTime.UtcNow;

    /// <summary>
    /// Cancel the cart.
    /// Changes the user's status to Inactive.
    /// </summary>
    public void Cancel()
    {
        Status = (short)SaleStatus.Canceled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(int saleNumber, DateTime saleDate, string branch, short status)
    {
        SaleNumber = saleNumber;
        SaleDate = saleDate;
        Branch = branch;
        Status = status;
        UpdatedAt = DateTime.UtcNow;

        ApplyDiscounts();
        SetTotalAmount();
    }

    public void ApplyDiscounts()
    {
        foreach (var product in Products)
            product.Discount = CalculateDiscount(product.Quantity, product.UnitPrice);
    }

    private decimal CalculateDiscount(int quantity, decimal unitPrice)
    {
        if (quantity >= 10 && quantity <= 20)
            return unitPrice * quantity * 0.2m;

        if (quantity >= 4)
            return unitPrice * quantity * 0.1m;

        return 0m;
    }

    public void SetTotalAmount()
        => TotalAmount = Products.Sum(item => (item.UnitPrice * item.Quantity) - item.Discount);
}
