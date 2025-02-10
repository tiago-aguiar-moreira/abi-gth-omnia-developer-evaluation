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
    public DateTime? Date { get; set; }
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public int? Quantity { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
}
