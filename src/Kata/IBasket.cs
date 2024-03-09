using Kata.Exceptions;
using Kata.Models;

namespace Kata;

public interface IBasket
{
    /// <summary>
    /// Gets all items in the basket.
    /// </summary>
    IReadOnlyCollection<BasketItem> Items { get; }

    /// <summary>
    /// Adds a single product to the basket.
    /// </summary>
    /// <param name="sku">The SKU of the product to fetch.</param>
    /// <exception cref="BasketArgumentException">Thrown when SKU is null empty or whitespace.</exception>
    void Add(string sku);

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
}