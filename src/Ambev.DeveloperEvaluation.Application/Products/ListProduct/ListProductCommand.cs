using Ambev.DeveloperEvaluation.Common.Helpers;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Command for retrieving a list of all products
/// </summary>
public class ListProductCommand : IRequest<PaginatedList<ListProductResult>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Order { get; set; }
}
