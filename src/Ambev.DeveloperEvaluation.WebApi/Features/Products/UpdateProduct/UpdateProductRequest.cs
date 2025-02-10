using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Request model for updating a user
/// </summary>
public class UpdateProductRequest
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
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
    public UpdateProductRateRequest Rating { get; set; } = new();
}

public class UpdateProductRateRequest
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
