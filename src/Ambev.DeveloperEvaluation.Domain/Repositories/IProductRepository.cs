using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository
{

    /// <summary>
    /// Creates a new product in the repository
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by their title
    /// </summary>
    /// <param name="id">The title of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of products
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page sizetoken</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products if found, empty list if not found</returns>
    Task<PaginatedList<Product>> ListAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of product category
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<List<string>> ListCategoryAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of products by a list of ID's
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products if found, empty list if not found</returns>
    Task<List<Product>> ListAsync(IEnumerable<Guid> productId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a new product in the repository
    /// </summary>
    /// <param name="product">The product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product</returns>
    Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of products by their category
    /// </summary>
    /// <param name="id">The category of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<PaginatedList<Product>> ListByCategoryAsync(string? categoryName, int? pageNumber, int? pageSize, CancellationToken cancellationToken = default);
}
