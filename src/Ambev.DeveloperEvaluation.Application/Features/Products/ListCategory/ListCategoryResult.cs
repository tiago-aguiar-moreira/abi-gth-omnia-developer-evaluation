namespace Ambev.DeveloperEvaluation.Application.Features.Products.ListCategory;

/// <summary>
/// Response model for ListCategory operation
/// </summary>
public class ListCategoryResult
{
    /// <summary>
    /// The product category.
    /// </summary>
    public string[] Category { get; set; } = [];
}
