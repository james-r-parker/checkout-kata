using Kata.Models;

namespace Kata;

public interface IPackagingCalculator
{
    Task<decimal> GetTotalPriceAsync(IReadOnlyCollection<BasketItem> items);
}