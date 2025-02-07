using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProduct;

/// <summary>
/// API response model for ListProduct operation
/// </summary>
public class ListProductRequest
{
    [FromQuery(Name = "_page")]
    public int? PageNumber { get; set; }

    [FromQuery(Name = "_size")]
    public int? PageSize { get; set; }

    [FromQuery(Name = "_order")]
    public string? Order { get; set; }
}
