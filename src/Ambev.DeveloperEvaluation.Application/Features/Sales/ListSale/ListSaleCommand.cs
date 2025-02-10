using Ambev.DeveloperEvaluation.Common.Helpers;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.ListSale;
public class ListSaleCommand : IRequest<PaginatedList<ListSaleResult>>
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
    public Guid? UserId { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? MinTotalAmount { get; set; }
    public decimal? MaxTotalAmount { get; set; }
    public string? Branch { get; set; }
    public short? Status { get; set; }
    public Guid? ProductId { get; set; }
    public int? Quantity { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? MinUnitPrice { get; set; }
    public decimal? MaxUnitPrice { get; set; }
    public decimal? Discount { get; set; }
    public decimal? MinDiscount { get; set; }
    public decimal? MaxDiscount { get; set; }
}
