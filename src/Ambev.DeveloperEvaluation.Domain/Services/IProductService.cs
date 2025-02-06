using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;
public interface IProductService
{
    Task SetProductPricesAsync(List<CartItem> cartItems, CancellationToken cancellationToken);
}
