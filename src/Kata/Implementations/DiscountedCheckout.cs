using Kata.Models;

namespace Kata.Implementations;

internal class DiscountedCheckout : Checkout
{
    public DiscountedCheckout(IPricingRepository pricingRepository) : base(pricingRepository)
    {
    }

    protected override decimal CalculateProductPrice(Product product, BasketItem item)
    {
        // If there is no offer, just return the base price.
        if (product.Pricing.Offer is null)
        {
            return base.CalculateProductPrice(product, item);
        }

        // How many time the offer is applicable.
        int offerQuantity = item.Quantity / product.Pricing.Offer.Quantity;
        // How many items are left after applying the offer.
        int remainingQuantity = item.Quantity % product.Pricing.Offer.Quantity;

        return (offerQuantity * product.Pricing.Offer.Price) + (remainingQuantity * product.Pricing.Price);
    }
}