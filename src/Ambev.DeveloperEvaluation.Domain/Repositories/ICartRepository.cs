using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface ICartRepository
{
    /// <summary>
    /// Creates a new cart in the repository
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart</returns>
    Task<Cart?> CreateAsync(Cart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a cart by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a cart by user id and date
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="date">The cart date</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    Task<Cart?> GetByUserIdAndDateAsync(Guid userId, DateTime date, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a cart from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of carts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of carts if found, empty list if not found</returns>
    Task<PaginatedList<Cart>> ListAsync(
        int? pageNumber,
        int? pageSize,
        List<(string PropertyName, bool Ascendent)> sortingFields,
        List<(string PropertyName, object? Value)> filters,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a cart from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was updated, false if not found</returns>
    Task<Cart?> UpdateAsync(Cart cart, CancellationToken cancellationToken = default);
}
