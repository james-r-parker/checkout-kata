using Checkout.Implementations;

namespace Checkout.Tests;

public class CheckoutTests
{
    private readonly ICheckout _sut = new Basket(new InMemoryPricingRepository());

    [Fact]
    public void DiscountedPrice()
    {
        _sut.Scan("B");
        _sut.Scan("A");
        _sut.Scan("B");
        
        var result = _sut.GetTotalPrice();
        Assert.Equal(95, result);
    }
}