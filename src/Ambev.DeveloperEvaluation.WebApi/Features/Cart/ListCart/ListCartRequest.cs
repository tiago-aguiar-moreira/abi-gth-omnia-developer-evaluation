using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCart;

/// <summary>
/// API response model for ListCart operation
/// </summary>
public class ListCartRequest
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

    public string? Branch { get; set; }

    public bool? IsCanceled { get; set; }

    public Guid? UserId { get; set; }

    // Fields related to ListCartItemResult
    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    [FromQuery(Name = "_minQuantity")]
    public int? MinQuantity { get; set; }

    [FromQuery(Name = "_maxQuantity")]
    public int? MaxQuantity { get; set; }
}
