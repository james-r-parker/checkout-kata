using Kata.Exceptions;
using Kata.Models;

namespace Kata.Implementations;

internal class QuantityBasedPackagingCalculator(IPricingRepository pricingRepository) : IPackagingCalculator
{
    public async Task<decimal> GetTotalPriceAsync(IReadOnlyCollection<BasketItem> items)
    {
        var price = await pricingRepository.GetPackagingPriceAsync();

        if (price == null)
        {
            throw new PackagingPriceNotFoundException();
        }

        int totalNumberOfItemsInBasket = items.Sum(x => x.Quantity);
        int quantityOfPackagingRequiredToFulfill = totalNumberOfItemsInBasket / price.Capacity;
        if (totalNumberOfItemsInBasket % price.Capacity > 0)
        {
            quantityOfPackagingRequiredToFulfill++;
        }

        return (quantityOfPackagingRequiredToFulfill * price.Price);
    }
}