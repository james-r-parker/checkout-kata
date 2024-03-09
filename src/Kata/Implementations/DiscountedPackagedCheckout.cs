using Kata.Models;

namespace Kata.Implementations;

internal class DiscountedPackagedCheckout : DiscountedCheckout
{
    private readonly IPackagingCalculator _packagingCalculator;

    public DiscountedPackagedCheckout(IPricingRepository pricingRepository, IPackagingCalculator packagingCalculator)
        : base(pricingRepository)
    {
        _packagingCalculator = packagingCalculator;
    }

    public override async Task<decimal> GetTotalPriceAsync(IReadOnlyCollection<BasketItem> items)
    {
        var total = await base.GetTotalPriceAsync(items);
        return total + await _packagingCalculator.GetTotalPriceAsync(items);
    }
}