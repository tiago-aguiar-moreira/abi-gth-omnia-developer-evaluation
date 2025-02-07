using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context) => _context = context;

    /// <summary>
    /// Creates a new product in the database
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Deletes a product from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a list of products by their category
    /// </summary>
    /// <param name="id">The category of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<PaginatedList<Product>> ListByCategoryAsync(
        string? categoryName, int? pageNumber, int? pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();

        if (!categoryName.IsNullOrEmpty())
            query = query.Where(w => w.Category.ToLower() == categoryName!.ToLower());

        return await PaginatedList<Product>.CreateAsync(query, pageNumber, pageSize, cancellationToken);
    }

    /// <summary>
    /// Retrieves a list of product category
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<List<string>> ListCategoryAsync(CancellationToken cancellationToken = default)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);

        return products.Select(s => s.Category).Distinct().ToList();
    }

    /// <summary>
    /// Retrieves a product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Products.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

    /// <summary>
    /// Retrieves a product by their title
    /// </summary>
    /// <param name="id">The title of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
        => await _context.Products.FirstOrDefaultAsync(f => f.Title == title, cancellationToken);

    /// <summary>
    /// Retrieves a list of products
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products if found, empty list if not found</returns>
    public async Task<PaginatedList<Product>> ListAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken = default)
        => await PaginatedList<Product>.CreateAsync(_context.Products, pageNumber, pageSize, cancellationToken);

    /// <summary>
    /// Retrieves a list of products by a list of ID's
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products if found, empty list if not found</returns>
    public async Task<List<Product>> ListAsync(IEnumerable<Guid> productId, CancellationToken cancellationToken = default)
        => await _context.Products.Where(w => productId.Contains(w.Id)).ToListAsync(cancellationToken);

    /// <summary>
    /// Updates a new product in the repository
    /// </summary>
    /// <param name="product">The product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product</returns>
    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var existingProduct = await GetByIdAsync(product.Id, cancellationToken);
        if (existingProduct == null)
            return false;

        existingProduct.Update(
            product.Title,
            product.Price,
            product.Description,
            product.Category,
            product.Image,
            product.Rate,
            product.Count);

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
