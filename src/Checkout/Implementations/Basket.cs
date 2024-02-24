namespace Checkout.Implementations;

public class Basket(IPricingRepository pricingRepository) : ICheckout
{
    public void Scan(string sku)
    {
        throw new NotImplementedException();
    }

    public decimal GetTotalPrice()
    {
        throw new NotImplementedException();
    }
}