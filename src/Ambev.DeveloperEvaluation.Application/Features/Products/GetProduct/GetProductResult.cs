﻿namespace Ambev.DeveloperEvaluation.Application.Features.Products.GetProduct;

/// <summary>
/// Response model for GetProduct operation
/// </summary>
public class GetProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created product.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created product in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// The product title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The product price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// A detailed description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The product category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the product image.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// The product rating information.
    /// </summary>
    public GetProductRateResult Rating { get; set; } = new();
}

public class GetProductRateResult
{
    /// <summary>
    /// The average rating of the product.
    /// </summary>
    public float Rate { get; set; }

    /// <summary>
    /// The total number of reviews.
    /// </summary>
    public int Count { get; set; }
}
