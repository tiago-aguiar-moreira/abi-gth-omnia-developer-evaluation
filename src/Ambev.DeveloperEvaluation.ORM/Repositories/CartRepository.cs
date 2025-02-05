using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
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
    public async Task<bool> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        cart.ApplyDiscounts();
        cart.SetTotalAmount();

        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.CartItems.AddRangeAsync(cart.Products, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
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

        _context.Carts.Update(cart);
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
    /// Retrieves a cart by their sale number
    /// </summary>
    /// <param name="id">The sale number of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    public async Task<Cart?> GetBySaleNumberAsync(int saleNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(o => o.SaleNumber == saleNumber, cancellationToken);
    }

    /// <summary>
    /// Retrieves a list of carts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The lis of carts if found, empty list if not found</returns>
    public async Task<List<Cart>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Products)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates a cart from the database
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was updated, false if not found</returns>
    public async Task<bool> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        var existingCart = await GetByIdAsync(cart.Id, cancellationToken);
        if (existingCart == null)
            return false;

        existingCart.Update(
            cart.SaleNumber, cart.SaleDate, cart.Branch, cart.Products);

        existingCart.ApplyDiscounts();
        existingCart.SetTotalAmount();

        _context.Carts.Update(existingCart);
        await _context.CartItems.AddRangeAsync(existingCart.Products, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
