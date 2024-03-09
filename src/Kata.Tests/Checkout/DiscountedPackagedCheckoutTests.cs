using Kata.Models;
using Kata.Tests.Mocks;
using Moq;

namespace Kata.Tests.Checkout;

public class DiscountedPackagedCheckoutTests
{
    private const decimal PackagePrice = 5;
    
    private readonly IEnumerable<Product> _products = new List<Product>()
    {
        new("A", new ProductPrice(50, new ProductPriceOffer(3, 100)))
    };
    
    private readonly Mock<IPackagingCalculator> _packagingCalculator;
    private readonly ICheckout _sut;

    public DiscountedPackagedCheckoutTests()
    {
        _packagingCalculator = new();
        _packagingCalculator.Setup(x => x.GetTotalPriceAsync(It.IsAny<List<BasketItem>>()))
            .ReturnsAsync(PackagePrice);

        _sut = new Implementations.DiscountedPackagedCheckout(new InMemoryPricingRepository(_products), _packagingCalculator.Object);
    }

    [Fact]
    public async Task Single_Product_Discounted()
    {
        var basket = new List<BasketItem>
        {
            new("A", 3),
        };

        decimal result = await _sut.GetTotalPriceAsync(basket);
        Assert.Equal(105, result);
        
        _packagingCalculator.Verify(x => x.GetTotalPriceAsync(basket), Times.Once);
    }
}