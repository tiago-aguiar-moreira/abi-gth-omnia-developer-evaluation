namespace Ambev.DeveloperEvaluation.Application.Features.Products.ListProductByCategory;

/// <summary>
/// Response model for ListProductByCategory operation
/// </summary>
public class ListProductByCategoryResult
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
    public ListProductByCategoryRateResult Rating { get; set; } = new();
}

public class ListProductByCategoryRateResult
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
