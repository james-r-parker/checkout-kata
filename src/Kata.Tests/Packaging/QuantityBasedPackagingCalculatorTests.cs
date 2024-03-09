using Kata.Implementations;
using Kata.Models;
using Kata.Tests.Mocks;

namespace Kata.Tests.Packaging;

public class QuantityBasedPackagingCalculatorTests
{
    private readonly BasketPackaging _basketPackaging = new(5,5);
    private readonly IPackagingCalculator _sut;

    public QuantityBasedPackagingCalculatorTests()
    {
        _sut = new QuantityBasedPackagingCalculator(
            new InMemoryPricingRepository(Enumerable.Empty<Product>(),_basketPackaging));
    }
    
    [Fact]
    public async Task One_Partly_Filled_Bag()
    {
        var basket = new List<BasketItem>
        {
            new("A", _basketPackaging.Capacity - 1),
        };
        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(_basketPackaging.Price, result);
    }
    
    [Fact]
    public async Task One_Filled_Bag()
    {
        var basket = new List<BasketItem>
        {
            new("A", _basketPackaging.Capacity),
        };
        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(_basketPackaging.Price, result);
    }
    
    [Fact]
    public async Task One_Filled_Bag_One_Partly_Filled_Bag()
    {
        var basket = new List<BasketItem>
        {
            new("A", _basketPackaging.Capacity + 1),
        };
        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(_basketPackaging.Price * 2, result);
    }
}