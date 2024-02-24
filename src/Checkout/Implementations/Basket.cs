using System.Collections.Concurrent;
using Checkout.Exceptions;

namespace Checkout.Implementations;

internal class Basket(IPricingRepository pricingRepository) : ICheckout
{
    private readonly ConcurrentDictionary<string, int> _basket = [];

    // TODO: Confirm with team if we should use the price we have at the point the item is added to the basket or the price when we calculate the total.
    public void Scan(string sku)
    {
        _basket.AddOrUpdate(sku, 1, (key, oldValue) => oldValue + 1);
    }

    public void Remove(string sku)
    {
        if (!_basket.TryRemove(sku, out _))
        {
            throw new BasketException($"Failed to remove product with SKU {sku} from the basket");
        }
    }

    public void Clear()
    {
        _basket.Clear();
    }

    public void Update(string sku, int quantity)
    {
        _basket.AddOrUpdate(sku, quantity, (key, oldValue) => quantity);
    }

    public async Task<decimal> GetTotalPriceAsync()
    {
        decimal totalPrice = 0;

        // Group the basket by product and count the quantity.
        // TODO : The price could be calculated in parallel.
        foreach (var item in _basket)
        {
            totalPrice += await CalculateProductPrice((item.Key, item.Value));
        }

        return totalPrice;
    }

    private async Task<decimal> CalculateProductPrice((string Sku, int Quantity) item)
    {
        var product = await pricingRepository.GetProductBySkuAsync(item.Sku);

        // If there is no offer, just return the base price.
        if (product.Pricing.Offer is null)
        {
            return product.Pricing.Price * item.Quantity;
        }

        // TODO: Confirm with the team if an offer price can be applied more than once, a boolean on the offer could set this.
        // How many time the offer is applicable.
        int offerQuantity = item.Quantity / product.Pricing.Offer.Quantity;
        // How many items are left after applying the offer.
        int remainingQuantity = item.Quantity % product.Pricing.Offer.Quantity;

        return (offerQuantity * product.Pricing.Offer.Price) + (remainingQuantity * product.Pricing.Price);
    }
}