﻿using Ambev.DeveloperEvaluation.Common.Helpers;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.ListProductByCategory;

/// <summary>
/// Command for retrieving a list of all categories
/// </summary>
public class ListProductByCategoryCommand : IRequest<PaginatedList<ListProductByCategoryResult>>
{
    public string? CategoryName { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public List<(string PropertyName, bool Ascendent)> Order { get; set; } = [];
    public string? Title { get; set; }
    public decimal? Price { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public float? Rate { get; set; }
    public float? MinRate { get; set; }
    public float? MaxRate { get; set; }
    public int? Count { get; set; }
    public int? MinCount { get; set; }
    public int? MaxCount { get; set; }
}
