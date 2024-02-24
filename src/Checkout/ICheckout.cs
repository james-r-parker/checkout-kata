using Checkout.Exceptions;

namespace Checkout;

public interface ICheckout
{
    /// <summary>
    /// Adds a single product to the basket.
    /// </summary>
    /// <param name="sku">The SKU of the product to fetch.</param>
    /// <exception cref="BasketArgumentException">Thrown when SKU is null empty or whitespace.</exception>
    void Scan(string sku);

    /// <summary>
    /// Attempts to remove a single product from the basket.
    /// </summary>
    /// <param name="sku">The SKU of product to remove</param>
    /// <exception cref="BasketArgumentException">Thrown when SKU is null empty or whitespace.</exception>
    /// <exception cref="BasketRemovalException">Thrown when there was a failure removing the item from basket.</exception>
    void Remove(string sku);

    /// <summary>
    /// Empties the basket, removing ALL items.
    /// </summary>
    void Clear();

    /// <summary>
    /// Updates the quantity of a product in the basket, if the SKU does not exist it is added. If the quantity is 0 the product is removed.
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="quantity"></param>
    /// <exception cref="BasketArgumentException">Thrown when SKU is null empty or whitespace.</exception>
    /// <exception cref="BasketRemovalException">Thrown when there was a failure removing the item from basket.</exception>
    void Update(string sku, int quantity);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ProductNotFoundException">Thrown when there is no produce with the provided SKU.</exception>
    Task<decimal> GetTotalPriceAsync();
}