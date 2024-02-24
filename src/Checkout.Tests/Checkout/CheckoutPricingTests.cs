using Checkout.Exceptions;
using Checkout.Implementations;

namespace Checkout.Tests.Checkout;

public class CheckoutPricingTests
{
    private readonly IEnumerable<Product> _products = new List<Product>()
    {
        new("A", new ProductPrice(50, new ProductPriceOffer(3, 130))),
        new("B", new ProductPrice(30, new ProductPriceOffer(2, 45))),
        new("C", new ProductPrice(20)),
        new("D", new ProductPrice(15)),
    };

    private readonly ICheckout _sut;

    public CheckoutPricingTests()
    {
        _sut = new Basket(new InMemoryPricingRepository(_products));
    }

    [Fact]
    /*
        For the A’s:
        1000 A’s.
        The special price for 3 A’s is £130.
        Divide 1000 by 3: 1000 // 3 = 333 (with a remainder of 1).
        You’ll pay for 333 sets of 3 A’s (totaling 999 A’s) at the special price: 333 × £130 = £43,290.
        Add the remaining A: £43,290 + £50 = £43,340.

        For the B’s:
        1000 B’s.
        The special price for 2 B’s is £45.
        Divide 1000 by 2: 1000 // 2 = 500.
        You’ll pay for 500 sets of 2 B’s (totaling 1000 B’s) at the special price: 500 × £45 = £22,500.

        For the C’s:
        1000 C’s.
        The unit price for C is £20.
        Total cost for 1000 C’s: 1000 × £20 = £20,000.

        For the D’s:
        1000 D’s.
        The unit price for D is £15.
        Total cost for 1000 D’s: 1000 × £15 = £15,000.

        Now add up the costs for each item: £43,340 (A) + £22,500 (B) + £20,000 © + £15,000 (D) = £100,840.
        The final price is £100,840. 🛒💰
     */
    public async Task Large_Basket()
    {
        foreach (var i in Enumerable.Range(0, 1000))
        {
            _sut.Scan("A");
            _sut.Scan("B");
            _sut.Scan("C");
            _sut.Scan("D");
        }

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(100840, result);
    }

    [Fact]
    public async Task Multiple_Items_With_Offers_Mixed_Order()
    {
        _sut.Scan("B");
        _sut.Scan("A");
        _sut.Scan("C");
        _sut.Scan("D");
        _sut.Scan("A");
        _sut.Scan("A");
        _sut.Scan("B");
        _sut.Scan("C");
        _sut.Scan("A");
        _sut.Scan("B");
        _sut.Scan("B");
        _sut.Scan("C");
        _sut.Scan("A");
        _sut.Scan("A");
        _sut.Scan("D");
        _sut.Scan("A");
        _sut.Scan("B");
        _sut.Scan("C");
        _sut.Scan("A");

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(590, result);
    }

    [Fact]
    public async Task Multiple_Items_With_Offers()
    {
        //Offer 1
        _sut.Scan("A");
        _sut.Scan("A");
        _sut.Scan("A");

        //Offer 2
        _sut.Scan("A");
        _sut.Scan("A");
        _sut.Scan("A");

        // Standard Price
        _sut.Scan("A");
        _sut.Scan("A");

        //Offer 1
        _sut.Scan("B");
        _sut.Scan("B");

        //Offer 2
        _sut.Scan("B");
        _sut.Scan("B");

        // Standard Price
        _sut.Scan("B");

        // Standard Price
        _sut.Scan("C");
        _sut.Scan("C");
        _sut.Scan("C");
        _sut.Scan("C");

        // Standard Price
        _sut.Scan("D");
        _sut.Scan("D");

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(590, result);
    }

    [Fact]
    public async Task Single_Product_No_Offer()
    {
        _sut.Scan("C");
        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(20, result);
    }

    [Fact]
    public async Task Discount_Product_Without_Offer()
    {
        _sut.Scan("B");
        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(30, result);
    }

    [Fact]
    public async Task Discount_Applied_Once_With_Remainder()
    {
        //Offer 1
        _sut.Scan("B");
        _sut.Scan("B");

        // Standard price
        _sut.Scan("B");

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(75, result);
    }

    [Fact]
    public async Task Discount_Applied_Twice_With_Remainder()
    {
        //Offer 1
        _sut.Scan("B");
        _sut.Scan("B");

        // Offer 2
        _sut.Scan("B");
        _sut.Scan("B");

        // Standard price
        _sut.Scan("B");

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(120, result);
    }

    [Fact]
    public async Task Single_Multi_Discount_With_Standard_Item()
    {
        _sut.Scan("B");
        _sut.Scan("A");
        _sut.Scan("B");

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(95, result);
    }

    [Fact]
    public async Task Unknown_Item_Throws_Exception()
    {
        _sut.Scan("A");
        _sut.Scan("E");
        var ex = await Assert.ThrowsAsync<ProductNotFoundException>(async () => await _sut.GetTotalPriceAsync());
        Assert.Equal("Product with SKU E not found",ex.Message);
    }

    [Fact]
    public async Task Empty_Basket_Returns_Zero()
    {
        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task Single_Product_With_Offer()
    {
        _sut.Scan("A");
        _sut.Scan("A");
        _sut.Scan("A");

        decimal result = await _sut.GetTotalPriceAsync();
        Assert.Equal(130, result);
    }
}