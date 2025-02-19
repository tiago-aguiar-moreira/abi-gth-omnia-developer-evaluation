﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.ORM.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task CheckAndSetProductPriceAsync(List<SaleItem> saleItems, CancellationToken cancellationToken)
    {
        var productIds = saleItems.Select(s => s.ProductId).Distinct();
        var products = await _productRepository.ListAsync(productIds, cancellationToken);

        foreach (var item in saleItems)
        {
            var selectedProduct = products.FirstOrDefault(f => f.Id == item.ProductId)
                ?? throw new KeyNotFoundException($"Product with ID {item.ProductId} not found");

            item.UnitPrice = selectedProduct.Price;
        }
    }

    public async Task CheckProductAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken)
    {
        var products = await _productRepository.ListAsync(productIds, cancellationToken);

        foreach (var productId in productIds)
        {
            var selectedProduct = products.FirstOrDefault(f => f.Id == productId)
                ?? throw new KeyNotFoundException($"Product with ID {productId} not found");
        }
    }
}
