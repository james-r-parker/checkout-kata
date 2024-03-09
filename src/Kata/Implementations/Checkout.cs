using Kata.Models;

namespace Kata.Implementations;

internal class Checkout(IPricingRepository pricingRepository) : ICheckout
{
    public virtual async Task<decimal> GetTotalPriceAsync(IReadOnlyCollection<BasketItem> items)
    {
        decimal totalPrice = 0;

        foreach (var item in items)
        {
            var product = await pricingRepository.GetProductBySkuAsync(item.Sku);
            totalPrice += CalculateProductPrice(product, item);
        }

        return totalPrice;
    }

    protected virtual decimal CalculateProductPrice(Product product, BasketItem item)
    {
        return product.Pricing.Price * item.Quantity;
    }
}