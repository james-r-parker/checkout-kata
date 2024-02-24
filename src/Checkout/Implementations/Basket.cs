using System.Collections.Concurrent;

namespace Checkout.Implementations;

public class Basket(IPricingRepository pricingRepository) : ICheckout
{
    private readonly ConcurrentBag<string> _basket = [];

    public void Scan(string sku)
    {
        _basket.Add(sku);
    }

    public async Task<decimal> GetTotalPrice()
    {
        decimal totalPrice = 0;
        
        foreach (var item in _basket.GroupBy(x => x))
        {
            totalPrice+= await CalculateProductPrice((item.Key, item.Count()));
        }

        return totalPrice;
    }

    private async Task<decimal> CalculateProductPrice((string Sku, int Quantity) item)
    {
        var product = await pricingRepository.GetProductBySku(item.Sku);
        
        // If there is no offer, just return the base price.
        if (product.Pricing.Offer is null)
        {
            return product.Pricing.Price * item.Quantity;
        }
        
        // How many time the offer is applicable.
        int offerQuantity = item.Quantity / product.Pricing.Offer.Quantity;
        // How many items are left after applying the offer.
        int remainingQuantity = item.Quantity % product.Pricing.Offer.Quantity;
        
        return (offerQuantity * product.Pricing.Offer.Price) + (remainingQuantity * product.Pricing.Price);
    }
}