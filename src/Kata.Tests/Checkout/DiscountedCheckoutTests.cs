using Kata.Implementations;
using Kata.Models;
using Kata.Tests.Mocks;

namespace Kata.Tests.Checkout;

public class DiscountedCheckoutTests
{
    private readonly IEnumerable<Product> _products = new List<Product>()
    {
        new("A", new ProductPrice(50, new ProductPriceOffer(3, 150))),
        new("B", new ProductPrice(30, new ProductPriceOffer(2, 45))),
    };

    private readonly ICheckout _sut;

    public DiscountedCheckoutTests()
    {
        _sut = new Implementations.DiscountedCheckout(new InMemoryPricingRepository(_products));
    }
    
    [Fact]
    public async Task Single_Product_Discounted()
    {
        var basket = new List<BasketItem>
        {
            new("A", 3),
        };

        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(150, result);
    }
    
    [Fact]
    public async Task Multiple_Products_Single_Discounted()
    {
        var basket = new List<BasketItem>
        {
            new("A", 1),
            new("B", 2),
        };

        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(95, result);
    }
    
    [Fact]
    public async Task Multiple_Products_Multiple_Discounted()
    {
        var basket = new List<BasketItem>
        {
            new("A", 3),
            new("B", 2),
        };

        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(195, result);
    }
}