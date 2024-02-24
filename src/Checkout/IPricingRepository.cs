namespace Checkout;

public interface IPricingRepository
{
    public Task<Product> GetProductBySku(string sku);
}