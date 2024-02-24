using System.Collections.Concurrent;

namespace Checkout.Implementations;

public class Basket(IPricingRepository pricingRepository) : ICheckout
{
    private readonly ConcurrentBag<string> _basket = [];

    // TODO: Confirm with team if we should use the price we have at the point the item is added to the basket or the price when we calculate the total.
    public void Scan(string sku)
    {
        _basket.Add(sku);
    }

    public async Task<decimal> GetTotalPrice()
    {
        decimal totalPrice = 0;
        
        // Group the basket by product and count the quantity.
        // TODO: This could become a performance issue if the basket is too big, we could use a dictionary instead. Ask the team the expected size of the basket.
        // TODO : The price could be calculated in parallel.
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
        
        // TODO: Confirm with the team if an offer price can be applied more than once, a boolean on the offer could set this.
        // How many time the offer is applicable.
        int offerQuantity = item.Quantity / product.Pricing.Offer.Quantity;
        // How many items are left after applying the offer.
        int remainingQuantity = item.Quantity % product.Pricing.Offer.Quantity;
        
        return (offerQuantity * product.Pricing.Offer.Price) + (remainingQuantity * product.Pricing.Price);
    }
}