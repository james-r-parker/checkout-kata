using Checkout.Exceptions;

namespace Checkout;

internal interface IPricingRepository
{
    /// <summary>
    /// Gets a single product by its SKU.
    /// </summary>
    /// <param name="sku">The SKU of the product to fetch.</param>
    /// <returns><see cref="Product"/></returns>
    /// <exception cref="ProductNotFoundException">Thrown when there is no produce with the provided SKU.</exception>
    Task<Product> GetProductBySkuAsync(string sku);
}