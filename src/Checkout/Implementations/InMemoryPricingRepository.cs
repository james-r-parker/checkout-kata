namespace Checkout.Implementations;

public class InMemoryPricingRepository : IPricingRepository
{
    public Task<Product> GetProductBySku(string sku)
    {
        throw new NotImplementedException();
    }
}