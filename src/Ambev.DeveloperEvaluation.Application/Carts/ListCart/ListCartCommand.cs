using Ambev.DeveloperEvaluation.Common.Helpers;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCart;

/// <summary>
/// Command for retrieving a list of all carts
/// </summary>
public class ListCartCommand : IRequest<PaginatedList<ListCartResult>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Order { get; set; }
}
