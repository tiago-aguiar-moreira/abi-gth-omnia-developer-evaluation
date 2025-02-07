using Ambev.DeveloperEvaluation.Common.Helpers;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductByCategory;

/// <summary>
/// Command for retrieving a list of all categories
/// </summary>
public class ListProductByCategoryCommand : IRequest<PaginatedList<ListProductByCategoryResult>>
{
    public string? CategoryName {  get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Order { get; set; }
}
