using System.Collections.Frozen;
using Checkout.Exceptions;

namespace Checkout.Implementations;

public class InMemoryPricingRepository : IPricingRepository
{
    private readonly IReadOnlyDictionary<string, Product> _products;

    public InMemoryPricingRepository(IEnumerable<Product> products)
    {
        //TODO : Confirm with the team if we want to throw an exception if there are duplicate products
        // Whilst testing assume we use the more expensive price.
        _products = products
            .GroupBy(x => x.Sku)
            .ToFrozenDictionary(
                x => x.Key,
                x => x.OrderByDescending(y => y.Pricing.Price).First());
    }

    public Task<Product> GetProductBySku(string sku)
    {
        if (!_products.TryGetValue(sku, out var price))
        {
            throw new ProductNotFoundException(sku);
        }
        
        return Task.FromResult(price);
    }
}