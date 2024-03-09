using Kata.Exceptions;
using Kata.Models;

namespace Kata;

public interface IPricingRepository
{
    /// <summary>
    /// Gets a single product by its SKU.
    /// </summary>
    /// <param name="sku">The SKU of the product to fetch.</param>
    /// <returns><see cref="Product"/></returns>
    /// <exception cref="ProductNotFoundException">Thrown when there is no produce with the provided SKU.</exception>
    Task<Product> GetProductBySkuAsync(string sku);

    /// <summary>
    /// Gets the price of the packaging.
    /// </summary>
    /// <returns><see cref="BasketPackaging"/></returns>
    Task<BasketPackaging?> GetPackagingPriceAsync();
}