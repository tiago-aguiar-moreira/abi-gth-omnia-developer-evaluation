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

    public string? Title { get; set; }

    public decimal? Price { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }
    
    public string? Description { get; set; }

    public string? Category { get; set; }

    public string? Image { get; set; }

    public float? Rate { get; set; }

    [FromQuery(Name = "_minRate")]
    public float? MinRate { get; set; }
    
    [FromQuery(Name = "_maxRate")]
    public float? MaxRate { get; set; }

    public int? Count { get; set; }

    [FromQuery(Name = "_minCount")]
    public int? MinCount { get; set; }

    [FromQuery(Name = "_minCount")]
    public int? MaxCount { get; set; }
}
