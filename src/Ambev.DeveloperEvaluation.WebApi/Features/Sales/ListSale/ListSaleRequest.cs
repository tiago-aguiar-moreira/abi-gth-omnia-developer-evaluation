using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;

/// <summary>
/// API response model for ListSale operation
/// </summary>
public class ListSaleRequest
{
    [FromQuery(Name = "_page")]
    public int? PageNumber { get; set; }

    [FromQuery(Name = "_size")]
    public int? PageSize { get; set; }

    [FromQuery(Name = "_order")]
    public string? Order { get; set; }

    public int? SaleNumber { get; set; }

    [FromQuery(Name = "_minSaleNumber")]
    public int? MinSaleNumber { get; set; }

    [FromQuery(Name = "_maxSaleNumber")]
    public int? MaxSaleNumber { get; set; }

    public DateTime? SaleDate { get; set; }

    [FromQuery(Name = "_minSaleDate")]
    public DateTime? MinSaleDate { get; set; }

    [FromQuery(Name = "_maxSaleDate")]
    public DateTime? MaxSaleDate { get; set; }

    public Guid? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    [FromQuery(Name = "_minTotalAmount")]
    public decimal? MinTotalAmount { get; set; }

    [FromQuery(Name = "_maxTotalAmount")]
    public decimal? MaxTotalAmount { get; set; }

    public string? Branch { get; set; }
    
    public short? Status { get; set; }

    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    [FromQuery(Name = "_minQuantity")]
    public int? MinQuantity { get; set; }

    [FromQuery(Name = "_maxQuantity")]
    public int? MaxQuantity { get; set; }

    public decimal? UnitPrice { get; set; }

    [FromQuery(Name = "_minUnitPrice")]
    public decimal? MinUnitPrice { get; set; }

    [FromQuery(Name = "_maxUnitPrice")]
    public decimal? MaxUnitPrice { get; set; }

    public decimal? Discount { get; set; }

    [FromQuery(Name = "_minDiscount")]
    public decimal? MinDiscount { get; set; }

    [FromQuery(Name = "_maxDiscount")]
    public decimal? MaxDiscount { get; set; }
}
