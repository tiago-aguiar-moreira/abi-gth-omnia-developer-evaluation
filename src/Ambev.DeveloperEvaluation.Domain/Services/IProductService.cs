using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;
public interface IProductService
{
    Task CheckAndSetProductPriceAsync(List<SaleItem> saleItems, CancellationToken cancellationToken);
    Task CheckProductAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken);
}
