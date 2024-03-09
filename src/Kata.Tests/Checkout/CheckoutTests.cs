using Kata.Implementations;
using Kata.Models;
using Kata.Tests.Mocks;

namespace Kata.Tests.Checkout;

public class CheckoutTests
{
    private readonly IEnumerable<Product> _products = new List<Product>()
    {
        new("A", new ProductPrice(50)),
        new("B", new ProductPrice(30)),
    };

    private readonly ICheckout _sut;

    public CheckoutTests()
    {
        _sut = new Implementations.Checkout(new InMemoryPricingRepository(_products));
    }
    
    [Fact]
    public async Task Single_Item()
    {
        var basket = new List<BasketItem>
        {
            new("A", 1),
        };

        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(50, result);
    }
    
    [Fact]
    public async Task Multiple_Items()
    {
        var basket = new List<BasketItem>
        {
            new("A", 1),
            new("B", 1),
        };

        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(80, result);
    }
    
    [Fact]
    public async Task Multiple_Items_With_Quantity()
    {
        var basket = new List<BasketItem>
        {
            new("A", 1),
            new("B", 2),
        };

        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(110, result);
    }
}