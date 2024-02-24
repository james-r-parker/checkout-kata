using Checkout.Implementations;

namespace Checkout.Tests;

public class CheckoutTests
{
    private readonly IEnumerable<Product> _products = new List<Product>()
    {
        new Product("A", new ProductPrice(50, new ProduceOffer(3,130))),
        new Product("B", new ProductPrice(30, new ProduceOffer(2,45))),
        new Product("C", new ProductPrice(20)),
        new Product("D", new ProductPrice(15)),
    };

    private readonly ICheckout _sut;
    
    public CheckoutTests()
    {
        _sut = new Basket(new InMemoryPricingRepository(_products));
    }

    [Fact]
    public async Task DiscountedPrice()
    {
        _sut.Scan("B");
        _sut.Scan("A");
        _sut.Scan("B");
        
        decimal result = await _sut.GetTotalPrice();
        Assert.Equal(95, result);
    }
}