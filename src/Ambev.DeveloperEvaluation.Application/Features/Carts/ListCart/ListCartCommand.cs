using Ambev.DeveloperEvaluation.Common.Helpers;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.ListCart;

/// <summary>
/// Command for retrieving a list of all carts
/// </summary>
public class ListCartCommand : IRequest<PaginatedList<ListCartResult>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public List<(string PropertyName, bool Ascendent)> Order { get; set; } = [];
    public int? SaleNumber { get; set; }
    public int? MinSaleNumber { get; set; }
    public int? MaxSaleNumber { get; set; }
    public DateTime? SaleDate { get; set; }
    public DateTime? MinSaleDate { get; set; }
    public DateTime? MaxSaleDate { get; set; }
    public string? Branch { get; set; }
    public bool? IsCanceled { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public int? Quantity { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public decimal? Price { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
