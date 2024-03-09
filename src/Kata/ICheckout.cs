using Kata.Exceptions;
using Kata.Models;

namespace Kata;

public interface ICheckout
{
    /// <summary>
    /// Calculates the total price of all items in the basket.
    /// </summary>
    /// <returns>Total price of the items in the basket.</returns>
    /// <exception cref="ProductNotFoundException">Thrown when there is no produce with the provided SKU.</exception>
    Task<decimal> GetTotalPriceAsync(IReadOnlyCollection<BasketItem> items);
}