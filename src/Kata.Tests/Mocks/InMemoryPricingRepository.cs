using System.Collections.Frozen;
using Kata.Exceptions;
using Kata.Models;

namespace Kata.Tests.Mocks;

internal class InMemoryPricingRepository : IPricingRepository
{
    private readonly IReadOnlyDictionary<string, Product> _products;
    private readonly BasketPackaging? _packagingPrices;

    public InMemoryPricingRepository(
        IEnumerable<Product> products,
        BasketPackaging? packagingPrices = null)
    {
        // Whilst testing assume we use the more expensive price.
        _products = products
            .GroupBy(x => x.Sku)
            .ToFrozenDictionary(
                x => x.Key,
                x => x.OrderByDescending(y => y.Pricing.Price).First());
        
        _packagingPrices = packagingPrices;
    }

    public Task<Product> GetProductBySkuAsync(string sku)
    {
        if (!_products.TryGetValue(sku, out var price))
        {
            throw new ProductNotFoundException(sku);
        }

        return Task.FromResult(price);
    }

    public Task<BasketPackaging?> GetPackagingPriceAsync()
    {
        return Task.FromResult(_packagingPrices);
    }
}