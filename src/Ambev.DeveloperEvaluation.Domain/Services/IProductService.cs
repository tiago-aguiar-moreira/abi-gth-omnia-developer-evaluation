using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;
public interface IProductService
{
    Task CheckAndSetProductPriceAsync(List<SaleItem> saleItems, CancellationToken cancellationToken);
    Task CheckProductAsync(List<CartItem> cartItems, CancellationToken cancellationToken);
}
