namespace Checkout;

public interface ICheckout
{
    void Scan(string sku);
    Task<decimal> GetTotalPrice();
}