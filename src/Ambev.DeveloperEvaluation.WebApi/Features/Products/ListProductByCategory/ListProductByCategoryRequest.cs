using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductByCategory;

/// <summary>
/// API response model for ListProductByCategory operation
/// </summary>
public class ListProductByCategoryRequest
{
    [FromRoute]
    public string? CategoryName { get; set; }

    [FromQuery(Name = "_page")]
    public int? PageNumber { get; set; }

    [FromQuery(Name = "_size")]
    public int? PageSize { get; set; }

    [FromQuery(Name = "_order")]
    public string? Order { get; set; }
}
