using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;
public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;

    public CartRepository(DefaultContext context) => _context = context;

    /// <summary>
    /// Creates a new cart in the database
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart</returns>
    public async Task<Cart?> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.CartItems.AddRangeAsync(cart.Products, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return cart;
    }

    /// <summary>
    /// Deletes a cart from the database
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a cart by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a cart by user id and date
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="date">The cart date</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    public async Task<Cart?> GetByUserIdAndDateAsync(Guid userId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .FirstOrDefaultAsync(o => o.UserId == userId && o.Date.Date == date.Date, cancellationToken);
    }

    /// <summary>
    /// Retrieves a list of carts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The lis of carts if found, empty list if not found</returns>
    public async Task<PaginatedList<Cart>> ListAsync(
        int? pageNumber,
        int? pageSize,
        List<(string PropertyName, bool Ascendent)> sortingFields,
        List<(string PropertyName, object? Value)> filters,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Carts.Include(c => c.Products).AsQueryable();

        foreach (var (property, value) in filters)
        {
            if (value is null) continue;

            query = property.ToLower() switch
            {
                "date" => query.Where(w => w.Date == (DateTime)value),
                "_mindate" => query.FilterRangeField(property,(DateTime)value),
                "_maxdate" => query.FilterRangeField(property,(DateTime)value),
                "userid" => query.Where(w => w.UserId == (Guid)value),
                "productid" => query.Where(w => w.Products.Any(a => a.ProductId == (Guid)value)),
                "quantity" => query.Where(w => w.Products.Any(a => a.Quantity == (int)value)),
                "_minquantity" => query.FilterRangeField(property, (int)value),
                "_maxquantity" => query.FilterRangeField(property, (int)value),
                _ => query
            };
        }

        return await PaginatedList<Cart>.CreateAsync(
            query.ApplyOrdering(sortingFields), pageNumber, pageSize, cancellationToken);
    }

    /// <summary>
    /// Updates a cart from the database
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was updated, false if not found</returns>
    public async Task<Cart?> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        var existingCart = await GetByIdAsync(cart.Id, cancellationToken);
        if (existingCart == null)
            return null;

        ReplaceCartItems(existingCart, cart.Products);

        existingCart.Update(cart.UserId, cart.Date);

        _context.Carts.Update(existingCart);
        await _context.SaveChangesAsync(cancellationToken);
        return existingCart;
    }

    private void ReplaceCartItems(Cart existingCart, List<CartItem> updatedProducts)
    {
        _context.CartItems.RemoveRange(existingCart.Products);

        existingCart.Products.Clear();
        existingCart.Products.AddRange(updatedProducts);
    }
}
