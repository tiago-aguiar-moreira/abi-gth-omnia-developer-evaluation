using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context) => _context = context;

    /// <summary>
    /// Creates a new sale in the database
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        sale.ApplyDiscounts();
        sale.SetTotalAmount();

        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Deletes a sale from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        cart.Cancel();

        _context.Sales.Update(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(c => c.Products)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a list of sales
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of sales if found, empty list if not found</returns>
    public async Task<PaginatedList<Sale>> ListAsync(
        int? pageNumber,
        int? pageSize,
        List<(string PropertyName, bool Ascendent)> sortingFields,
        List<(string PropertyName, object? Value)> filters,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Sales.Include(c => c.Products).AsQueryable();

        foreach (var (property, value) in filters)
        {
            if (value is null) continue;

            query = property.ToLower() switch
            {
                "salenumber" => query.Where(w => w.SaleNumber == (int)value),
                "_minsalenumber" => query.FilterRangeField(property, (int)value),
                "_maxsalenumber" => query.FilterRangeField(property, (int)value),
                "saledate" => query.Where(w => w.SaleDate == (DateTime)value),
                "_minsaledate" => query.FilterRangeField(property, (DateTime)value),
                "_maxsaledate" => query.FilterRangeField(property, (DateTime)value),
                "totalamount" => query.Where(w => w.TotalAmount == (decimal)value),
                "_mintotalamount" => query.FilterRangeField(property, (decimal)value),
                "_maxtotalamount" => query.FilterRangeField(property, (decimal)value),
                "branch" => query.FilterStringField(property, (string)value),
                "iscanceled" => query.Where(w => w.Status == (short)value),
                "quantity" => query.Where(w => w.Products.Any(a => a.Quantity == (int)value)),
                "_minquantity" => query.Where(w => w.Products.Any(a => a.Quantity >= (int)value)),
                "_maxquantity" => query.Where(w => w.Products.Any(a => a.Quantity <= (int)value)),
                "unitprice" => query.Where(w => w.Products.Any(a => a.UnitPrice == (decimal)value)),
                "_minunitprice" => query.Where(w => w.Products.Any(a => a.UnitPrice >= (decimal)value)),
                "_maxunitprice" => query.Where(w => w.Products.Any(a => a.UnitPrice <= (decimal)value)),
                "discount" => query.Where(w => w.Products.Any(a => a.Discount == (decimal)value)),
                "_mindiscount" => query.Where(w => w.Products.Any(a => a.Discount >= (decimal)value)),
                "_maxdiscount" => query.Where(w => w.Products.Any(a => a.Discount <= (decimal)value)),
                _ => query
            };
        }

        return await PaginatedList<Sale>.CreateAsync(
            query.ApplyOrdering(sortingFields), pageNumber, pageSize, cancellationToken);
    }

    /// <summary>
    /// Updates a new sale in the repository
    /// </summary>
    /// <param name="sale">The sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    public async Task<Sale?> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var existingSale = await GetByIdAsync(sale.Id, cancellationToken);
        if (existingSale == null)
            return null;

        ReplaceProducts(existingSale, sale.Products);

        existingSale.Update(sale.SaleNumber, sale.SaleDate, sale.Branch, sale.Status);

        _context.Sales.Update(existingSale);
        await _context.SaveChangesAsync(cancellationToken);
        return existingSale;
    }

    private void ReplaceProducts(Sale existingSale, List<SaleItem> updatedProducts)
    {
        _context.SaleItems.RemoveRange(existingSale.Products);

        existingSale.Products.Clear();
        existingSale.Products.AddRange(updatedProducts);
    }
}
